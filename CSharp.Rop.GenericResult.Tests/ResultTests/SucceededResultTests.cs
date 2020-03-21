using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class SucceededResultTests
    {
        [Fact]
        public void Can_create_a_non_generic_version()
        {
            var result = GenericResult.Result.Success();

            result.IsFailure.Should().Be(false);
            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void Can_create_a_generic_version()
        {
            var myClass = new MyClass();

            var result = GenericResult.Result.Success(myClass);

            result.IsFailure.Should().Be(false);
            result.IsSuccess.Should().Be(true);
            result.Value.Should().Be(myClass);
        }

        [Fact]
        public void Can_create_a_generic_version_implicit()
        {
            var myClass = new MyClass();

            Result<MyClass> result = myClass;

            result.IsFailure.Should().Be(false);
            result.IsSuccess.Should().Be(true);
            result.Value.Should().Be(myClass);
        }

        [Fact]
        public void Can_create_without_Value()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { GenericResult.Result.Success((MyClass)null); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Cannot_access_Error_non_generic_version()
        {
            var result = GenericResult.Result.Success();

            Action action = () =>
            {
                var error = result.Error;
            };

            action.Should().Throw<ResultSuccessException>();
        }

        [Fact]
        public void Cannot_access_Error_generic_version()
        {
            var result = GenericResult.Result.Success(new MyClass());

            Action action = () =>
            {
                var error = result.Error;
            };

            action.Should().Throw<ResultSuccessException>();
        }

        private class MyClass
        {
        }

    }
}
