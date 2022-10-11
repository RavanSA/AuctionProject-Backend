namespace AuctionSystem.Infrastructure.Identity
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application;
    using Application.Common.Interfaces;
    using Application.Common.Models;
    using Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserManagerService : IUserManager
    {
        private readonly IAuctionSystemDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AuctionUser> userManager;

        public UserManagerService(
            UserManager<AuctionUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuctionSystemDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var result = await this.context
                .Users
                .Where(u => u.Id == id)
                .SingleOrDefaultAsync();

            if (result == null)
            {
                return null;
            }

            var user = new User
            {
                Id = result.Id,
                Email = result.Email,
                UserName = result.UserName,
                FullName = result.FullName,
                PhoneNumber = result.PhoneNumber,
            };

            return user;
        }

        public async Task<Result> CreateUserAsync(string email, string password, string fullName)
        {
            var user = new AuctionUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var result = await this.userManager.CreateAsync(user, password);
            return result.ToApplicationResult();
        }

        public async Task<Result> CreateUserAsync(AuctionUser user, string password)
        {
            var result = await this.userManager.CreateAsync(user, password);
            return result.ToApplicationResult();
        }

        public async Task<(Result Result, string UserId)> SignIn(string email, string password)
        {
            var user = await this.GetDomainUserByEmailAsync(email);
            if (user == null)
            {
                return (Result.Failure(ExceptionMessages.User.InvalidCredentials), null);
            }

            if (await this.userManager.IsLockedOutAsync(user))
            {
                return (
                    Result.Failure(
                        ExceptionMessages.User.AccountLockout), null);
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                await this.userManager.AccessFailedAsync(user);
                return (Result.Failure(ExceptionMessages.User.InvalidCredentials), null);
            }

           

            return (Result.Success(), user.Id);
        }


        public async Task<string> GetFirstUserId()
        {
            var user = await this.context.Users.FirstAsync();
            return user.Id;
        }

        private async Task<AuctionUser> GetDomainUserByEmailAsync(string email)
            => await this.context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();

        private async Task<RefreshToken> GetLastValidToken(string currentUserId, CancellationToken cancellationToken)
            => await this.context.RefreshTokens.SingleOrDefaultAsync(
                x => x.UserId == currentUserId && !x.Invalidated,
                cancellationToken);
    }
}