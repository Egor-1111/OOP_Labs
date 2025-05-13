using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Domain;

namespace StudentManagementSystem.Application
{
    public interface IStudentService
    {
        Task<(Student student, Quote quote)> AddStudent(StudentDto studentDto);
        void UpdateStudent(Student student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(int id);
    }
}