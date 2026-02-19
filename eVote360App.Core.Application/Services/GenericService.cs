using AutoMapper;
using eVote360App.Core.Application.Interfaces.Services;
using eVote360App.Core.Domain.Common;
using eVote360App.Core.Domain.Interfaces;

namespace eVote360App.Core.Application.Services
{
    public class GenericService<SaveViewModel, ViewModel, Model> :
        IGenericService<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : BasicEntity<int>
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;
        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> AddAsync(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);
            entity = await _repository.AddAsync(entity);
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModelAsync()
        {
            var entityList = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entityList);
        }

        public virtual async Task<SaveViewModel> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task<SaveViewModel> UpdateAsync(SaveViewModel vm, int id)
        {
            Model entity = _mapper.Map<Model>(vm);
            entity = await _repository.UpdateAsync(id, entity);
            return _mapper.Map<SaveViewModel>(entity);
        }
    }
}
