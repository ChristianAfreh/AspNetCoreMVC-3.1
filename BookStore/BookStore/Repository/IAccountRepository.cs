using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        public Task<SignInResult> PasswordSignInAsync(SignInUserModel signInUserModel);

        public Task SignOutAsync();
    }
}