using AutoMapper;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Repositories;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Services
{
    public class BrandService : IBrandService
    {
        private readonly IMapper autoMapper;
        private readonly IBrandRepository brandRepository;

        public BrandService(IMapper autoMapper, IBrandRepository brandRepository)
        {
            this.autoMapper = autoMapper;
            this.brandRepository = brandRepository;
        }

        public IList<BrandResponseDTO> GetAll()
        {
            return brandRepository.GetAll()
               .Select(v => autoMapper.Map<BrandResponseDTO>(v))
               .ToList();
        }
    }
}
