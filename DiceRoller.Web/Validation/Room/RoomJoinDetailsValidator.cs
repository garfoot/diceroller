using System;
using DiceRoller.Web.Models.Room;
using FluentValidation;

namespace DiceRoller.Web.Validation.Room
{
    public class RoomJoinDetailsValidator : AbstractValidator<RoomJoinDetails>
    {
        public RoomJoinDetailsValidator()
        {
            RuleFor(i => i.RoomId)
                .NotEmpty();

            RuleFor(i => i.Name)
                .NotEmpty();
        }
    }
}