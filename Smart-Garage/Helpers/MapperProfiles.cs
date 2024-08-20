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
            CreateMap<User, UserResponseDTO>();


            // UserRequestDTO
            CreateMap<UserRequestDTO, User>();


            // UpdateUserRequestDTO
            CreateMap<UpdateUserRequestDTO, User>();

            // UserResponseDTO
            CreateMap<UserResponseDTO, UserRequestDTO>();
            CreateMap<UserResponseDTO, CustomerViewModel>();
            CreateMap<UserResponseDTO, CustomerVisitViewModel>();

            // SignUpViewModel
            CreateMap<SignUpViewModel, SignUpUserRequestDTO>();

            // CustomerViewModel
            CreateMap<CustomerViewModel, UpdateUserRequestDTO>();


            // Vehicle
            CreateMap<Vehicle, VehicleViewModel>();

            CreateMap<Vehicle, VehicleViewModel>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name));

            CreateMap<Vehicle, VehicleResponseDTO>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            //this.CreateMap<Vehicle, VehicleResponseDTO>()
            //.ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
            //.ForMember(dest => dest.VIN, opt => opt.MapFrom(src => src.VIN))
            //.ForMember(dest => dest.CreationYear, opt => opt.MapFrom(src => src.CreationYear))
            //.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
            //.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
            //.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            //.ForMember(dest => dest.Visits, opt => opt.MapFrom(src => src.Visits));

            //ServiceResponseDTO
            CreateMap<ServiceReponseDTO, ServiceViewModel>();

            // VehicleResponseDTO
            CreateMap<VehicleResponseDTO, VehicleViewModel>();
            CreateMap<VehicleResponseDTO, VehicleVisitViewModel>();

            // VehicleRequestDTO
            CreateMap<VehicleRequestDTO, Vehicle>();
            CreateMap<VehicleRequestDTO, CreateVehicleViewModel>();
            CreateMap<VehicleRequestDTO, Vehicle>()
                .ForPath(dest => dest.Model.Name, opt => opt.MapFrom(src => src.Model))
                .ForPath(dest => dest.Model.Brand.Name, opt => opt.MapFrom(src => src.Brand))
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            // VehicleResponseDTO
            CreateMap<VehicleResponseDTO, Vehicle>();
            CreateMap<VehicleResponseDTO, VehicleViewModel>();


            //Service
            CreateMap<Service, ServiceReponseDTO>();


            // Service
            CreateMap<Service, UpdateServiceResponseDTO>();
            CreateMap<Service, CreateServiceResponseDTO>();
            CreateMap<Service, DeleteServiceResponseDTO>();
            CreateMap<Service, ServiceReponseDTO>();

            // ServiceReponseDTO
            CreateMap<ServiceReponseDTO, ServiceViewModel>();


            // Mechanic
            CreateMap<Mechanic, MechanicResponseDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}"));

            // MechanicResponseDTO
            CreateMap<MechanicResponseDTO, MechanicViewModel>();

            // CreateMechanicViewModel
            CreateMap<CreateMechanicViewModel, MechanicRequestDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // MechanicRequestDTO
            CreateMap<MechanicRequestDTO, Mechanic>();

            // Part
            CreateMap<Part, PartResponseDTO>();

            // PartResponseDTO
            CreateMap<PartResponseDTO, PartViewModel>();


            // Visit
            CreateMap<Visit, VisitResponseDTO>()
                .ForPath(dest => dest.Vehicle.LicensePlate, opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForPath(dest => dest.Vehicle.VIN, opt => opt.MapFrom(src => src.Vehicle.VIN))
                .ForPath(dest => dest.Vehicle.CreationYear, opt => opt.MapFrom(src => src.Vehicle.CreationYear))
                .ForPath(dest => dest.Vehicle.Brand, opt => opt.MapFrom(src => src.Vehicle.Model.Brand.Name))
                .ForPath(dest => dest.Vehicle.Model, opt => opt.MapFrom(src => src.Vehicle.Model.Name))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.Vehicle.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.Vehicle.User.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.Vehicle.User.LastName))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.Vehicle.User.PhoneNumber))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Vehicle.User.Email));
                //.ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstanceResponseDTO
                //{
                //    Mechanic = new MechanicResponseDTO
                //    {
                //        Id = si.Mechanic.Id,
                //        Name = si.Mechanic.Name
                //    },
                //    Service = new ServiceReponseDTO
                //    {
                //        Id = si.Service.Id,
                //        Name = si.Service.Name,
                //        Price = si.Service.Price
                //    },
                //    Part = new PartResponseDTO
                //    {
                //        Id = si.Part.Id,
                //        Name = si.Part.Name,
                //        UnitPrice = si.Part.UnitPrice,

                //    },
                //    PartQuantity = si.PartQuantity,
                //    PartUnitPrice = si.PartUnitPrice,
                //    ServicePrice = si.ServicePrice
                //})));

            CreateMap<UserVisitResponseDTO, CustomerVisitViewModel>();

            // VisitResponseDTO
            CreateMap<VisitResponseDTO, VisitViewModel>()
                .ForPath(dest => dest.Vehicle.LicensePlate, opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForPath(dest => dest.Vehicle.VIN, opt => opt.MapFrom(src => src.Vehicle.VIN))
                .ForPath(dest => dest.Vehicle.CreationYear, opt => opt.MapFrom(src => src.Vehicle.CreationYear))
                .ForPath(dest => dest.Vehicle.Brand, opt => opt.MapFrom(src => src.Vehicle.Brand))
                .ForPath(dest => dest.Vehicle.Model, opt => opt.MapFrom(src => src.Vehicle.Model))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
            //.ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstanceViewModel
            //{
            //    Mechanic = new MechanicViewModel
            //    {
            //        Id = si.Mechanic.Id,
            //        Name = si.Mechanic.Name
            //    },
            //    Service = new ServiceViewModel
            //    {
            //        Id = si.Service.Id,
            //        Name = si.Service.Name,
            //        Price = si.Service.Price
            //    },
            //    Part = new PartViewModel
            //    {
            //        Id = si.Part.Id,
            //        Name = si.Part.Name,
            //        UnitPrice = si.Part.UnitPrice,
            //    },
            //    PartQuantity = si.PartQuantity,
            //    PartUnitPrice = si.PartUnitPrice,
            //    ServicePrice = si.ServicePrice
            //})));

            // VisitViewModel
            CreateMap<VisitViewModel, VisitRequestDTO>()
                .ForPath(dest => dest.PartsTotalPrice, opt => opt.MapFrom(src => src.PartsTotalPrice))
                .ForPath(dest => dest.ServicesTotalPrice, opt => opt.MapFrom(src => src.ServicesTotalPrice))
                .ForPath(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
                //.ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstanceRequestDTO
                //{
                //    Mechanic = si.Mechanic != null ? new MechanicRequestDTO
                //    {
                //        Id = si.Mechanic.Id,
                //        Name = si.Mechanic.Name,
                //    } : null,
                //    Service = si.Service != null ? new ServiceRequestDTO
                //    {
                //        Id = si.Service.Id,
                //        Name = si.Service.Name,
                //        Price = si.Service.Price
                //    } : null,
                //    Part = si.Part != null ? new PartRequestDTO
                //    {
                //        Id = si.Part.Id,
                //        Name = si.Part.Name,
                //        UnitPrice = si.Part.UnitPrice,
                //    } : null,
                //    PartQuantity = si.PartQuantity,
                //    PartUnitPrice = si.PartUnitPrice,
                //    ServicePrice = si.ServicePrice
                //})));

            // VisitRequestDTO
            CreateMap<VisitRequestDTO, Visit>();
                //.ForPath(dest => dest.PartsTotalPrice, opt => opt.MapFrom(src => src.PartsTotalPrice))
                //.ForPath(dest => dest.ServicesTotalPrice, opt => opt.MapFrom(src => src.ServicesTotalPrice))
                //.ForPath(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
                //.ForMember(dest => dest.ServiceInstances, opt => opt.MapFrom(src => src.ServiceInstances.Select(si => new ServiceInstance
                //{
                //    Mechanic = si.Mechanic != null ? new Mechanic
                //    {
                //        Id = si.Mechanic.Id,
                //        Name = si.Mechanic.Name
                //    } : null,
                //    Service = si.Service != null ? new Service
                //    {
                //        Id = si.Service.Id,
                //        Name = si.Service.Name,
                //        Price = si.Service.Price
                //    } : null,
                //    Part = si.Part != null ? new Part
                //    {
                //        Id = si.Part.Id,
                //        Name = si.Part.Name,
                //        UnitPrice = si.Part.UnitPrice,
                //    } : null,
                //    PartQuantity = si.PartQuantity,
                //    PartUnitPrice = si.PartUnitPrice,
                //    ServicePrice = si.ServicePrice
                //})));

            // Model
            CreateMap<VehicleModel, ModelResponseDTO>();
            CreateMap<ModelResponseDTO, ModelViewModel>();


            // Brand
            CreateMap<VehicleBrand, BrandResponseDTO>()
                .ForPath(dest => dest.Models, opt => opt.MapFrom(src => src.Models));

            CreateMap<BrandResponseDTO, BrandViewModel>()
                .ForPath(dest => dest.Models, opt => opt.MapFrom(src => src.Models));

            // ServiceViewModel
            CreateMap<ServiceViewModel, ServiceRequestDTO>();
            CreateMap<ServiceViewModel, ServiceRequestDTO>();

            //VehicleViewModel
            CreateMap<VehicleViewModel, VehicleResponseDTO>();
            CreateMap<VehicleViewModel, VehicleRequestDTO>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));

            // VisitResponseDTO
            CreateMap<VisitResponseDTO, VisitViewModel>()
                .ForPath(dest => dest.Vehicle.Model, opt => opt.MapFrom(src => src.Vehicle.Model))
                .ForPath(dest => dest.Vehicle.Brand, opt => opt.MapFrom(src => src.Vehicle.Brand))
                .ForPath(dest => dest.Vehicle.LicensePlate, opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForPath(dest => dest.Vehicle.VIN, opt => opt.MapFrom(src => src.Vehicle.VIN))
                .ForPath(dest => dest.Vehicle.CreationYear, opt => opt.MapFrom(src => src.Vehicle.CreationYear))
                .ForPath(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<PaginatedList<Visit>, PaginatedList<VisitResponseDTO>>()
                .ConvertUsing((src, dest, context) =>
                {
                    var totalPages = src.TotalPages;
                    var pageNumber = src.PageNumber;
                    var visitsResponseDTOs = context.Mapper.Map<List<VisitResponseDTO>>(src);
                    return new PaginatedList<VisitResponseDTO>(visitsResponseDTOs, totalPages, pageNumber);
                });

            CreateMap<PaginatedList<VisitResponseDTO>, PaginatedList<VisitViewModel>>()
                .ConvertUsing((src, dest, context) =>
                {
                    var totalPages = src.TotalPages;
                    var pageNumber = src.PageNumber;
                    var visitViewModels = context.Mapper.Map<List<VisitViewModel>>(src);
                    return new PaginatedList<VisitViewModel>(visitViewModels, totalPages, pageNumber);
                });
        }
    }
}
