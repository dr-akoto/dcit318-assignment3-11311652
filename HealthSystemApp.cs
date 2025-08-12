using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceSystem
{
    /// <summary>
    /// Main healthcare system application that manages patients and prescriptions
    /// </summary>
    public class HealthSystemApp
    {
        /// <summary>
        /// Repository for managing patient data
        /// </summary>
        private Repository<Patient> _patientRepo;

        /// <summary>
        /// Repository for managing prescription data
        /// </summary>
        private Repository<Prescription> _prescriptionRepo;

        /// <summary>
        /// Dictionary mapping patient IDs to their list of prescriptions
        /// Key: Patient ID, Value: List of prescriptions for that patient
        /// </summary>
        private Dictionary<int, List<Prescription>> _prescriptionMap;

        /// <summary>
        /// Initializes a new instance of the HealthSystemApp
        /// </summary>
        public HealthSystemApp()
        {
            _patientRepo = new Repository<Patient>();
            _prescriptionRepo = new Repository<Prescription>();
            _prescriptionMap = new Dictionary<int, List<Prescription>>();
        }

        /// <summary>
        /// Main execution method that runs the interactive healthcare management system
        /// </summary>
        public void Run()
        {
            Console.WriteLine("=== Healthcare Management System ===\n");

            // Initialize with some sample data
            SeedInitialData();
            BuildPrescriptionMap();

            bool continueProcessing = true;

            while (continueProcessing)
            {
                try
                {
                    int operationType = GetOperationType();

                    switch (operationType)
                    {
                        case 0:
                            continueProcessing = false;
                            Console.WriteLine("Thank you for using the Healthcare Management System!");
                            break;

                        case 1:
                            AddNewPatient();
                            break;

                        case 2:
                            AddNewPrescription();
                            break;

                        case 3:
                            ViewAllPatients();
                            break;

                        case 4:
                            ViewPatientPrescriptions();
                            break;

                        case 5:
                            SearchPatients();
                            break;

                        case 6:
                            ViewSystemSummary();
                            break;

                        case 7:
                            RemovePatient();
                            break;

                        case 8:
                            RemovePrescription();
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}\n");
                    Console.WriteLine("Please try again.\n");
                }
            }
        }

        /// <summary>
        /// Seeds the system with initial sample patient and prescription data
        /// </summary>
        private void SeedInitialData()
        {
            // Add sample patients
            var patients = new[]
            {
                new Patient(1, "John Doe", 35, "Male"),
                new Patient(2, "Jane Smith", 28, "Female"),
                new Patient(3, "Robert Johnson", 45, "Male")
            };

            foreach (var patient in patients)
            {
                _patientRepo.Add(patient);
            }

            // Add sample prescriptions
            var prescriptions = new[]
            {
                new Prescription(101, 1, "Amoxicillin 500mg", DateTime.Now.AddDays(-5)),
                new Prescription(102, 1, "Ibuprofen 200mg", DateTime.Now.AddDays(-3)),
                new Prescription(103, 2, "Metformin 1000mg", DateTime.Now.AddDays(-7)),
                new Prescription(104, 2, "Lisinopril 10mg", DateTime.Now.AddDays(-2)),
                new Prescription(105, 3, "Atorvastatin 20mg", DateTime.Now.AddDays(-10))
            };

            foreach (var prescription in prescriptions)
            {
                _prescriptionRepo.Add(prescription);
            }

            Console.WriteLine($"System initialized with {_patientRepo.Count} patients and {_prescriptionRepo.Count} prescriptions.\n");
        }

        /// <summary>
        /// Gets the operation type from user input
        /// </summary>
        /// <returns>The selected operation type</returns>
        private int GetOperationType()
        {
            Console.WriteLine("Healthcare Management System - Select an option:");
            Console.WriteLine("1. Add New Patient");
            Console.WriteLine("2. Add New Prescription");
            Console.WriteLine("3. View All Patients");
            Console.WriteLine("4. View Patient Prescriptions");
            Console.WriteLine("5. Search Patients");
            Console.WriteLine("6. View System Summary");
            Console.WriteLine("7. Remove Patient");
            Console.WriteLine("8. Remove Prescription");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice (0-8): ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= 8)
            {
                Console.WriteLine();
                return choice;
            }

            Console.WriteLine("Invalid input. Please enter a number between 0 and 8.\n");
            return -1;
        }

        /// <summary>
        /// Adds a new patient to the system
        /// </summary>
        private void AddNewPatient()
        {
            Console.WriteLine("=== Add New Patient ===");

            try
            {
                Console.Write("Enter Patient ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                // Check if patient ID already exists
                var existingPatient = _patientRepo.GetById(p => p.Id == id);
                if (existingPatient != null)
                {
                    Console.WriteLine($"Patient with ID {id} already exists.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                Console.Write("Enter Patient Name: ");
                string name = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                Console.Write("Enter Patient Age: ");
                if (!int.TryParse(Console.ReadLine(), out int age) || age < 0 || age > 150)
                {
                    Console.WriteLine("Invalid age. Please enter a valid age between 0 and 150.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                Console.Write("Enter Patient Gender (Male/Female/Other): ");
                string gender = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(gender))
                {
                    Console.WriteLine("Gender cannot be empty.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                var newPatient = new Patient(id, name, age, gender);
                _patientRepo.Add(newPatient);

                Console.WriteLine($"Successfully added patient: {newPatient}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding patient: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Adds a new prescription to the system
        /// </summary>
        private void AddNewPrescription()
        {
            Console.WriteLine("=== Add New Prescription ===");

            try
            {
                Console.Write("Enter Prescription ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                // Check if prescription ID already exists
                var existingPrescription = _prescriptionRepo.GetById(p => p.Id == id);
                if (existingPrescription != null)
                {
                    Console.WriteLine($"Prescription with ID {id} already exists.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                Console.Write("Enter Patient ID: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Invalid Patient ID. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                // Verify patient exists
                var patient = _patientRepo.GetById(p => p.Id == patientId);
                if (patient == null)
                {
                    Console.WriteLine($"Patient with ID {patientId} not found. Please add the patient first.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                Console.Write("Enter Medication Name: ");
                string medicationName = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(medicationName))
                {
                    Console.WriteLine("Medication name cannot be empty.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                Console.Write("Enter Date Issued (YYYY-MM-DD) or press Enter for today: ");
                string dateInput = Console.ReadLine()?.Trim() ?? "";
                DateTime dateIssued;

                if (string.IsNullOrEmpty(dateInput))
                {
                    dateIssued = DateTime.Now;
                }
                else if (!DateTime.TryParse(dateInput, out dateIssued))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine();
                    return;
                }

                var newPrescription = new Prescription(id, patientId, medicationName, dateIssued);
                _prescriptionRepo.Add(newPrescription);

                // Update prescription map
                BuildPrescriptionMap();

                Console.WriteLine($"Successfully added prescription: {newPrescription}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding prescription: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Views all patients in the system
        /// </summary>
        private void ViewAllPatients()
        {
            Console.WriteLine("=== All Patients ===");

            var patients = _patientRepo.GetAll();

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found in the system.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            foreach (var patient in patients.OrderBy(p => p.Id))
            {
                Console.WriteLine($"  {patient}");
            }

            Console.WriteLine($"\nTotal patients: {patients.Count}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Views prescriptions for a specific patient
        /// </summary>
        private void ViewPatientPrescriptions()
        {
            Console.WriteLine("=== View Patient Prescriptions ===");

            Console.Write("Enter Patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("Invalid Patient ID. Please enter a valid number.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            PrintPrescriptionsForPatient(patientId);
        }

        /// <summary>
        /// Searches for patients by name or other criteria
        /// </summary>
        private void SearchPatients()
        {
            Console.WriteLine("=== Search Patients ===");
            Console.WriteLine("1. Search by Name");
            Console.WriteLine("2. Search by Age Range");
            Console.WriteLine("3. Search by Gender");
            Console.Write("Select search type (1-3): ");

            if (!int.TryParse(Console.ReadLine(), out int searchType) || searchType < 1 || searchType > 3)
            {
                Console.WriteLine("Invalid search type. Please enter 1, 2, or 3.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            var allPatients = _patientRepo.GetAll();
            List<Patient> results = new List<Patient>();

            switch (searchType)
            {
                case 1:
                    Console.Write("Enter name to search for: ");
                    string searchName = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrEmpty(searchName))
                    {
                        results = allPatients.Where(p => p.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    break;

                case 2:
                    Console.Write("Enter minimum age: ");
                    if (int.TryParse(Console.ReadLine(), out int minAge))
                    {
                        Console.Write("Enter maximum age: ");
                        if (int.TryParse(Console.ReadLine(), out int maxAge))
                        {
                            results = allPatients.Where(p => p.Age >= minAge && p.Age <= maxAge).ToList();
                        }
                    }
                    break;

                case 3:
                    Console.Write("Enter gender to search for: ");
                    string searchGender = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrEmpty(searchGender))
                    {
                        results = allPatients.Where(p => p.Gender.Equals(searchGender, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    break;
            }

            if (results.Count == 0)
            {
                Console.WriteLine("No patients found matching the search criteria.");
            }
            else
            {
                Console.WriteLine($"\nSearch Results ({results.Count} patient(s) found):");
                foreach (var patient in results.OrderBy(p => p.Id))
                {
                    Console.WriteLine($"  {patient}");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Views system summary information
        /// </summary>
        private void ViewSystemSummary()
        {
            PrintSystemSummary();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Removes a patient from the system
        /// </summary>
        private void RemovePatient()
        {
            Console.WriteLine("=== Remove Patient ===");

            Console.Write("Enter Patient ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("Invalid Patient ID. Please enter a valid number.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            var patient = _patientRepo.GetById(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine($"Patient with ID {patientId} not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"Found patient: {patient}");
            Console.Write("Are you sure you want to remove this patient? (y/N): ");
            string confirmation = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (confirmation == "y" || confirmation == "yes")
            {
                bool removed = _patientRepo.Remove(p => p.Id == patientId);
                if (removed)
                {
                    // Also remove all prescriptions for this patient
                    var prescriptionsRemoved = 0;
                    var allPrescriptions = _prescriptionRepo.GetAll().Where(p => p.PatientId == patientId).ToList();
                    foreach (var prescription in allPrescriptions)
                    {
                        if (_prescriptionRepo.Remove(p => p.Id == prescription.Id))
                        {
                            prescriptionsRemoved++;
                        }
                    }

                    // Rebuild prescription map
                    BuildPrescriptionMap();

                    Console.WriteLine($"Successfully removed patient and {prescriptionsRemoved} associated prescription(s).");
                }
                else
                {
                    Console.WriteLine("Failed to remove patient.");
                }
            }
            else
            {
                Console.WriteLine("Patient removal cancelled.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Removes a prescription from the system
        /// </summary>
        private void RemovePrescription()
        {
            Console.WriteLine("=== Remove Prescription ===");

            Console.Write("Enter Prescription ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int prescriptionId))
            {
                Console.WriteLine("Invalid Prescription ID. Please enter a valid number.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            var prescription = _prescriptionRepo.GetById(p => p.Id == prescriptionId);
            if (prescription == null)
            {
                Console.WriteLine($"Prescription with ID {prescriptionId} not found.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"Found prescription: {prescription}");
            Console.Write("Are you sure you want to remove this prescription? (y/N): ");
            string confirmation = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (confirmation == "y" || confirmation == "yes")
            {
                bool removed = _prescriptionRepo.Remove(p => p.Id == prescriptionId);
                if (removed)
                {
                    // Rebuild prescription map
                    BuildPrescriptionMap();
                    Console.WriteLine("Successfully removed prescription.");
                }
                else
                {
                    Console.WriteLine("Failed to remove prescription.");
                }
            }
            else
            {
                Console.WriteLine("Prescription removal cancelled.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Builds the prescription mapping by grouping prescriptions by patient ID
        /// </summary>
        private void BuildPrescriptionMap()
        {
            _prescriptionMap.Clear();

            // Get all prescriptions and group them by PatientId
            var allPrescriptions = _prescriptionRepo.GetAll();
            var groupedPrescriptions = allPrescriptions.GroupBy(p => p.PatientId);

            foreach (var group in groupedPrescriptions)
            {
                int patientId = group.Key;
                List<Prescription> patientPrescriptions = group.ToList();
                _prescriptionMap[patientId] = patientPrescriptions;
            }
        }

        /// <summary>
        /// Prints all prescriptions for a specific patient
        /// </summary>
        /// <param name="patientId">The ID of the patient</param>
        public void PrintPrescriptionsForPatient(int patientId)
        {
            Console.WriteLine($"=== Prescriptions for Patient ID: {patientId} ===");

            // First, verify the patient exists
            var patient = _patientRepo.GetById(p => p.Id == patientId);
            if (patient == null)
            {
                Console.WriteLine($"Patient with ID {patientId} not found in the system.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            Console.WriteLine($"\nPatient: {patient}");

            // Get prescriptions using the map
            var prescriptions = GetPrescriptionsByPatientId(patientId);

            if (prescriptions.Count == 0)
            {
                Console.WriteLine("\nNo prescriptions found for this patient.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            Console.WriteLine("\nPrescriptions:");
            foreach (var prescription in prescriptions.OrderBy(p => p.DateIssued))
            {
                Console.WriteLine($"  - {prescription}");
            }

            Console.WriteLine($"\nTotal prescriptions: {prescriptions.Count}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Retrieves prescriptions for a specific patient from the dictionary
        /// </summary>
        /// <param name="patientId">The patient ID to look up</param>
        /// <returns>List of prescriptions for the patient, or empty list if none found</returns>
        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            if (_prescriptionMap.TryGetValue(patientId, out List<Prescription>? prescriptions))
            {
                return prescriptions;
            }
            return new List<Prescription>();
        }

        /// <summary>
        /// Displays a summary of the entire healthcare system
        /// </summary>
        public void PrintSystemSummary()
        {
            Console.WriteLine("=== Healthcare System Summary ===");
            Console.WriteLine($"Total Patients: {_patientRepo.Count}");
            Console.WriteLine($"Total Prescriptions: {_prescriptionRepo.Count}");
            Console.WriteLine($"Patients with Prescriptions: {_prescriptionMap.Count}");

            if (_prescriptionMap.Count > 0)
            {
                var totalPrescriptions = _prescriptionMap.Values.Sum(list => list.Count);
                var averagePrescriptions = (double)totalPrescriptions / _prescriptionMap.Count;
                Console.WriteLine($"Average Prescriptions per Patient: {averagePrescriptions:F1}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates additional repository operations
        /// </summary>
        public void DemonstrateRepositoryOperations()
        {
            Console.WriteLine("=== Repository Operations Demo ===\n");

            // Find a specific patient
            var johnDoe = _patientRepo.GetById(p => p.Name.Contains("John"));
            if (johnDoe != null)
            {
                Console.WriteLine($"Found patient: {johnDoe}");
            }

            // Find prescriptions for a specific medication
            var allPrescriptions = _prescriptionRepo.GetAll();
            var ibuprofenPrescriptions = allPrescriptions.Where(p => p.MedicationName.Contains("Ibuprofen"));

            Console.WriteLine("\nIbuprofen prescriptions:");
            foreach (var prescription in ibuprofenPrescriptions)
            {
                Console.WriteLine($"  - {prescription}");
            }

            Console.WriteLine();
        }
    }
}
