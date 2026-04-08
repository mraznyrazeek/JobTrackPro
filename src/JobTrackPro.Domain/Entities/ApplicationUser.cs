using Microsoft.AspNetCore.Identity;

namespace JobTrackPro.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}