using Build_IT_WebApplication.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public class CalculateBeamCommandValidator : AbstractValidator<CalculateBeamCommand>
    {
        public CalculateBeamCommandValidator()
        {
            RuleFor(v => v.BeamResource)
                .NotEmpty().WithMessage("Beam needs to be provided.");
        }
    }
}
