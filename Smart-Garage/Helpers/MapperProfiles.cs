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

            // UserResponseDTO
            this.CreateMap<UserResponseDTO, UserRequestDTO>();
            this.CreateMap<UserResponseDTO, CustomerViewModel>();
            this.CreateMap<UserResponseDTO, CustomerVisitViewModel>();

            // SignUpViewModel
            this.CreateMap<SignUpViewModel, SignUpUserRequestDTO>();

            // CustomerViewModel
            this.CreateMap<CustomerViewModel, UpdateUserRequestDTO>();


            // Vehicle
            this.CreateMap<Vehicle, VehicleViewModel>();
            this.CreateMap<Vehicle, VehicleResponseDTO>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
            //ServiceResponseDTO
            this.CreateMap<ServiceReponseDTO, ServiceViewModel>();

            // VehicleResponseDTO
            this.CreateMap<VehicleResponseDTO, VehicleViewModel>();
            this.CreateMap<VehicleResponseDTO, VehicleVisitViewModel>();

            // VehicleRequestDTO
            this.CreateMap<VehicleRequestDTO, Vehicle>();
            this.CreateMap<VehicleRequestDTO, CreateVehicleViewModel>();

            // VehicleResponseDTO
            this.CreateMap<VehicleResponseDTO, Vehicle>();
            this.CreateMap<VehicleResponseDTO, VehicleViewModel>();

            //Service
            this.CreateMap<Service, ServiceReponseDTO>();


            // Service
            this.CreateMap<Service, UpdateServiceResponseDTO>();
            this.CreateMap<Service, CreateServiceResponseDTO>();
            this.CreateMap<Service, DeleteServiceResponseDTO>();
            this.CreateMap<Service, ServiceReponseDTO>();

            // ServiceReponseDTO
            this.CreateMap<ServiceReponseDTO, ServiceViewModel>();


            // Mechanic
            this.CreateMap<Mechanic, MechanicResponseDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}"));

            // MechanicResponseDTO
            this.CreateMap<MechanicResponseDTO, MechanicViewModel>();

            // CreateMechanicViewModel
            this.CreateMap<CreateMechanicViewModel, MechanicRequestDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // MechanicRequestDTO
            this.CreateMap<MechanicRequestDTO, Mechanic>();

            // Part
            this.CreateMap<Part, PartResponseDTO>();

            // PartResponseDTO
            this.CreateMap<PartResponseDTO, PartViewModel>();


            // Visit
            this.CreateMap<Visit, VisitResponseDTO>()
                .ForPath(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle))
                .ForPath(dest => dest.Vehicle.Brand, opt => opt.MapFrom(src => src.Vehicle.Model.Brand.Name))
                .ForPath(dest => dest.Vehicle.Model, opt => opt.MapFrom(src => src.Vehicle.Model.Name))
                .ForPath(dest => dest.Vehicle.User, opt => opt.MapFrom(src => src.Vehicle.User))
                .ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstanceResponseDTO
                {
                    Mechanic = new MechanicResponseDTO
                    {
                        Id = si.Mechanic.Id,
                        Name = si.Mechanic.Name
                    },
                    Service = new ServiceReponseDTO
                    {
                        Id = si.Service.Id,
                        Name = si.Service.Name,
                        Price = si.Service.Price
                    },
                    Part = new PartResponseDTO
                    {
                        Id = si.Part.Id,
                        Name = si.Part.Name,
                        UnitPrice = si.Part.UnitPrice,

                    },
                    PartQuantity = si.PartQuantity,
                    PartUnitPrice = si.PartUnitPrice,
                    ServicePrice = si.ServicePrice
                })));

            // VisitResponseDTO
            this.CreateMap<VisitResponseDTO, VisitViewModel>()
                .ForPath(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle))
                .ForPath(dest => dest.User, opt => opt.MapFrom(src => src.Vehicle.User))
                .ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstanceViewModel
                {
                    Mechanic = new MechanicViewModel
                    {
                        Id = si.Mechanic.Id,
                        Name = si.Mechanic.Name
                    },
                    Service = new ServiceViewModel
                    {
                        Id = si.Service.Id,
                        Name = si.Service.Name,
                        Price = si.Service.Price
                    },
                    Part = new PartViewModel
                    {
                        Id = si.Part.Id,
                        Name = si.Part.Name,
                        UnitPrice = si.Part.UnitPrice,
                    },
                    PartQuantity = si.PartQuantity,
                    PartUnitPrice = si.PartUnitPrice,
                    ServicePrice = si.ServicePrice
                })));

            // VisitViewModel
            this.CreateMap<VisitViewModel, VisitRequestDTO>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.PartsTotalPrice, opt => opt.MapFrom(src => src.PartsTotalPrice))
                .ForPath(dest => dest.ServicesTotalPrice, opt => opt.MapFrom(src => src.ServicesTotalPrice))
                .ForPath(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstanceRequestDTO
                {
                    Mechanic = si.Mechanic != null ? new MechanicRequestDTO
                    {
                        Id = si.Mechanic.Id,
                        Name = si.Mechanic.Name,
                    } : null,
                    Service = si.Service != null ? new ServiceRequestDTO
                    {
                        Id = si.Service.Id,
                        Name = si.Service.Name,
                        Price = si.Service.Price
                    } : null,
                    Part = si.Part != null ? new PartRequestDTO
                    {
                        Id = si.Part.Id,
                        Name = si.Part.Name,
                        UnitPrice = si.Part.UnitPrice,
                    } : null,
                    PartQuantity = si.PartQuantity,
                    PartUnitPrice = si.PartUnitPrice,
                    ServicePrice = si.ServicePrice
                }))) ;

            // VisitRequestDTO
            this.CreateMap<VisitRequestDTO, Visit>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.PartsTotalPrice, opt => opt.MapFrom(src => src.PartsTotalPrice))
                .ForPath(dest => dest.ServicesTotalPrice, opt => opt.MapFrom(src => src.ServicesTotalPrice))
                .ForPath(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstance
                {
                    Mechanic = si.Mechanic != null ? new Mechanic
                    {
                        Id = si.Mechanic.Id,
                        Name = si.Mechanic.Name
                    } : null,
                    Service = si.Service != null ? new Service
                    {
                        Id = si.Service.Id,
                        Name = si.Service.Name,
                        Price = si.Service.Price
                    } : null,
                    Part = si.Part != null ? new Part
                    {
                        Id = si.Part.Id,
                        Name = si.Part.Name,
                        UnitPrice = si.Part.UnitPrice,
                    } : null,
                    PartQuantity = si.PartQuantity,
                    PartUnitPrice = si.PartUnitPrice,
                    ServicePrice = si.ServicePrice
                })));

            // Model
            this.CreateMap<Model, ModelResponseDTO>();
            this.CreateMap<ModelResponseDTO, ModelViewModel>();


            // Brand
            this.CreateMap<Brand, BrandResponseDTO>()
                .ForPath(dest => dest.Models, opt => opt.MapFrom(src => src.Models));

            this.CreateMap<BrandResponseDTO, BrandViewModel>()
                .ForPath(dest => dest.Models, opt => opt.MapFrom(src => src.Models));
            //ServiceViewModel
            this.CreateMap<ServiceViewModel, ServiceRequestDTO>();
            this.CreateMap<ServiceViewModel, ServiceRequestDTO>();

            //VehicleViewModel
            this.CreateMap<VehicleViewModel, VehicleResponseDTO>();

            // VisitResponseDTO
            this.CreateMap<VisitResponseDTO, VisitViewModel>()
                .ForPath(dest => dest.Vehicle.Model, opt => opt.MapFrom(src => src.Vehicle.Model))
                .ForPath(dest => dest.Vehicle.Brand, opt => opt.MapFrom(src => src.Vehicle.Brand))
                .ForPath(dest => dest.Vehicle.LicensePlate, opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForPath(dest => dest.Vehicle.VIN, opt => opt.MapFrom(src => src.Vehicle.VIN))
                .ForPath(dest => dest.Vehicle.CreationYear, opt => opt.MapFrom(src => src.Vehicle.CreationYear))
                .ForPath(dest => dest.User, opt => opt.MapFrom(src => src.Vehicle.User));

            //string
            this.CreateMap<string, Model>();
        }
    }
}
