using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FortisAccountGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = ContextPrepare();
            var accessToken = Auth.TokenGet("serviceone", "Service1");

            var headerParams = new Dictionary<string, string>() { {"Authorization", accessToken} };
            var jsonBody = JsonConvert.SerializeObject(context);

            var responseFortisAccountCreate = Auth.Post(headerParams: headerParams, jsonBody: jsonBody).Result;
            
            if (responseFortisAccountCreate.StatusCode != HttpStatusCode.Created)
            {
                if (responseFortisAccountCreate.StatusCode == HttpStatusCode.InternalServerError)
                {
                    JObject jobject = (JObject) null;
                    try
                    {
                        jobject = JObject.Parse(responseFortisAccountCreate.Content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Message: {ex.Message} \r\n StackTrace:{ex.StackTrace}");
                        throw;
                    }
                    Console.WriteLine($"Ошибка при создании аккаунта! Детали ошибки: \r\n {jobject["detail"]}");
                    
                }
                Console.WriteLine("Что-то пошло не так, попробуйте ещё раз");
            }
            
            Console.WriteLine("Компания успешно создана! На указанный email было выслано письмо с ссылкой на установление  пароля.");
        }

        private static Dictionary<string, object> ContextPrepare()
        {
            Console.WriteLine("General Info:\r\n    Введите название компании:");
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

            var context = new Dictionary<string, object>();
            context.Add("name", companyName);
            context.Add("country", "ARE");
            context.Add("business_category", "fitness");
            context.Add("business_subcategory", "yoga_studio");
            context.Add("owner_name", ownerName);
            context.Add("owner_phone", ownerPhone);
            context.Add("owner_email", ownerEmail);

            return context;
        }
    }
}