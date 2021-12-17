using System.Text;

namespace SushiMarcet.Models
{
    public static class Cart
    {
        public static List<object> _cartList = new() { "Cart\n"};

        public static void Add(object item)
        {
            _cartList.Add(item);
        }

        public static void Delete(object item)
        {
            _cartList.Remove(item);
        }

        public static string ShowTheContents()
        {
            StringBuilder sb = new();

            foreach (var content in _cartList)
            {
                sb.AppendLine($"{content}");
            }

            return sb.ToString();
        }
    }
}
