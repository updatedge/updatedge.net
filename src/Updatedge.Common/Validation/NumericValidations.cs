
namespace Updatedge.Common.Validation
{
    /// <summary>
    /// Validates numerics
    /// </summary>
    public class NumericValidations : BaseValidations
    {
        private readonly int _value;

        /// <summary>
        /// Constructor
        /// </summary>
        public NumericValidations(int value) : base() { _value = value; }


        /// <summary>
        /// Validates an integer is between two values (inclusive)
        /// </summary>
        /// <param name="start">Start of range</param>
        /// <param name="end">End of range</param>
        /// <param name="paramName">Name of paramter</param>
        /// <returns></returns>
        public NumericValidations NumberIsBetweenInclusive(int start, int end, string paramName)
        {
            if (_value >= start && _value <= end) return this;

            Add(paramName, string.Format(Constants.ErrorMessages.XMustBeBetweenYAndZ, start, end));
            return this;
        }
    }
}
