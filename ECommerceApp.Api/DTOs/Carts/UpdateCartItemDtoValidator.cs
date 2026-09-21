using FluentValidation;

namespace ECommerceApp.Api.DTOs
{
    public class UpdateCartItemDtoValidator : AbstractValidator<UpdateItemCartDto>
    {
        public UpdateCartItemDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}

