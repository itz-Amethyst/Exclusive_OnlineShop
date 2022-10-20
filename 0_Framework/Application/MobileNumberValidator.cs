using System.Text.RegularExpressions;

namespace _0_Framework.Application
{
    public class MobileNumberValidator
    {
        public static bool IsMobileNumberValid(string input)
        {
            const string pattern = @"^09[0|1|2|3][0-9]{8}$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(input);
        }
        
    }
}
