namespace Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Items.Queries.Details;
    using Application.Users.Commands.CreateUser;
    using Application.Users.Commands.Jwt.Refresh;
    using Application.Users.Commands.LoginUser;
    using Application.Users.Commands.Logout;
    using Application.Users.Commands.UpdateUserInfo;
    using global::Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : BaseController
    {
        private readonly IDateTime dateTime;

        public IdentityController(IDateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(CreateUserCommand model)
        {
            
            await this.Mediator.Send(model);
            return this.Ok();
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(LoginUserCommand model)
        {
    
            var result = await this.Mediator.Send(model);
            this.SetCookies(result.Data.Token, result.Data.RefreshToken.ToString());
            return this.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            var result = await this.Mediator.Send(new GetUserInfoById(id));
            return this.Ok(result);
        }

        [HttpPost]
        [Route(nameof(Refresh))] 
        public async Task<IActionResult> Refresh(JwtRefreshTokenCommand model)
        {
            var refreshToken = this.Request.Cookies[Constants.RefreshToken];
            var jwtToken = this.Request.Cookies[Constants.JwtToken];

            if (refreshToken == null || jwtToken == null)
            {
                return this.Unauthorized();
            }

            model.RefreshToken = Guid.Parse(refreshToken);
            model.Token = jwtToken;
            var result = await this.Mediator.Send(model);
            this.SetCookies(result.Data.Token, result.Data.RefreshToken.ToString());
            return this.Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserInfoCommand model)
        {

            await this.Mediator.Send(model);
            return this.NoContent();
        }

        [HttpPost]
        [Route(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            this.Request.Cookies.TryGetValue(Constants.RefreshToken, out var refreshToken);
            this.Response.Cookies.Delete(Constants.JwtToken);
            this.Response.Cookies.Delete(Constants.RefreshToken);

            await this.Mediator.Send(new LogoutUserCommand {RefreshToken = refreshToken});
            return this.Ok();
        }

        private void SetCookies(string jwtToken, string refreshToken)
        {
            this.SetJwtTokenCookie(jwtToken);
            this.SetRefreshTokenCookie(refreshToken);
        }

        private void SetRefreshTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = this.dateTime.UtcNow.AddMonths(1)
            };

            this.Response.Cookies.Append(Constants.RefreshToken, token, cookieOptions);
        }

        private void SetJwtTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.MaxValue
            };

            this.Response.Cookies.Append(Constants.JwtToken, token, cookieOptions);
        }

    }
}