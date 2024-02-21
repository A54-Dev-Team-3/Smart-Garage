using AutoMapper;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Services
{
    public class ModelService : IModelService
    {
        private readonly IMapper autoMapper;
        private readonly IModelRepository modelRepository;

        public ModelService(IMapper autoMapper, IModelRepository modelRepository)
        {
            this.autoMapper = autoMapper;
            this.modelRepository = modelRepository;
        }

        public IList<ModelResponseDTO> GetAll()
        {
            return modelRepository.GetAll()
                .Select(v => autoMapper.Map<ModelResponseDTO>(v))
                .ToList();
        }

        public IList<ModelResponseDTO> GetModelsByBrandId(int brandId)
        {
            return autoMapper.Map<IList<ModelResponseDTO>>(modelRepository.GetModelsByBrandId(brandId));
        }
    }
}
