using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class FailedResultTests
    {
        [Fact]
        public void Can_create_a_non_generic_version()
        {
            var result = GenericResult.Result.Failure("Error message");

            result.Error.Should().Be(new Error("Error message"));
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Can_create_a_generic_version()
        {
            var result = GenericResult.Result.Failure<MyClass>("Error message");

            result.Error.Should().Be(new Error("Error message"));
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Can_create_a_generic_version_implicit()
        {
            Result<MyClass> result = new Error("Error message");

            result.Error.Should().Be(new Error("Error message"));
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Cannot_access_Value_property()
        {
            var result = GenericResult.Result.Failure<MyClass>("Error message");

            Action action = () => { var myClass = result.Value; };

            action.Should().Throw<ResultFailureException>();
        }

        [Fact]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Cannot_create_without_error_message()
        {
            Action action1 = () => { GenericResult.Result.Failure(null); };
            Action action2 = () => { GenericResult.Result.Failure(string.Empty); };
            Action action3 = () => { GenericResult.Result.Failure<MyClass>(null); };
            Action action4 = () => { GenericResult.Result.Failure<MyClass>(string.Empty); };

            action1.Should().Throw<ArgumentNullException>();
            action2.Should().Throw<ArgumentNullException>();
            action3.Should().Throw<ArgumentNullException>();
            action4.Should().Throw<ArgumentNullException>();
        }


        [UsedImplicitly]
        private class MyClass
        {
        }

    }
}
