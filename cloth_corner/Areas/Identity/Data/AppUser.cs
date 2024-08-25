using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace cloth_corner.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public required string Name { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public required string Gender { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public required string Age { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    [AllowNull]
    public string Address { get; set; } = String.Empty;
}

