using ShiftManagementSystem.Models;

namespace ShiftManagementSystem.Services
{
    public interface IUserRepository
    {
            UserDataModel GetUserByEmail(string email);
    }
}
