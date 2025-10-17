using System.Security.Cryptography;
using System.Text;

namespace ToDoListBlazorServer.Services.Auth
{
    public static class  HashPasswordService
    {
        public static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
