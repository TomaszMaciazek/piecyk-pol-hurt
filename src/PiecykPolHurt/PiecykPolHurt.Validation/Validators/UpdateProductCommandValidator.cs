using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;

namespace PiecykPolHurt.Validation.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty");

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
                        .AnyAsync(x => x.Id != cmd.Id && (x.Code.ToUpper() == cmd.Code.ToUpper() || x.Name == cmd.Name));
                })
                .WithMessage("Code or name is not unique");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0.01m)
                .WithMessage("Price must be equal or greater than 0.01");
        }
    }
}
