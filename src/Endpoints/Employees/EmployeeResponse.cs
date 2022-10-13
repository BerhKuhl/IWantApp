namespace IWantApp.Endpoints.Employees;

public class EmployeeResponse
{
    public string Email { get; set; }
    public string Name { get; set; }

    public EmployeeResponse(string email, string name)
    {
        Email = email;
        Name = name;
    }
}