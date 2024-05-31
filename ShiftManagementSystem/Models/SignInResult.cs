namespace ShiftManagementSystem.Models
{
    public class SignInResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserDataModel User { get; set; }
    }
}
