using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem2.HospitalEmergency
{
    public class EmergencyManager
    {
        private SortedDictionary<int, Queue<Patient>> emergencyQueue = new SortedDictionary<int, Queue<Patient>>();
        private const int MaxQueueSize = 50;

        public void AddPatient(Patient patient)
        {
            if (patient.Severity < 1 || patient.Severity > 10)
                throw new InvalidSeverityException("Severity must be between 1 (Critical) and 10 (Minor)");

            if (!emergencyQueue.ContainsKey(patient.Severity))
                emergencyQueue[patient.Severity] = new Queue<Patient>();

            if (emergencyQueue[patient.Severity].Count >= MaxQueueSize)
                throw new QueueOverflowException($"Queue for severity {patient.Severity} is full");

            emergencyQueue[patient.Severity].Enqueue(patient);
            Console.WriteLine($"Added patient {patient.Name} with severity {patient.Severity}");
        }

        public Patient GetNextPatient()
        {
            if (emergencyQueue.Count == 0)
                throw new PatientNotFoundException("No patients in queue");

            var firstQueue = emergencyQueue.First();
            if (firstQueue.Value.Count == 0)
            {
                emergencyQueue.Remove(firstQueue.Key);
                return GetNextPatient();
            }

            var patient = firstQueue.Value.Dequeue();
            Console.WriteLine($"Next patient: {patient.Name} (Severity: {patient.Severity})");
            return patient;
        }

        public void RemovePatient(string patientId)
        {
            foreach (var kvp in emergencyQueue)
            {
                var patients = kvp.Value.ToList();
                var patient = patients.FirstOrDefault(p => p.PatientId == patientId);
                if (patient != null)
                {
                    kvp.Value.Clear();
                    foreach (var p in patients.Where(p => p.PatientId != patientId))
                        kvp.Value.Enqueue(p);
                    Console.WriteLine($"Removed patient {patient.Name}");
                    return;
                }
            }
            throw new PatientNotFoundException($"Patient with ID {patientId} not found");
        }

        public void DisplayQueue()
        {
            Console.WriteLine("\n=== Emergency Queue ===");
            foreach (var kvp in emergencyQueue)
            {
                Console.WriteLine($"\nSeverity {kvp.Key}:");
                foreach (var patient in kvp.Value)
                    Console.WriteLine($"  - {patient.Name} ({patient.Condition})");
            }
        }
    }
}
