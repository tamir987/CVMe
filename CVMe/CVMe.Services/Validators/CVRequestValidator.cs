using CVMe.DataObjects.Requests;
using FluentValidation;

namespace CVMe.Services.Validators
{
    public interface ICVRequestValidator : IValidator<CVRequest> { }

    public class CVRequestValidator : AbstractValidator<CVRequest>, ICVRequestValidator
    {
        public CVRequestValidator()
        {
            RuleFor(cvRequest => cvRequest.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(cvRequest => cvRequest.TemplateId)
                .GreaterThan(0);
        }
    }
}
