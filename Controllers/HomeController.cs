using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NomHadopi.Extensions;
using NomHadopi.Models;
using NomHadopi.Models.EF;

namespace NomHadopi.Controllers
{
    public class HomeController : Controller
    {
        public NameSuggestDbContext DbContext { get; set; }

        public HomeController(NameSuggestDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("post-suggestion"), ValidateAntiForgeryToken]
        public IActionResult SubmitResults([FromForm]SuggestNameModel model)
        {
            var sugState =
                model.GetSuggestionValidationState(Request.HttpContext.Connection.RemoteIpAddress.ToString(), DbContext);
            return ModelState.IsValid && sugState == SuggestionValidationState.VALID
                ? OkSubmit(model)
                : FailSubmit(sugState, model);
        }

        private IActionResult OkSubmit(SuggestNameModel model)
        {
            var suggestion = model.CreateSuggestion(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            DbContext.Suggestions.Add(suggestion);
            DbContext.SaveChanges();
            return View("SubmitResults");
        }

        private IActionResult FailSubmit(SuggestionValidationState sugState, SuggestNameModel model)
        {

            if (sugState == SuggestionValidationState.SUGGESTIONEXISTS)
            {
                if (UpVote.TryAdd(DbContext, model.Proposition,
                    Request.HttpContext.Connection.RemoteIpAddress.ToString()))
                {
                    sugState = SuggestionValidationState.SUGGESTIONUPVOTED;
                }
            }
            ErrorSuggestModel errorSuggestModel = new ErrorSuggestModel(sugState);
            return View("ErrorSubmit", errorSuggestModel);
        }
        [HttpGet("suggestions")]
        public async Task<IActionResult> GetSuggestions(int page = 1)
        {
            if (page < 1)
                return RedirectToAction("GetSuggestions", new { page = 1 });
            const int nbPerPage = 20;
            var total = await DbContext.Suggestions.CountAsync();
            var maxPage = Convert.ToInt32(Math.Ceiling(total / (double)nbPerPage));

            if (page > maxPage && maxPage > 0)
                return RedirectToAction("GetSuggestions", new { page = maxPage });

            var skipValue = (page - 1) * nbPerPage;
            var results = await DbContext.Suggestions.OrderByDescending(x => x.DateSuggested).Skip(skipValue).Take(nbPerPage).ToListAsync();


            var upvotesDictionary = results.GetUpvotesDictionary(dbContext: DbContext);
            SuggestionTableViewModel viewModel =
                new SuggestionTableViewModel() { DictionaryUpvotes = upvotesDictionary, Suggestions = results, Page = page, MaxPage = maxPage };
            return View(viewModel);
        }

        [HttpPost("upvote")]
        public async Task<IActionResult> DoUpvote(int id)
        {
            if (UpVote.TryAdd(DbContext, id, Request.HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                int upvotes = await DbContext.UpVotes.CountAsync(x => x.SuggestionId == id);
                return Ok(new { upvotes });
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
