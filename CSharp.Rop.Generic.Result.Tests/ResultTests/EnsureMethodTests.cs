using System;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.Generic.Result.Tests
{
    public class EnsureMethodTests
    {
        [Fact]
        public void Ensure_source_result_is_failure_predicate_do_not_invoked_expect_is_result_failure()
        {
            var sut = Result.Failure("some error");

            var result = sut.Ensure(() => true, Error.Empty);

            result.Should().Be(sut);
        }
        
        [Fact]
        public void Ensure_source_result_is_success_predicate_is_failed_expected_result_failure()
        {
            var sut = Result.Success();

            var result = sut.Ensure(() => false, new Error("predicate failed"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("predicate failed"));
        }
        
        [Fact]
        public void Ensure_source_result_is_success_predicate_is_passed_expected_result_success()
        {
            var sut = Result.Success();
            
            var result = sut.Ensure(() => true, Error.Empty);

            result.Should().Be(sut);
        }

        [Fact]
        public void Ensure_generic_source_result_is_failure_predicate_do_not_invoked_expect_is_error_result_failure()
        {
            var sut = Result.Failure<TimeSpan>("some error");

            var result = sut.Ensure(time => true, new Error("test error"));

            result.Should().Be(sut);
        }
        
        [Fact]
        public void Ensure_generic_source_result_is_success_predicate_is_failed_expected_error_result_failure()
        {
            var sut = Result.Success(10101);

            var result = sut.Ensure(i => false, new Error("test error"));

            result.Should().NotBe(sut);
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("test error"));
        }
        
        [Fact]
        public void Ensure_generic_source_result_is_success_predicate_is_passed_expected_error_result_success()
        {
            var sut = Result.Success(.03m);

            var result = sut.Ensure(d => true, new Error("test error"));

            result.Should().Be(sut);
        }

    }
}