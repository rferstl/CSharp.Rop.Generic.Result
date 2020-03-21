using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.Generic.Result.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Ok_argument_is_null_Success_result_expected()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { Result.Success<string>(null); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create_value_is_null_Success_result_expected()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { Result.SuccessIf<string>(true, null, default(Error)); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create_argument_is_true_Success_result_expected()
        {
            var result = Result.SuccessIf(true, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void Create_argument_is_false_Failure_result_expected()
        {
            var result = Result.SuccessIf(false, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }
        
        [Fact]
        public void Create_predicate_is_true_Success_result_expected()
        {   
            var result = Result.SuccessIf(() => true, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void Create_predicate_is_false_Failure_result_expected()
        {
            var result = Result.SuccessIf(() => false, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }
        
        [Fact]
        public void CreateFailure_value_is_null_Success_result_expected()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { Result.FailureIf<string>(false, null, default(Error)); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CreateFailure_argument_is_false_Success_result_expected()
        {
            var result = Result.FailureIf(false, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void CreateFailure_argument_is_true_Failure_result_expected()
        {
            var result = Result.FailureIf(true, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }

        [Fact]
        public void CreateFailure_predicate_is_false_Success_result_expected()
        {
            var result = Result.FailureIf(() => false, Error.Empty);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void CreateFailure_predicate_is_true_Failure_result_expected()
        {
            var result = Result.FailureIf(() => true, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }

        [Fact]
        public void Create_generic_argument_is_true_Success_result_expected()
        {
            const byte val = 7;
            var result = Result.SuccessIf(true, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }
        
        [Fact]
        public void Create_generic_argument_is_false_Failure_result_expected()
        {
            const double val = .56;
            var result = Result.SuccessIf(false, val, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }
        
        [Fact]
        public void Create_generic_predicate_is_true_Success_result_expected()
        {
            var val = new DateTime(2000, 1, 1);
            
            var result = Result.SuccessIf(() => true, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }
        
        [Fact]
        public void Create_generic_predicate_is_false_Failure_result_expected()
        {
            const string val = "string value";
            
            var result = Result.SuccessIf(() => false, val, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }
        
        [Fact]
        public void CreateFailure_generic_argument_is_false_Success_result_expected()
        {
            const byte val = 7;
            var result = Result.FailureIf(false, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void CreateFailure_generic_argument_is_true_Failure_result_expected()
        {
            const double val = .56;
            var result = Result.FailureIf(true, val, new Error("simple result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("simple result error"));
        }

        [Fact]
        public void CreateFailure_generic_predicate_is_false_Success_result_expected()
        {
            var val = new DateTime(2000, 1, 1);

            var result = Result.FailureIf(() => false, val, Error.Empty);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(val);
        }

        [Fact]
        public void CreateFailure_generic_predicate_is_true_Failure_result_expected()
        {
            const string val = "string value";

            var result = Result.FailureIf(() => true, val, new Error("predicate result error"));

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate result error"));
        }

        [Fact]
        public void Can_work_with_nullable_sructs()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Action action = () => { Result.Success((DateTime?)null); };

            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Try_execute_function_success_without_error_handler_function_result_expected()
        {
            Func<int> func = () => 5;
            
            var result = Result.Try(func);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(5);
        }
        
        [Fact]
        public void Try_execute_function_failed_without_error_handler_failed_result_expected()
        {
            Func<int> func = () => throw new Exception("func error");
            
            var result = Result.Try(func);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("func error"));
        }
        
        [Fact]
        public void Try_execute_function_failed_with_error_handler_failed_result_expected()
        {
            Func<int> func = () => throw new Exception("func error");
            Func<Exception, Error> handler = exc => new Error("execute error");
            
            var result = Result.Try(func, handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute error"));
        }
        
        [Fact]
        public void Try_execute_action_success_without_error_handler_function_result_expected()
        {
            Action action = () => { };
            
            var result = Result.Try(action);

            result.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void Try_execute_action_failed_without_error_handler_failed_result_expected()
        {
            Action action = () => throw new Exception("func error");
            
            var result = Result.Try(action);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("func error"));
        }
        
        [Fact]
        public void Try_execute_action_failed_with_error_handler_failed_result_expected()
        {
            Action action = () => throw new Exception("func error");
            Func<Exception, Error> handler = exc => new Error("execute error");
            
            var result = Result.Try(action, handler);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("execute error"));
        }
        
        [Fact]
        public void Try_with_error_execute_function_success_without_error_success_result_expected()
        {
            Func<string> func = () => "execution result";
            var error = new Error("error message");
            
            var result = Result.Try(func, exc => error);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be("execution result");
        }
        
        [Fact]
        public void Try_with_error_execute_function_failed_with_error_handler_failed_result_expected()
        {
            Func<int> func = () => throw new Exception("func error");
            var error = new Error("error message");
            
            var result = Result.Try(func, exc => error);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(error);
        }

    }
}