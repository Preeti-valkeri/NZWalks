using FluentValidation;

namespace NZWalks.API.Validation
{
    public class LoginValidatior:AbstractValidator<DTO.LoginRequest>
    {
        public LoginValidatior()
        {
            RuleFor(x=>x.UserName).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty();
        }
    }
}
