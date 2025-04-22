using AutoMapper;
using FlightProject.Domain.Entities;
using MyAirlines.ViewModels;
using FlightProject.Domain.Entities;
using MyAirlines.ViewModels;

namespace EmployeeAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
             CreateMap<Place, PlaceVM>();
            CreateMap<Flight, FlightsByAirportVM>();

            CreateMap<Flight, FlightVM>()
                .ForMember(dest => dest.DeparturePlace,
                opts => opts.MapFrom(
                src => src.Departure.Place.Name))
                .ForMember(dest => dest.ArrivalPlace,
                opts => opts.MapFrom(
                    src => src.Arrival.Place.Name
                ));

            // arrival/departure mappen naar een VM met place om de name enzo te krijgen
            CreateMap<DeparturePlace, DeparturePlaceVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DepartureId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Place.Name));

            // Map ArrivalPlace naar ArrivalVM
            CreateMap<ArrivalPlace, ArrivalPlaceVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ArrivalId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Place.Name));

            CreateMap<Meal, MealVM>();
            CreateMap<Class, ClassVM>();
        }
    }
}
