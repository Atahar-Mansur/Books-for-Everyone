using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksForEveryone.Data
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            //identity.AddClaim(new Claim("ContactName", user.ContactName ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("Name", user.Name ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("MobileNumber", user.MobileNumber ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("Address", user.Address ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("ZipCode", user.ZipCode.ToString() ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("AreaThana", user.AreaThana ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("District", user.District ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("Book1Name", user.Book1Name ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("Book1WriName", user.Book1WriName ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("Book2Name", user.Book2Name ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("Book2WriName", user.Book2WriName ?? "[Click to edit profile]"));

            return identity;
        }
    }
}
