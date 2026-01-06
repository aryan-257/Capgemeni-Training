using System;

namespace Question5
{
    class AdmissionEligibility
    {
        public static void CheckEligibility()
        {
            Console.Write("Enter Math marks: ");
            int math = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Physics marks: ");
            int phys = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Chemistry marks: ");
            int chem = Convert.ToInt32(Console.ReadLine());

            int total = math + phys + chem;

            if (math >= 65 && phys >= 55 && chem >= 50 &&
                (total >= 180 || (math + phys) >= 140))
            {
                Console.WriteLine("Candidate is eligible for admission.");
            }
            else
            {
                Console.WriteLine("Candidate is not eligible for admission.");
            }
        }
    }
}
