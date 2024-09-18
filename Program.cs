using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistration
{
    class Program
    {
        const int OptionRegisterUser = 1;
        const int OptionRemoveUser = 2;
        const int OptionListUsers = 3;
        const int OptionExit = 4;
        public struct Data
        {
            public string fullName { get; set; }
            public DateTime dateOfBirth { get; set; }
            public int generalRegistry { get; set; }
            public string address { get; set; }
            public int addressNumber { get; set; }
        }

        static List<Data> ListUsers = new List<Data>();

        public static void DisplayMainMenu()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("User registration program in C#");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Enter an option:");
            Console.WriteLine("1 -> Register new user");
            Console.WriteLine("2 -> Remove a user from the database");
            Console.WriteLine("3 -> Read all users in the database");
            Console.WriteLine("4 -> Exit");
            Console.WriteLine("----------------------------------------");
            Console.Write("-> ");
        }
        public static Data CreateUser()
        {
            Data newUser = new Data();

            Console.Write("Enter your full name: ");
            newUser.fullName = Console.ReadLine();

            Console.Write("Enter your general registry: ");
            string tempID = Console.ReadLine();
            newUser.generalRegistry = Convert.ToInt32(tempID);

            Console.Write("Enter your of birth (dd/MM/yyyy): ");
            string tempBirth = Console.ReadLine();
            newUser.dateOfBirth = Convert.ToDateTime(tempBirth);

            Console.Write("Enter your address: ");
            newUser.address = Console.ReadLine();

            Console.Write("Enter your address number: ");
            string tempAdressNumber = Console.ReadLine();
            newUser.addressNumber = Convert.ToInt32(tempAdressNumber);

            return newUser;
        }
        public static void AddNewUser(Data newData)
        {
            var user = ListUsers.FirstOrDefault(u => u.generalRegistry == newData.generalRegistry);

            if (user.Equals(default(Data)))
            {
                ListUsers.Add(newData);
                Console.WriteLine("User registered successfully!");
            }
            else
            {
                Console.WriteLine("User already registered!");
            }
        }
        public static void DeleteUser(int registry)
        {
            var userToRemove = ListUsers.FirstOrDefault(u => u.generalRegistry == registry);

            if (userToRemove.Equals(default(Data)))
            {
                Console.WriteLine("User [" + registry + "] not found.");
            }
            else
            {
                ListUsers.Remove(userToRemove);
                Console.WriteLine("User [" + registry + "] removed.");
            }
        }
        public static void ListUsersData()
        {
            if(ListUsers.Count != 0)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Users in the database:");
                Console.WriteLine("----------------------");

                for (int i = 0; i < ListUsers.Count; i++)
                {
                    Console.WriteLine("User ["+i+"]:");
                    Console.WriteLine("Name: " + ListUsers[i].fullName);
                    Console.WriteLine("Registry: " + ListUsers[i].generalRegistry);
                    string formattedDate = ListUsers[i].dateOfBirth.ToString("dd/MM/yyyy"); 
                    Console.WriteLine("Date Of Birth: " + formattedDate);
                    Console.WriteLine("Address: " + ListUsers[i].address + ", " + ListUsers[i].addressNumber);
                    Console.WriteLine("----------------------");
                }
            }
            else
            {
                Console.WriteLine("No registered users!");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                int close = 0;

                do
                {
                    DisplayMainMenu();
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int option))
                    {
                        Console.Clear();

                        switch (option)
                        {
                            case OptionRegisterUser:
                            {
                                    Console.WriteLine("[Register new user]");
                                    AddNewUser(CreateUser()); 
                                    break;
                            }
                            case OptionRemoveUser:
                            {
                                    Console.WriteLine("[Remove a user from the database]");

                                    Console.Write("Enter the registration number of the user to be removed: ");
                                    if (int.TryParse(Console.ReadLine(), out int registry))
                                    {
                                        DeleteUser(registry);
                                    }
                                    break;
                                }
                            case OptionListUsers:
                            {
                                    ListUsersData();
                                    break;
                            }
                            case OptionExit:
                            {
                                    close = OptionExit;
                                    break;
                            }
                            default:
                            {
                                    Console.WriteLine("---Invalid option---");
                                    break;
                            }
                        }

                        Console.WriteLine("Press any key to exit");
                        Console.ReadKey();
                    }

                } while (close != OptionExit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
