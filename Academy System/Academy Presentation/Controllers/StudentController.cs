using Academy_Presentation.Helpers;
using Domain.Entities;
using Service.Services.Implementations;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Academy_Presentation.Controllers
{
    public class StudentController
    {
        StudentService _studentService = new();

        public void Create()
        {
        GroupId:
            Helper.PrintConsole(ConsoleColor.Blue, "Add Group Id");

            string groupId = Console.ReadLine();
            int selectedGroupId;

            bool isSelectedId = int.TryParse(groupId, out selectedGroupId);

            if (isSelectedId)
            {
                Helper.PrintConsole(ConsoleColor.Blue, "Add Student Name");
                string studentName = Console.ReadLine();

                Helper.PrintConsole(ConsoleColor.Blue, "Add Student Surname");
                string studentSurname = Console.ReadLine();

                Helper.PrintConsole(ConsoleColor.Blue, "Add Student Age");
                string ageInput = Console.ReadLine();

                if (!int.TryParse(ageInput, out int age))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Please enter a valid age!");
                    goto GroupId;
                }
                if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(studentSurname) || string.IsNullOrWhiteSpace(ageInput))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: Empty field");

                    goto GroupId;
                }
                if (studentName.Length >= 30)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: Name limited"); return;
                }
                if (studentSurname.Length >=30)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: groupTeacher limited"); return;
                }
                if (age<=16)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: There are no students of this age.");return;
                }
                foreach (char c in studentName)
                {
                    if (!char.IsLetter(c) && c != ' ' && c != '-')
                    {
                        Helper.PrintConsole(ConsoleColor.Red, "Error: Teacher name invalid (no numbers or special characters)");
                        return;
                    }
                }
                foreach (char c in studentSurname)
                {
                    if (!char.IsLetter(c) && c != ' ' && c != '-')
                    {
                        Helper.PrintConsole(ConsoleColor.Red, "Error: Teacher name invalid (no numbers or special characters)");
                        return;
                    }
                }
                if (!studentSurname.All(c => (char.IsLetter(c) || c == ' ' || c == '-') && !char.IsDigit(c)))
                {

                    Helper.PrintConsole(ConsoleColor.Red, "Error: Name invalid"); return;
                }
                if (studentName.Equals(studentSurname, StringComparison.OrdinalIgnoreCase))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: Name and Surname cannot be the same.");
                    return;
                }
                studentName = char.ToUpper(studentName[0]) + studentName.Substring(1).ToLower();
                studentSurname = char.ToUpper(studentSurname[0]) + studentSurname.Substring(1).ToLower();
                Student student = new Student
                {
                    Name = studentName,
                    Surname = studentSurname,
                    Age = age
                };

                var result = _studentService.Create(selectedGroupId, student);

                if (result != null)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Student Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age}, Group: {student.Group.Name}");
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Group not found, please add correct group id!");
                    goto GroupId;
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Add correct group id type!");
                goto GroupId;
            }
        }

        public void UpdateStudent()
        {
            while (true)
            {
                Helper.PrintConsole(ConsoleColor.Blue, "Add Student Id (or press Enter to cancel):");
                string studentIdInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(studentIdInput))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Update operation cancelled.");
                    return;
                }

                if (!int.TryParse(studentIdInput, out int studentId))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Please enter a valid numeric Student Id.");
                    continue;
                }

                var student = _studentService.GetById(studentId);
                if (student == null)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Student not found!");
                    continue;
                }

                // Name
                Helper.PrintConsole(ConsoleColor.Blue, $"Current Name: {student.Name}. Add new Name (or press Enter to keep current):");
                string newName = Console.ReadLine();
                newName = string.IsNullOrWhiteSpace(newName) ? student.Name : newName;

                // Surname
                Helper.PrintConsole(ConsoleColor.Blue, $"Current Surname: {student.Surname}. Add new Surname (or press Enter to keep current):");
                string newSurname = Console.ReadLine();
                newSurname = string.IsNullOrWhiteSpace(newSurname) ? student.Surname : newSurname;

                // Age
                Helper.PrintConsole(ConsoleColor.Blue, $"Current Age: {student.Age}. Add new Age (or press Enter to keep current):");
                string newAgeInput = Console.ReadLine();
                int newAge = student.Age;
                if (!string.IsNullOrWhiteSpace(newAgeInput) && int.TryParse(newAgeInput, out int parsedAge))
                {
                    newAge = parsedAge;
                }

                // GroupId
                Helper.PrintConsole(ConsoleColor.Blue, $"Current Group Id: {student.GroupId}. Add new Group Id (or press Enter to keep current):");
                string newGroupIdInput = Console.ReadLine();
                int newGroupId = (int)student.GroupId;
                if (!string.IsNullOrWhiteSpace(newGroupIdInput) && int.TryParse(newGroupIdInput, out int parsedGroupId))
                {
                    newGroupId = parsedGroupId;
                }

                // Prepare updated student
                Student updatedStudent = new Student
                {
                    Name = newName,
                    Surname = newSurname,
                    Age = newAge,
                    GroupId = newGroupId
                };

                var result = _studentService.Update(studentId, updatedStudent);

                if (result != null)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Student updated successfully!\nId: {result.Id}, Name: {result.Name}, Surname: {result.Surname}, Age: {result.Age}, Group Id: {result.GroupId}");
                    break; // exit loop after successful update
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Student not found!");
                }
            }
        }




        public void Delete()
        {
            {
            StudentId: Helper.PrintConsole(ConsoleColor.Green, "Add Group Id:");
                string studentId = Console.ReadLine();

                int id;

                bool isStudentId = int.TryParse(studentId, out id);
                if (string.IsNullOrWhiteSpace(studentId))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Delete operation cancelled.");
                  goto StudentId;
                }
                
                

                if (id <= 0)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Add correct positive Student Id");
                    goto StudentId;
                   
                }

                if (isStudentId)
                {
                    _studentService.Delete(id);
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Add correct GroupId type");
                    goto StudentId;
                }
            }
        }
        public void GetById()
        {
        StudentId: Helper.PrintConsole(ConsoleColor.Green, "Add Group Id:");
            string studentId = Console.ReadLine();
            int id;
            bool isStudentId = int.TryParse(studentId, out id) || id <= 0;
            if (isStudentId)
            {
                Student student = _studentService.GetById(id);
                if (student != null)
                {
                    Helper.PrintConsole(ConsoleColor.Cyan, $"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age},Group: {student.Group}");
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Error: Group not found!");
                    goto StudentId;
                }
                
            }
            
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Add correct GroupId type!");
                goto StudentId;
            }
        }
        public void GetByAge()

        {
            while (true)
            {
                Helper.PrintConsole(ConsoleColor.Blue, "Add Student Age:");
                string ageInput = Console.ReadLine();

                if (!int.TryParse(ageInput, out int age))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Please enter a valid number!");
                    continue;
                }
                if (age <= 0)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Age must be a positive number!");
                    continue;
                }

                List<Student> students = (List<Student>)_studentService.GetByAge(age);

                if (students == null || students.Count == 0)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "No student found with this age!");
                    continue;

                }
                foreach (Student student in students)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Student Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age}, Group: {student.Group}");

                    break;
                }
            }
        }


        public void GetByGroupId()
        {

            while (true)
            {
                Helper.PrintConsole(ConsoleColor.Blue, "Add Group Id:");
                string groupIdInput = Console.ReadLine();

                if (!int.TryParse(groupIdInput, out int groupId))
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Please enter a valid number!");
                    continue;
                }

                List<Student> students = _studentService.GetAllByGroupId(groupId)?.ToList();

                if (students == null || students.Count == 0)
                {
                    Helper.PrintConsole(ConsoleColor.Red, "No students found in this group!");
                    continue;
                }

                Helper.PrintConsole(ConsoleColor.Yellow, $"Students in group ID {groupId}:\n");

                foreach (Student student in students)
                {
                    Helper.PrintConsole(ConsoleColor.Green,
                        $"Student Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age}, Group: {student.Group?.Name}");
                }

                break;
            }
        }


        public void Search()
        {
            {
            SearchText: Helper.PrintConsole(ConsoleColor.Blue, "Add Group search text");

                string searchName = Console.ReadLine();
                List<Student> students = _studentService.Search(searchName);
                if (students==null ||students.Count != 0)
                {
                    foreach (var student in students)
                    {
                        Helper.PrintConsole(ConsoleColor.Cyan, $"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Age: {student.Age},Group: {student.Group}");
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
}
