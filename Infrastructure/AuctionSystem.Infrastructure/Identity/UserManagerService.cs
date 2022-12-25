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
        private readonly IAuctionSystemDbContext _context;
        private readonly UserManager<AuctionUser> _userManager;

        public UserManagerService(
            UserManager<AuctionUser> userManager,
            IAuctionSystemDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var result = await _context
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

            var result = await _userManager.CreateAsync(user, password);
            return result.ToApplicationResult();
        }

        public async Task<Result> CreateUserAsync(AuctionUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.ToApplicationResult();
        }

        public async Task<(Result Result, string UserId)> SignIn(string email, string password)
        {
            var user = await this.GetUserByEmailAsync(email);
            if (user == null)
            {
                return (Result.Failure("Error Occured"), null);
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                await _userManager.AccessFailedAsync(user);
                return (Result.Failure("Error Occured"), null);
            }

            return (Result.Success(), user.Id);
        }


        public async Task<string> GetFirstUserId()
        {
            var user = await _context.Users.FirstAsync();
            return user.Id;
        }

        private async Task<AuctionUser> GetUserByEmailAsync(string email)
            => await _context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
    }

}