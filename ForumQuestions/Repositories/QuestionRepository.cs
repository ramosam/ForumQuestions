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


        public List<Question> Questions
        {
            get
            {
                var questions = context.Question.ToList();
                return questions;
            }
        }

        public List<Reply> Replies
        {
            get
            {
                var replies = context.Reply.ToList();
                return replies;
            }
        }


        public List<Question> GetQuestionsByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public void AddQuestion(Question q)
        {
            context.Question.Add(q);
            context.SaveChanges();
        }

        public void AddReply(Question q, Reply r)
        {
            r.QuestionPost = q;
            context.Reply.Add(r);
            context.Question.Update(q);
            context.SaveChanges();
        }

        public Question FindQuestionByQuestionHeader(string questionHeader)
        {
            Question q = context.Question.FirstOrDefault(quest => quest.QuestionHeader == questionHeader);
            return q;
        }

        public Question FindQuestionByID(int id)
        {
            Question q = context.Question.FirstOrDefault(quest => quest.QuestionID == id);
            return q;
        }
    }
}
