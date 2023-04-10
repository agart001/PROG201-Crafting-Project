using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROG201_Crafting_Project
{
    // Source: https://onedrive.live.com/?cid=860B5379E3318DDA&id=860B5379E3318DDA%21394&parId=860B5379E3318DDA%21314&o=OneUp
    /// <summary>
    /// Represents a rational number
    /// </summary>
    public struct Fraction
    {
        public bool IsPositive;
        public int WholeNumber;
        public int Numerator;
        public int Denominator;

        /// <summary>
        /// Constructor
        /// </summary>
        public Fraction(bool isPositive, int wholeNumber, int numerator, int denominator)
        {
            this.IsPositive = isPositive;
            this.WholeNumber = wholeNumber;
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        /// <summary>
        /// Approximates a fraction from the provided double
        /// </summary>
        public static Fraction Parse(double d)
        {
            return ApproximateFraction(d, 0.0000001d);
        }

        //Added by Alex Gartner
        public static Fraction StringToFrac(string str)
        {
            return StrGetFrac(str);
        }

        public double? ToDouble()
        {
            if (this.Denominator == 0)
                return null;

            double value = ((WholeNumber * (double)Denominator) + Numerator) / (double)Denominator;
            if (!IsPositive) value = value * -1;

            return value;
        }

        /// <summary>
        /// Returns this fraction expressed as a double, rounded to the specified number of decimal places.
        /// Returns double.NaN if denominator is zero
        /// </summary>
        public double ToDouble(int decimalPlaces)
        {
            if (this.Denominator == 0)
                return double.NaN;

            double value = System.Math.Round(((WholeNumber * (double)Denominator) + Numerator) / (double)Denominator, decimalPlaces);
            if (!IsPositive) value = value * -1;

            return value;
        }

        public override string ToString()
        {
            return ToString(null, String.Empty);
        }
        /// <summary>
        /// Outputs the fraction as a string
        /// </summary>
        /// <param name="acceptedDenominators">List of accepted denominators, if the denominator isn't in this list, the decimal format is returned.</param>
        /// <param name="decimalFormat">The decimal format to be used if the decimal format is returned.</param>
        /// <returns></returns>
        public string ToString(List<int> acceptedDenominators, string decimalFormat)
        {
            if (String.IsNullOrEmpty(decimalFormat))
            {
                decimalFormat = "G";
            }

            if (Numerator == Denominator || Numerator == 0)
            {
                if (WholeNumber != 0)
                    return String.Format(@"{1}{0}", WholeNumber, IsPositive ? String.Empty : "-");
                else
                    return String.Format(@"{0}", WholeNumber);
            }

            if (acceptedDenominators!= null && !acceptedDenominators.Contains(Denominator))
            {
                return ToDouble().HasValue ? ToDouble().Value.ToString(decimalFormat) : String.Empty;
            }
            else if (WholeNumber != 0)
            {
                return String.Format(@"{3}{0} {1}/{2}", WholeNumber, Numerator, Denominator, IsPositive ? String.Empty : "-");
            }
            else
            {
                return String.Format(@"{2}{0}/{1}", Numerator, Denominator, IsPositive ? String.Empty : "-");
            }
        }

        /// <summary>
        /// Approximates the provided value to a fraction.
        /// </summary>
        /// <param name="value">The double being approximated as a fraction.</param>
        /// <param name="precision">Maximum difference targeted for the fraction to be considered equal to the value.</param>
        /// <returns>Fraction struct representing the value.</returns>
        private static Fraction ApproximateFraction(double value, double precision)
        {
            bool positive = value > 0;
            int wholeNumber = 0; 
            int numerator = 1; 
            int denominator = 1;
            double fraction = numerator / denominator;

            while (System.Math.Abs(fraction - value) > precision)
            {
                if (fraction < value)
                {
                    numerator++;
                }
                else
                {
                    denominator++;
                    numerator = (int)System.Math.Round(value * denominator);
                }

                fraction = numerator / (double)denominator;
            }

            if (numerator < 0) numerator = numerator * -1;

            while (numerator >= denominator)
            {
                wholeNumber += 1;
                numerator -= denominator;
            }

            return new Fraction(positive, wholeNumber, numerator, denominator);
        }
        
        //Added by Alex Gartner
        private static Fraction StrGetFrac(string str)
        {
            List<char> chars = str.ToList();

            int whole = 0;

            if (chars.Contains(' '))
            {
                char[] whole_chars = chars.TakeWhile(c => c != ' ').ToArray();
                chars = chars.SkipWhile(c => c != ' ').ToList();
                whole = Convert.ToInt32(new string(whole_chars));
            }

            char[] numer_chars = chars.TakeWhile(c => c != '/').ToArray();
            numer_chars = numer_chars.Where(c => c != ' ').ToArray();

            chars = chars.SkipWhile(c => c != '/').ToList();
            char[] denom_chars = chars.Where(c => c != '/').ToArray(); ;


            int numer = Convert.ToInt32(new string(numer_chars));
            int denom = Convert.ToInt32(new string(denom_chars));

            return new Fraction(true, whole, numer, denom);
        }
    }
}
