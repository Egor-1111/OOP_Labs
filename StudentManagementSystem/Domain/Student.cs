namespace StudentManagementSystem.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }

        public static bool ValidateGrade(int grade)
        {
            return grade >= 0 && grade <= 100;
        }

        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length <= 100;
        }
    }
}