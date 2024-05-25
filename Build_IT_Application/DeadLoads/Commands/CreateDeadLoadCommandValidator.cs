using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.DeadLoads.Commands
{
    public class CreateDeadLoadCommandValidator : AbstractValidator<CreateDeadLoadCommand>
    {
        public CreateDeadLoadCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
