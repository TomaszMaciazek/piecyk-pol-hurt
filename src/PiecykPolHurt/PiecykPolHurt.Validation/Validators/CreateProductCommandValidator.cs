using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;

namespace PiecykPolHurt.Validation.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code cannot be empty");

            RuleFor(x => x)
                .MustAsync(async (cmd, token) =>
                {
                    return !await unitOfWork.ProductRepository.GetAll()
                        .AnyAsync(x => x.Code.ToUpper() == cmd.Code.ToUpper() || x.Name == cmd.Name);
                })
                .WithMessage("Code or name is not unique");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0.01m)
                .WithMessage("Price must be equal or greater than 0.01");
        }
    }
}
