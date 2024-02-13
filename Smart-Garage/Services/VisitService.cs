using AutoMapper;
using Smart_Garage.Exceptions;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository visitsRepository;
        private readonly IUserService userService;
        private readonly IMapper autoMapper;

        public VisitService(IVisitRepository visitRepository, IUserService usersService, IMapper autoMapper)
        {
            this.visitsRepository = visitRepository;
            this.userService = usersService;
            this.autoMapper = autoMapper;
        }
        
        public VisitResponseDTO Create(int VehicleId, string username)
        {

            userService.IsCurrentUserAdmin(username);

            Visit visit = new Visit()
            {
                VehicleId = VehicleId,
                Date = DateTime.Now,
            };

            return autoMapper.Map<VisitResponseDTO>(visitsRepository.Create(visit));
        }

        public IList<VisitResponseDTO> GetAll(string username)
        {
            userService.IsCurrentUserAdmin(username);

            return visitsRepository.GetAll()
                .Select(v => autoMapper.Map<VisitResponseDTO>(v))
                .ToList();
        }

        public IList<VisitResponseDTO> FilterBy(VisitQueryParameters filterParameters, string username)
        {
            //userService.IsCurrentUserAdmin(username);

            return visitsRepository.FilterBy(filterParameters)
                            .Select(u => autoMapper.Map<VisitResponseDTO>(u))
                            .ToList();
        }

        public VisitResponseDTO GetById(int id)
        {
            //userService.IsCurrentUserAdmin(username);
            return autoMapper.Map<VisitResponseDTO>(visitsRepository.GetById(id));
        }

        public VisitResponseDTO Delete(int id, string username)
        {
            userService.IsCurrentUserAdmin(username);
            return autoMapper.Map<VisitResponseDTO>(visitsRepository.Delete(id));
        }

        public bool VisitExists(int id)
        {
            return visitsRepository.VisitExists(id);
        }
    }
}
