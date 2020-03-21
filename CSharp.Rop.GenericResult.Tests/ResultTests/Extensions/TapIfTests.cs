using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class TapIf : TestBase
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TapIf_executes_action_conditionally_and_returns_self(bool condition)
        {
            var result = GenericResult.Result.Success();
            var actionExecuted = false;
            void Action() => actionExecuted = true;

            var returned = result.TapIf(condition, Action);

            actionExecuted.Should().Be(condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TapIf_T_executes_action_conditionally_and_returns_self(bool condition)
        {
            var result = GenericResult.Result.Success(T.Value);
            var actionExecuted = false;
            void Action() => actionExecuted = true;

            var returned = result.TapIf(condition, (Action) Action);

            actionExecuted.Should().Be(condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TapIf_T_executes_action_T_conditionally_and_returns_self(bool condition)
        {
            var result = GenericResult.Result.Success(T.Value);
            var actionExecuted = false;
            void Action(T _) => actionExecuted = true;

            var returned = result.TapIf(condition, (Action<T>) Action);

            actionExecuted.Should().Be(condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TapIf_executes_func_conditionally_and_returns_self(bool condition)
        {
            var result = GenericResult.Result.Success();
            var actionExecuted = false;

            Discard Func()
            {
                actionExecuted = true;
                return Discard.Value;
            }

            var returned = result.TapIf(condition, Func);

            actionExecuted.Should().Be(condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TapIf_T_executes_func_conditionally_and_returns_self(bool condition)
        {
            var result = GenericResult.Result.Success(T.Value);
            var actionExecuted = false;

            Discard Func()
            {
                actionExecuted = true;
                return Discard.Value;
            }

            var returned = result.TapIf(condition, (Func<Discard>) Func);

            actionExecuted.Should().Be(condition);
            result.Should().Be(returned);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TapIf_T_executes_func_T_conditionally_and_returns_self(bool condition)
        {
            var result = GenericResult.Result.Success(T.Value);
            var actionExecuted = false;

            Discard Func(T _)
            {
                actionExecuted = true;
                return Discard.Value;
            }

            var returned = result.TapIf(condition, (Func<T, Discard>) Func);

            actionExecuted.Should().Be(condition);
            result.Should().Be(returned);
        }

    }
}
