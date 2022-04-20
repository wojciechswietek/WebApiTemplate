using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WeatherForecast.Tests
{
    public class DbContextBasicTests : IClassFixture<DbContextFactory>
    {
        private readonly DbContextFactory _factory;

        public DbContextBasicTests(DbContextFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateDbContextAndOpenConnection()
        {
            using var context = _factory.GetContext();
            await context.Database.OpenConnectionAsync();
        }
    }
}
