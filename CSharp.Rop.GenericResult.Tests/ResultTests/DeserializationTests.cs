using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace CSharp.Rop.GenericResult.Tests
{
    public class DeserializationTests
    {
        [NotNull] private const string ErrorMessage = "this failed";

        [Fact]
        public void Deserialize_sets_correct_statuses_on_success_result()
        {
            var okResult = GenericResult.Result.Success();
            var serialized = Serialize(okResult);

            var result = Deserialize<GenericResult.Result>(serialized);

            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
        }

        [Fact]
        public void Deserialize_sets_correct_statuses_on_failure_result()
        {
            var okResult = GenericResult.Result.Failure(ErrorMessage);
            var serialized = Serialize(okResult);

            var result = Deserialize<GenericResult.Result>(serialized);

            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Deserialize_adds_message_in_context_on_failure_result()
        {
            var failResult = GenericResult.Result.Failure(ErrorMessage);
            var serialized = Serialize(failResult);

            var result = Deserialize<GenericResult.Result>(serialized);

            result.Error.Should().Be(new Error(ErrorMessage));
        }

        [Fact]
        public void Deserialize_of_generic_result_adds_object_in_context_when_success_result()
        {
            var language = new DeserializationTestObject { Number = 232, String = "C#" };
            var failResult = GenericResult.Result.Success(language);
            var serialized = Serialize(failResult);

            var result = Deserialize<Result<DeserializationTestObject>>(serialized);

            result.Value.Should().BeEquivalentTo(language);
        }

        private static Stream Serialize(object source)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            return stream;
        }

        private static T Deserialize<T>(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }

        [Serializable]
        private class DeserializationTestObject
        {
            public string String { get; set; }
            public int Number { get; set; }
        }
    }
}