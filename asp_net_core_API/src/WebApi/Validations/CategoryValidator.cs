using FluentValidation;
using WebApi.Models;

namespace WebApi.Validations
{
    public class CategoryValidator: AbstractValidator<CategoryEditModel>
    {
        public CategoryValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("tên chủ đề không được để trống")
                .MaximumLength(256).
                WithMessage("tên chủ đề tối đa 100 ký tự ");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Desciption không được để trống")
                .MaximumLength(1000).
                WithMessage("tên chủ đề tối đa 1000 ký tự ");
        }
    }
}
