using System;

namespace BMI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the BMI Calculator!");

            // Prompt for height in feet
            Console.Write("Enter your height (feet): ");
            double feet = double.Parse(Console.ReadLine() ?? "0");

            // Prompt for height in inches
            Console.Write("Enter remaining inches: ");
            double inches = double.Parse(Console.ReadLine() ?? "0");

            // Prompt for weight in pounds
            Console.Write("Enter your weight in pounds: ");
            double pounds = double.Parse(Console.ReadLine() ?? "0");

            try
            {
                double bmiValue = BMICalculator.CalculateBMI(feet, inches, pounds);
                string category = BMICalculator.DetermineBMICategory(bmiValue);

                Console.WriteLine($"\nYour BMI is {bmiValue:F2}. Category: {category}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    public static class BMICalculator
    {
        // 1) Calculate the numeric BMI value
        public static double CalculateBMI(double heightFeet, double heightInches, double weightPounds)
        {
            if (heightFeet < 0 || heightInches < 0 || (heightFeet == 0 && heightInches == 0))
                throw new ArgumentException("Height must be greater than 0.");
            if (weightPounds <= 0)
                throw new ArgumentException("Weight must be greater than 0.");

            // Convert total height to inches
            double totalInches = (heightFeet * 12) + heightInches;
            // Convert to meters
            double heightMeters = totalInches * 0.0254;
            // Convert weight to kilograms
            double weightKg = weightPounds * 0.45359237;

            double bmi = weightKg / (heightMeters * heightMeters);
            return bmi;
        }

        // 2) Determine the category based on BMI
        public static string DetermineBMICategory(double bmi)
        {
            // The correct boundary for Underweight vs Normal is 18.5
            // The correct boundary for Normal vs Overweight is 25.0
            // The correct boundary for Overweight vs Obese is 30.0

            if (bmi < 18.5)
            {
                return "Underweight";
            }
            else if (bmi < 25.0)
            {
                return "Normal";
            }
            else if (bmi < 30.0)
            {
                return "Overweight";
            }
            else
            {
                return "Obese";
            }
        }
    }
}
