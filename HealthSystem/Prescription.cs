using System;

namespace FinanceSystem.HealthSystem
{
    /// <summary>
    /// Represents a prescription issued to a patient
    /// </summary>
    public class Prescription
    {
        /// <summary>
        /// Unique identifier for the prescription
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// ID of the patient this prescription is for
        /// </summary>
        public int PatientId { get; }

        /// <summary>
        /// Name of the prescribed medication
        /// </summary>
        public string MedicationName { get; }

        /// <summary>
        /// Date when the prescription was issued
        /// </summary>
        public DateTime DateIssued { get; }

        /// <summary>
        /// Initializes a new instance of the Prescription class
        /// </summary>
        /// <param name="id">Unique prescription identifier</param>
        /// <param name="patientId">ID of the patient</param>
        /// <param name="medicationName">Name of the medication</param>
        /// <param name="dateIssued">Date the prescription was issued</param>
        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        {
            Id = id;
            PatientId = patientId;
            MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
            DateIssued = dateIssued;
        }

        /// <summary>
        /// Returns a string representation of the prescription
        /// </summary>
        /// <returns>Formatted prescription information</returns>
        public override string ToString()
        {
            return $"Prescription ID: {Id}, Patient ID: {PatientId}, Medication: {MedicationName}, Date Issued: {DateIssued:yyyy-MM-dd}";
        }

        /// <summary>
        /// Determines whether two Prescription objects are equal based on their ID
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if the prescriptions have the same ID, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            return obj is Prescription other && Id == other.Id;
        }

        /// <summary>
        /// Returns the hash code for this prescription based on the ID
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
