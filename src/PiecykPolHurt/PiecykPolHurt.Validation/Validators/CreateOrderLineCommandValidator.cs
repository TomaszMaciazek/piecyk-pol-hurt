using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;

namespace PiecykPolHurt.Validation.Validators
{
    public class CreateOrderLineCommandValidator : AbstractValidator<CreateOrderLineCommand>
    {
        public CreateOrderLineCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.PriceForOneItem)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            RuleFor(x => x.ItemsQuantity)
                .GreaterThan(0)
                .WithMessage("Product quantity must be greater than 0");

            RuleFor(x => x.ProductId)
                .MustAsync(async (id, token) =>
                {
                    return await unitOfWork.ProductRepository.GetAll().AnyAsync(x => x.Id == id);
                });
        }
    }
}
