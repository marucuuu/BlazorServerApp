namespace BlazorServerApp.Models
{
    public class Users
    {
        public int Id { get; set; }  // Identity uses "Id", not "ID"
        public string EmployeeID { get; set; } = string.Empty; // new Employee ID property

        public string UserName { get; set; } = string.Empty;  // equivalent to Identity's UserName

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;  // matches Identity's naming

        public string? PhoneNumber { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Role { get; set; } = "User";  // consistent with simple role-based auth

        public string? SessionToken { get; set; }  // new
    }
}