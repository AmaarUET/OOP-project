using project.common;
using project.studentrepositories;
using project.studentservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class uistudent
    {
        static void Main(string[] args)
        {
            studentrepository repository = new studentrepository();
            studentservice service = new studentservice(repository);
            if (repository.GetAll().Count == 0)
            {
                service.addstudent(new modelstudent("AMAAR", "2026s-cs-7", 700000, 0, 700000));
                service.addstudent(new modelstudent("ARMAN", "2026s-cs-3", 800000, 0, 800000));
                service.addstudent(new modelstudent("SAMIE", "2026s-cs-9", 900000, 0, 900000));
                service.addstudent(new modelstudent("TALAL", "2026s-cs-4", 400000, 0, 400000));
                service.addstudent(new modelstudent("AHMAD", "2026s-cs-6", 500000, 0, 500000));

            }
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("========== UNIVERSITY FEE MANAGEMENT SYSTEM ==========");
                Console.WriteLine("1. Finance Manager Login");
                Console.WriteLine("2. Student");
                Console.WriteLine("3. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    if (adminlogin(service))
                    {
                        ADMINMANU(service);
                    }
                }
                else if (choice == "2")
                {
                    studentmenu(service);
                }
                else if (choice == "3")
                {
                    service.saveall();
                    running = false;
                    Console.WriteLine("GOOD BYE! PROGRAM END");
                    service.pause();
                }
                else
                {
                    Console.Write("INVALID CHOICE");
                    service.pause();
                }

                
            }


        }
        static bool adminlogin(studentservice service)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("==========ADMIN LOGIN ATTEMPT==========");
                Console.Write("USERNAME:");
                string username = Console.ReadLine();
                Console.Write("ENTER PASSWORD:");
                string password = Console.ReadLine();
                if (username == "AMAAR" && password == "123")
                {
                    Console.WriteLine("login successfully!");
                    service.pause();
                    return true;
                }
                else
                {
                    Console.WriteLine("INVALID USERNAME OR PASSWORD");
                    service.pause();
                }


            }
            return false;
        }
        static bool ADMINMANU(studentservice service)
        {
            bool adminlogin = true;
            string option;
            do
            {
                Console.Clear();
                if (adminlogin)
                {
                    Console.WriteLine("===== FINANCE MANAGER MENU =====");
                    Console.WriteLine("1. Show all student fee details");
                    Console.WriteLine("2. Search by roll number");
                    Console.WriteLine("3. Search by name");
                    Console.WriteLine("4. Add new student");
                    Console.WriteLine("5. Show students with paid fee");
                    Console.WriteLine("6. Show students with pending fee");
                    Console.WriteLine("7. Delete student");
                    Console.WriteLine("8. Logout");
                    Console.Write("Enter option: ");
                     option = Console.ReadLine();
                    if (option == "1")

                    {
                        Console.Clear();
                        service.showallstudent();
                        service.pause();
                    }
                    else if (option == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("==========SEARCH STUDENT BY ROLLNO============");
                        Console.Write("Enter roll number: ");
                        modelstudent byroll = service.searchbyrollno(Console.ReadLine());
                        if (byroll == null)
                        {
                            Console.WriteLine("Record not found.");
                        }
                        else
                        {

                            byroll.display();
                        }
                        service.pause();

                    }
                    else if (option == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("==========SEARCH STUDENT BY NAME==========");
                        Console.Write("Enter name: ");
                        modelstudent byname = service.searchbyname(Console.ReadLine());
                        if (byname == null)
                        {
                            Console.WriteLine("Record not found.");

                        }
                        else
                        {
                           
                            byname.display();
                        }
                        service.pause();
                    }
                    else if (option == "4")
                    {
                        Console.Clear();
                        Console.WriteLine("==========ADD STUDENT==========");
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter roll number: ");
                        string roll = Console.ReadLine();
                        Console.Write("Enter total fee: ");
                        int total = int.Parse(Console.ReadLine());
                        Console.Write("Enter paid fee: ");
                        int paid = int.Parse(Console.ReadLine());
                        if (total < 0 || paid < 0 || paid > total)
                        {
                            Console.WriteLine("Invalid fee amounts.");
                        }
                        else
                        {
                            modelstudent newStudent = new modelstudent(
                                name, roll, total, paid, total - paid);
                            service.addstudent(newStudent);
                        }
                        service.pause();
                    }
                    else if (option == "5")
                    {
                        Console.Clear();
                        Console.WriteLine("==========STUDENT WITH PAID FEE==========");
                        Console.WriteLine("NAME\tROLLNO\t\tTOTALFEE\tPAIDFEE\t\tPENDINGFEE");
                        service.paid();
                        service.pause();
                    }
                    else if (option == "6")
                    {
                        Console.Clear();
                        Console.WriteLine("===========STUDENT WITH PENDING FEE");
                        Console.Clear();
                        service.pending();
                        service.pause();

                    }
                    else if (option == "7")
                    {
                        Console.Write("ENTER THE ROLLNO OF STUDENT WANT TO DELETE:");
                        string rollno = Console.ReadLine();
                        service.delete(rollno);
                        service.pause();

                    }
                    else if (option == "8")
                    {
                        break;
                    }
                   


                }
                else
                {
                    Console.Write("INVALID OPTION ");
                    return false;
                    service.pause();
                }
            }
            while (option != "8");
            return true;
        }
        static void studentmenu(studentservice service) {
            bool found = false;
            Console.Clear();
            Console.WriteLine("=============STUDENT MENU===========");
            Console.WriteLine("1.SUBMIT FEE");
            Console.WriteLine("2.VIEW FEE RECORD");
            Console.WriteLine("3.EXIT");
            Console.Write("enter the option:");
            string option = Console.ReadLine();
            if (option == "1")
            {
                Console.Clear();
                Console.WriteLine("===========SUBMIT FEE============");
                Console.Write("enter name:");
                string name= Console.ReadLine();
                Console.Write("ENTER ROLLNO:");
                string rollno= Console.ReadLine();
                Console.Write("ENTER THE FEE SUBMIT BY CHALLEN:");
                int submitfee;
                submitfee=int.Parse(Console.ReadLine());
                service.submitfee(rollno,name, submitfee);
                service.pause();
                found = true;
            }
            if (option == "2")
            {
                Console.Clear();
                Console.WriteLine("==========VIEW FEE RECORD==========");
                Console.Write("ENTER NAME:");
                string name = Console.ReadLine();
                Console.Write("ENTER ROLLNO:");
           
                modelstudent byroll = service.searchbyrollno(Console.ReadLine());
                if (byroll == null)
                {
                    Console.WriteLine("Record not found.");
                }
                else
                {

                    byroll.display();
                }
                
                service.pause();
                found= true;
            }
            if (option == "3")
            {

            }
            if (!found) {
                found = false;
                Console.WriteLine("INVALID OPTION");
                service.pause();
            }

        }

    }
   
}
