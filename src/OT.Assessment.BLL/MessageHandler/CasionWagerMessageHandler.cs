using AutoMapper;
using OT.Assessment.Infrastructure.DTO;
using OT.Assessment.Infrastructure.IRepository;
using OT.Assessment.Infrastructure.MessageHandler;
using OT.Assessment.Tester.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OT.Assessment.BLL.MessageHandler
{
    public class CasionWagerMessageHandler : IMessageHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CasionWagerMessageHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async void HandleMessage(byte[] message)
        {
            if (message.Length == 0)
                return;

            var addCasinoWagerDTO = JsonSerializer.Deserialize<AddCasionWagerDTO>(message);

            if (addCasinoWagerDTO.Duration < 0)
                return ;

            if (addCasinoWagerDTO.NumberOfBets < 1)
                return;

            var player = await _unitOfWork.Players.FindOne(x => x.Id.Equals(addCasinoWagerDTO.AccountId));
            if (player == null)
                return ;

            var provider = await _unitOfWork.Providers.FindOne(x => x.Name.Equals(addCasinoWagerDTO.Provider));
            if (provider == null)
                return;

            var games = await _unitOfWork.Games.FindOne(x => x.Name.Equals(addCasinoWagerDTO.GameName) && x.ProviderId.Equals(provider.Id));
            if (games == null)
                return;

            var casionWager = _mapper.Map<CasinoWager>(addCasinoWagerDTO);

            await _unitOfWork.Wagers.InsertOne(casionWager);

            _unitOfWork.Commit();

        }
    }
}
