using AlaktuManager.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlaktuManager.Client.Services
{
    public interface IPiplineService
    {
        Task<IEnumerable<Pipline>> GetPiplines();
        Task<Pipline> GetPipline(int employeeId);
        Task<Pipline> AddPipline(Pipline pipline);
        Task<Pipline> UpdatePipline(Pipline pipline);
        Task DeletePipline(int Id);
    }
}