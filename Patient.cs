using System;

namespace FinanceSystem
{
    /// <summary>
    /// Represents a patient in the healthcare system
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Unique identifier for the patient
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Full name of the patient
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Age of the patient in years
        /// </summary>
        public int Age { get; }

        /// <summary>
        /// Gender of the patient
        /// </summary>
        public string Gender { get; }

        /// <summary>
        /// Initializes a new instance of the Patient class
        /// </summary>
        /// <param name="id">Unique patient identifier</param>
        /// <param name="name">Patient's full name</param>
        /// <param name="age">Patient's age</param>
        /// <param name="gender">Patient's gender</param>
        public Patient(int id, string name, int age, string gender)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age;
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
        }

        /// <summary>
        /// Returns a string representation of the patient
        /// </summary>
        /// <returns>Formatted patient information</returns>
        public override string ToString()
        {
            return $"Patient ID: {Id}, Name: {Name}, Age: {Age}, Gender: {Gender}";
        }

        /// <summary>
        /// Determines whether two Patient objects are equal based on their ID
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if the patients have the same ID, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            return obj is Patient other && Id == other.Id;
        }

        /// <summary>
        /// Returns the hash code for this patient based on the ID
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
