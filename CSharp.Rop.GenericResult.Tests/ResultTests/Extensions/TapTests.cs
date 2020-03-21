using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class Tap : TestBase
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_executes_action_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = GenericResult.Result.SuccessIf(isSuccess, new Error("Error"));
            var actionExecuted = false;
            void Action() => actionExecuted = true;

            var returned = result.Tap(Action);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_action_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = GenericResult.Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;
            void Action() => actionExecuted = true;

            var returned = result.Tap((Action) Action);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_action_T_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = GenericResult.Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;
            void Action(T _) => actionExecuted = true;

            var returned = result.Tap((Action<T>) Action);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_executes_func_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = GenericResult.Result.SuccessIf(isSuccess, new Error( "Error"));
            var actionExecuted = false;

            Discard Func()
            {
                actionExecuted = true;
                return Discard.Value;
            }

            var returned = result.Tap(Func);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_func_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = GenericResult.Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;

            Discard Func()
            {
                actionExecuted = true;
                return Discard.Value;
            }

            var returned = result.Tap((Func<Discard>) Func);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Tap_T_executes_func_T_on_result_success_and_returns_self(bool isSuccess)
        {
            var result = GenericResult.Result.SuccessIf(isSuccess, T.Value, new Error("Error"));
            var actionExecuted = false;

            Discard Func(T _)
            {
                actionExecuted = true;
                return Discard.Value;
            }

            var returned = result.Tap((Func<T, Discard>) Func);

            actionExecuted.Should().Be(isSuccess);
            result.Should().Be(returned);
        }

    }
}
