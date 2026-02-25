using System;

namespace Problem5.UniversityCourse
{
    public class CourseFullException : Exception
    {
        public CourseFullException(string message) : base(message) { }
    }

    public class InvalidGPAException : Exception
    {
        public InvalidGPAException(string message) : base(message) { }
    }

    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException(string message) : base(message) { }
    }
}
