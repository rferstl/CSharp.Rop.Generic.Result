using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.Generic.Result.Tests
{
    public class CombineMethodTests
    {
        [Fact]
        public void FirstFailureOrSuccess_returns_the_first_failed_result()
        {
            var result1 = Result.Success();
            var result2 = Result.Failure("Failure 1");
            var result3 = Result.Failure("Failure 2");

            var result = Result.FirstFailureOrSuccess(result1, result2, result3);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("Failure 1"));
        }

        [Fact]
        public void FirstFailureOrSuccess_returns_Ok_if_no_failures()
        {
            var result1 = Result.Success();
            var result2 = Result.Success();
            var result3 = Result.Success();

            var result = Result.FirstFailureOrSuccess(result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_combines_all_errors_together()
        {
            var result1 = Result.Success();
            var result2 = Result.Failure("Failure 1");
            var result3 = Result.Failure("Failure 2");

            var result = Result.Combine(";", result1, result2, result3);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("Failure 1;Failure 2"));
        }

        [Fact]
        public void Combine_returns_Ok_if_no_failures()
        {
            var result1 = Result.Success();
            var result2 = Result.Success();
            var result3 = Result.Success("Some string");

            var result = Result.Combine(";", result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ErrorMessagesSeparator_Combine_combines_all_errors_with_configured_ErrorMessagesSeparator_together()
        {
            var previousErrorMessagesSeparator = Result.ErrorMessagesSeparator;

            var result1 = Result.Failure("E1");
            var result2 = Result.Failure("E2");

            Result.ErrorMessagesSeparator = "{Separator}";
            var result = Result.Combine(result1, result2);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("E1{Separator}E2"));

            Result.ErrorMessagesSeparator = previousErrorMessagesSeparator;
        }

        [Fact]
        public void ErrorMessagesSeparator_Combine_combines_all_collection_errors_with_configured_ErrorMessagesSeparator_together()
        {
            var previousErrorMessagesSeparator = Result.ErrorMessagesSeparator;

            IEnumerable<Result> results = new Result[]
            {
                Result.Failure("E1"),
                Result.Failure("E2")
            };

            Result.ErrorMessagesSeparator = "{Separator}";
            var result = results.Combine();

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("E1{Separator}E2"));

            Result.ErrorMessagesSeparator = previousErrorMessagesSeparator;
        }

        [Fact]
        public void Combine_works_with_array_of_Generic_results_success()
        {
            var results = new Result<string>[] { Result.Success(""), Result.Success("") };

            var result = Result.Combine(";", results);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_works_with_array_of_Generic_results_failure()
        {
            var results = new Result<string>[] { Result.Success(""), Result.Failure<string>("m") };

            var result = Result.Combine(";", results);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Combine_combines_all_collection_errors_together()
        {
            IEnumerable<Result> results = new Result[]
            {
                Result.Success(),
                Result.Failure("Failure 1"),
                Result.Failure("Failure 2")
            };

            var result = results.Combine(";");

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("Failure 1;Failure 2"));
        }

        [Fact]
        public void Combine_returns_Ok_if_no_failures_in_collection()
        {
            IEnumerable<Result> results = new Result[]
            {
                Result.Success(),
                Result.Success(),
                Result.Success("Some string")
            };

            var result = results.Combine(";");

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_combines_all_Generic_results_collection_errors_together()
        {
            IEnumerable<Result<string>> results = new Result<string>[]
            {
                Result.Success<string>("str 1"),
                Result.Failure<string>("Failure 1"),
                Result.Failure<string>("Failure 2")
            };

            var result = results.Combine(";");

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("Failure 1;Failure 2"));
        }

        [Fact]
        public void Combine_returns_Ok_if_no_failures_in_Generic_results_collection()
        {
            IEnumerable<Result<int>> results = new Result<int>[]
            {
                Result.Success(21),
                Result.Success(34),
                Result.Success(55)
            };

            var result = results.Combine(";");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(new[] { 21, 34, 55 });
        }

        [Fact]
        public void Combine_works_with_collection_of_Generic_results_success()
        {
            IEnumerable<Result<string>> results = new Result<string>[]
            {
                Result.Success("one"),
                Result.Success("two"),
                Result.Success("three")
            };

            var result = results.Combine(";");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo("one", "two", "three");
        }

        [Fact]
        public void Combine_works_with_collection_of_Generic_results_failure()
        {
            IEnumerable<Result<string>> results = new Result<string>[]
            {
                Result.Success(""),
                Result.Failure<string>("m"),
                Result.Failure<string>("o")
            };

            Result result = results.Combine(";");

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("m;o"));
        }

        [Fact]
        public void Combine_works_with_collection_of_results_and_compose_to_new_result_success()
        {
            IEnumerable<Result<int>> results = new Result<int>[]
            {
                Result.Success(10),
                Result.Success(20),
                Result.Success(30),
            };

            var result = results.Combine(values => values == null ? 0 : (double)values.Max() / 100, ";");

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(.3);
        }

        [Fact]
        public void Combine_works_with_collection_of_results_and_compose_to_new_result_failure()
        {
            IEnumerable<Result<string>> results = new Result<string>[]
            {
                Result.Success("one"),
                Result.Success("five"),
                Result.Success("three"),
                Result.Failure<string>("error 1"),
                Result.Failure<string>("error 2")
            };

            var result = results.Combine(values => values == null ? "" : values.OrderBy(e => e.Length).First(), ";");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("error 1;error 2"));
        }

    }
}
