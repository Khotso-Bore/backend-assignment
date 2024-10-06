using AutoMapper;
using OT.Assessment.Infrastructure.DTO;
using OT.Assessment.Tester.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddCasionWagerDTO, CasinoWager>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WagerId));
        }
    }
}
