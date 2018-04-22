using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NomHadopi.Models.EF
{
    public class UpVote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UpVoteID { get; set; }

        [ForeignKey("SuggestionId")]
        public Suggestion Suggestion { get; set; }


        public int SuggestionId { get; set; }
        public string IpUser { get; set; }

        public static bool TryAdd(NameSuggestDbContext dbContext, int suggestId, string userIp)
        {
            var isVoted = dbContext.UpVotes.Any(x => x.IpUser == userIp && x.Suggestion.SuggestionID == suggestId);
            if (isVoted)
            {
                return false;
            }

            UpVote upVote = new UpVote() { IpUser = userIp, SuggestionId = suggestId };
            dbContext.UpVotes.Add(upVote);
            dbContext.SaveChanges();
            return true;
        }

        public static bool TryAdd(NameSuggestDbContext dbContext, string suggestionValue, string userIp)
        {
            var suggestion = dbContext.Suggestions.FirstOrDefault(x => x.SuggestionValue == suggestionValue);
            return suggestion != null && TryAdd(dbContext, suggestion.SuggestionID, userIp);
        }


    }
}