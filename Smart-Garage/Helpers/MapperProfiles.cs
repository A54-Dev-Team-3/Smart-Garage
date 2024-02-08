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
            this.CreateMap<User, UserResponseDTO>();
            this.CreateMap<UpdateUserRequestDTO, User>();
            this.CreateMap<Service, UpdateServiceResponseDTO>();
            this.CreateMap<Service, CreateServiceResponseDTO>();
            this.CreateMap<Service, DeleteServiceResponseDTO>();
            this.CreateMap<UserRequestDTO, User>();
            this.CreateMap<UserResponseDTO, UserRequestDTO>();
            this.CreateMap<VehicleRequestDTO, Vehicle>();
            this.CreateMap<VehicleResponseDTO, Vehicle>();
            this.CreateMap<Visit, VisitResponseDTO>();
            this.CreateMap<Mechanic, MechanicResponseDTO>();
            this.CreateMap<Part, PartResponseDTO>();
            this.CreateMap<SignUpViewModel, SignUpUserRequestDTO>();
            this.CreateMap<MechanicResponseDTO, MechanicViewModel>();
            this.CreateMap<Vehicle, VehicleViewModel>();
            this.CreateMap<UserResponseDTO, CustomerViewModel>();
            this.CreateMap<CustomerViewModel, UpdateUserRequestDTO>();

            this.CreateMap<Vehicle, VehicleResponseDTO>();
        }
    }
}
