using System.Linq;
using AutoMapper;
using Xunit;

namespace TestTemplate10.Api.Tests
{
    public class AutoMapperConfigurationsTests
    {
        [Fact]
        public void AutoMapperConfigurations_AreAllValid()
        {
            // Arrange - get all profiles in the TestTemplate10.Application assembly.
            var profiles = ApiAssemblyInfo.Value.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));

            // Act
            var target = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }).CreateMapper();

            // Assert
            target.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
