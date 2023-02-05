using FluentValidation;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;

namespace PiecykPolHurt.Validation.Validators;

public class UpdateProductSendPointCommandValidator : AbstractValidator<UpdateProductSendPointCommand>
{
    public UpdateProductSendPointCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.ForDate)
            .NotEmpty()
            .WithMessage("Date cannot be empty");
        
        RuleFor(x => x.AvailableQuantity)
            .NotEmpty()
            .WithMessage("Quantity cannot be empty");
        
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Must(x => x > -1)
            .WithMessage("Product must be specified");
        
        RuleFor(x => x.SendPointId)
            .NotEmpty()
            .Must(x => x > -1)
            .WithMessage("SendPoint must be specified");
        
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(x => x > -1)
            .WithMessage("SendPoint must be specified");
    }
}