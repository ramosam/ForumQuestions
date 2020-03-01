﻿using ForumQuestions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumQuestions.Repositories
{
    public class FakeRepository : IRepository
    {
        private List<Question> questions = new List<Question>();
        private List<Reply> replies = new List<Reply>();

        public FakeRepository()
        {
            if (questions.Count == 0) { AddTestData(); }
        }

        private void AddTestData()
        {
            throw new NotImplementedException();
        }

        public List<Question> Questions => questions;

        public List<Reply> Replies => replies;

        public void AddQuestion(Question q) => questions.Add(q);
        

        public void AddReply(Question q, Reply r)
        {
            q.AddReply(r);
            replies.Add(r);
        }

        public Question FindForumQuestionByID(int id)
        {
            Question q = questions.Find(fq => fq.QuestionID == id);
            if (q == null)
            {
                q = new Question
                {
                    QuestionHeader = "defaultQuestionHeader",
                    QuestionBody = "defaultQuestionBody"
                };
            }
            return q;
        }

        public Question FindForumQuestionByQuestionHeader(string questionHeader)
        {
            Question q = questions.Find(fq => fq.QuestionHeader == questionHeader);
            if (q == null)
            {
                q = new Question
                {
                    QuestionHeader = "defaultQuestionHeader",
                    QuestionBody = "defaultQuestionBody"
                };
            }
            return q;
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
            // For each word
            for (int i = 0; i < words.Length; i++)
            {
                // For each question
                for (int q = 0; q < questions.Count; q++)
                {
                    // Find match
                    if (questions[q].Keywords.Contains(words[i]))
                    {
                        // Add to bucket
                        questionsByKeyword.Add(questions[q]);
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
    }
}