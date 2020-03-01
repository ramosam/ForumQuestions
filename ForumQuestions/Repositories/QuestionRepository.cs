using ForumQuestions.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ForumQuestions.Data;

namespace ForumQuestions.Repositories
{
    public class QuestionRepository : IRepository
    {

        private ApplicationDbContext context;

        public QuestionRepository(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }


        public List<Question> Questions => throw new NotImplementedException();

        public List<Reply> Replies => throw new NotImplementedException();

        public List<Question> GetQuestionsByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public void AddQuestion(Question q)
        {
            throw new NotImplementedException();
        }

        public void AddReply(Question q, Reply r)
        {
            throw new NotImplementedException();
        }

        public Question FindQuestionByQuestionHeader(string questionHeader)
        {
            throw new NotImplementedException();
        }

        public Question FindQuestionByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
