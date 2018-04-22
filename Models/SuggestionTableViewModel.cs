using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NomHadopi.Models.EF;

namespace NomHadopi.Models
{
    public class SuggestionTableViewModel
    {
        public IEnumerable<Suggestion> Suggestions { get; set; }
        public Dictionary<int, int> DictionaryUpvotes { get; set; }
        public int Page { get; set; }
        public int MaxPage { get; set; }

        public int PrevPage
        {
            get
            {
                var prevValue = Page  - 1 ;
                return prevValue > 0 ? prevValue : 1;
            }
        }

        public int NextPage
        {
            get
            {
                var nextValue = Page + 1;
                return nextValue <= MaxPage ? nextValue : MaxPage;
            }
        }
    }
}
