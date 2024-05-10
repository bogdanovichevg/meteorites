using Application.DTO;
using Domain.Consts;
using FluentValidation;

namespace Application.Validation
{
    public class GetMeteoritesInfoValidation: AbstractValidator<ReqFilterMeteorites>
    {
        public GetMeteoritesInfoValidation()
        {
            Include(new PaginationValidation { });

            RuleFor(meteorite => meteorite.FromYear)
                .LessThanOrEqualTo(2100)
                .GreaterThanOrEqualTo(0);

            RuleFor(meteorite => meteorite.ToYear)
                .LessThanOrEqualTo(2100)
                .GreaterThanOrEqualTo(0);

            RuleFor(meteorite => meteorite.ClassName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(100);

            RuleFor(meteorite => meteorite.MeteoriteName)
                .MaximumLength(100);

            RuleFor(meteorite => meteorite.SortField)
                .NotNull()
                .NotEmpty()
                .IsEnumName(typeof(SortableField), caseSensitive: false)
                .WithMessage("'Sort Field' must contains 'Mass' or 'Count' or 'Year'.");

        }
    }
}
