using System;

namespace HotelBooking.BAL
{
    public class CustomMath
    {
        //Create 4 function
        // Additional, Subtract, Multiple, Division

        public int Additional(int number1, int number2)
        {
            return number1 + number2;
        }

        public int Subtraction(int number1, int number2)
        {
            return number1 - number2;
        }

        public int Multiply(int number1, int number2)
        {
            return number1 * number2;
        }

        public float Division(float number1, float number2)
        {
            //if (number2 == 0)
            //    return "Divison by zero";
            //return (number1 / number2).ToString();
            try
            {
                return number1 / number2;
            }
            catch (Exception)
            {
                return float.MinValue;
            }
        }
    }
}