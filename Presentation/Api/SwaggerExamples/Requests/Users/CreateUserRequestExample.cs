namespace Api.SwaggerExamples.Requests.Users
{
    using Application.Users.Commands.CreateUser;
    using Swashbuckle.AspNetCore.Filters;

    public class CreateUserRequestExample : IExamplesProvider<CreateUserCommand>
    {
        public CreateUserCommand GetExamples()
            => new CreateUserCommand { Email = "test@test.com", FullName = "TEST TEST", Password = "Test123" };
    }
}