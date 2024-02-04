using AutoMapper;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Repositories;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;
using System.Xml.Linq;

namespace Smart_Garage.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository partRepository;
        private readonly IUserService userService;
        private readonly IMapper autoMapper;

        public PartService(IPartRepository partRepository, IUserService userService, IMapper autoMapper)
        {
            this.partRepository = partRepository;
            this.userService = userService;
            this.autoMapper = autoMapper;
        }

        public PartResponseDTO Create(PartRequestDTO newPartDTO, string username)
        {
            userService.IsCurrentUserAdmin(username);

            Part part = new Part()
            {
                Name = newPartDTO.Name,
                Price = newPartDTO.Price
            };

            return autoMapper.Map<PartResponseDTO>(partRepository.Create(part));
        }

        public IList<PartResponseDTO> GetAll(string username)
        {
            userService.IsCurrentUserAdmin(username);

            return partRepository.GetAll()
                .Select(p => autoMapper.Map<PartResponseDTO>(p))
                .ToList();
        }

        public IList<PartResponseDTO> FilterBy(PartQueryParameters filterParameters, string username)
        {
            userService.IsCurrentUserAdmin(username);

            return partRepository.FilterBy(filterParameters)
                            .Select(p => autoMapper.Map<PartResponseDTO>(p))
                            .ToList();
        }

        public PartResponseDTO GetById(int id, string username)
        {
            userService.IsCurrentUserAdmin(username);
            return autoMapper.Map<PartResponseDTO>(partRepository.GetById(id));
        }

        public PartResponseDTO Update(int id, PartRequestDTO newPartDTO, string username)
        {
            userService.IsCurrentUserAdmin(username);

            Part part = new Part()
            {
                Name = newPartDTO.Name,
                Price = newPartDTO.Price
            };

            return autoMapper.Map<PartResponseDTO>(partRepository.Update(id, part));
        }

        public PartResponseDTO Delete(int id, string username)
        {
            userService.IsCurrentUserAdmin(username);

            return autoMapper.Map<PartResponseDTO>(partRepository.Delete(id));
        }

        public bool PartExists(string name)
        {
            return partRepository.PartExists(name);
        }

    }
}
