using Academy_Presentation.Helpers;
using Domain.Entities;
using Domain.Entities;
using Service.Services.Implementations;
using System;
using System.ComponentModel.Design;
using System.Xml.Linq;
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
            string groupTeacher = Console.ReadLine().Trim();


            Helper.PrintConsole(ConsoleColor.Green, "Add Group Room:");
            string groupRoom = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(groupName) || string.IsNullOrWhiteSpace(groupTeacher) || string.IsNullOrWhiteSpace(groupRoom))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Error: Empty field");
                return;
            }
            if (groupName.Length > 50) 
            {
                Helper.PrintConsole(ConsoleColor.Red, "Error: Group name too long."); return;
            }

            if (groupTeacher.Length > 30)
            {
                Helper.PrintConsole(ConsoleColor.Red, "Error: groupTeacher limited"); return;
            }
            if (groupRoom.Length >20)
            { 
                
               Helper.PrintConsole(ConsoleColor.Red, "Error: groupRoom limited"); return; 
            }



            foreach (char c in groupTeacher)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '-')
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: Teacher name invalid (no numbers or special characters)");
                    return;
                }
            }
            groupName = char.ToUpper(groupName[0]) + groupName.Substring(1).ToLower();
            groupTeacher = char.ToUpper(groupTeacher[0]) + groupTeacher.Substring(1).ToLower();

           



            Group group = new Group { Name = groupName, Teacher = groupTeacher, Room = groupRoom };

            var groupResult = _groupService.Create(group);

            if (groupResult != null)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $" Group added successfully! \nId: {groupResult.Id}, \nGroupName: {groupResult.Name},\nGroupTeacher: {group.Teacher},\nGroupRoom: {group.Room}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Error: Could not add group");


            }
        }

 
        public void UpdateGroup()
        {
        GroupId:
            Helper.PrintConsole(ConsoleColor.Blue, "Yenilemek istediyin qrupun ID-sini daxil et (ve ya Enter basaraq legv et):");

            string groupId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(groupId))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Yenileme emeliyyatı legv edildi.");
                return;
            }

            int id;
            bool isGroupId = int.TryParse(groupId, out id);

            if (isGroupId)
            {
                var findGroup = _groupService.GetById(id);

                if (findGroup != null)
                {
                    // Name
                    Helper.PrintConsole(ConsoleColor.Blue, $"Current Name: {findGroup.Name}. Add new name (or press Enter to keep current):");
                    string newName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        newName = findGroup.Name;
                    }

                    // Teacher
                    Helper.PrintConsole(ConsoleColor.Blue, $"Current Teacher: {findGroup.Teacher}. Add new Teacher (or press Enter to keep current):");
                    string newTeacher = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newTeacher))
                    {
                        newTeacher = findGroup.Teacher;
                    }

                    // Room
                    Helper.PrintConsole(ConsoleColor.Blue, $"Current Room: {findGroup.Room}. Add new Room (or press Enter to keep current):");
                    string newRoom = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newRoom))
                    {
                        newRoom = findGroup.Room;
                    }

                    // Yarat və update et
                    Group updatedGroup = new Group
                    {
                        Name = newName,
                        Teacher = newTeacher,
                        Room = newRoom
                    };

                    var result = _groupService.Update(id, updatedGroup);

                    if (result != null)
                    {
                        Helper.PrintConsole(ConsoleColor.Green, $"Group updated successfully! \nId: {result.Id}, Name: {result.Name}, Teacher: {result.Teacher}, Room: {result.Room}");
                    }
                    else
                    {
                        Helper.PrintConsole(ConsoleColor.Red, "Group not found.");
                        goto GroupId;
                    }
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Group not found.");
                    goto GroupId;
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Add correct Group Id type.");
                goto GroupId;
            }
        } 
        public void Delete()
        {
            {
            GroupId: Helper.PrintConsole(ConsoleColor.Green, "Add Group Id:");
                string groupId = Console.ReadLine();

                int id;

                bool isGroupId = int.TryParse(groupId, out id);
                if (string.IsNullOrWhiteSpace(groupId))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Delete operation cancelled.");
                    goto GroupId;
                }
                if (id <= 0)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Add correct positive Student Id");
                    goto GroupId;

                }

                if (isGroupId)
                {
                    _groupService.Delete(id);
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Add correct GroupId type");
                    goto GroupId;
                }
            }
        }
        public void GetById()

        {
        GroupId: Helper.PrintConsole(ConsoleColor.Green, "Add Group Id:");
            string groupId = Console.ReadLine();
            int id;
            bool isGroupId = int.TryParse(groupId, out id) || id <= 0;
            if (isGroupId)
            {
                Group group = _groupService.GetById(id);
                if (group != null)
                {
                    Helper.PrintConsole(ConsoleColor.Yellow, $" Group added successfully! \nId: {group.Id}, \nGroupName: {group.Name},\nGroupTeacher: {group.Teacher},\nGroupRoom: {group.Room}");
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: Group not found!");
                    goto GroupId;
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Add correct GroupId type!");
                goto GroupId;
            }
        }
        public void GetAll()
        {
            List<Group> groups = _groupService.GetAll();
            if (groups.Count != 0)
            {
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.Yellow, $"  \nId: {group.Id}, \nGroupName: {group.Name},\nGroupTeacher: {group.Teacher},\nGroupRoom: {group.Room}");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Error: Please Create Group");
            }

        }
        public void GetByTeacher()
        {
            Helper.PrintConsole(ConsoleColor.Blue, "Enter teacher name:");
            string teacherName = Console.ReadLine();

            var groups = _groupService.GetByTeacher(teacherName);

            if (groups!=null)
            {
                
                
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Group Id: {groups.Id}, Name: {groups.Name}, Teacher: {groups.Teacher}, Room: {groups.Room}");
                
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "No groups found for this teacher!");
            }
        }

        public void GetByRoom()
        {
      
            Helper.PrintConsole(ConsoleColor.Green, "Add Group Room:");
            string groupRoom = Console.ReadLine().Trim().ToUpper();

            var groups = _groupService.GetByRoom(groupRoom);

            if (groups != null)
            {
                Helper.PrintConsole(ConsoleColor.Green, $"Group Id: {groups.Id}, Name: {groups.Name}, Teacher: {groups.Teacher}, Room: {groups.Room}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "No groups found for this room!");
            }

        
        }
        public void Search()
        {
        SearchText: Helper.PrintConsole(ConsoleColor.Blue, "Add Group search text");

            string searchName = Console.ReadLine();
            List<Group>groups=_groupService.Search(searchName);
            if (groups.Count != 0)
            {
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.Cyan,$"Id: {group.Id}, GroupName: {group.Name}, GroupTeacher: {group.Teacher}, GroupRoom: {group.Room}");
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Groups not found for search text!");
                goto SearchText;
            }
        }
    }
}


