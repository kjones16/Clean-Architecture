using System.Threading.Tasks;

namespace Cln.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}
