using System.Collections.Generic;
using project.common;
using project.studentrepositories;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.studentservices
{
    public class studentservice
    {
        private List<modelstudent> students;
        private studentrepository repository;
        public studentservice(studentrepository repo)
        {
            repository = repo;
            students = repository.GetAll();
        }
        public void header()
        {
            Console.WriteLine("====================STUDENT DETAILS=======================");
            Console.WriteLine("NAME\tROLLNO\t\tTOTALFEE\tPAIDFEE\t\tPENDINGFEE");
        }
        public void addstudent(modelstudent student)
        {
            foreach (modelstudent s in students)
            {
                if (s.rollno == student.rollno)
                {
                    Console.Write("student already exists");
                    return;
                }
            }
            students.Add(student);
            repository.saveall(students);
            Console.Write("student added successfully");
        }
       
        public modelstudent searchbyrollno(string rollno)
        {
            header();
            foreach(modelstudent s in students)
            {
                if(s.rollno== rollno)
                {
                    return s;
                    
                }
            }
            return null
                ;
        }
        public modelstudent searchbyname(string name)
        {
            header();
            foreach (modelstudent s in students)
            {
                if (s.name == name)
                {
                    return s;

                }
            }
            return null;
        }
        public void feedetail(string rollno,string name)
        {
            modelstudent s = searchbyrollno(rollno);
            if(s==null || s.name != name) {
                Console.Write("student does not exist");
                return;
            }
            header();
            s.display();
           
        }
        public void submitfee(string rollno, string name,int submitfee)
        {
            modelstudent s = searchbyrollno(rollno);
            if (s == null || s.name != name)
            {
                Console.Write("student not exist");
                return;
            }
            if (submitfee < s.totalfee)
            {
                Console.WriteLine("INVALID! You must submit the full total fee.");
                return;
            }

            if (submitfee > s.pendingfee)
            {
                Console.WriteLine("INVALID! Fee exceeds pending amount.");
                return;
            }
            s.paidfee = s.paidfee + submitfee;
            s.pendingfee = s.pendingfee - submitfee;
            repository.saveall(students);

            Console.WriteLine("Fee submitted successfully.");

            
            s.display();
        }
        public void paid()
        {
            bool found = false;
            foreach (modelstudent s in students)
            {

                if (s.paidfee > 0)
                {
                    s.display();
                    found = true;
                }
            }
                if(!found)
                {
                    Console.WriteLine("NO STUDENT PIAD FEE YET!");
                }
            
        }
        public void pending()
        {
            bool found = false;
            Console.WriteLine("==============PENDING FEE==============");
            
            foreach (modelstudent s in students)
            {
                
                if (s.pendingfee > 0)
                {
                    s.display();
                    found= true;
                }
            }
            if (!found)
            {
                Console.Write("NO STUDENT WITH PENDING FEE!");
            }
        }
      
      public void showallstudent()
        {
            header();

            foreach (modelstudent s in students)
            {
                s.display();
            }
        }
        public void saveall()
        {
            repository.saveall(students);
        }
        public void delete(string rollno)
        {
            modelstudent s = searchbyrollno(rollno);

            if (s == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            students.Remove(s);
            repository.saveall(students);

            Console.WriteLine("Student deleted successfully.");

        }
        public void pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }
}