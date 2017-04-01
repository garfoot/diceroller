using System;
using System.Threading.Tasks;
using DiceRoller.Web.Models.Room;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DiceRoller.Web.Controllers
{
    [Route("rooms")]
    public class RoomController : Controller
    {
        private readonly Lazy<IValidator<RoomJoinDetails>> _joinDetailsValidator;

        public RoomController(
            Lazy<IValidator<RoomJoinDetails>> joinDetailsValidator
            )
        {
            _joinDetailsValidator = joinDetailsValidator;
        }

        [Route("{roomId}")]
        public async Task<ActionResult> Index(RoomJoinDetails joinDetails)
        {
            ValidationResult validationResult = await _joinDetailsValidator.Value.ValidateAsync(joinDetails);
            if (!validationResult.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new IndexVm{RoomId = joinDetails.RoomId, Player = joinDetails.Name});
        }

        [HttpPost]
        [Route("joinRoom")]
        [ValidateAntiForgeryToken]
        public IActionResult JoinRoom(string roomId, string name)
        {
            return RedirectToAction(nameof(Index), new {roomId, name});
        }
    }
}