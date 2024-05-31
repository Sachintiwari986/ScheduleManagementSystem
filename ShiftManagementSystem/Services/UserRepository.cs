using ShiftManagementSystem.Models;

namespace ShiftManagementSystem.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ShiftManagementCoreDbContext _db;

        public UserRepository(ShiftManagementCoreDbContext db)
        {
            _db = db;
        }
        public UserDataModel GetUserByEmail(string email)
        {
            return _db.Users.SingleOrDefault(u => u.Email == email);
        }
    }
}
