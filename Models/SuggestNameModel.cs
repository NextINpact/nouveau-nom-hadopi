using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NomHadopi.Models.EF;

namespace NomHadopi.Models
{
    public class SuggestNameModel
    {
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, MaxLength(255)]
        public string Proposition { get; set; }




        public SuggestionValidationState GetSuggestionValidationState(string userIp, NameSuggestDbContext dbContext)
        {
            if (dbContext.Suggestions.Any(x => x.SuggestionValue == Proposition))
                return SuggestionValidationState.SUGGESTIONEXISTS;

            if (dbContext.Suggestions.Count(x => x.UserIP == userIp) >= 3)
            {
                return SuggestionValidationState.IPALREADYUSED;
            }



            return SuggestionValidationState.VALID;

        }
        public Suggestion CreateSuggestion(string userIp)
        {
            return new Suggestion()
            {
                AuthorEmail = UserEmail,
                AuthorName = UserName,
                DateSuggested = DateTime.UtcNow,
                SuggestionValue = Proposition,
                UserIP = userIp
            };
        }


    }

    public enum SuggestionValidationState
    {
        VALID,
        IPALREADYUSED,
        SUGGESTIONEXISTS,
        SUGGESTIONUPVOTED,

    }

}
