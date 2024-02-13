using AutoMapper;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.ViewModel;

namespace Smart_Garage.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            // User
            this.CreateMap<User, UserResponseDTO>();


            // UserRequestDTO
            this.CreateMap<UserRequestDTO, User>();


            // UpdateUserRequestDTO
            this.CreateMap<UpdateUserRequestDTO, User>();


            // Service
            this.CreateMap<Service, UpdateServiceResponseDTO>();
            this.CreateMap<Service, CreateServiceResponseDTO>();
            this.CreateMap<Service, DeleteServiceResponseDTO>();


            // UserResponseDTO
            this.CreateMap<UserResponseDTO, UserRequestDTO>();
            this.CreateMap<UserResponseDTO, CustomerViewModel>();


            // VehicleResponseDTO
            this.CreateMap<VehicleResponseDTO, VehicleViewModel>();


            // VehicleRequestDTO
            this.CreateMap<VehicleRequestDTO, Vehicle>();


            // VehicleResponseDTO
            this.CreateMap<VehicleResponseDTO, Vehicle>();
            this.CreateMap<VehicleResponseDTO, VehicleViewModel>();


            // Visit
            this.CreateMap<Visit, VisitResponseDTO>();


            // Mechanic
            this.CreateMap<Mechanic, MechanicResponseDTO>();


            // Part
            this.CreateMap<Part, PartResponseDTO>();


            // SignUpViewModel
            this.CreateMap<SignUpViewModel, SignUpUserRequestDTO>();


            // MechanicResponseDTO
            this.CreateMap<MechanicResponseDTO, MechanicViewModel>();


            // Vehicle
            this.CreateMap<Vehicle, VehicleViewModel>();

            //this.CreateMap<Vehicle, VehicleResponseDTO>();
            this.CreateMap<Vehicle, VehicleResponseDTO>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            // CustomerViewModel
            this.CreateMap<CustomerViewModel, UpdateUserRequestDTO>();

            // VisitResponseDTO
            this.CreateMap<VisitResponseDTO, VisitViewModel>()
                .ForPath(dest => dest.Vehicle.Model, opt => opt.MapFrom(src => src.Vehicle.Model))
                .ForPath(dest => dest.Vehicle.Brand, opt => opt.MapFrom(src => src.Vehicle.Brand))
                .ForPath(dest => dest.Vehicle.LicensePlate, opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForPath(dest => dest.Vehicle.VIN, opt => opt.MapFrom(src => src.Vehicle.VIN))
                .ForPath(dest => dest.Vehicle.CreationYear, opt => opt.MapFrom(src => src.Vehicle.CreationYear))
                .ForPath(dest => dest.Vehicle.User, opt => opt.MapFrom(src => src.Vehicle.User));
        }
    }
}
