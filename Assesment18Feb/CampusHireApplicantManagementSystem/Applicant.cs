using System;

namespace CampusHireApplicantManagementSystem
{
    [Serializable]
    public class Applicant
    {
        public string ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string CurrentLocation { get; set; }
        public string PreferredJobLocation { get; set; }
        public string CoreCompetency { get; set; }
        public int PassingYear { get; set; }

        public Applicant() { }

        public Applicant(string applicantId, string applicantName, string currentLocation, 
                        string preferredJobLocation, string coreCompetency, int passingYear)
        {
            ApplicantId = applicantId;
            ApplicantName = applicantName;
            CurrentLocation = currentLocation;
            PreferredJobLocation = preferredJobLocation;
            CoreCompetency = coreCompetency;
            PassingYear = passingYear;
        }

        public override string ToString()
        {
            return $"ID: {ApplicantId}, Name: {ApplicantName}, Current Location: {CurrentLocation}, " +
                   $"Preferred Location: {PreferredJobLocation}, Competency: {CoreCompetency}, Passing Year: {PassingYear}";
        }
    }
}
