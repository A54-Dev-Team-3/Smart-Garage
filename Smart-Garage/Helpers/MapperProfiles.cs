﻿using AutoMapper;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;

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
            this.CreateMap<VehicleResponseDTO, Vehicle>();
            this.CreateMap<Visit, VisitResponseDTO>();
            this.CreateMap<Mechanic, MechanicResponseDTO>();
        }
    }
}
