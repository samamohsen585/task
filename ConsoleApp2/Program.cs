using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentManagementApp
{
    // Task 19
    public enum StudentStatus
    {
        Active,
        Graduated,
        Suspended
    }

    // Task 18
    public abstract class Report
    {
        public abstract void PrintReport();
    }

    public class StudentReport : Report
    {
        private List<Student> studentsList;

        public StudentReport(List<Student> students)
        {
            studentsList = students;
        }

        public override void PrintReport()
        {
            Console.WriteLine("   General Students Report   ");
            Console.WriteLine("Total Students: " + studentsList.Count);
            foreach (var s in studentsList)
            {
                s.DisplayInfo();
                Console.WriteLine("-------------------");
            }
        }
    }

    // Task 18
    public interface ISearchable
    {
        List<Student> Search(string keyword, List<Student> students);
    }

    public class StudentSearchService : ISearchable
    {
        public List<Student> Search(string keyword, List<Student> students)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<Student>();

            return students.Where(s =>
                s.Name.ToLower().Contains(keyword.ToLower()) ||
                s.DepartmentName.ToLower().Contains(keyword.ToLower())
            ).ToList();
        }
    }

    // Task 16-17
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Task 15
        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 5)
                {
                    Console.WriteLine("Invalid age!");
                    age = 5;
                }
                else
                {
                    age = value;
                }
            }
        }

        public Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        // Task 17
        public virtual void DisplayInfo()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Age: " + Age);
        }
    }

    // Task 13-15-16-17-19
    public class Student : Person
    {
        private int grade;
        public int Grade
        {
            get { return grade; }
            set
            {
                if (value < 0 || value > 100)
                {
                    Console.WriteLine("Invalid Grade!");
                    grade = 0;
                }
                else
                {
                    grade = value;
                }
            }
        }

        public string DepartmentName { get; set; }
        public StudentStatus Status { get; set; }

        public Student(int id, string name, int age, int grade, string departmentName, StudentStatus status = StudentStatus.Active)
            : base(id, name, age)
        {
            Grade = grade;
            DepartmentName = departmentName;
            Status = status;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Grade: " + Grade);
            Console.WriteLine("Department: " + DepartmentName);
            Console.WriteLine("Status: " + Status);
        }

        // Task 23
        public string ToFileString()
        {
            return $"{Id}|{Name}|{Age}|{Grade}|{DepartmentName}|{Status}";
        }

        public static Student FromFileString(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;

            var parts = line.Split('|');
            if (parts.Length == 6)
            {
                if (int.TryParse(parts[0], out int id) &&
                    int.TryParse(parts[2], out int age) &&
                    int.TryParse(parts[3], out int grade) &&
                    Enum.TryParse(parts[5], out StudentStatus status))
                {
                    return new Student(id, parts[1], age, grade, parts[4], status);
                }
            }
            return null;
        }
    }

    public class Teacher : Person
    {
        public string Subject { get; set; }

        public Teacher(int id, string name, int age, string subject)
            : base(id, name, age)
        {
            Subject = subject;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Subject: " + Subject);
        }
    }

    // Task 14
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Department(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    internal class Program
    {
        private static readonly string FilePath = "students.txt";

        // Task 11
        public static int ReadInteger()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Invalid input!");
            }
            return result;
        }

        // Task 5-11
        public static void CalculateResult(int grade)
        {
            if (grade >= 90 && grade <= 100) Console.WriteLine("Excellent");
            else if (grade >= 70 && grade < 90) Console.WriteLine("Very Good");
            else if (grade >= 60 && grade < 70) Console.WriteLine("Good");
            else if (grade >= 50 && grade < 60) Console.WriteLine("Pass");
            else if (grade >= 0 && grade < 50) Console.WriteLine("Fail");
            else Console.WriteLine("Invalid Grade!");
        }

        // Task 12
        public static void PrintStudent(string name)
        {
            Console.WriteLine("Student Name: " + name);
        }

        public static void PrintStudent(string name, int age)
        {
            Console.WriteLine($"Student Name: {name}, Age: {age}");
        }

        public static void PrintStudent(string name, int age, string department)
        {
            Console.WriteLine($"Student Name: {name}, Age: {age}, Department: {department}");
        }

        private static void RunBasicTasks1To10()
        {
            // Task 1
            //declaring personal info 
            int age = 13;
            string name = "Sama";
            string Department = "IT";
            //displaying welcome message + info 
            Console.WriteLine("Hello");
            Console.WriteLine("name is:" + name + "age is: " + age + "department is:" + Department);

            // Task 3  
            int num1 = 10, num2 = 5;
            Console.WriteLine($"Numbers: {num1}, {num2}");
            Console.WriteLine($"Add: {num1 + num2}, Sub: {num1 - num2}, Mul: {num1 * num2}, Div: {num1 / num2}, Mod: {num1 % num2}");

            // Task 5 
            Console.Write("Grade 85 Result: ");
            CalculateResult(85);

            // Task 6 
            int ageDemo = 19;
            bool isValidAge = ageDemo >= 0 && ageDemo <= 100;
            if (isValidAge)
            {
                if (ageDemo <= 12) Console.WriteLine("Category: Child");
                else if (ageDemo <= 19) Console.WriteLine("Category: Teenager");
                else if (ageDemo <= 59) Console.WriteLine("Category: Adult");
                else Console.WriteLine("Category: Senior");
            }

            // Task 7
            string fname = "Sama", lname = "Mohsen";
            string fullname = fname + " " + lname;
            Console.WriteLine("Fullname: " + fullname);
            Console.WriteLine("Upper: " + fullname.ToUpper());
            Console.WriteLine("Lower: " + fullname.ToLower());
            Console.WriteLine("Length: " + fullname.Length);
            Console.WriteLine("Contains 'a': " + fullname.ToLower().Contains("a"));

            // Task 8 
            int[] demoArr = { 5, 12, 3, 20, 8 };
            Console.WriteLine($"Array Sum: {demoArr.Sum()}, Max: {demoArr.Max()}, Min: {demoArr.Min()}, Avg: {demoArr.Average()}");

            // Task 9 
            Console.Write("Numbers from 1 to 20 -skipping % 3 + ignore after 17- : ");
            for (int i = 1; i <= 20; i++)
            {
                if (i % 3 == 0) continue;
                if (i == 17) break;
                Console.Write(i + " ");
            }
            Console.WriteLine();

            // Task 10 
            string[] names = { "Sara", "Ali", "Mona", "Omar", "Hassan" };
            int[] ages = { 17, 19, 21, 25, 20 };
            Console.WriteLine("Students aged 18-22:");
            for (int i = 0; i < 5; i++)
            {
                if (ages[i] >= 18 && ages[i] <= 22)
                {
                    Console.WriteLine($"- {names[i]} ({ages[i]} years)");
                }
            }
        }

        // Task 23
        public static List<Student> LoadStudentsFromFile()
        {
            List<Student> list = new List<Student>();
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);
                foreach (string line in lines)
                {
                    Student s = Student.FromFileString(line);
                    if (s != null) list.Add(s);
                }
            }
            return list;
        }

        // Task 23
        public static void SaveStudentsToFile(List<Student> students)
        {
            List<string> lines = students.Select(s => s.ToFileString()).ToList();
            File.WriteAllLines(FilePath, lines);
        }

        static void Main(string[] args)
        {
            List<Student> students = LoadStudentsFromFile();

            // Task 20-21-22-24
            while (true)
            {
                Console.WriteLine("     STUDENT MANAGEMENT SYSTEM    ");
                Console.WriteLine("==================================");
                Console.WriteLine("1-Add Student");
                Console.WriteLine("2-Show All Students");
                Console.WriteLine("3-Search Student");
                Console.WriteLine("4-Edit Student");
                Console.WriteLine("5-Delete Student");
                Console.WriteLine("6-Department Statistics");
                Console.WriteLine("7-Run Early Basic Tasks (Tasks 1 to 10)");
                Console.WriteLine("8-Run Advanced Demonstrations (Tasks 11 to 18)");
                Console.WriteLine("9-Exit");
                Console.Write("Choose an option: ");
                int choice = ReadInteger();
                switch (choice)
                {
                    case 1:
                        AddStudentUI(students);
                        SaveStudentsToFile(students);
                        break;
                    case 2:
                        ShowAllStudentsUI(students);
                        break;
                    case 3:
                        SearchStudentUI(students);
                        break;
                    case 4:
                        EditStudentUI(students);
                        SaveStudentsToFile(students);
                        break;
                    case 5:
                        DeleteStudentUI(students);
                        SaveStudentsToFile(students);
                        break;
                    case 6:
                        ShowDepartmentStatisticsUI(students);
                        break;
                    case 7:
                        RunBasicTasks1To10();
                        break;
                    case 8:
                        RunDemonstrations();
                        break;
                    case 9:
                        SaveStudentsToFile(students);
                        Console.WriteLine("Data saved \n Exiting program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Try again");
                        break;
                }
            }
        }

        // Task 21
        private static void AddStudentUI(List<Student> students)
        {
            Console.Write("Enter ID: ");
            int id = ReadInteger();

            if (students.Any(s => s.Id == id))
            {
                Console.WriteLine("Error! ID already exist!");
                return;
            }

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Age: ");
            int age = ReadInteger();

            Console.Write("Enter Grade: ");
            int grade = ReadInteger();

            Console.Write("Enter Department Name: ");
            string dept = Console.ReadLine();

            Console.WriteLine("Select Status (0-Active, 1-Graduated, 2-Suspended): ");
            int statusChoice = ReadInteger();

            StudentStatus status = StudentStatus.Active;
            if (Enum.IsDefined(typeof(StudentStatus), statusChoice))
            {
                status = (StudentStatus)statusChoice;
            }

            students.Add(new Student(id, name, age, grade, dept, status));
            Console.WriteLine("Student added successfully!");
        }

        private static void ShowAllStudentsUI(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            Console.WriteLine("\n All Students List  ");
            foreach (var s in students)
            {
                s.DisplayInfo();
                Console.WriteLine("-------------------");
            }
        }

        private static void SearchStudentUI(List<Student> students)
        {
            Console.Write("Enter student name or department to search ");
            string keyword = Console.ReadLine();

            ISearchable searchService = new StudentSearchService();
            List<Student> results = searchService.Search(keyword, students);

            if (results.Count == 0)
            {
                Console.WriteLine("No students found");
            }
            else
            {
                Console.WriteLine($"\nFound: {results.Count} ");
                foreach (var s in results)
                {
                    s.DisplayInfo();
                    Console.WriteLine("-------------------");
                }
            }
        }

        private static void EditStudentUI(List<Student> students)
        {
            Console.Write("Enter Student ID to Edit: ");
            int id = ReadInteger();

            Student student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                return;
            }

            Console.Write($"Enter New Name (Current: {student.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) student.Name = name;

            Console.Write($"Enter New Age (Current: {student.Age}): ");
            student.Age = ReadInteger();

            Console.Write($"Enter New Grade (Current: {student.Grade}): ");
            student.Grade = ReadInteger();

            Console.Write($"Enter New Department (Current: {student.DepartmentName}): ");
            string dept = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dept)) student.DepartmentName = dept;

            Console.WriteLine("Student updated successfully");
        }

        private static void DeleteStudentUI(List<Student> students)
        {
            Console.Write("Enter Student ID to Delete: ");
            int id = ReadInteger();

            Student student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                return;
            }

            students.Remove(student);
            Console.WriteLine("Student deleted successfully!");
        }

        // Task 22
        private static void ShowDepartmentStatisticsUI(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No student data available to calculate statistics.");
                return;
            }

            var grouped = students.GroupBy(s => s.DepartmentName);
            Console.WriteLine("\n=== Department Statistics ===");

            foreach (var group in grouped)
            {
                Console.WriteLine($"\nDepartment: {group.Key}");
                Console.WriteLine($" - Total Students: {group.Count()}");
                Console.WriteLine($" - Average Age: {group.Average(s => s.Age):F2}");
                Console.WriteLine($" - Oldest Age: {group.Max(s => s.Age)}");
                Console.WriteLine($" - Youngest Age: {group.Min(s => s.Age)}");
            }

            var highestDept = grouped.OrderByDescending(g => g.Count()).FirstOrDefault();
            var lowestDept = grouped.OrderBy(g => g.Count()).FirstOrDefault();

            if (highestDept != null)
                Console.WriteLine($"\nDepartment with Most Students: {highestDept.Key} ({highestDept.Count()} students)");

            if (lowestDept != null)
                Console.WriteLine($"Department with Fewest Students: {lowestDept.Key} ({lowestDept.Count()} students)");
        }

        private static void RunDemonstrations()
        {
            CalculateResult(80);

            PrintStudent("Ali");
            PrintStudent("Ali", 19);
            PrintStudent("Ali", 19, "Information System");

            List<Person> people = new List<Person>
            {
                new Student(1, "Sara", 21, 95, "Computer Science"),
                new Teacher(2, "Dr. Omar", 35, "Programming")
            };

            foreach (var person in people)
            {
                person.DisplayInfo();
                Console.WriteLine();
            }

            List<Student> demoStudents = new List<Student>
            {
                new Student(1, "Mona", 20, 90, "CS"),
                new Student(2, "Hassan", 22, 85, "IS")
            };

            Report report = new StudentReport(demoStudents);
            report.PrintReport();
        }
    }
}