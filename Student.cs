namespace FinanceSystem
{
    /// <summary>
    /// Represents a student with ID, full name, and score
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the student ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the student's full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the student's score
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Initializes a new instance of the Student class
        /// </summary>
        /// <param name="id">The student ID</param>
        /// <param name="fullName">The student's full name</param>
        /// <param name="score">The student's score</param>
        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        /// <summary>
        /// Determines the letter grade based on the student's score
        /// </summary>
        /// <returns>Letter grade (A, B, C, D, or F)</returns>
        public string GetGrade()
        {
            return Score switch
            {
                >= 80 and <= 100 => "A",
                >= 70 and <= 79 => "B",
                >= 60 and <= 69 => "C",
                >= 50 and <= 59 => "D",
                < 50 => "F",
                _ => "Invalid" // For scores above 100 or negative
            };
        }

        /// <summary>
        /// Returns a string representation of the student
        /// </summary>
        /// <returns>Formatted string with student information</returns>
        public override string ToString()
        {
            return $"{FullName} (ID: {Id}): Score = {Score}, Grade = {GetGrade()}";
        }
    }
}
