using ForumQuestions.Controllers;
using ForumQuestions.Models;
using ForumQuestions.Repositories;
using System.Collections.Generic;
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
            questionController.AddForumQuestion("testQuestion1", "testQuestionBody1");

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
            questionController.AddForumQuestion("testQuestion1", "testQuestionBody1");
            Question q1 = questionController.FindQuestionByQuestionHeader("testQuestion1");
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

            Question quest = new Question
            {
                QuestionID = 3,
                QuestionHeader = "header",
                QuestionBody = "body"
            };
            // Act
            questionController.AddQuestion(quest);

            Question q = questionController.FindQuestionByID(3);

            // Assert
            Assert.Equal("header", q.QuestionHeader);
        }

        [Fact]
        public void TestFindQuestionByQuestionHeader()
        {
            // Arrange
            var repo = new FakeRepository();
            var questionController = new QuestionsController(repo);
            Question quest = new Question
            {
                QuestionID = 3,
                QuestionHeader = "header",
                QuestionBody = "body"
            };
            // Act
            questionController.AddQuestion(quest);

            Question q = questionController.FindQuestionByQuestionHeader("header");

            // Assert
            Assert.Equal(q.QuestionHeader, repo.Questions[3].QuestionHeader);
        }

        [Fact]
        public void TestQuestionsByType()
        {
            var repo = new FakeRepository();

            Question q1 = new Question
            {
                QuestionID = repo.Questions.Count,
                QuestionHeader = "testQuestion1",
                QuestionBody = "testQuestionBody1",
                Type = "KB"
            };
            Question q2 = new Question
            {
                QuestionID = repo.Questions.Count,
                QuestionHeader = "testQuestion2",
                QuestionBody = "testQuestionBody2",
                Type = "FQ"
            };
            repo.AddQuestion(q1);
            repo.AddQuestion(q2);

            List<Question> sortedQuestions = repo.FindQuestionsByType("KB");
            Assert.Equal("testQuestion1", sortedQuestions[0].QuestionHeader);
            var questionsController = new QuestionsController(repo);
            List<Question> remainingQuestions = questionsController.FindQuestionsByType("FQ");
            Assert.Equal("testQuestionBody2", remainingQuestions[0].QuestionBody);

        }

    }
}
