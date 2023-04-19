using FluentValidation;
using WebApi.Models;

namespace WebApi.Validations
{
    public class FoodValidator:AbstractValidator<FoodEditModel>
    {
        public FoodValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên sản phẩm không được để trống")
                .MaximumLength(100)
                .WithMessage("tên sản phẩm không được quá 100 kí tự");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Tên desciption  không được để trống")
                .MaximumLength(500)
                .WithMessage(" desciption không được quá 500 kí tự");

            RuleFor(x=>x.Unit)
                 .NotEmpty()
                .WithMessage("unit không được để trống")
                .MaximumLength(50)
                .WithMessage("unit không được quá 100 kí tự");
            RuleFor(x => x.Weight)
                .NotEmpty()
                .WithMessage("trọng lượng của sản phẩm không được để trống");
            RuleFor(x => x.Price)
               .NotEmpty()
               .WithMessage("Price của sản phẩm không được để trống");
            RuleFor(x => x.CategoriesId)
              .NotEmpty()
              .WithMessage("Categories của sản phẩm không được để trống");


        }
    }
}
