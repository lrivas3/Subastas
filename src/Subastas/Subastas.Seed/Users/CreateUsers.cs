using Microsoft.Extensions.DependencyInjection;
using Subastas.Interfaces;

namespace Subastas.Seed.Users
{
    public class CreateUsers : IClassFixture<TestFixture>
    {
        private readonly IUserService _userService;

        public CreateUsers(TestFixture fixture)
        {
            _userService = fixture.ServiceProvider.GetService<IUserService>();
        }

        [Fact]
        public async Task CreateUsersSeed()
        {
            var users = await _userService.GetAll();
            var user = users.FirstOrDefault();
            Assert.Null(user);
        }
    }
}
