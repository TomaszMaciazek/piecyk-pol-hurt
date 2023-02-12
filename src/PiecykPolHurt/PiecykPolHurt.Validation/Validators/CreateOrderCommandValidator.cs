using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;

namespace PiecykPolHurt.Validation.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.RequestedReceptionDate).NotEmpty();

            RuleFor(x => x.SendPointId)
                .MustAsync(async (id, token) =>
                {
                    return await unitOfWork.SendPointRepository.GetAll().AnyAsync(x => x.Id == id, cancellationToken: token);
                });

            RuleForEach(x => x.Lines)
                .SetValidator(new CreateOrderLineCommandValidator(unitOfWork));
        }
    }
}
