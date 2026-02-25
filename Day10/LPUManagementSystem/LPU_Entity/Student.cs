namespace LPU_Entity
{
    public enum CourseType
    {
        Mechanical,
        Electrical,
        Civil,
        CSE,
        IT
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseType Course { get; set; } //Property of type Enum
        public string Address {  get; set; }


    }
}
