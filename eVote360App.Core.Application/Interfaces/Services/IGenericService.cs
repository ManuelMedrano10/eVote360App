using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360App.Core.Domain.Common;

namespace eVote360App.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Model> 
    where SaveViewModel : class
    where ViewModel : class
    where Model : BasicEntity<int>
    {
        Task<SaveViewModel> AddAsync(SaveViewModel vm);
        Task<SaveViewModel> UpdateAsync(SaveViewModel vm, int id);
        Task DeleteAsync(int id);
        Task<List<ViewModel>> GetAllViewModelAsync();
        Task<SaveViewModel> GetByIdAsync(int id);
        Task ChangeStatusAsync(int id);
    }
}
