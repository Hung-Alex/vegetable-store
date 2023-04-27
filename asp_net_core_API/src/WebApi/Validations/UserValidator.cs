using FluentValidation;
using store_vegetable.Services.StoreVegetable;
using WebApi.Models;

namespace WebApi.Validations
{
    public class UserValidator:AbstractValidator<UserEditModel>
    {
        private readonly List<string> Roles= new List<string>() { "admin","user"};
        public UserValidator() 
        {
            RuleFor(x => x.Role)
                .NotNull()
                .NotEmpty()
                .WithErrorCode("Quyền của người dùng  không được để trống")
                .MustAsync(CheckRoleContainInRoles)
                .WithMessage("vui lòng lựa chọn đúng yêu cầu vai trò");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên tài khoản không được để trống");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("mật khẩu không được để trống")
                .MaximumLength(20)
                .WithMessage("mật khẩu không được quá 20 kí tự");
                
        }
        private async Task<bool> CheckRoleContainInRoles(UserEditModel model,string role,CancellationToken cancellationToken=default)
        {
            return Roles.Any(x => x == model.Role);
           
            
        }
    }
}
