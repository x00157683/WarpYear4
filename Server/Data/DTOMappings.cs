using AutoMapper;
using Shared.Models;

namespace Server.Data
{
    internal sealed class DTOMappings : Profile
    {


        public DTOMappings()
        {
            CreateMap<Car,CarDTO>().ReverseMap();
            CreateMap<Booking, BookingDTO>().ReverseMap();
        }
    }
}
