using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NomHadopi.Models.EF
{
    public class Suggestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuggestionID { get; set; }


        public string UserIP { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime DateSuggested { get; set; }
        public string SuggestionValue { get; set; }
        public ICollection<UpVote> UpVotes { get; set; }


    }
}