using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.Generic.Result.Tests
{
    public class Tap : TestBase
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_executes_action_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = Result.SuccessIf(isSuccess, new Error("Error"));
            var actionExecuted = false;
            Action action = () => actionExecuted = true;

            var returned = result.Tap(action);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_action_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;
            Action action = () => actionExecuted = true;

            var returned = result.Tap(action);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_action_T_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;
            Action<T> action = (T _) => actionExecuted = true;

            var returned = result.Tap(action);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_executes_func_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = Result.SuccessIf(isSuccess, new Error( "Error"));
            var actionExecuted = false;
            Func<Discard> func = () => { actionExecuted = true; return Discard.Value; };

            var returned = result.Tap(func);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_func_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;
            Func<Discard> func = () => { actionExecuted = true; return Discard.Value; };

            var returned = result.Tap(func);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_func_T_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;
            Func<T, Discard> func = (T _) => { actionExecuted = true; return Discard.Value; };

            var returned = result.Tap(func);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

    }
}
