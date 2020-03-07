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

            }
        }

    }
}
