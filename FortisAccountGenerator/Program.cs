using System.Text.RegularExpressions;

namespace FortisAccountGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ContextPrepare();

        }

        private static Dictionary<string, object> ContextPrepare()
        {
            /*Console.WriteLine("General Info:\r\n    Введите название компании:");
            string companyName = Console.ReadLine();
            
            Console.WriteLine("Owner Info:\r\n    Введите имя владельца компании:");
            string ownerName = Console.ReadLine();
            
            Console.WriteLine("    Введите номер телефона (это будущий логин для входа. Телефон строго в формате +971XXXXXXXXX, т.е. 9 цифр после кода страны):");
            Regex phoneMask = new Regex("\\+971\\d{9}$");
            string ownerPhone = Console.ReadLine();
            while (!phoneMask.IsMatch(ownerPhone))
            {
                Console.WriteLine("    Неверный формат номера телефона, попробуйте еще раз:");
                ownerPhone = Console.ReadLine();
            }
            
            Console.WriteLine("    Введите НАСТОЯЩИЙ E-mail. На него придёт письмо с установкой пароля!!!:");
            string ownerEmail = Console.ReadLine();
            */

            var token = Auth.TokenGet("serviceone", "Service1");

            Dictionary<string, object> kek = new Dictionary<string, object>();
            return kek;
        }
    }
}