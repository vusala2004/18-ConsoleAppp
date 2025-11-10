using Academy_Presentation.Helpers;
using Domain.Entities;
using Service.Services.Implementations;
using System;

namespace Academy_Presentation.Controllers
{
    public class GroupController
    {
        GroupService _groupService = new();

        public void Create()
        {
            Helper.PrintConsole(ConsoleColor.Green, "Add Group Name:");
            string groupName = Console.ReadLine().Trim().ToUpper();

            Helper.PrintConsole(ConsoleColor.Green, "Add Group Teacher:");
            string groupTeacher = Console.ReadLine().Trim().ToUpper();


            Helper.PrintConsole(ConsoleColor.Green, "Add Group Room:");
            string groupRoom = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(groupName) || string.IsNullOrWhiteSpace(groupTeacher) || string.IsNullOrWhiteSpace(groupRoom))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Error: All fields must be filled!");
                return;
            }

            Group group = new Group { Name = groupName,Teacher = groupTeacher, Room = groupRoom };

            var groupResult = _groupService.Create(group);

            if (groupResult != null)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $" Group added successfully! \nId: {groupResult.Id}, \nGroupName: {groupResult.Name},\nGroupTeacher: {groupTeacher},\nGroupRoom: {groupRoom}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Error: Could not add group");
            }
        }
        public void Update() { }
    }
}
        //public void Update() { }

         //public void Delete()
         //  {

         //    }
//        public void GetById(int id) { }
//        public void GetByTeacher() { }
//        public void GetByRoom(); {}
//         public void GetAll() { }
//    }
//}
