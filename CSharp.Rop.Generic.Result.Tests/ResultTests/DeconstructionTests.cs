using FluentAssertions;
using Xunit;

namespace CSharp.Rop.Generic.Result.Tests
{
    public class DeconstructionTests
    {

        [Fact]
        public void Can_deconstruct_non_generic_Ok_to_isSuccess_and_isFailure_and_error()
        {
            var (isSuccess, error) = Result.Success();

            isSuccess.Should().Be(true);
            error.Should().Be(default(Error));
        }

        [Fact]
        public void Can_deconstruct_non_generic_Fail_to_isSuccess_and_isFailure_and_error()
        {
            var (isSuccess, error) = Result.Failure("fail");

            isSuccess.Should().Be(false);
            error.Should().Be(new Error("fail"));
        }

        [Fact]
        public void Can_deconstruct_generic_Fail_to_isSuccess_and_isFailure()
        {
            var (isSuccess, hasValue) = Result.Failure<bool>("fail");

            isSuccess.Should().Be(false);
            hasValue.Should().Be(false);
        }

        [Fact]
        public void Can_deconstruct_generic_Ok_to_isSuccess_and_isFailure_and_value()
        {
            var (isSuccess, hasValue, value) = Result.Success(100);

            isSuccess.Should().Be(true);
            hasValue.Should().Be(true);
            value.Should().Be(100);
        }

        [Fact]
        public void Can_deconstruct_generic_Ok_to_isSuccess_and_isFailure_and_value_with_ignored_error()
        {
            var (isSuccess, hasValue, value, _) = Result.Success(100);

            isSuccess.Should().Be(true);
            hasValue.Should().Be(true);
            value.Should().Be(100);
        }

        [Fact]
        public void Can_deconstruct_generic_Ok_to_isSuccess_and_isFailure_and_error_with_ignored_value()
        {
            var (isSuccess, hasValue, _, error) = Result.Success(100);

            isSuccess.Should().Be(true);
            hasValue.Should().Be(true);
            error.Should().Be(default(Error));
        }

        [Fact]
        public void Can_deconstruct_generic_Fail_to_isSuccess_and_isFailure_and_error_with_ignored_value()
        {
            var (isSuccess, hasValue, _, error) = Result.Failure<bool>("fail");

            isSuccess.Should().Be(false);
            hasValue.Should().Be(false);
            error.Should().Be(new Error("fail"));
        }

    }
}
