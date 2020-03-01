using ForumQuestions.Models;
using System.Collections.Generic;


namespace ForumQuestions.Repositories
{
    public interface IRepository
    {

        List<Question> Questions { get; }

        List<Reply> Replies { get; }

        List<Question> GetQuestionsByKeyword(string keyword);

        void AddQuestion(Question q);

        void AddReply(Question q, Reply r);

        Question FindQuestionByQuestionHeader(string questionHeader);

        Question FindQuestionByID(int id);

    }
}
