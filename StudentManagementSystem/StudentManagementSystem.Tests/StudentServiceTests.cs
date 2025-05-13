using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using StudentManagementSystem.Application;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Data;
using StudentManagementSystem.Domain;
using Xunit;

namespace StudentManagementSystem.Tests
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentRepository> _mockRepo;
        private readonly Mock<IQuoteService> _mockQuoteService;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            _mockRepo = new Mock<IStudentRepository>();
            _mockQuoteService = new Mock<IQuoteService>();
            _service = new StudentService(_mockRepo.Object, _mockQuoteService.Object);
        }

        [Fact]
        public async Task AddStudent_ValidData_ReturnsStudentAndQuote()
        {
            // Arrange
            var studentDto = new StudentDto { Name = "John Doe", Grade = 85 };
            var expectedQuote = new QuoteDto { Content = "Test quote", Author = "Test Author" };

            _mockQuoteService
                .Setup(s => s.GetMotivationalQuote())
                .ReturnsAsync(expectedQuote);

            // Act
            var (student, quote) = await _service.AddStudent(studentDto);

            // Assert
            Assert.Equal("John Doe", student.Name);
            Assert.Equal(85, student.Grade);
            Assert.Equal(expectedQuote.Content, quote.Content);
            Assert.Equal(expectedQuote.Author, quote.Author);

            _mockRepo.Verify(r => r.Add(It.IsAny<Student>()), Times.Once);
            _mockQuoteService.Verify(s => s.GetMotivationalQuote(), Times.Once);
        }

        [Fact]
        public void GetAllStudents_ReturnsAllStudents()
        {
            // Arrange
            var testStudents = new List<Student>
            {
                new Student { Id = 1, Name = "Test 1", Grade = 90 },
                new Student { Id = 2, Name = "Test 2", Grade = 85 }
            };

            _mockRepo
                .Setup(r => r.GetAll())
                .Returns(testStudents);

            // Act
            var result = _service.GetAllStudents();

            // Assert
            Assert.Equal(2, result.Count());
            _mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetStudent_Exists_ReturnsStudent()
        {
            // Arrange
            var testStudent = new Student { Id = 1, Name = "Test", Grade = 90 };
            _mockRepo.Setup(r => r.GetById(1)).Returns(testStudent);

            // Act
            var result = _service.GetStudent(1);

            // Assert
            Assert.Equal(testStudent, result);
        }

        [Fact]
        public void GetStudent_NotExists_ReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Student)null);

            // Act
            var result = _service.GetStudent(999);

            // Assert
            Assert.Null(result);
        }
    }
}