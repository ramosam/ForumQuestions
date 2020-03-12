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
                var questions = context.Question.Include(q => q.Replies).ToList();
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
            // Create bucket for fqs
            List<Question> questionsByKeyword = new List<Question>();

            // Create lowercase version of user search string
            string lowerKeyword = keyword;
            // Separate into lowercase string array based on spaces
            // Note: Punctuation affects keyword
            string[] words = lowerKeyword.Split(' ');

            /* For each word in keyword, loop to see if there are any fqs
             * with keyword matches.
             * 
             * Next, make sure that there are no duplicate entries.
             * 
             * Then, if there is a match and is unique, add that fq to the
             * result bucket list.
             */
            // Get list of current questions

            List<Question> currentIQs = context.Question.ToList();
            
            // For each word
            for (int i = 0; i < words.Length; i++)
            {
                // For each question
                for (int q = 0; q < currentIQs.Count; q++)
                {
                    // Make sure that there are keywords
                    currentIQs[q].FindKeywords();
                    // Find match
                    if (currentIQs[q].Keywords.Contains(words[i]))
                    {
                        // Add to bucket
                        questionsByKeyword.Add(currentIQs[q]);
                    }
                }
            }

            List<Question> singleQByKeyword = RemoveDuplicates(questionsByKeyword);


            return singleQByKeyword;
        }

        private List<Question> RemoveDuplicates(List<Question> origFQList)
        {
            // Create shortList bucket
            List<Question> shortList = new List<Question>();
            // Create list for header comparison
            List<string> headerCheckList = new List<string>();
            // Loop through each question 
            for (int i = 0; i < origFQList.Count; i++)
            {
                // Check if unique
                if (!headerCheckList.Contains(origFQList[i].QuestionHeader))
                {
                    // Add to checkList
                    headerCheckList.Add(origFQList[i].QuestionHeader);
                    // Add to collection bucket
                    shortList.Add(origFQList[i]);
                }
            }
            // Return shortened list
            return shortList;
        }

        public void AddQuestion(Question q)
        {
            context.Question.Add(q);
            context.SaveChanges();
        }

        public void AddReply(Question q, Reply r)
        {
            
            context.Reply.Add(r);
            q.AddReply(r);
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

        public List<Question> FindQuestionsByType(string type)
        {
            List<Question> questions = context.Question.Include(q => q.Replies).ToList();
            List<Question> sortedQuestions = questions.FindAll(q => q.Type == type);
            return sortedQuestions;
        }
    }
}
