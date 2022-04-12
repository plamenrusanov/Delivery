using AutoMapper;
using Delivery.Core.Contracts;
using Delivery.Core.ViewModels.Extras;
using Delivery.Infrastructure.Models;
using Delivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Core.DataServices
{
    public class ExtrasService : IExtrasService
    {
        private const string ExtraNotExist = "Добавката не съществува";
        private readonly IRepository<Extra> extrasRepository;
        private readonly IMapper mapper;

        public ExtrasService(IRepository<Extra> extrasRepository,
            IMapper mapper)
        {
            this.extrasRepository = extrasRepository;
            this.mapper = mapper;
        }

        public Task<List<ExtraViewModel>> AllAsync()
        {
            return extrasRepository
                .All()
                .Where(x => !x.IsDeleted)
                .Select(x => new ExtraViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Weight = x.Weight,
                }).ToListAsync();
        }
        public async Task AddExtraAsync(ExtraInpitModel model)
        {
            Extra extra = mapper.Map<Extra>(model);
            await extrasRepository.AddAsync(extra);
            await extrasRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var extra = await extrasRepository
                    .All()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (extra is null)
            {
                throw new ArgumentException(ExtraNotExist);
            }
            extrasRepository.Delete(extra);
            await extrasRepository.SaveChangesAsync();
        }

        public async Task<ExtraEditModel> GetEditModelAsync(int id)
        {
            var extra = await extrasRepository
                    .All()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

            if (extra is null)
            {
                throw new ArgumentException(ExtraNotExist);
            }

            return mapper.Map<ExtraEditModel>(extra);
        }

        public async Task UpdateExtraAsync(ExtraEditModel model)
        {
            var extra = mapper.Map<Extra>(model);
            extrasRepository.Update(extra);
            await extrasRepository.SaveChangesAsync();
        }
    }
}
