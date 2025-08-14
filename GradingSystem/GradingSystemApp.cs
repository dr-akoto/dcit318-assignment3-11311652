using System;
using System.IO;

namespace FinanceSystem.GradingSystem
{
    /// <summary>
    /// Main application class for the Student Grading System
    /// </summary>
    public class GradingSystemApp
    {
        /// <summary>
        /// Runs the interactive grading system application
        /// </summary>
        public void Run()
        {
            Console.WriteLine("=== Student Grading System ===\n");

            while (true)
            {
                try
                {
                    Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                   Student Grading System                    ║");
                    Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║  1. Process Student Grades from File                        ║");
                    Console.WriteLine("║  2. Create Sample Input File                                 ║");
                    Console.WriteLine("║  3. View Sample Input Format                                 ║");
                    Console.WriteLine("║  0. Exit                                                     ║");
                    Console.WriteLine("║  end. Return to Main Menu                                    ║");
                    Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                    Console.Write("Enter your choice (0-3, or 'end'): ");

                    var input = Console.ReadLine()?.Trim().ToLower() ?? "";

                    if (input == "end")
                    {
                        Console.WriteLine("Returning to main menu...\n");
                        return; // Return to main menu
                    }

                    if (!int.TryParse(input, out int choice))
                    {
                        Console.WriteLine("❌ Invalid input. Please enter a number or 'end'.\n");
                        continue;
                    }

                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Exiting Grading System...\n");
                            return;

                        case 1:
                            ProcessGradesFromFile();
                            break;

                        case 2:
                            CreateSampleInputFile();
                            break;

                        case 3:
                            ShowSampleFormat();
                            break;

                        default:
                            Console.WriteLine("❌ Invalid choice. Please enter a number between 0 and 3.\n");
                            break;
                    }

                    if (choice != 0)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ An unexpected error occurred: {ex.Message}\n");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Processes student grades from a user-specified input file
        /// </summary>
        private void ProcessGradesFromFile()
        {
            Console.Clear();
            Console.WriteLine("=== Process Student Grades ===\n");

            // Get input file path
            string inputFilePath = GetInputFilePath();
            if (string.IsNullOrEmpty(inputFilePath))
            {
                Console.WriteLine("Operation cancelled.\n");
                return;
            }

            // Get output file path
            string outputFilePath = GetOutputFilePath();
            if (string.IsNullOrEmpty(outputFilePath))
            {
                Console.WriteLine("Operation cancelled.\n");
                return;
            }

            Console.WriteLine("\n" + "=".PadRight(60, '='));
            Console.WriteLine("PROCESSING STUDENT GRADES");
            Console.WriteLine("=".PadRight(60, '=') + "\n");

            // Process the files
            var processor = new StudentResultProcessor();
            processor.ProcessStudentGrades(inputFilePath, outputFilePath);
        }

        /// <summary>
        /// Gets the input file path from the user
        /// </summary>
        /// <returns>The input file path or empty string if cancelled</returns>
        private string GetInputFilePath()
        {
            while (true)
            {
                Console.WriteLine("Enter the path to the input file containing student data:");
                Console.WriteLine("(Example: C:\\Students\\input.txt or just 'students.txt' for current directory)");
                Console.Write("Input file path (or 'cancel' to abort): ");

                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input) || input.ToLower() == "cancel")
                {
                    return "";
                }

                // If just a filename is provided, use current directory
                if (!Path.IsPathRooted(input))
                {
                    input = Path.Combine(Directory.GetCurrentDirectory(), input);
                }

                if (File.Exists(input))
                {
                    Console.WriteLine($"✓ Found input file: {input}\n");
                    return input;
                }

                Console.WriteLine($"❌ File not found: {input}");
                Console.WriteLine("Please check the path and try again, or type 'cancel' to abort.\n");
            }
        }

        /// <summary>
        /// Gets the output file path from the user
        /// </summary>
        /// <returns>The output file path or empty string if cancelled</returns>
        private string GetOutputFilePath()
        {
            while (true)
            {
                Console.WriteLine("Enter the path for the output report file:");
                Console.WriteLine("(Example: C:\\Reports\\grade_report.txt or just 'report.txt' for current directory)");
                Console.Write("Output file path (or 'cancel' to abort): ");

                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input) || input.ToLower() == "cancel")
                {
                    return "";
                }

