using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;

namespace PiecykPolHurt.Validation.Validators
{
    public class CreateSendPointCommandValidator : AbstractValidator<CreateSendPointCommand>
    {
        public CreateSendPointCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code cannot be empty");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");

            RuleFor(x => x)
                .MustAsync(async (cmd, token) => {
                    return !await unitOfWork.SendPointRepository.GetAll().AnyAsync(x => x.Code.ToUpper() == cmd.Code.ToUpper() && x.Name != cmd.Name);
                })
                .WithMessage("Code or name is not unique");
        }
    }
}
