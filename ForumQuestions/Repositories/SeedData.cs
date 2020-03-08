using ForumQuestions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ForumQuestions.Models;

namespace ForumQuestions.Repositories
{
    public class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Question.Any())
            {

                Question fq1 = new Question
                {
                    QuestionHeader = "Year, Month, and Day parameters " +
               "describe an un-representable DateTime Exception",
                    QuestionBody = "I'm getting this error when I try to " +
               "print a DateTime object on my website.",
                };

                Question fq2 = new Question
                {
                    QuestionHeader = "Null pointer references when the .cshtml page tries to render Comment objects.",
                    QuestionBody = "The debugger shows that there is a comment object, but the user name is Null.",
                };
                // Populate keywords
                fq1.FindKeywords();
                fq2.FindKeywords();
                // Moving adding and updating Questions and users after replies are accounted for.
                context.Question.Add(fq1);
                context.Question.Add(fq2);


                // Make Replies
                Reply r1 = new Reply()
                {
                    ReplyBody = "Have you tried adding a .ToString?",
                };
                Reply r2 = new Reply()
                {
                    ReplyBody = "Try adding a date string formatter.  I used this " +
   " to get year-month-day:  dateObj.ToString('yyyy - MM - dd')",
                };
                // Find keywords in replies
                r1.FindKeywords();
                r2.FindKeywords();
                // Add replies to context
                context.Reply.Add(r1);
                context.Reply.Add(r2);

                // Add reply to ForumQ
                fq1.AddReply(r1);
                fq1.AddReply(r2);

                // Make FAQs/Kbs
                Question faq1 = new Question
                {
                    // Title
                    QuestionHeader = "What is a method and why do I have to call it?",
                    // Body
                    QuestionBody = "A method is a code block that contains a series " +
                "of statements. A program causes the statements to be executed " +
                "by calling the method and specifying any required method " +
                "arguments. In C#, every executed instruction is performed in " +
                "the context of a method. The Main method is the entry point " +
                "for every C# application and it's called by the common " +
                "language runtime (CLR) when the program is started. " +
                "The method definition specifies the names and types of any" +
                " parameters that are required. When calling code calls the" +
                " method, it provides concrete values called arguments for each" +
                " parameter. The arguments must be compatible with the " +
                "parameter type but the argument name (if any) used in the " +
                "calling code doesn't have to be the same as the parameter " +
                "named defined in the method. ",
                };


                Question faq2 = new Question
                {
                    QuestionHeader = "What is the difference between ref & out " +
                "parameters?",
                    QuestionBody = "An argument passed as ref must be initialized" +
                " before passing to the method whereas the out parameter needs not " +
                "to be initialized before passing to a method. ",
                };


                Question faq3 = new Question
                {
                    QuestionHeader = "What is the difference between public, " +
                "static, and void?",
                    QuestionBody = "Public declared variables or methods are " +
                "accessible anywhere in the application. Static declared " +
                "variables or methods are globally accessible without creating " +
                "an instance of the class. Static member are by default not" +
                " globally accessible it depends upon the type of access " +
                "modified used. The compiler stores the address of the method " +
                "as the entry point and uses this information to begin" +
                " execution before any objects are created. And Void is a " +
                "type modifier that states that the method or variable does " +
                "not return any value. ",
                };
                // Add keywords
                faq1.FindKeywords();
                faq2.FindKeywords();
                faq3.FindKeywords();
                // Add FAQs to context
                context.Question.Add(faq1);
                context.Question.Add(faq2);
                context.Question.Add(faq3);

                context.SaveChanges();
            }
        }

    }
}
