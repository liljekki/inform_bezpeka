using NLog;
using NLog.Fluent;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
class Program
{
    static void Main()
    {
        var logger = NLog.LogManager.GetCurrentClassLogger();

        logger.Trace("Register");
        //регістр користувачів
        string role_admin = "Admins";
        Console.WriteLine("Registr:");
        for (int i = 0; i < 4; i++)
        {
            logger.Info($"Reg user: {i}") ;
            string[] roles = { "Users", "Junior", "Senior" };
            if (i % 2 == 1)
            {
                logger.Debug($"Change first role {roles[0]} to {role_admin} ");
                roles[0] = role_admin;   
            };
            var user = Protector.Register("alex" + i, "pass" + i, roles);
            logger.Debug($"set login and pass to {"alex"+i} and {"pass"+i}");

            Console.WriteLine("\n Login: " + user.Login + "\n Roles:" );
            for (int j = 0; j < user.Roles.Length; j++)
            {
                Console.WriteLine("   " + user.Roles[j]);
            }
        }

        logger.Trace("do_while loop");
        //логін 
        do
        {
            logger.Info("Enter login and pass");
            Console.WriteLine("-----------------------------------------------------\n");
            Console.WriteLine("Login:");
            string login = Console.ReadLine();
            Console.WriteLine("Password:");
            string pass = Console.ReadLine();
            
            logger.Trace("LogIn");
            try
            {
                Protector.LogIn(login, pass);
            }
            catch (WrongPassword ex)
            {
                logger.Warn(ex, "Wrong pass or login");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Authentication failed");
            }
            //перевірка на роль адміна
            logger.Info("Check pass before enter check_admin");
            try
            {
                logger.Trace("Only for admins");
                Protector.OnlyForAdminsFeature(login, pass);
            }
            catch (WrongPassword ex)
            {
                logger.Fatal(ex, "login or pass is empty");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Admins part failed");
            }
        } while (true);
    }
    public class Protector
    {
        private static Dictionary<string, User> Users = new Dictionary<string, User>();
        public class User
        {
            public string Login { get; set; }
            public string PasswordHash { get; set; }
            public string Salt { get; set; }
            public string[] Roles { get; set; }
        }
        public static User Register(string username, string password, string[] roles)
        {
            if (Users.ContainsKey(username))
            {
                Console.WriteLine("This username already registered");
                return Users[username];
            }
            // генерація солі
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            // rfc
            var hashed_pass = new Rfc2898DeriveBytes(password, saltBytes, 2000);
            var user = new User
            {
                Login = username,
                Salt = saltText,
                PasswordHash = Convert.ToBase64String(hashed_pass.GetBytes(16)),
                Roles = roles
            };
            Users.Add(user.Login, user);
            return user;
        }
        public static bool CheckPassword(string username, string password)
        {
            //перевірка логіна у словнику
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            var user = Users[username];
            var saltBytes = new byte[16];
            saltBytes = Convert.FromBase64String(user.Salt);
            var hashed_pass = new Rfc2898DeriveBytes(password, saltBytes, 2000);
            //співставлення хешів  
            if(Convert.ToBase64String(hashed_pass.GetBytes(16)) != user.PasswordHash)
            {
                return (false);
            }
                return (true);
        }
        public static void LogIn(string userName, string password)
        {
            // Перевірка пароля
            if (CheckPassword(userName, password))
            {
                // Створюється екземпляр автентифікованого користувача
                var identity = new GenericIdentity(userName, "OIBAuth");
                // Виконується прив’язка до ролей, до яких належить користувач
                var principal = new GenericPrincipal(identity, Users[userName].Roles);
                // Створений екземпляр автентифікованого користувача з відповідними
                // ролями присвоюється потоку, в якому виконується програма
                System.Threading.Thread.CurrentPrincipal = principal;
            }
            else
            {
                throw new WrongPassword("Wrong pass or login");
            }
        }
        public static void OnlyForAdminsFeature(string login, string pass)
        {
            if (login == "" || pass == "")
            {
                throw new WrongPassword("login or pass - empty");
            }
            if (Protector.CheckPassword(login, pass))
            {
                // Перевірка того, що потік програми виконується автентифікованим користувачем із певними ролями
                if (Thread.CurrentPrincipal == null)
                {
                    throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
                }
                // Перевірка того, що автентифікований користувач належить до ролі "Admins"
                if (!Thread.CurrentPrincipal.IsInRole("Admins"))
                {
                    throw new SecurityException("User must be a member of Admins to access this feature.");
                }
                // У разі, якщо перевірка пройшла успішно, виконується захищена частина програми
                Console.WriteLine("You have access to this secure feature.");
            } 
        }
    }
    public class WrongPassword : System.Exception
    {
        public WrongPassword() { }
        public WrongPassword(string message) : base(message) { }
        public WrongPassword(string message, Exception inner) : base(message, inner) { }
    }
}