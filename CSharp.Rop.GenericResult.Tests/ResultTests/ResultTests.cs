using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Ok_argument_is_null_Success_result_expected()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { GenericResult.Result.Success<string>(null); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create_value_is_null_Success_result_expected()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { GenericResult.Result.SuccessIf<string>(true, null, default(Error)); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create_argument_is_true_Success_result_expected()
        {
            var result = GenericResult.Result.SuccessIf(true, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void Create_argument_is_false_Failure_result_expected()
        {
            var result = GenericResult.Result.SuccessIf(false, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }
        
        [Fact]
        public void Create_predicate_is_true_Success_result_expected()
        {   
            var result = GenericResult.Result.SuccessIf(() => true, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void Create_predicate_is_false_Failure_result_expected()
        {
            var result = GenericResult.Result.SuccessIf(() => false, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }
        
        [Fact]
        public void CreateFailure_value_is_null_Success_result_expected()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { GenericResult.Result.FailureIf<string>(false, null, default(Error)); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CreateFailure_argument_is_false_Success_result_expected()
        {
            var result = GenericResult.Result.FailureIf(false, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void CreateFailure_argument_is_true_Failure_result_expected()
        {
            var result = GenericResult.Result.FailureIf(true, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }

        [Fact]
        public void CreateFailure_predicate_is_false_Success_result_expected()
        {
            var result = GenericResult.Result.FailureIf(() => false, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void CreateFailure_predicate_is_true_Failure_result_expected()
        {
            var result = GenericResult.Result.FailureIf(() => true, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }

        [Fact]
        public void Create_generic_argument_is_true_Success_result_expected()
        {
            const byte val = 7;
            var result = GenericResult.Result.SuccessIf(true, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }
        
        [Fact]
        public void Create_generic_argument_is_false_Failure_result_expected()
        {
            const double val = .56;
            var result = GenericResult.Result.SuccessIf(false, val, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }
        
        [Fact]
        public void Create_generic_predicate_is_true_Success_result_expected()
        {
            var val = new DateTime(2000, 1, 1);
            
            var result = GenericResult.Result.SuccessIf(() => true, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }
        
        [Fact]
        public void Create_generic_predicate_is_false_Failure_result_expected()
        {
            const string val = "string value";
            
            var result = GenericResult.Result.SuccessIf(() => false, val, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }
        
        [Fact]
        public void CreateFailure_generic_argument_is_false_Success_result_expected()
        {
            const byte val = 7;
            var result = GenericResult.Result.FailureIf(false, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void CreateFailure_generic_argument_is_true_Failure_result_expected()
        {
            const double val = .56;
            var result = GenericResult.Result.FailureIf(true, val, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }

        [Fact]
        public void CreateFailure_generic_predicate_is_false_Success_result_expected()
        {
            var val = new DateTime(2000, 1, 1);

            var result = GenericResult.Result.FailureIf(() => false, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void CreateFailure_generic_predicate_is_true_Failure_result_expected()
        {
            const string val = "string value";

            var result = GenericResult.Result.FailureIf(() => true, val, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }

        [Fact]
        public void Can_work_with_nullable_sructs()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { GenericResult.Result.Success((DateTime?)null); };

            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Try_execute_function_success_without_error_handler_function_result_expected()
        {
            int Func() => 5;

            var result = GenericResult.Result.Try((Func<int>) Func);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(5);
        }
        
        [Fact]
        public void Try_execute_function_failed_without_error_handler_failed_result_expected()
        {
            int Func() => throw new Exception("func error");

            var result = GenericResult.Result.Try((Func<int>) Func);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("func error"));
        }
        
        [Fact]
        public void Try_execute_function_failed_with_error_handler_failed_result_expected()
        {
            int Func() => throw new Exception("func error");
            Error Handler(Exception exc) => new Error("execute error");

            var result = GenericResult.Result.Try((Func<int>) Func, Handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute error"));
        }
        
        [Fact]
        public void Try_execute_action_success_without_error_handler_function_result_expected()
        {
            void Action()
            {
            }

            var result = GenericResult.Result.Try(Action);

            result.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void Try_execute_action_failed_without_error_handler_failed_result_expected()
        {
            void Action() => throw new Exception("func error");

            var result = GenericResult.Result.Try(Action);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("func error"));
        }
        
        [Fact]
        public void Try_execute_action_failed_with_error_handler_failed_result_expected()
        {
            void Action() => throw new Exception("func error");
            Error Handler(Exception exc) => new Error("execute error");

            var result = GenericResult.Result.Try(Action, Handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute error"));
        }
        
        [Fact]
        public void Try_with_error_execute_function_success_without_error_success_result_expected()
        {
            string Func() => "execution result";
            var error = new Error("error message");
            
            var result = GenericResult.Result.Try((Func<string>) Func, exc => error);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("execution result");
        }
        
        [Fact]
        public void Try_with_error_execute_function_failed_with_error_handler_failed_result_expected()
        {
            int Func() => throw new Exception("func error");
            var error = new Error("error message");
            
            var result = GenericResult.Result.Try((Func<int>) Func, exc => error);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(error);
        }

    }
}