using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    // task 19
    public enum StudentStatus
    {
        Active,
        Graduated,
        Suspended
    }
    // task 18
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
            Console.WriteLine(" General Students Report ");
            Console.WriteLine("Total Students: " + studentsList.Count);
            foreach (var s in studentsList)
            {
                s.Display_Info();
                Console.WriteLine("-------------------");
            }
        }
    }
    public interface ISearchable
    {
        List<Student> Search(string keyword, List<Student> students);
    }
    public class StudentSearchService : ISearchable
    {
        public List<Student> Search(string keyword, List<Student> students)
        {
            return students.Where(s => s.Name.ToLower().Contains(keyword.ToLower()) ||
                                       s.DepartmentName.ToLower().Contains(keyword.ToLower())).ToList();
        }
    }
    // task 16-17
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // task 15
        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 5)
                {
                    Console.WriteLine("Invalid age! Age cannot be less than 5. Setting default to 5.");
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
        // task 17
        public virtual void Display_Info()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Age: " + Age);
        }
    }

    // task 13-17-19
    public class Student : Person
    {
        // task 15 
        private int grade;
        public int Grade
        {
            get { return grade; }
            set
            {
                if (value < 0 || value > 100)
                {
                    Console.WriteLine("Invalid Grade! Must be between 0 and 100. Setting default to 0.");
                    grade = 0;
                }
                else
                {
                    grade = value;
                }
            }
        }
        public string DepartmentName { get; set; }

        // task 19
        public StudentStatus Status { get; set; }
        public Student(int id, string name, int age, int grade, string departmentName, StudentStatus status = StudentStatus.Active)
            : base(id, name, age)
        {
            Grade = grade;
            DepartmentName = departmentName;
            Status = status;
        }
        // task 17
        public override void Display_Info()
        {
            base.Display_Info();
            Console.WriteLine("Grade: " + Grade);
            Console.WriteLine("Department: " + DepartmentName);
            Console.WriteLine("Status: " + Status);
        }
        public void DisplayStudent()
        {
            Display_Info();
        }
        // task 23
        public string ToFileString()
        {
            return $"{Id}|{Name}|{Age}|{Grade}|{DepartmentName}|{Status}";
        }
        public static Student FromFileString(string line)
        {
            var parts = line.Split('|');
            if (parts.Length == 6)
            {
                int id = int.Parse(parts[0]);
                string name = parts[1];
                int age = int.Parse(parts[2]);
                int grade = int.Parse(parts[3]);
                string dept = parts[4];
                StudentStatus status = (StudentStatus)Enum.Parse(typeof(StudentStatus), parts[5]);
                return new Student(id, name, age, grade, dept, status);
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
        // task 17
        public override void Display_Info()
        {
            base.Display_Info();
            Console.WriteLine("Subject: " + Subject);
        }
        public void DisplayTeacher()
        {
            Display_Info();
        }
    }

    // task 14
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
        // task 23
        private static string filePath = "students.txt"; 
        // task 11
        public static int ReadInteger()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Invalid input! Please enter a valid number: ");
            }
            return result;
        }
        public static void calculate_result(int grade)
        {
            if (grade >= 90 && grade <= 100)
            {
                Console.WriteLine("Excellent");
            }
            else if (grade >= 70 && grade <= 80)
            {
                Console.WriteLine("Very Good");
            }
            else if (grade >= 60 && grade < 70)
            {
                Console.WriteLine("Good");
            }
            else if (grade >= 50 && grade < 60)
            {
                Console.WriteLine("Pass");
            }
            else if (grade < 50 && grade >= 0)
            {
                Console.WriteLine("Fail");
            }
            else
            {
                Console.WriteLine("Invalid Grade!");
            }
        }
        public static void display_student_info(string name, string department, int age)
        {
            Console.WriteLine("student name is:" + name + " student age is: " + age + " student department is:" + department);
        }
        // task 12
        public static void PrintStudent(string name)
        {
            Console.WriteLine("student name is:" + name);
        }
        public static void PrintStudent(string name, int age)
        {
            Console.WriteLine("student name is:" + name + " student age is: " + age);
        }

        public static void PrintStudent(string name, int age, string department)
        {
            Console.WriteLine("student name is:" + name + " student age is: " + age + " student department is:" + department);
        }
        // task 23
        public static List<Student> LoadStudentsFromFile()
        {
            List<Student> list = new List<Student>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    Student s = Student.FromFileString(line);
                    if (s != null)
                        list.Add(s);
                }
            }
            return list;
        }
        // task 23
        public static void SaveStudentsToFile(List<Student> students)
        {
            List<string> lines = new List<string>();
            foreach (var s in students)
            {
                lines.Add(s.ToFileString());
            }
            File.WriteAllLines(filePath, lines);
        }
        static void Main(string[] args)
        {
            List<Student> students = LoadStudentsFromFile(); 

            // task 20-21-22-24
            while (true)
            {
                Console.WriteLine("     STUDENT MANAGEMENT SYSTEM     ");
                Console.WriteLine("==================================");
                Console.WriteLine("1- Add Student");
                Console.WriteLine("2- Show All Students");
                Console.WriteLine("3- Search Student");
                Console.WriteLine("4- Edit Student");
                Console.WriteLine("5- Delete Student");
                Console.WriteLine("6- Department Statistics");
                Console.WriteLine("7- Run Demonstration Tasks (11-18)");
                Console.WriteLine("8- Exit");
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
                        RunDemonstrations();
                        break;
                    case 8:
                        SaveStudentsToFile(students);
                        Console.WriteLine("Exiting program... Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Try again.");
                        break;
                }
            }
        }
        // task 21
        private static void AddStudentUI(List<Student> students)
        {
            Console.Write("Enter ID: ");
            int id = ReadInteger();
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
            Student newStudent = new Student(id, name, age, grade, dept, status);
            students.Add(newStudent);
            Console.WriteLine("Student added successfully!");
        }
        private static void ShowAllStudentsUI(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }
            Console.WriteLine("  All Students  ");
            foreach (var s in students)
            {
                s.Display_Info();
            }
        }
        private static void SearchStudentUI(List<Student> students)
        {
            Console.Write("Enter student name or department to search: ");
            string keyword = Console.ReadLine();
            ISearchable searchService = new StudentSearchService();
            List<Student> results = searchService.Search(keyword, students);

            if (results.Count == 0)
            {
                Console.WriteLine("No student found ");
            }
            else
            {
                Console.WriteLine($"\nFound: {results.Count}");
                foreach (var s in results)
                {
                    s.Display_Info();                }
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
            Console.WriteLine("Student updated successfully!");
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
        // task 22
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
            Console.WriteLine("\n--- Running Task Demonstrations ---");
            // task 11
            calculate_result(80);
            display_student_info("sama", "IS", 20);
            // task 12
            PrintStudent("ali");
            PrintStudent("ali", 19);
            PrintStudent("ali", 19, "Information System");
            // task 13
            Student student1 = new Student(2029, "ola", 22, 88, "Information System");
            Student student2 = new Student(3078, "ahmed", 21, 60, "Computer Science");
            student1.Display_Info();
            student2.Display_Info();
            // task 14
            List<Department> departments = new List<Department>
            {
                new Department(1, "Computer Science"),
                new Department(2, "Information System")
            };
            List<Student> studentsList = new List<Student>
            {
                new Student(1, "Sara", 20, 90, "Computer Science"),
                new Student(2, "Mona", 21, 85, "Computer Science"),
                new Student(3, "Ali", 19, 95, "Information System")
            };
            foreach (Student s in studentsList)
            {
                s.Display_Info();
            }
            // task 16-17
            List<Person> people = new List<Person>
            {
                new Student(1, "Salma", 21, 95, "Computer Science"),
                new Teacher(2, "Omar", 35, "Programming")
            };
            Console.WriteLine("\n--- Displaying Polymorphism (Task 17) ---");
            foreach (var person in people)
            {
                person.Display_Info();
                Console.WriteLine();
            }
            // task 18
            Report report = new StudentReport(studentsList);
            report.PrintReport();
        }
    }
}