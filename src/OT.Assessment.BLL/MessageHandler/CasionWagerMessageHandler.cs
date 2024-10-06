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

        public async Task HandleMessage(byte[] message)
        {
            if (message.Length == 0)
                return;

            var addCasinoWagerDTOs = JsonSerializer.Deserialize<List<AddCasionWagerDTO>>(message);

            foreach (var item in addCasinoWagerDTOs)
            {
                try
                {
                    var casionWager = _mapper.Map<CasinoWager>(item);

                    await _unitOfWork.Wagers.InsertOne(casionWager);
                    await _unitOfWork.Commit();
                }
                catch (Exception e) { 
                
                    continue;
                }
                
            }
            //await _unitOfWork.Commit();

        }
    }
}
