using System.Threading.Tasks;
using QueryCEP.API.Entities;

namespace QueryCEP.API.Services
{
    public interface IInfoService
    {
        Task<Cep> ProcessInfo(string cep);
    }
}