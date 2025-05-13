using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StudentManagementSystem.Application;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Data;
using StudentManagementSystem.Domain;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;


namespace StudentManagementSystem.ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var studentService = serviceProvider.GetService<IStudentService>();
            var repository = serviceProvider.GetService<IStudentRepository>();

            // Load data from file
            repository.LoadFromFile("students.json");

            while (true)
            {
                Console.WriteLine("\nStudent Management System");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await AddStudent(studentService);
                        break;
                    case "2":
                        ViewAllStudents(studentService);
                        break;
                    case "3":
                        UpdateStudent(studentService);
                        break;
                    case "4":
                        repository.SaveToFile("students.json");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static async Task AddStudent(IStudentService studentService)
        {
            Console.Write("Enter student name: ");
            var name = Console.ReadLine();

            Console.Write("Enter student grade (0-100): ");
            if (!int.TryParse(Console.ReadLine(), out int grade) || !Student.ValidateGrade(grade))
            {
                Console.WriteLine("Invalid grade. Please enter a number between 0 and 100.");
                return;
            }

            var studentDto = new StudentDto { Name = name, Grade = grade };

            try
            {
                var (student, quote) = await studentService.AddStudent(studentDto);
                Console.WriteLine($"\nStudent added successfully! ID: {student.Id}");
                Console.WriteLine($"Motivational Quote: \"{quote.Content}\" - {quote.Author}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student: {ex.Message}");
            }
        }

        private static void ViewAllStudents(IStudentService studentService)
        {
            var students = studentService.GetAllStudents();

            Console.WriteLine("\nList of Students:");
            Console.WriteLine("ID\tName\tGrade");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id}\t{student.Name}\t{student.Grade}");
            }
        }

        private static void UpdateStudent(IStudentService studentService)
        {
            Console.Write("Enter student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var existingStudent = studentService.GetStudent(id);
            if (existingStudent == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write($"Enter new name (current: {existingStudent.Name}): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                existingStudent.Name = name;
            }

            Console.Write($"Enter new grade (current: {existingStudent.Grade}): ");
            var gradeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gradeInput) && int.TryParse(gradeInput, out int grade) && Student.ValidateGrade(grade))
            {
                existingStudent.Grade = grade;
            }

            studentService.UpdateStudent(existingStudent);
            Console.WriteLine("Student updated successfully!");
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IStudentRepository, StudentRepository>()
                .AddHttpClient<IQuoteService, QuoteService>()
                    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    })
                    .Services
                .AddScoped<IStudentService, StudentService>()
                .BuildServiceProvider();
        }
    }
}