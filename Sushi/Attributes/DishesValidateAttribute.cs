using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SushiMarcet.Attributes
{
    public class DishesValidateAttribute : ValidationAttribute
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
                SauceAndDishes dishes = (SauceAndDishes)value;
                string currentMethod = GetMethodName();

                if (currentMethod == "ValidateDishAndCreate")
                {
                    if (IsId(dishes) == false || IsName(dishes) == false || IsDescription(dishes) == false || IsPrice(dishes) == false)
                    {
                        Logger.Debug($"Failed validation ({dishes.ShowDataForAdmin()})");
                        return false;
                    }
                    else
                    {
                        Logger.Debug($"Passed validation ({dishes.ShowDataForAdmin()})");
                        return true;
                    }

                }
                else if (currentMethod == "ValidateDishAndUpdate")
                {
                    if (IsName(dishes) == false || IsDescription(dishes) == false || IsPrice(dishes) == false)
                    {
                        Logger.Debug($"Failed validation ({dishes.ShowDataForAdmin()})");
                        return false;
                    }
                    else
                    {
                        Logger.Debug($"Passed validation ({dishes.ShowDataForAdmin()})");
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

        private bool IsName(SauceAndDishes dish)
        {
            if (string.IsNullOrEmpty(dish.Name) || string.IsNullOrWhiteSpace(dish.Name) || dish.Name.Length > 50)
            {
                return false;
            }
            return true;
        }

        private bool IsDescription(SauceAndDishes dish)
        {
            if (string.IsNullOrEmpty(dish.Description) || string.IsNullOrWhiteSpace(dish.Description) || dish.Description.Length > 500)
            {
                return false;
            }
            return true;
        }

        private bool IsId(SauceAndDishes dish)
        {
            SqlDishesRepository sqlDishesRepository = new SqlDishesRepository();

            if (dish.Id == 0 || dish.Id <= 0 || sqlDishesRepository.GetItem(dish.Id) is not null)
            {
                return false;
            }
            else
                return true;
        }

        private bool IsPrice(SauceAndDishes dish)
        {
            if (dish.Price <= 0 || dish.Price >= 50)
            {
                return false;
            }
            return true;
        }
    }
}
