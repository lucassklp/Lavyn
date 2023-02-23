using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Lavyn.Persistence
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DaoContext>
    {
        public DaoContext CreateDbContext(string[] args)
        {
            var config = Directory.GetParent(Directory.GetCurrentDirectory())
                            .GetDirectories()
                            .Where(x => x.Name == "Lavyn.Web")?.First()?.FullName;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? EnvironmentName.Development;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(config)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json")
                .Build();

            var builder = new DbContextOptions<DaoContext>();
            return new DaoContext(builder, NullLoggerFactory.Instance, configuration);
        }
    }

}
