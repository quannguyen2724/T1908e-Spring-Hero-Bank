using System;

namespace T1908e_Spring_Hero_Bank.View
{
    public class MainView
    {
        public static void GenerateMenu()
        {
            // var controller = new StudentController();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--------Student Manager---------");
                Console.WriteLine("1. Add student.");
                Console.WriteLine("2. List student.");
                Console.WriteLine("3. Edit student.");
                Console.WriteLine("4. Delete student.");
                Console.WriteLine("5. Loggin student.");
                Console.WriteLine("6. Exist.");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Enter your choice: ");
                var choice =  int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // controller.CreateStudent();
                        break;
                    case 2:
                        // controller.ShowListStudent("");
                        break;
                    case 3:
                        // controller.EditStudent();
                        break;
                    case 4:
                        // controller.DeleteStudent();
                        break;
                    case 5:
                        // controller.LogginStudent();
                        break;
                    case 6:
                        Console.WriteLine("Good bye");
                        break;
                }
                Console.ReadLine();
                if (choice == 6)
                {
                    break;
                }
            }
        }
    }
}