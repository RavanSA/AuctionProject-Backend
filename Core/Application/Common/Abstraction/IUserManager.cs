namespace Application.Common.Interfaces
{
    using System.Threading.Tasks;
    using Domain.Entities;
    using Models;

    public interface IUserManager
    {
        Task<User> GetUserByIdAsync(string id);

        Task<Result> CreateUserAsync(string email, string password, string fullName,string phoneNo);

        Task<Result> CreateUserAsync(AuctionUser user, string password);

        Task<(Result Result, string UserId)> SignIn(string email, string password);

        Task<string> GetFirstUserId();

    }
}