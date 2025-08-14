using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinanceSystem.GradingSystem
{
    /// <summary>
    /// Processes student results from input files and generates reports
    /// </summary>
    public class StudentResultProcessor
    {
        /// <summary>
        /// Reads student data from a text file and validates the data
        /// </summary>
        /// <param name="inputFilePath">Path to the input file containing student data</param>
        /// <returns>List of valid Student objects</returns>
        /// <exception cref="FileNotFoundException">Thrown when the input file is not found</exception>
        /// <exception cref="InvalidScoreFormatException">Thrown when score cannot be converted to integer</exception>
        /// <exception cref="MissingFieldException">Thrown when required fields are missing</exception>
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();
            int lineNumber = 0;

            using (var reader = new StreamReader(inputFilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;

                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    try
                    {
                        // Split the line by comma and trim whitespace
                        string[] fields = line.Split(',');
                        for (int i = 0; i < fields.Length; i++)
                        {
                            fields[i] = fields[i].Trim();
                        }

                        // Validate that we have exactly 3 fields
                        if (fields.Length != 3)
                        {
                            throw new MissingFieldException(
                                $"Line {lineNumber}: Expected 3 fields (ID, FullName, Score), but found {fields.Length}. " +
                                $"Line content: '{line}'");
                        }

                        // Validate that none of the fields are empty
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(fields[i]))
                            {
                                string fieldName = i switch
                                {
                                    0 => "Student ID",
                                    1 => "Full Name",
                                    2 => "Score",
                                    _ => $"Field {i + 1}"
                                };
                                throw new MissingFieldException(
                                    $"Line {lineNumber}: {fieldName} is empty or missing. Line content: '{line}'");
                            }
                        }

                        // Parse student ID
                        if (!int.TryParse(fields[0], out int studentId))
                        {
                            throw new InvalidScoreFormatException(
                                $"Line {lineNumber}: Student ID '{fields[0]}' is not a valid integer. Line content: '{line}'");
                        }

                        // Parse score
                        if (!int.TryParse(fields[2], out int score))
                        {
                            throw new InvalidScoreFormatException(
                                $"Line {lineNumber}: Score '{fields[2]}' is not a valid integer. Line content: '{line}'");
                        }

                        // Validate score range (optional - you can remove this if you want to allow any score)
                        if (score < 0 || score > 100)
                        {
                            Console.WriteLine($"Warning - Line {lineNumber}: Score {score} is outside typical range (0-100). " +
                                            $"Student: {fields[1]}");
                        }

                        // Create and add the student
                        var student = new Student(studentId, fields[1], score);
                        students.Add(student);

                        Console.WriteLine($"✓ Successfully processed: {student.FullName} (ID: {student.Id})");
                    }
                    catch (Exception ex) when (ex is InvalidScoreFormatException || ex is MissingFieldException)
                    {
                        // Re-throw our custom exceptions with line context
                        throw;
                    }
                    catch (Exception ex)
                    {
                        // Wrap unexpected exceptions
                        throw new Exception($"Line {lineNumber}: Unexpected error processing line '{line}': {ex.Message}", ex);
                    }
                }
            }

            Console.WriteLine($"\nSuccessfully processed {students.Count} students from file.");
            return students;
        }

        /// <summary>
        /// Writes a formatted report of student grades to an output file
        /// </summary>
        /// <param name="students">List of students to include in the report</param>
        /// <param name="outputFilePath">Path where the report will be written</param>
        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (var writer = new StreamWriter(outputFilePath))
            {
                // Write header
                writer.WriteLine("=".PadRight(80, '='));
                writer.WriteLine("                          STUDENT GRADE REPORT");
                writer.WriteLine("=".PadRight(80, '='));
                writer.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"Total Students: {students.Count}");
                writer.WriteLine();

                // Write individual student results
                writer.WriteLine("INDIVIDUAL RESULTS:");
                writer.WriteLine("-".PadRight(80, '-'));

                foreach (var student in students.OrderBy(s => s.Id))
                {
                    writer.WriteLine(student.ToString());
                }

                // Write summary statistics
                writer.WriteLine();
                writer.WriteLine("GRADE DISTRIBUTION:");
                writer.WriteLine("-".PadRight(80, '-'));

                var gradeDistribution = students
                    .GroupBy(s => s.GetGrade())
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => g.Key, g => g.Count());

                foreach (var grade in new[] { "A", "B", "C", "D", "F" })
                {
                    int count = gradeDistribution.ContainsKey(grade) ? gradeDistribution[grade] : 0;
                    double percentage = students.Count > 0 ? (count * 100.0 / students.Count) : 0;
                    writer.WriteLine($"Grade {grade}: {count,3} students ({percentage,5:F1}%)");
                }

                // Write class statistics
                writer.WriteLine();
                writer.WriteLine("CLASS STATISTICS:");
                writer.WriteLine("-".PadRight(80, '-'));

                if (students.Count > 0)
                {
                    double averageScore = students.Average(s => s.Score);
                    int highestScore = students.Max(s => s.Score);
                    int lowestScore = students.Min(s => s.Score);
                    var topStudent = students.First(s => s.Score == highestScore);

                    writer.WriteLine($"Class Average: {averageScore:F2}");
                    writer.WriteLine($"Highest Score: {highestScore} ({topStudent.FullName})");
                    writer.WriteLine($"Lowest Score: {lowestScore}");
                    writer.WriteLine($"Pass Rate (≥50): {students.Count(s => s.Score >= 50) * 100.0 / students.Count:F1}%");
                }

                writer.WriteLine();
                writer.WriteLine("=".PadRight(80, '='));
                writer.WriteLine("                              END OF REPORT");
                writer.WriteLine("=".PadRight(80, '='));
            }

            Console.WriteLine($"\n✓ Report successfully written to: {outputFilePath}");
        }

        /// <summary>
        /// Processes student data from input file and generates a comprehensive report
        /// </summary>
        /// <param name="inputFilePath">Path to the input file</param>
        /// <param name="outputFilePath">Path for the output report</param>
        public void ProcessStudentGrades(string inputFilePath, string outputFilePath)
        {
            try
            {
                Console.WriteLine("=== Student Grade Processing System ===\n");
                Console.WriteLine($"Reading student data from: {inputFilePath}");

                // Read and validate student data
                var students = ReadStudentsFromFile(inputFilePath);

                if (students.Count == 0)
                {
                    Console.WriteLine("⚠️  No valid student records found in the input file.");
                    return;
                }

                // Generate and write report
                Console.WriteLine($"\nGenerating report to: {outputFilePath}");
                WriteReportToFile(students, outputFilePath);

                Console.WriteLine("\n✅ Grade processing completed successfully!");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"❌ File Error: The input file '{inputFilePath}' was not found.");
                Console.WriteLine($"   Please check that the file exists and the path is correct.");
                Console.WriteLine($"   Details: {ex.Message}");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"❌ Score Format Error: {ex.Message}");
                Console.WriteLine("   Please ensure all scores are valid integers.");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($"❌ Missing Field Error: {ex.Message}");
                Console.WriteLine("   Please ensure each line has exactly 3 fields: ID, FullName, Score");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"❌ Access Error: Unable to access the file.");
                Console.WriteLine($"   Details: {ex.Message}");
                Console.WriteLine("   Please check file permissions and ensure the file is not open in another program.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ File I/O Error: {ex.Message}");
                Console.WriteLine("   There was an error reading from or writing to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected Error: An unforeseen error occurred.");
                Console.WriteLine($"   Details: {ex.Message}");
                Console.WriteLine($"   Type: {ex.GetType().Name}");
            }
        }
    }
}
