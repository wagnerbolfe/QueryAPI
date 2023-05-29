using System.Collections.Generic;
using System.Threading.Tasks;
using QueryCEP.API.Entities;

namespace QueryCEP.API.Repositories
{
    public interface IInfoRepository
    {
        Task<IReadOnlyCollection<Cep>> GetAllAsync();
        Task<Cep> GetAsync(string cep_number);
        Task CreateAsync(Cep entity);
        Task UpdateAsync(Cep entity);
        Task RemoveAsync(string cep_number);
    }
}