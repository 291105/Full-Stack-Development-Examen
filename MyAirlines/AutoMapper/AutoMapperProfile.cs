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

            // Flight mappen naar FlightVM
            CreateMap<Flight, FlightVM>()
    .ForMember(dest => dest.DeparturePlace,
        opt => opt.MapFrom(src => src.DepartureAirport.City))
    .ForMember(dest => dest.ArrivalPlace,
        opt => opt.MapFrom(src => src.ArrivalAirport.City))
    .ForMember(dest => dest.AircraftId,
        opt => opt.MapFrom(src => src.AircraftAircraftId))
    // Combineer de datum (DateOnly) met de tijd (TimeOnly)
    .ForMember(dest => dest.DepartureTime,
        opt => opt.MapFrom(src => src.DepartureTime))  // Combineer datum en tijd
    .ForMember(dest => dest.ArrivalTime,
        opt => opt.MapFrom(src => src.ArrivalTime))  // Combineer datum en tijd
                                                     // Conversie van Duration, als er een waarde is
    .ForMember(dest => dest.Duration,
        opt => opt.MapFrom(src => src.Duration.HasValue
            ? TimeSpan.FromHours(src.Duration.Value.Hour) + TimeSpan.FromMinutes(src.Duration.Value.Minute)
            : (TimeSpan?)null));




            // Flight mappen naar LayoverVM
            CreateMap<Flight, LayoverVM>()
                .ForMember(dest => dest.DepartureAirport,
                    opt => opt.MapFrom(src => src.DepartureAirport.Name))
                .ForMember(dest => dest.ArrivalAirport,
                    opt => opt.MapFrom(src => src.ArrivalAirport.Name));


            // Airport mappen naar AirportVM (voor drop-downs)
            CreateMap<Airport, AirportVM>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.AirportId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country,
                    opt => opt.MapFrom(src => src.Country));

            // Meal naar MealVM
            CreateMap<Meal, MealVM>()
                .ForMember(dest => dest.MealId,
                    opt => opt.MapFrom(src => src.MealId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Kind,
                    opt => opt.MapFrom(src => src.Kind))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ImageUrl));


            // Class naar ClassVM
            CreateMap<Class, ClassVM>()
                .ForMember(dest => dest.ClassId,
                    opt => opt.MapFrom(src => src.ClassId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));

            CreateMap<Ticket, TicketVM>()
                .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Meal.Name))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.SeatNumber))
                //.ForMember(dest => dest.DepartureAirport, opt => opt.MapFrom(src => src.FlightTickets.FirstOrDefault().Flight.DepartureAirport.Name))
                .ForMember(dest => dest.DepartureAirport, opt => opt.MapFrom(src => src.Departure))
                .ForMember(dest => dest.ArrivalAirport, opt => opt.MapFrom(src => src.Arrival));

        }
    }
}

