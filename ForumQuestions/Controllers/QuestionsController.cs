using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForumQuestions.Data;
using ForumQuestions.Models;
using ForumQuestions.Repositories;

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
        public IActionResult Index()
        {
            return View(context.Questions.ToList());
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

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuestion([Bind("QuestionID,QuestionHeader,QuestionBody")] Question question)
        {
            if (ModelState.IsValid)
            {
                context.AddQuestion(question);
                //context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
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

    }
}
