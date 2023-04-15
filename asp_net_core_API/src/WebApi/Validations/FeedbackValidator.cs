using FluentValidation;
using WebApi.Models;

namespace WebApi.Validations
{
    public class FeedbackValidator: AbstractValidator<FeedbackEditModel>
    {
        public FeedbackValidator() {

            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("Khong duoc de tron tieu de")
                .MaximumLength(100)
                .WithMessage("tieu de lhong dai qua 100 ki tu");
            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Khong duoc de tron Description")
                .MaximumLength(500)
                .WithMessage("Description lhong dai qua 500 ki tu");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Khong duoc de tron Email")
                .MaximumLength(100)
                .WithMessage("Email lhong dai qua 100 ki tu");
            RuleFor(x => x.Meta).NotEmpty()
                .WithMessage("Khong duoc de tron Meta")
                .MaximumLength(1000)
                .WithMessage("Meta lhong dai qua 1000 ki tu");

        }
    }
}
