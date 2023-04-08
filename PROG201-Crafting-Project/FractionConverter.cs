using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace PROG201_Crafting_Project
{
    // Source: https://onedrive.live.com/?cid=860B5379E3318DDA&id=860B5379E3318DDA%21394&parId=860B5379E3318DDA%21314&o=OneUp

    [ValueConversion(typeof(double?), typeof(String))]
    public class FractionConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a double into a string.
        /// </summary>
        /// <param name="value">A nullable double to get converted into a string</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">true or false, determines if we use the denominator restrictions</param>
        /// <param name="culture"></param>
        /// <returns>A string representing the double value, maybe in decimal format, maybe in fractional format.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string decimalFormatString = "0.######";
            bool restrictDenominator = false;

            if (parameter != null)
            {
                if (!bool.TryParse(parameter.ToString(), out restrictDenominator))
                {
                    restrictDenominator = false;
                }
            }

            double? dValue = value as Nullable<double>;
            if (restrictDenominator && dValue != null && dValue.HasValue)
            {
                Fraction asFraction = Fraction.Parse(dValue.Value);
                var validDenominators = new List<int>(new int[] { 2,3,4,5,6,7,8,9,16,32,64 });
                if (validDenominators.Contains(asFraction.Denominator))
                {
                    return asFraction.ToString(validDenominators, decimalFormatString);
                }
                else
                {
                    return dValue.Value.ToString(decimalFormatString);
                }
            }
            else if (!dValue.HasValue)
            {
                return dValue;
            }
            else
            {
                Fraction asFraction = Fraction.Parse(dValue.Value);
                return asFraction.ToString(null, String.Empty);
            }
        }

        /// <summary>
        /// Converts a string into a double
        /// </summary>
        /// <param name="value">A string value that should be transformable into a double</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                double rValue;
                string rawValue = value.ToString().Trim();
                rawValue = rawValue.Replace("- ", "-");
                while(rawValue.Contains("  "))
                {
                    rawValue = rawValue.Replace("  ", " ");
                }

                // Regular Expression that represents a number in Fraction format.
                Regex FractionRegex = new Regex(@"^-?([0-9]* )?[0-9]+\/[0-9]+$");

                // If the value can be parsed as a double, do it and return
                if (double.TryParse(rawValue, out rValue))
                {
                    return rValue;
                }
                // Else if the value can be read as a fractional value, extract the number and return the a double from it. 
                else if (FractionRegex.IsMatch(rawValue))
                {
                    // Check to see if the input 
                    if (FractionRegex.IsMatch(rawValue))
                    {
                        try
                        {
                            Regex numeratorRegex = new Regex(@"(\s|^|-)[0-9]+\/");
                            bool isNegative;
                            int wholeNumber;
                            int numerator;
                            int denominator;

                            isNegative = rawValue.StartsWith("-");
                            wholeNumber = Math.Abs(rawValue.Any(x => x == ' ') ? int.Parse(rawValue.Remove(rawValue.IndexOf(" "))) : 0);
                            denominator = int.Parse(rawValue.Substring(rawValue.LastIndexOf("/") + 1));
                            numerator = Math.Abs(int.Parse((numeratorRegex.Match(rawValue)).Value.Replace("/", "")));

                            return new Fraction(!isNegative, wholeNumber, numerator, denominator).ToDouble();
                        }
                        catch
                        {
                            throw new FormatException(String.Format("Invalid Format:  {0} cannot be converted to a numeric value.", value.ToString()));
                        }
                    }
                }
                //  This value could not be parsed as a double and didn't match a fraction using our Fractional Regular Expression, throw a FormatException.
                else
                {
                    throw new FormatException(String.Format("Invalid Format:  {0} cannot be converted to a numeric value.", value.ToString()));
                }
            }

            return null;
        }

        #endregion
    }

}
