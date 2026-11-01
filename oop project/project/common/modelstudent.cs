using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.common
{
    public class modelstudent
    {
        public string name;
        public string rollno;
        public int totalfee;
        public int paidfee;
        public int pendingfee;

        public modelstudent(string name,string rollno,int totalfee,int paidfee,int pendingfee){
            this.name = name;
            this.rollno = rollno;
            this.totalfee = totalfee;
            this.paidfee = paidfee;
            this.pendingfee= pendingfee;
        }
        public override string ToString()
        {
            return name + "|" + rollno + "|" +
                   totalfee + "|" + paidfee + "|" + pendingfee;
        }
        public void display()
        {
            
            Console.WriteLine(name + "\t" + rollno + "\t" + totalfee+ "\t"+"\t"+paidfee + "\t"+"\t"+pendingfee+ "\t");
            Console.WriteLine();
        }
    }
}
