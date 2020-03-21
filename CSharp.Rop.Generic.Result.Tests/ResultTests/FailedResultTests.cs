using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.Generic.Result.Tests
{
    public class FailedResultTests
    {
        [Fact]
        public void Can_create_a_non_generic_version()
        {
            var result = Result.Failure("Error message");

            result.Error.Should().Be(new Error("Error message"));
            result.IsFailure.Should().Be(true);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public void Can_create_a_generic_version()
        {
            var result = Result.Failure<MyClass>("Error message");

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
            var result = Result.Failure<MyClass>("Error message");

            Action action = () => { var myClass = result.Value; };

            action.Should().Throw<ResultFailureException>();
        }

        [Fact]
        public void Cannot_create_without_error_message()
        {
            Action action1 = () => { Result.Failure(null); };
            Action action2 = () => { Result.Failure(string.Empty); };
            Action action3 = () => { Result.Failure<MyClass>(null); };
            Action action4 = () => { Result.Failure<MyClass>(string.Empty); };

            action1.Should().Throw<ArgumentNullException>();
            action2.Should().Throw<ArgumentNullException>();
            action3.Should().Throw<ArgumentNullException>();
            action4.Should().Throw<ArgumentNullException>();
        }


        private class MyClass
        {
        }

    }
}
