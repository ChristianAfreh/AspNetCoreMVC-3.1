using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IAccountRepository
    {
        public Task<ApplicationUser> GetUserByEmailAsync(string email);
        public Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        public Task<SignInResult> PasswordSignInAsync(SignInUserModel signInUserModel);

        public Task SignOutAsync();
        public Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        public Task<IdentityResult> ConfirmEmail(string uid, string token);
        public Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
    }
}