using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NomHadopi.Models
{
    //public class ErrorSuggestModel
    //{
    //    public string Message { get; protected set; }

    //    public ErrorSuggestModel(SuggestionValidationState state)
    //    {
    //        switch (state)
    //        {
    //            case SuggestionValidationState.IPALREADYUSED:
    //                Message = "Nous ne pouvons ajouter votre proposition car vous avez atteint la limite de propositions possibles.<br/><a href='/suggestions'> Néanmoins, vous pouvez voter pour d'autres propositions </a>";
    //                break;
    //            case SuggestionValidationState.SUGGESTIONEXISTS:
    //                Message = "Cette proposition existe déjà dans notre base de donnée. Nous avons ajouté un Upvote à cette proposition.";
    //                break;

    //        }
    //    }
    //}

    public class ErrorSuggestModel
    {
        public SuggestionValidationState State { get; protected set; }

        public ErrorSuggestModel(SuggestionValidationState state)
        {
            State = state;
        }
    }
}
