using System.Collections.Generic;
using StudentManagementSystem.Domain;

namespace StudentManagementSystem.Data
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Update(Student student);
        IEnumerable<Student> GetAll();
        Student GetById(int id);
        void SaveToFile(string filePath);
        void LoadFromFile(string filePath);
    }
}