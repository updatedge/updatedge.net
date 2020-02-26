using System.Collections.Generic;

namespace Udatedge.Common.Extensions
{
    /// <summary>
    /// Useful string extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to camelcase
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <returns></returns>
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        /// <summary>
        /// Converts a param and message to a dictionary
        /// </summary>
        /// <param name="message"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Dictionary<string, List<string>> ToErrorsDictionary(this string message, string param)
        {
            return new Dictionary<string, List<string>>
            {
                {
                    param,
                    new List<string>()
                    {
                        message
                    }
                }
            };
        }
    }
}
