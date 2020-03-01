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
            questionController.AddQuestion(q1);

            // Assert
            Assert.Equal(4, repo.Questions.Count);

        }

        [Fact]
        public void TestAddReply()
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
            questionController.AddQuestion(q1);
            Reply r1 = new Reply
            {
                QuestionPost = q1,
                ReplyBody = "testReply1"
            };
            questionController.AddReply(q1, r1);

            // Assert
            Assert.Equal(4, repo.Replies.Count);

        }

        [Fact]
        public void TestFindQuestionByID()
        {
            // Arrange
            var repo = new FakeRepository();
            var questionController = new QuestionsController(repo);

            // Act
            Question q1 = new Question
            {
                QuestionID = repo.Questions.Count,
                QuestionHeader = "testQuestion1",
                QuestionBody = "testQuestionBody1"
            };
            questionController.AddQuestion(q1);

            Question q = questionController.FindQuestionByID(3);

            // Assert
            Assert.Equal("testQuestion1", q.QuestionHeader);
        }

        [Fact]
        public void TestFindQuestionByQuestionHeader()
        {
            // Arrange
            var repo = new FakeRepository();
            var questionController = new QuestionsController(repo);

            // Act
            Question q1 = new Question
            {
                QuestionID = repo.Questions.Count,
                QuestionHeader = "testQuestion1",
                QuestionBody = "testQuestionBody1"
            };
            questionController.AddQuestion(q1);

            Question q = questionController.FindQuestionByQuestionHeader("questionHeader1");

            // Assert
            Assert.Equal(q.QuestionHeader, repo.Questions[0].QuestionHeader);
        }

    }
}