                // If just a filename is provided, use current directory
                if (!Path.IsPathRooted(input))
                {
                    input = Path.Combine(Directory.GetCurrentDirectory(), input);
                }

                try
                {
                    // Check if the directory exists, create if it doesn't
                    string? directory = Path.GetDirectoryName(input);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                        Console.WriteLine($"✓ Created directory: {directory}");
                    }

                    // Check if we can write to this location
                    using (var test = File.Create(input))
                    {
                        // File creation successful, delete the test file
                    }
                    File.Delete(input);

                    Console.WriteLine($"✓ Output will be written to: {input}\n");
                    return input;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Cannot write to: {input}");
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please try a different path or type 'cancel' to abort.\n");
                }
            }
        }

        /// <summary>
        /// Creates a sample input file for testing
        /// </summary>
        private void CreateSampleInputFile()
        {
            Console.Clear();
            Console.WriteLine("=== Create Sample Input File ===\n");

            Console.Write("Enter filename for sample file (e.g., 'sample_students.txt'): ");
            string filename = Console.ReadLine()?.Trim() ?? "sample_students.txt";

            if (!filename.EndsWith(".txt"))
            {
                filename += ".txt";
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);

            try
            {
                using (var writer = new StreamWriter(fullPath))
                {
                    // Write sample data
                    writer.WriteLine("101, Alice Johnson, 85");
                    writer.WriteLine("102, Bob Smith, 92");
                    writer.WriteLine("103, Carol Davis, 78");
                    writer.WriteLine("104, David Wilson, 67");
                    writer.WriteLine("105, Eva Brown, 94");
                    writer.WriteLine("106, Frank Miller, 56");
                    writer.WriteLine("107, Grace Taylor, 73");
                    writer.WriteLine("108, Henry Clark, 88");
                    writer.WriteLine("109, Iris Anderson, 45");
                    writer.WriteLine("110, Jack Thompson, 91");
                }

                Console.WriteLine($"✅ Sample file created successfully!");
                Console.WriteLine($"📁 Location: {fullPath}");
                Console.WriteLine($"📊 Contains 10 sample student records");
                Console.WriteLine("\nSample content:");
                Console.WriteLine("101, Alice Johnson, 85");
                Console.WriteLine("102, Bob Smith, 92");
                Console.WriteLine("...(8 more records)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error creating sample file: {ex.Message}");
            }
        }

        /// <summary>
        /// Shows the expected input file format
        /// </summary>
        private void ShowSampleFormat()
        {
            Console.Clear();
            Console.WriteLine("=== Input File Format ===\n");

            Console.WriteLine("The input file should be a plain text file (.txt) with the following format:");
            Console.WriteLine();
            Console.WriteLine("📋 FORMAT: StudentID, FullName, Score");
            Console.WriteLine("─".PadRight(50, '─'));
            Console.WriteLine("• Each line represents one student");
            Console.WriteLine("• Fields are separated by commas");
            Console.WriteLine("• StudentID must be a valid integer");
            Console.WriteLine("• FullName can contain spaces");
            Console.WriteLine("• Score must be a valid integer (typically 0-100)");
            Console.WriteLine();

            Console.WriteLine("📝 EXAMPLE:");
            Console.WriteLine("─".PadRight(30, '─'));
            Console.WriteLine("101, John Doe, 85");
            Console.WriteLine("102, Jane Smith, 92");
            Console.WriteLine("103, Bob Johnson, 78");
            Console.WriteLine("104, Alice Brown, 67");
            Console.WriteLine();

            Console.WriteLine("🚫 INVALID EXAMPLES:");
            Console.WriteLine("─".PadRight(30, '─'));
            Console.WriteLine("101, John Doe          ❌ (Missing score)");
            Console.WriteLine("ABC, Jane Smith, 92    ❌ (Invalid ID)");
            Console.WriteLine("103, Bob Johnson, XYZ  ❌ (Invalid score)");
            Console.WriteLine(", Alice Brown, 67      ❌ (Missing ID)");
            Console.WriteLine();

            Console.WriteLine("💡 TIPS:");
            Console.WriteLine("─".PadRight(20, '─'));
            Console.WriteLine("• Empty lines are ignored");
            Console.WriteLine("• Extra spaces around commas are automatically trimmed");
            Console.WriteLine("• Use 'Create Sample Input File' option to generate test data");
        }
    }
}
