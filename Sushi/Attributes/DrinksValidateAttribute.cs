using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SushiMarcet.Attributes
{
    public class DrinksValidateAttribute : ValidationAttribute
    {
        Logger Logger = new Logger();

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetMethodName()
        {
            var st = new StackTrace(new StackFrame(8));
            return st.GetFrame(0).GetMethod().Name;
        }

        public override bool IsValid(object? value)
        {
            if (value is not null)
            {
                Drinks drink = (Drinks)value;
                string currentMethod = GetMethodName();

                if (currentMethod == "ValidateDrinkAndCreate")
                {
                    if (IsId(drink) == false || IsName(drink) == false || IsDescription(drink) == false || IsPrice(drink) == false)
                    {
                        Logger.Debug($"Failed validation ({drink.ShowDataForAdmin()})");
                        return false;
                    }
                    else
                    {
                        Logger.Debug($"Passed validation ({ drink.ShowDataForAdmin()})");
                        return true;
                    }
                }
                else if (currentMethod == "ValidateDrinkAndUpdate")
                {
                    if (IsName(drink) == false || IsDescription(drink) == false || IsPrice(drink) == false)
                    {
                        Logger.Debug($"Failed validation ({drink.ShowDataForAdmin()})");
                        return false;
                    }
                    else
                    {
                        Logger.Debug($"Passed validation ({ drink.ShowDataForAdmin()})");
                        return true;
                    }
                }
                else
                {
                    Logger.Warning($"The desired method was not found  ({currentMethod})");
                    return false;
                }
                    
            }
            else
            {
                return false;
            }
        }

        private bool IsName(Drinks drink)
        {
            if (string.IsNullOrEmpty(drink.Name) || string.IsNullOrWhiteSpace(drink.Name) || drink.Name.Length > 50)
            {
                return false;
            }
            return true;
        }

        private bool IsDescription(Drinks drink)
        {
            if (string.IsNullOrEmpty(drink.Description) || string.IsNullOrWhiteSpace(drink.Description) || drink.Description.Length > 500)
            {
                return false;
            }
            return true;
        }

        private bool IsId(Drinks drink)
        {
            SqlDrinksRepository sqlDrinksRepository = new SqlDrinksRepository();

            if (drink.Id == 0 || drink.Id <= 0 || sqlDrinksRepository.GetItem(drink.Id) is not null)
            {
                return false;
            }
            else
                return true;
        }

        private bool IsPrice(Drinks drink)
        {
            if (drink.Price <= 0 || drink.Price >= 50)
            {
                return false;
            }
            return true;
        }
    }
}
