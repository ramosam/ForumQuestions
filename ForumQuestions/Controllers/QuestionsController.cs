﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForumQuestions.Data;
using ForumQuestions.Models;
using ForumQuestions.Repositories;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace ForumQuestions.Controllers
{
    public class QuestionsController : Controller
    {

        private readonly IRepository context;
        public QuestionsController(IRepository r)
        {
            context = r;
        }

        // GET: Questions
        // Knowledge Base
        public IActionResult KnowledgeBase()
        {
            
            return View(context.FindQuestionsByType("KB"));
        }

        public IActionResult Forum()
        {
            return View(context.FindQuestionsByType("FQ"));
        }

        public ActionResult SearchByKeyword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KeywordResults(string userSearchString)
        {
            // Empty fqList
            List<Question> fqResults = new List<Question>();

            // Check if user entered anything
            if (userSearchString != null)
            {
                // Grab the userSting to process
                fqResults = context.GetQuestionsByKeyword(userSearchString.ToLower());
            }

            return View("KeywordResults", fqResults);
        }



        public IActionResult AddForumReply(string questionheader)
        {
            return View("AddForumReply", HttpUtility.HtmlDecode(questionheader));
            
        }

        public IActionResult AddForumQuestion()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Member, Admin")]
        public RedirectToActionResult AddForumQuestion(string questionHeader, string questionBody)
        {
            if (questionHeader == null || questionBody == null)
            {
                return RedirectToAction("AddForumQuestion");
            }
            else
            {

                Question newFQ = new Question
                {
                    Type = "FQ",
                    QuestionHeader = questionHeader,
                    QuestionBody = questionBody,

                };

                newFQ.FindKeywords();
                context.AddQuestion(newFQ);
            }


            return RedirectToAction("Forum");
        }



        [HttpPost]
        [Authorize(Roles = "Member, Admin")]
        public RedirectToActionResult AddForumReply(string questionheader, string replyBody)
        {
            Question fq = context.FindQuestionByQuestionHeader(questionheader);

            Reply reply = new Reply
            {
                QuestionPost = fq,
                ReplyBody = replyBody
            };

            context.AddReply(fq, reply);

            return RedirectToAction("Forum", context.FindQuestionsByType("FQ"));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public RedirectToActionResult AddQuestion(Question quest)
        {
            if (quest == null)
            {
                return RedirectToAction("Create");
            }
            else
            {

                quest.FindKeywords();
                context.AddQuestion(quest);
            }


            return RedirectToAction("KnowledgeBase");
        }

        // GET: Questions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = context.Questions
                .FirstOrDefault(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        

        // GET: Questions/Edit/5
        public IActionResult Edit(int id)
        {

            var question = context.Questions.Find(q => q.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("QuestionID,QuestionHeader,QuestionBody")] Question question)
        {
            if (id != question.QuestionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //context.Update(question);
                    //await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = context.Questions
                .FirstOrDefault(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var question = context.Questions.Find(q => q.QuestionID == id);
            context.Questions.Remove(question);
            
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return context.Questions.Any(e => e.QuestionID == id);
        }


        public IActionResult AddReply()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddReply(Question q, Reply r)
        {
            context.AddReply(q, r);
            return View();
        }

        public Question FindQuestionByID(int id)
        {
            Question q = context.FindQuestionByID(id);
            return q;
        }

        public Question FindQuestionByQuestionHeader(string header)
        {
            Question q = context.FindQuestionByQuestionHeader(header);
            return q;
        }

        public List<Question> FindQuestionsByType(string type)
        {
            List<Question> sortedQuestions = context.FindQuestionsByType(type);
            return sortedQuestions;
        }


    }
}
