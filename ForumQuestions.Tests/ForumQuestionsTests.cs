using ForumQuestions.Controllers;
using ForumQuestions.Models;
using ForumQuestions.Repositories;
using Xunit;

namespace ForumQuestions.Tests
{
    public class ForumQuestionsTests
    {
        [Fact]
        public void TestAddQuestion()
        {
            // Arrange
            var repo = new FakeRepository();
            var questionController = new QuestionsController(repo);

            // Act
            Question q1 = new Question
            {
                QuestionHeader = "testQuestion1",
                QuestionBody = "testQuestionBody1"
            };
            questionController.Create(q1);

            // Assert
            Assert.Equal(4, repo.Questions.Count);

        }

        [Fact]
        public void Test

    }
}
