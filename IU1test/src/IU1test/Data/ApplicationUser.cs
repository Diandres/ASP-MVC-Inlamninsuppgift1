using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TomasosPizzeriaAndres.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
