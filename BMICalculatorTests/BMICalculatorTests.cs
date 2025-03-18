using BMI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculatorTests
{
    [TestClass]
    public class BMICalculatorTests
    {
        // *** Tests for CalculateBMI ***

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_ZeroOrNegativeHeight_ShouldThrow()
        {
            // TDD Step: We define desired behavior → Implementation must pass
            BMICalculator.CalculateBMI(0, 0, 150);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_ZeroOrNegativeWeight_ShouldThrow()
        {
            BMICalculator.CalculateBMI(5, 6, 0);
        }

        [TestMethod]
        public void Test_CalculateBMI_BasicCheck()
        {
            double bmi = BMICalculator.CalculateBMI(5, 6, 150); // 5'6", 150 lb
            // Approx check
            Assert.IsTrue(Math.Abs(bmi - 24.21) < 0.5, "BMI was off from expected range");
        }

        [TestMethod]
        public void Test_CalculateBMI_AnotherCase()
        {
            double bmi = BMICalculator.CalculateBMI(6, 0, 180); // 6'0", 180 lb
            // We can do a rough check
            Assert.IsTrue(Math.Abs(bmi - 24.41) < 0.5, "BMI is off from expected range");
        }

        [TestMethod]
        public void Test_CalculateBMI_SmallestAllowableChange()
        {
            // For instance, check increments of 0.1 pound or 0.1 inch
            double baseBmi = BMICalculator.CalculateBMI(5, 6, 150);
            double slightlyHeavierBmi = BMICalculator.CalculateBMI(5, 6, 150.1);
            Assert.IsTrue(Math.Abs(baseBmi - slightlyHeavierBmi) < 0.2,
                "BMI difference should be small with a 0.1 lb difference");
        }

        // *** Tests for DetermineBMICategory ***

        [TestMethod]
        public void Test_UnderweightCategory()
        {
            string category = BMICalculator.DetermineBMICategory(18.4);
            Assert.AreEqual("Underweight", category);
        }

        [TestMethod]
        public void Test_NormalCategory_LowBoundary()
        {
            // If the boundary is exactly 18.5, this should be "Normal"
            string category = BMICalculator.DetermineBMICategory(18.5);
            Assert.AreEqual("Normal", category);
        }

        [TestMethod]
        public void Test_NormalCategory_HighBoundary()
        {
            // If the boundary is just below 25.0
            string category = BMICalculator.DetermineBMICategory(24.9);
            Assert.AreEqual("Normal", category);
        }

        [TestMethod]
        public void Test_OverweightCategory()
        {
            string category = BMICalculator.DetermineBMICategory(27.5);
            Assert.AreEqual("Overweight", category);
        }

        [TestMethod]
        public void Test_ObeseCategory()
        {
            string category = BMICalculator.DetermineBMICategory(30.0);
            Assert.AreEqual("Obese", category);
        }
    }
}
