using Application.DTO;
using FluentValidation;

namespace Application.Validation
{
    public class PaginationValidation: AbstractValidator<Pagination>
    {
        public PaginationValidation() 
        {
            RuleFor(x => x.Take)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Skip)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(0);
        }
    }
}
