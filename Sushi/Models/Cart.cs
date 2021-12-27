using System.Reflection;
using System.Text;

namespace SushiMarcet.Models
{
    public static class Cart
    {
        public static List<object> cartList = new();

        public static void Add(object item)
        {
            cartList.Add(item);
        }

        public static string ShowTheContents()
        {

            StringBuilder sb = new();

            foreach (var content in cartList)
            {
                sb.AppendLine($"{content}");
            }

            return sb.ToString();
        }

        public static string ReturnOrderAmount()
        {
            decimal result = default;
            Type type;
            PropertyInfo property;

            foreach (var content in cartList)
            {
                type = content.GetType();
                property = type.GetProperty("Price");
                result += (decimal)property.GetValue(content, null);
            }

            return $"\nOrder amount: {result:c}";
        }
    }
}
