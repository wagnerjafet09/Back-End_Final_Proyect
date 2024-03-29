
namespace Biblioteca.BL.Core
{
    public interface IBaseService
    {
        Task<ServiceResult> GetAll();
        Task<ServiceResult> GetById(int id);
    }
}
