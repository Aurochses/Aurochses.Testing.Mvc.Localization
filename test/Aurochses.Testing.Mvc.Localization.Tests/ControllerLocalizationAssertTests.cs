using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace Aurochses.Testing.Mvc.Localization.Tests
{
    public class ControllerLocalizationAssertTests
    {
        private readonly string _projectPath;

        public ControllerLocalizationAssertTests()
        {
            _projectPath = PlatformServices.Default.Application.ApplicationBasePath;
        }

        [Fact]
        public void Validate_Success()
        {
            // Arrange
            var list = new List<LocalizedFileItem>
            {
                new LocalizedFileItem(_projectPath, @"\Fakes\ControllerLocalizationAssert\Validate\Controllers", "", "HomeController.cs")
                {
                    Names =
                    {
                        "TestName",
                        "SecondTestName",
                        "PredefinedTestName"
                    }
                }
            };

            var predefinedLocalizedFileItems = new List<LocalizedFileItem>
            {
                new LocalizedFileItem(_projectPath, @"\Fakes\ControllerLocalizationAssert\Validate\Controllers", "", "HomeController.cs")
                {
                    Names =
                    {
                        "PredefinedTestName"
                    }
                }
            };

            // Act
            var localizedFileItems = ControllerLocalizationAssert.Validate(
                _projectPath,
                @"\Fakes\ControllerLocalizationAssert\Validate\Resources\Controllers",
                @"\Fakes\ControllerLocalizationAssert\Validate\Controllers",
                predefinedLocalizedFileItems
            );

            // Assert
            Assert.Equal(1, localizedFileItems.Count);
            foreach (var item in list)
            {
                var localizedFileItem = localizedFileItems.FirstOrDefault(x => x.RelativePath == item.RelativePath && x.FileName == item.FileName);

                Assert.NotNull(localizedFileItem);
                Assert.Equal(item.Names, localizedFileItem.Names);
            }
        }
    }
}