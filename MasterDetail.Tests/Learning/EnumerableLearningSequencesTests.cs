using System.Linq;
using MasterDetail.Core.Helper;
using NUnit.Framework;

namespace MasterDetail.Tests.Helpers
{
    [TestFixture]
    public class EnumerableLearningSequencesTests
    {
        [Test]
        public void BuildIntSequenceTest()
        {
            // Arrange
            var listBuilder = new EnumerableLearningSequences();

            // Act
            var result = listBuilder.BuildIntSequence();

            // Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item.ToString());
            }

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void BuildStringSequenceTest()
        {
            // Arrange
            var listBuilder = new EnumerableLearningSequences();

            // Act
            var result = listBuilder.BuildStringSequence();

            // Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item);
            }

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void BuildPersonSequenceTest()
        {
            // Arrange
            var listBuilder = new EnumerableLearningSequences();

            // Act
            var result = listBuilder.BuildPersonSequence();

            // Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item.Name);
            }

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CompareSequencesIntersectTest()
        {
            // Arrange
            var listBuilder = new EnumerableLearningSequences();

            // Act
            var result = listBuilder.CompareSequencesIntersect();

            // Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item);
            }

            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void CompareSequencesExceptTest()
        {
            // Arrange
            var listBuilder = new EnumerableLearningSequences();

            // Act
            var result = listBuilder.CompareSequencesExcept();

            // Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item);
            }

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CompareSequencesConcatTest()
        {
            // Arrange
            var listBuilder = new EnumerableLearningSequences();

            // Act
            var result = listBuilder.CompareSequencesConcat();

            // Analyze
            foreach (var item in result)
            {
                TestContext.WriteLine(item);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Distinct().Count() <= result.Count());
        }
    }
}
