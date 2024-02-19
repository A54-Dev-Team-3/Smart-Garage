using AutoMapper;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Repositories;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Services
{
    public class MechanicService : IMechanicService
    {
        private readonly IUserService userService;
        private readonly IMechanicRepository mechanicRepository;
        private readonly IMapper autoMapper;

        public MechanicService(IUserService userService, IMapper autoMapper, IMechanicRepository mechanicRepository)
        {
            this.userService = userService;
            this.mechanicRepository = mechanicRepository;
            this.autoMapper = autoMapper;
        }

        public MechanicResponseDTO Create(MechanicRequestDTO newMechanic)
        {
            var mechanic = autoMapper.Map<Mechanic>(newMechanic);
            return autoMapper.Map<MechanicResponseDTO>(mechanicRepository.Create(mechanic));
        }

        public IList<MechanicResponseDTO> GetAll()
        {
            //userService.IsCurrentUserAdmin(username);

            return mechanicRepository.GetAll()
                .Select(v => autoMapper.Map<MechanicResponseDTO>(v))
                .ToList();
        }

        public MechanicResponseDTO GetById(int id)
        {
            return autoMapper.Map<MechanicResponseDTO>(mechanicRepository.GetById(id));
        }

        public MechanicResponseDTO GetByName(string name)
        {
            //userService.IsCurrentUserAdmin(username);
            return autoMapper.Map<MechanicResponseDTO>(mechanicRepository.GetByName(name));
        }

        public MechanicResponseDTO Delete(int id, string username)
        {
            userService.IsCurrentUserAdmin(username);

            return autoMapper.Map<MechanicResponseDTO>(mechanicRepository.Delete(id));
        }

        public bool MechanicExists(string name)
        {
            return mechanicRepository.VisitExists(name);
        }
    }
}
