using Microsoft.AspNetCore.Mvc;
using ShiftManagementSystem.Models;
using System.Security.Cryptography;

namespace ShiftManagementSystem.Controllers
{
    public class ClientController : Controller
    {
        private readonly ShiftManagementCoreDbContext _db;


        public ClientController(ShiftManagementCoreDbContext db)
        {
            _db=db;
        }

        const int keySize = 64;
        const int iterations = 35000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(UserDataViewModel userDataModel)
        {
            if (ModelState.IsValid)
            {
                var response = SignIn(userDataModel);
                if (response.Success)
                {
                    
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.Message = response.Message;
                    return View();
                }
            }
            return View();
        }

        private SignInResponse SignIn(UserDataViewModel user)
        {
            if (!string.IsNullOrEmpty(user.Email))
            {
                user.Email = user.Email.Trim();
            }

            var result = new SignInResponse();
            var userinfo = _db.Users.SingleOrDefault(u => u.Email == user.Email.ToLower());
            if (userinfo == null)
            {
                result.Success = false;
                result.Message = "Credential Didn't Match!!";
                return result;
            }
            else
            {
                byte[] salt = HexStringToByteArray(userinfo.Salt);

                var isCorrectPassword = VerifyPassword(user.Password, userinfo.HashedPassword, salt);
                if (isCorrectPassword)
                {
                    result.User = userinfo;
                    result.Success = true;
                    result.Message = "Login Successfully";
                    return result;
                }
                else
                {
                    result.Success = false;
                    result.Message = "Credential Didn't Match!!";
                    return result;
                }
            }
        }

        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        public byte[] HexStringToByteArray(string hex)
        {
            hex = hex.Replace(" ", ""); 
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Hexadecimal string must have an even number of characters.");
            }

            byte[] byteArray = new byte[hex.Length / 2];

            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return byteArray;
        }

    }
}
