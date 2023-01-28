using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecykPolHurt.Validation.Validators
{
    public class UpdateSendPointCommandValidator : AbstractValidator<UpdateSendPointCommand>
    {
        public UpdateSendPointCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id cannot be empty")
                .MustAsync(async (id, token) =>
                {
                    return await unitOfWork.SendPointRepository.GetAll().AnyAsync(x => x.Id == id);
                })
                .WithMessage("Send Point with given id does not exist");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code cannot be empty");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");

            RuleFor(x => x)
            .MustAsync(async (cmd, token) => {
                    return !await unitOfWork.SendPointRepository.GetAll().AnyAsync(x => 
                        x.Id == cmd.Id && x.Code.ToUpper() == cmd.Code.ToUpper() && x.Name != cmd.Name
                    );
                })
                .WithMessage("Code or name is not unique");
        }
    }
}
