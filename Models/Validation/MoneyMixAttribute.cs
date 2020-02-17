using System.ComponentModel.DataAnnotations;

namespace Models.Validation
{
    public class MoneyMixAttribute : ValidationAttribute
    {
        private decimal _money;

        public MoneyMixAttribute(double money)
        {
            _money = (decimal)money;
        }


        public override bool IsValid(object value)
        {
            var money = (decimal)value;

            if (money >= _money)
            {
                return true;
            }
            return false;
        }
    }
}