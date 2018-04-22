using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NomHadopi.Models.EF;

namespace NomHadopi.Extensions
{
    public static class SuggestExtensions
    {
        public static Dictionary<int, int> GetUpvotesDictionary(this IEnumerable<int> suggestionsId, NameSuggestDbContext dbContext)
        {
            if (!suggestionsId.Any())
                return new Dictionary<int, int>();

            var dbConnection = dbContext.Database.GetDbConnection();
            var results = dbConnection.Query("Select SuggestionID as Id, COUNT(UpVoteID) as Count  FROM UpVotes WHERE SuggestionID IN @suggestionsID  GROUP BY SuggestionID", new { suggestionsId }).ToDictionary(row => (int)row.Id, row => (int)row.Count);

            return results;
        }

        public static Dictionary<int, int> GetUpvotesDictionary(this IEnumerable<Suggestion> suggestions, NameSuggestDbContext dbContext)
        {
            var ids = suggestions.Select(x => x.SuggestionID);
            return GetUpvotesDictionary(ids, dbContext);
        }
    }
}
