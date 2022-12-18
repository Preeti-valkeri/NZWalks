using FluentValidation;


namespace NZWalks.API.Validation
{
    public class AddRegionRequestValidator:AbstractValidator<DTO.AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x => x.Lat).GreaterThan(0);
            RuleFor(x=>x.Area).GreaterThan(0);
            RuleFor(x => x.Long).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
