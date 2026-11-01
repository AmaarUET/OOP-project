using project.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.studentrepositories
{
    public class studentrepository
    {
        private readonly string _filepath = "C:\\Users\\User\\Downloads\\New folder (2)\\student.txt";
        public void save(modelstudent student)
        {
            using (StreamWriter write = new StreamWriter(_filepath, false))
            {
                write.WriteLine(student.ToString());
            }
        }
        public void saveall(List<modelstudent> students)
        {
            using (StreamWriter write = new StreamWriter(_filepath, false))
            {
                foreach ( modelstudent s in students)
                {
                    write.WriteLine(s.ToString());
                }
            }
        }
        public List<modelstudent> GetAll()
        {
            List<modelstudent> students = new List<modelstudent>();
            if (!File.Exists(_filepath))
            {
                return students;
            }
            using (StreamReader read = new StreamReader(_filepath))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    string[] data = line.Split('|');
                    if (data.Length == 5)
                    {
                        modelstudent student = new modelstudent(
                        data[0],
                            data[1],
                            int.Parse(data[2]),
                            int.Parse(data[3]),
                            int.Parse(data[4]));
                        students.Add(student);
                    }
                }

            }
            return students;

        }

        public void clearall()
        {
            File.WriteAllText(_filepath, string.Empty);
        }
    }
}
