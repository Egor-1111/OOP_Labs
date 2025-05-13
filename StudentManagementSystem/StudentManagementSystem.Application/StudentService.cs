using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Data;
using StudentManagementSystem.Domain;

namespace StudentManagementSystem.Application
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IQuoteService _quoteService;

        public StudentService(IStudentRepository repository, IQuoteService quoteService)
        {
            _repository = repository;
            _quoteService = quoteService;
        }

        public async Task<(Student student, Quote quote)> AddStudent(StudentDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Grade = studentDto.Grade
            };

            _repository.Add(student);

            var quoteDto = await _quoteService.GetMotivationalQuote();
            var quote = new Quote
            {
                Content = quoteDto.Content,
                Author = quoteDto.Author
            };

            return (student, quote);
        }

        public void UpdateStudent(Student student)
        {
            _repository.Update(student);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _repository.GetAll();
        }

        public Student GetStudent(int id)
        {
            return _repository.GetById(id);
        }
    }
}