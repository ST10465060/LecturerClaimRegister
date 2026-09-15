using LecturerClaimRegister.Models;
using Microsoft.AspNetCore.Mvc;

namespace LecturerClaimRegister.Controllers
{
    /// <summary>
    /// Handles the MVC workflow for viewing and creating lecturer claims.
    /// Acts as the "C" in MVC - it receives requests, works with the model
    /// and decides which view to return.
    /// </summary>
    public class ClaimsController : Controller
    {
        /// <summary>
        /// GET: /Claims
        /// Displays every claim currently held in the in-memory store.
        /// </summary>
        public IActionResult Index()
        {
            return View(ClaimStore.GetAll());
        }

        /// <summary>
        /// GET: /Claims/Create
        /// Returns an empty form. A new Claim is passed in so the tag helpers
        /// can bind to it and pick up the default Draft status.
        /// </summary>
        public IActionResult Create()
        {
            return View(new Claim());
        }

        /// <summary>
        /// POST: /Claims/Create
        /// Validates the submitted claim, stores it and redirects to the list.
        /// Invalid input falls through and redisplays the form with messages.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Claim claim)
        {
            if (!ModelState.IsValid)
            {
                // Return the populated model so the user does not retype everything.
                return View(claim);
            }

            // New claims always start in Draft, regardless of posted data.
            claim.Status = "Draft";
            ClaimStore.AddClaim(claim);

            return RedirectToAction(nameof(Index));
        }
    }
}
