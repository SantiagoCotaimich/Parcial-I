using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public interface ICriptoRepository
    {
        List<Criptomoneda> GetByCategory(int categoria);

        bool Delete(int id);

        bool Update(string simbolo, double nuevoValor);

        Criptomoneda? GetById(int id);
    }
}
