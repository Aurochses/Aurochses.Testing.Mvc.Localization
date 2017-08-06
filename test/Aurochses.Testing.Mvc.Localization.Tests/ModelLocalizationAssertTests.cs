﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace Aurochses.Testing.Mvc.Localization.Tests
{
    public class ModelLocalizationAssertTests
    {
        private readonly string _projectPath;

        public ModelLocalizationAssertTests()
        {
            _projectPath = PlatformServices.Default.Application.ApplicationBasePath;
        }

        [Fact]
        public void Validate_Success()
        {
            // Arrange
            var list = new List<LocalizedFileItem>
            {
                new LocalizedFileItem(_projectPath, @"\Fakes\ModelLocalizationAssert\Validate\Models", "", "HomeViewModel.cs")
                {
                    Names =
                    {
                        "Email",
                        "Email.Prompt",
                        "Email.Description"
                    }
                }
            };

            // Act
            var localizedFileItems = ModelLocalizationAssert.Validate(
                typeof(ModelLocalizationAssertTests).GetTypeInfo().Assembly,
                _projectPath,
                @"\Fakes\ModelLocalizationAssert\Validate\Resources\Models",
                @"\Fakes\ModelLocalizationAssert\Validate\Models"
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