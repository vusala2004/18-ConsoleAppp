using Academy_Presentation.Controllers;
using Academy_Presentation.Helpers;
using Service.Services.Implementations;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace Academy_Presentation
{
    public class Program
    {
        static void Main(string[] args)
        {
            GroupService groupService = new();
            GroupController groupController = new();


            Helper.PrintConsole(ConsoleColor.Blue, "Select one option");
            Helper.PrintConsole(ConsoleColor.Cyan, "1 - Create Group,\n2 - Update group,\n3 - Delete Group, \n4 - Get group  by id,\n5 - Get all groups  by teacher , \n6 - Get all groups by room, \n7 - Get all groups,\n8 - Create Student , \n9 - Update Student   , \n10- Get student  by id, \n11 - Delete student,\n12 - Get students   by age, \n13 - Get all students  by group id , \n14- Search method for groups by name, \n15 - Search method for students by name or surname.\r\n");

            while (true)
            {
            selectOption: string selectOption = Console.ReadLine();
                int selectTrueOption;
                bool isSelectOption = int.TryParse(selectOption, out selectTrueOption);
                if (isSelectOption)
                {
                    switch (selectTrueOption)
                    {

                        case 1:
                            groupController.Create();
                            break;
                        case 2:
                            groupController.Update();
                            break;
                    }
                }
                else
                {

                    Helper.PrintConsole(ConsoleColor.Red, "Error: select correct option");
                    goto selectOption;
                }
            }
        }
    }
}


                      
                                //case 3:
                                //    groupController.Delete();
                                //    break;

                                //case 4:
                                //    groupController.GetById();
                                //    break;

                                //case 5:
                                //    groupController.GetByTeacher();
                                //    break;

                                //case 6:
                                //    groupController.GetByRoom();
                                //    break;

                                //case 7:
                                //    groupController.GetAll();
                                //    break;

                                // ===== STUDENT METHODS =====
                                //case "8":
                                //    studentController.Create();
                                //    break;

                                //case "9":
                                //    studentController.Update();
                                //    break;

                                //case "10":
                                //    studentController.GetById();
                                //    break;

                                //case "11":
                                //    studentController.Delete();
                                //    break;

                                //case "12":
                                //    studentController.GetByAge();
                                //    break;

                                //case "13":
                                //    studentController.GetByGroupId();
                                //    break;

                                //case "14":
                                //    groupController.Search();
                                //    break;

                                //case "15":
                                //    studentController.Search();
                                //    break;

                                //case "0":
                                //    return;

                                //default:
                                //    Helper.PrintConsole(ConsoleColor.Red, "Unknown option, please try again!");
                                //    break;
                                




                
                
            
        
    

            

        
