using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.DataLayer.Common;
using PiecykPolHurt.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecykPolHurt.ApplicationLogic.Services
{
    public interface ISendPointService
    {
        Task<IList<SimpleSendPointDto>> GetAllSendPoints();
    }

    public class SendPointService : ISendPointService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SendPointService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<SimpleSendPointDto>> GetAllSendPoints() => await _unitOfWork.SendPointRepository
            .GetAll()
            .ProjectTo<SimpleSendPointDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
