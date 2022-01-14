using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SushiMarcet.Attributes
{
    public class SushiValidateAttribute : ValidationAttribute
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
            if(value is not null)
            {
                Sushi sushi = (Sushi)value;
                string currentMethod = GetMethodName();

                if(currentMethod == "ValidateSushiAndCreate")
                {
                    if(IsId(sushi) == false || IsType(sushi) == false || IsName(sushi) == false || IsDescription(sushi) == false || IsPrice(sushi) == false)
                    {
                        Logger.Debug($"Failed validation ({sushi.ShowDataForAdmin()})");
                        return false;
                    }
                    else
                    {
                        Logger.Debug($"Passed validation ({sushi.ShowDataForAdmin()})");
                        return true;
                    }
                }
                else if(currentMethod == "ValidateSushiAndUpdate")
                {
                    if (IsType(sushi) == false || IsName(sushi) == false || IsDescription(sushi) == false || IsPrice(sushi) == false)
                    {
                        Logger.Debug($"Failed validation ({sushi.ShowDataForAdmin()})");
                        return false;
                    }
                    else
                    {
                        Logger.Debug($"Passed validation ({sushi.ShowDataForAdmin()})");
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

        private bool IsType(Sushi sushi)
        {
            if (sushi.Type == Observer.Uramaki || sushi.Type == Observer.Nigiri || sushi.Type == Observer.Futomaki || sushi.Type == Observer.BakedSushi)
            {
                return true;
            }
            return false;            
        }

        private bool IsName(Sushi sushi)
        {
            if(string.IsNullOrEmpty(sushi.Name) || string.IsNullOrWhiteSpace(sushi.Name) || sushi.Name.Length > 50)
            {
                return false ;
            }
            return true;
        }

        private bool IsDescription(Sushi sushi)
        {
            if (string.IsNullOrEmpty(sushi.Description) || string.IsNullOrWhiteSpace(sushi.Description) || sushi.Description.Length > 500)
            {
                return false;
            }
            return true;
        }

        private bool IsId(Sushi sushi)
        {           
            SqlSushiRepository sqlSushiRepository = new SqlSushiRepository();           

            if(sushi.Id == 0 || sushi.Id <= 0 || sqlSushiRepository.GetItem(sushi.Id) is not null)
            {
                return false;
            }
            else
                return true;
        }

        private bool IsPrice(Sushi sushi)
        {
            if(sushi.Price <= 0 || sushi.Price > 100)
            {
                return false;
            }
            return true;
        }
    }
}
