using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class CombineMethodTests
    {
        [Fact]
        public void FirstFailureOrSuccess_returns_the_first_failed_result()
        {
            var result1 = GenericResult.Result.Success();
            var result2 = GenericResult.Result.Failure("Failure 1");
            var result3 = GenericResult.Result.Failure("Failure 2");

            var result = GenericResult.Result.FirstFailureOrSuccess(result1, result2, result3);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("Failure 1"));
        }

        [Fact]
        public void FirstFailureOrSuccess_returns_Ok_if_no_failures()
        {
            var result1 = GenericResult.Result.Success();
            var result2 = GenericResult.Result.Success();
            var result3 = GenericResult.Result.Success();

            var result = GenericResult.Result.FirstFailureOrSuccess(result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_combines_all_errors_together()
        {
            var result1 = GenericResult.Result.Success();
            var result2 = GenericResult.Result.Failure("Failure 1");
            var result3 = GenericResult.Result.Failure("Failure 2");

            var result = GenericResult.Result.Combine(";", result1, result2, result3);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("Failure 1;Failure 2"));
        }

        [Fact]
        public void Combine_returns_Ok_if_no_failures()
        {
            var result1 = GenericResult.Result.Success();
            var result2 = GenericResult.Result.Success();
            var result3 = GenericResult.Result.Success("Some string");

            var result = GenericResult.Result.Combine(";", result1, result2, result3);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ErrorMessagesSeparator_Combine_combines_all_errors_with_configured_ErrorMessagesSeparator_together()
        {
            var previousErrorMessagesSeparator = GenericResult.Result.ErrorMessagesSeparator;

            var result1 = GenericResult.Result.Failure("E1");
            var result2 = GenericResult.Result.Failure("E2");

            GenericResult.Result.ErrorMessagesSeparator = "{Separator}";
            var result = GenericResult.Result.Combine(result1, result2);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("E1{Separator}E2"));

            GenericResult.Result.ErrorMessagesSeparator = previousErrorMessagesSeparator;
        }

        [Fact]
        public void ErrorMessagesSeparator_Combine_combines_all_collection_errors_with_configured_ErrorMessagesSeparator_together()
        {
            var previousErrorMessagesSeparator = GenericResult.Result.ErrorMessagesSeparator;

            IEnumerable<GenericResult.Result> results = new GenericResult.Result[]
            {
                GenericResult.Result.Failure("E1"),
                GenericResult.Result.Failure("E2")
            };

            GenericResult.Result.ErrorMessagesSeparator = "{Separator}";
            var result = results.Combine();

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("E1{Separator}E2"));

            GenericResult.Result.ErrorMessagesSeparator = previousErrorMessagesSeparator;
        }

        [Fact]
        public void Combine_works_with_array_of_Generic_results_success()
        {
            var results = new Result<string>[] { GenericResult.Result.Success(""), GenericResult.Result.Success("") };

            var result = GenericResult.Result.Combine(";", results);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_works_with_array_of_Generic_results_failure()
        {
            var results = new Result<string>[] { GenericResult.Result.Success(""), GenericResult.Result.Failure<string>("m") };

            var result = GenericResult.Result.Combine(";", results);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void Combine_combines_all_collection_errors_together()
        {
            IEnumerable<GenericResult.Result> results = new GenericResult.Result[]
            {
                GenericResult.Result.Success(),
                GenericResult.Result.Failure("Failure 1"),
                GenericResult.Result.Failure("Failure 2")
            };

            var result = results.Combine(";");

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("Failure 1;Failure 2"));
        }

        [Fact]
        public void Combine_returns_Ok_if_no_failures_in_collection()
        {
            IEnumerable<GenericResult.Result> results = new GenericResult.Result[]
            {
                GenericResult.Result.Success(),
                GenericResult.Result.Success(),
                GenericResult.Result.Success("Some string")
            };

            var result = results.Combine(";");

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Combine_combines_all_Generic_results_collection_errors_together()
        {
            IEnumerable<Result<string>> results = new Result<string>[]
            {
                GenericResult.Result.Success<string>("str 1"),
                GenericResult.Result.Failure<string>("Failure 1"),
                GenericResult.Result.Failure<string>("Failure 2")
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
                GenericResult.Result.Success(21),
                GenericResult.Result.Success(34),
                GenericResult.Result.Success(55)
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
                GenericResult.Result.Success("one"),
                GenericResult.Result.Success("two"),
                GenericResult.Result.Success("three")
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
                GenericResult.Result.Success(""),
                GenericResult.Result.Failure<string>("m"),
                GenericResult.Result.Failure<string>("o")
            };

            GenericResult.Result result = results.Combine(";");

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(new Error("m;o"));
        }

        [Fact]
        public void Combine_works_with_collection_of_results_and_compose_to_new_result_success()
        {
            IEnumerable<Result<int>> results = new Result<int>[]
            {
                GenericResult.Result.Success(10),
                GenericResult.Result.Success(20),
                GenericResult.Result.Success(30),
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
                GenericResult.Result.Success("one"),
                GenericResult.Result.Success("five"),
                GenericResult.Result.Success("three"),
                GenericResult.Result.Failure<string>("error 1"),
                GenericResult.Result.Failure<string>("error 2")
            };

            var result = results.Combine(values => values == null ? "" : values.OrderBy(e => e.Length).First(), ";");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("error 1;error 2"));
        }

    }
}
