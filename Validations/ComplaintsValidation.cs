using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models;
using complainSystem.models.ComplainDto;
using complainSystem.models.Complains;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace complainSystem.Validations
{
    internal class AddComplainDtoValidation : AbstractValidator<Complain>
    {
        public AddComplainDtoValidation()
        {
            RuleFor(x => x.ComplainTitle).Length(3, 100).NotEmpty().WithMessage("Complain Title is required");
            RuleFor(x => x.ComplainDescription).Length(3, 500).NotEmpty().WithMessage("Complain Description is required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category Type is required");

        }

    }

    internal class UpdateComplainDtoValidation : AbstractValidator<UpdateComplainDto>
    {
        public UpdateComplainDtoValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Complain Id is required");
            RuleFor(x => x.ComplainTitle).Length(3, 100).NotEmpty().WithMessage("Complain Title is required");
            RuleFor(x => x.ComplainDescription).Length(3, 500).NotEmpty().WithMessage("Complain Description is required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category Type is required");

        }

    }







}