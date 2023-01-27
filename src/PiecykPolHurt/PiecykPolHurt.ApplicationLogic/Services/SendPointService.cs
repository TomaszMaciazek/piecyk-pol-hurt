using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.ApplicationLogic.Extensions;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Entities;
using PiecykPolHurt.Model.Enums;
using PiecykPolHurt.Model.Queries;

namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface ISendPointService
    {
        Task<bool> CreateSendPointAsync(CreateSendPointCommand command);
        Task<bool> DeleteSendPointAsync(int id);
        Task<IList<SimpleSendPointDto>> GetAllSendPointsAsync(bool onlyActive = true);
        Task<PaginatedList<SendPointListItemDto>> GetSendPointsAsync(SendPointQuery query);
        Task<SendPointDto> GetSendPointByIdAsync(int id);
        Task<bool> UpdateSendPointAsync(UpdateSendPointCommand command);
    }

    public class SendPointService : ISendPointService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateSendPointCommand> _createValidator;
        private readonly IValidator<UpdateSendPointCommand> _updateValidator;

        public SendPointService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateSendPointCommand> createValidator, IValidator<UpdateSendPointCommand> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PaginatedList<SendPointListItemDto>> GetSendPointsAsync(SendPointQuery query)
        {
            var sendPoints = _unitOfWork.SendPointRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrEmpty(query.Name))
            {
                sendPoints = sendPoints.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(query.Code))
            {
                sendPoints = sendPoints.Where(x => x.Code.ToLower().Contains(query.Code.ToLower()));
            }

            if (query.IsActive.HasValue)
            {
                sendPoints = sendPoints.Where(x => x.IsActive == query.IsActive.Value);
            }

            sendPoints = query.SortOption switch
            {
                SendPointSortOption.NameAsc => sendPoints.OrderBy(x => x.Name),
                SendPointSortOption.NameDesc => sendPoints.OrderByDescending(x => x.Name),
                SendPointSortOption.CodeAsc => sendPoints.OrderBy(x => x.Code),
                SendPointSortOption.CodeDesc => sendPoints.OrderByDescending(x => x.Code),
                _ => sendPoints.OrderBy(x => x.Code)
            };


            return await sendPoints.ProjectTo<SendPointListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<IList<SimpleSendPointDto>> GetAllSendPointsAsync(bool onlyActive = true)
        {
            var sendPoints = _unitOfWork.SendPointRepository.GetAll().AsNoTracking();

            if (onlyActive)
            {
                sendPoints = sendPoints.Where(x => x.IsActive);
            }

            return await sendPoints.ProjectTo<SimpleSendPointDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<SendPointDto> GetSendPointByIdAsync(int id) 
            => await _unitOfWork.SendPointRepository.GetById(id)
            .AsNoTracking()
            .ProjectTo<SendPointDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();


        public async Task<bool> CreateSendPointAsync(CreateSendPointCommand command)
        {
            var validationResult = await _createValidator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var sendPoint = _mapper.Map<SendPoint>(command);
                _unitOfWork.SendPointRepository.Add(sendPoint);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSendPointAsync(UpdateSendPointCommand command)
        {
            var validationResult = await _updateValidator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var sendPoint = await _unitOfWork.SendPointRepository.GetById(command.Id).FirstOrDefaultAsync();
                sendPoint = _mapper.Map(command, sendPoint);
                _unitOfWork.SendPointRepository.Update(sendPoint);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<bool> DeleteSendPointAsync(int id)
        {
            if (!await _unitOfWork.SendPointRepository.WasSendPointUsedInOrder(id))
            {
                _unitOfWork.SendPointRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
