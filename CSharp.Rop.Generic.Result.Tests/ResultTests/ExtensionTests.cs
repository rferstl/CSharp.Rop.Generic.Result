using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.Generic.Result.Tests
{
    public class ExtensionTests
    {
        private const string ErrorMessage = "this failed";

        [Fact]
        public void Should_execute_action_on_failure()
        {
            var myBool = false;

            var myResult = Result.Failure(ErrorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_execute_action_on_generic_failure()
        {
            var myBool = false;

            var myResult = Result.Failure<MyClass>(ErrorMessage);
            myResult.OnFailure(() => myBool = true);

            myBool.Should().Be(true);
        }

        [Fact]
        public void Should_exexcute_action_with_result_on_generic_failure()
        {
            var myError = Error.Empty;

            var myResult = Result.Failure<MyClass>(ErrorMessage);
            myResult.OnFailure(error => myError = error);

            myError.Should().Be(new Error(ErrorMessage));
        }

        [Fact]
        public void Should_execute_compensate_func_on_failure_returns_Ok()
        {
            var myResult = Result.Failure(ErrorMessage);
            var newResult = myResult.OnFailureCompensate(() => Result.Success());

            newResult.IsSuccess.Should().Be(true);
        }

        [Fact]
        public void Should_execute_compensate_func_on_generic_failure_returns_Ok()
        {
            var expectedValue = new MyClass();

            var myResult = Result.Failure<MyClass>(ErrorMessage);
            var newResult = myResult.OnFailureCompensate(() => Result.Success(expectedValue));

            newResult.IsSuccess.Should().BeTrue();
            newResult.Value.Should().Be(expectedValue);
        }

        [Fact]
        public void Should_execute_compensate_func_with_result_on_generic_failure_returns_Ok()
        {
            var expectedValue = new MyClass();

            var myResult = Result.Failure<MyClass>(ErrorMessage);
            var newResult = myResult.OnFailureCompensate(error => Result.Success(expectedValue));

            newResult.IsSuccess.Should().BeTrue();
            newResult.HasValue.Should().BeTrue();
            newResult.Value.Should().Be(expectedValue);
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_action_original_failed_result_expected()
        {
            var originalResult = Result.Failure("error");

            var result = originalResult.OnSuccessTry(() => { });

            result.IsFailure.Should().BeTrue();
            result.Should().Be(originalResult);
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_success_result_expected()
        {
            var originalResult = Result.Success();
            var isExecuted = false;

            var result = originalResult.OnSuccessTry(() =>
            {
                isExecuted = true;
            });

            result.IsSuccess.Should().BeTrue();

            isExecuted.Should().BeTrue();
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_throw_exception_failed_result_expected()
        {
            var originalResult = Result.Success();

            var result = originalResult.OnSuccessTry(() => throw new Exception("execute action exception."));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute action exception."));
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_function_new_failed_result_expected()
        {
            var originalResult = Result.Failure("original result error message");

            var result = originalResult.OnSuccessTry(() => 3);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("original result error message"));
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_success_result_expected()
        {
            var originalResult = Result.Success();

            var result = originalResult.OnSuccessTry(() => 7);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(7);
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_throw_exception_failed_result_expected()
        {
            var originalResult = Result.Success();
            Func<DateTime> func = () => throw new Exception("execute action exception.");

            var result = originalResult.OnSuccessTry(func);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute action exception."));
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_function_with_argument_new_failed_result_expected()
        {
            var originalResult = Result.Failure<DateTime>("original result error message");

            var result = originalResult.OnSuccessTry(date => date.Day);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("original result error message"));
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_with_argument_success_result_expected()
        {
            var originalResult = Result.Success<byte>(2);

            var result = originalResult.OnSuccessTry(val => val * val);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(4);
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_function_with_argument_throw_exception_failed_result_expected()
        {
            var originalResult = Result.Success(2);
            Func<int, DateTime> func = val => throw new Exception("execute action exception");

            var result = originalResult.OnSuccessTry(func);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute action exception"));
        }

        [Fact]
        public void OnSuccessTry_failed_result_execute_action_with_argument_new_failed_result_expected()
        {
            var originalResult = Result.Failure<DateTime>("original result error message");

            var result = originalResult.OnSuccessTry(date => { });

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("original result error message"));
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_with_argument_success_result_expected()
        {
            var originalResult = Result.Success<byte>(2);
            var isExecuted = false;

            var result = originalResult.OnSuccessTry(val => { isExecuted = true; });

            result.IsSuccess.Should().BeTrue();

            isExecuted.Should().BeTrue();
        }

        [Fact]
        public void OnSuccessTry_success_result_execute_action_with_argument_throw_exception_failed_result_expected()
        {
            var originalResult = Result.Success(2);
            Action<int> action = val => throw new Exception("execute action exception");

            var result = originalResult.OnSuccessTry(action);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute action exception"));
        }


        [Fact]
        public void Match_for_Result_of_int_follows_Ok_branch_where_there_is_a_value()
        {
            var result = Result.Success(20);

            result.Match(
                value => value.Should().Be(20),
                () => throw new FieldAccessException("Accessed NoValue path while result is Ok with value"),
                _ => throw new FieldAccessException("Accessed Failure path while result is Ok with value")
            );
        }

        [Fact]
        public void Match_for_Result_of_int_follows_Failure_branch_where_is_no_value()
        {
            var result = Result.Failure<int>("error");

            result.Match(
                _ => throw new FieldAccessException("Accessed Ok path while result is Failure"),
                () => throw new FieldAccessException("Accessed Ok path while result is Failure"),
                error => error.Should().Be(new Error("error"))
            );
        }

        [Fact]
        public void Match_for_empty_Result_follows_Ok_branch_where_there_is_a_value()
        {
            var result = Result.Success();

            result.Match(
                () => Assert.True(true),
                _ => throw new FieldAccessException("Accessed Failure path while result is Ok")
            );
        }

        [Fact]
        public void Match_for_empty_Result_follows_Failure_branch_where_is_no_value()
        {
            var result = Result.Failure("error");

            result.Match(
                () => throw new FieldAccessException("Accessed Ok path while result is Failure"),
                message => message.Should().Be(new Error("error"))
            );
        }

        private class MyClass
        {
            public string Property { get; set; }
        }
    }
}
