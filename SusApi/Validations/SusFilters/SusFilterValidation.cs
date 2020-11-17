using FluentValidation;
using SusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SusApi.Validations.SusFilters
{
    public class SusFilterValidation : AbstractValidator<SusFilter>
    {
        public SusFilterValidation()
        {
            ModuloValidate();
            AnosValidate();
            UfsValidate();
        }


        protected void ModuloValidate() {

            RuleFor(s => s.Modulo)
                .NotEmpty()
                .NotNull();
        }
        protected void AnosValidate() {
            RuleFor(s => s.Anos)
                .NotNull()
                .NotEmpty();
        }

        protected void UfsValidate() {

            RuleFor(s => s.Ufs)
                    .NotNull()
                    .NotEmpty();

        }
    }
}
