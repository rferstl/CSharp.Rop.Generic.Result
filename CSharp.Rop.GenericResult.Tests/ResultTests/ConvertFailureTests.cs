using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class ConvertFailureTests
    {

        [Fact]
        public void Can_not_convert_okResult_without_value_to_okResult_with_value()
        {
            var okResultWithoutValue = GenericResult.Result.Success();

            Action action = () => okResultWithoutValue.ConvertFailure<MyValueClass>();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Can_convert_failedResult_without_value_to_failedResult_with_value()
        {
            var failedResultWithoutValue = GenericResult.Result.Failure("Failed");

            var failedResultWithValue = failedResultWithoutValue.ConvertFailure<MyValueClass>();

            failedResultWithValue.IsFailure.Should().BeTrue();
            failedResultWithValue.Error.Should().Be(new Error("Failed"));
        }

        [Fact]
        public void Can_not_convert_okResult_with_value_to_okResult_without_value()
        {
            var okResultWithValue = GenericResult.Result.Success(new MyValueClass());

            Action action = () => okResultWithValue.ConvertFailure();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Can_convert_failedResult_with_value_to_failedResult_without_value()
        {
            var failedResultWithValue = GenericResult.Result.Failure<MyValueClass>("Failed");

            GenericResult.Result failedResultWithoutValue = failedResultWithValue;

            failedResultWithoutValue.IsFailure.Should().BeTrue();
            failedResultWithoutValue.Error.Should().Be(new Error("Failed"));
        }

        [Fact]
        public void Can_not_convert_okResult_with_value_to_okResult_with_otherValue()
        {
            var okResultWithValue = GenericResult.Result.Success(new MyValueClass());

            Action action = () => okResultWithValue.ConvertFailure<MyValueClass2>();

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Can_convert_failedResult_with_value_to_failedResult_with_other_value()
        {
            var failedResultWithValue = GenericResult.Result.Failure<MyValueClass>("Failed");

            var failedResultWithOtherValue = failedResultWithValue.ConvertFailure<MyValueClass2>();

            failedResultWithOtherValue.IsFailure.Should().BeTrue();
            failedResultWithOtherValue.Error.Should().Be(new Error("Failed"));
        }

    }

    public class MyValueClass
    {
        public int Prop { get; set; }
    }

    public class MyValueClass2
    {
        public int Prop { get; set; }
    }

    public class MyErrorClass
    {
        public string Prop { get; set; }
    }
}