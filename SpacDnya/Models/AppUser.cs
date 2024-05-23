using Microsoft.AspNetCore.Identity;

namespace SpacDnya.Models;

public class AppUser:IdentityUser
{
    public string Name;
    public string Surname;
    public DateTime? BirtDate;
}
