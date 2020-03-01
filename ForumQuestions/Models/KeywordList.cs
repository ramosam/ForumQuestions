using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumQuestions.Models
{
    public class KeywordList
    {

        private List<string> usefulKeywords = new List<string>
        {
            "method", "code", "block", "statements", "definition", "argument", "parameter", "out", "ref", "initialize", "public", "static", "void", "declared", "access", "global", "ToString", "date"
        };

        private List<string> notUsefulKeywords = new List<string>
        {
            "no", "yes", "I", "you", "the", "a", "it", "its", "it's", "they", "them", "there", "their", "your", "yours", "is", "what", "how", "because", "and", "any", "more", "of", "have", "not", "tried", "when", "to", "or", "are", "in", "an", "by", "why", "be", "that", "but", "as", "I'm", "am", "me", "my", "mine", "you're", "can", "can't", "cannot", "do", "does", "doesn't", "don't",
        };

        public List<string> UsefulKeywords { get { return usefulKeywords; } }
        public List<string> NotUsefulKeywords { get { return notUsefulKeywords; } }

        public void AddUsefulKeyword(string newKeyword) => usefulKeywords.Add(newKeyword);
        public void AddNotUsefulKeyword(string newKeyword) => notUsefulKeywords.Add(newKeyword);

    }
}
