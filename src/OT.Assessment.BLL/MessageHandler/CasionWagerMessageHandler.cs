using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using OT.Assessment.Infrastructure.DTO;
using OT.Assessment.Infrastructure.IRepository;
using OT.Assessment.Infrastructure.MessageHandler;
using OT.Assessment.Tester.Infrastructure;
using RabbitMQ.Client.Events;
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
            //var message = args.Body.ToArray();

            if (message.Length == 0)
                return;

            var addCasinoWagerDTOs = JsonSerializer.Deserialize<List<AddCasionWagerDTO>>(message);

            foreach (var item in addCasinoWagerDTOs)
            {
                try
                {
                    var casionWager = _mapper.Map<CasinoWager>(item);

                    var response = await _unitOfWork.Wagers.FindOne(x => x.WagerId.Equals(casionWager.WagerId));
                    if (response == null)
                    {
                        await _unitOfWork.Wagers.InsertOne(casionWager);
                        await _unitOfWork.Commit();
                    }
                }
                catch (Exception e) { 
                
                    continue;
                }
                
            }
            Console.WriteLine("done");
            //channel.BasicAck(args.DeliveryTag, false)


        }
    }
}
