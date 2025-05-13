using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using StudentManagementSystem.Domain;

namespace StudentManagementSystem.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new List<Student>();
        private int _nextId = 1;

        public void Add(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
        }

        public void Update(Student student)
        {
            var existing = _students.FirstOrDefault(s => s.Id == student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Grade = student.Grade;
            }
        }

        public IEnumerable<Student> GetAll() => _students;

        public Student GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public void SaveToFile(string filePath)
        {
            var json = JsonSerializer.Serialize(_students);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var students = JsonSerializer.Deserialize<List<Student>>(json);
                _students.Clear();
                _students.AddRange(students);
                _nextId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            }
        }
    }
}