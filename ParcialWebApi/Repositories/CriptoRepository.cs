using Microsoft.EntityFrameworkCore;
using ParcialWebApi.Models;
using System.IO;

namespace ParcialWebApi.Repositories
{
    public class CriptoRepository : ICriptoRepository
    {

        private CriptoContext _context;

        public CriptoRepository(CriptoContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var cripto = GetById(id);

            if (cripto == null)
            {
                return false;
            }

            if (cripto.Estado == "H")
            {
                cripto.Estado = "NH"; 
                _context.Criptomonedas.Update(cripto);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }


        public List<Criptomoneda> GetByCategory(int categoria)
        {
            var fechaLimite = DateTime.UtcNow.AddDays(-1);

            var criptomonedas = _context.Criptomonedas
                .Where(c => c.Categoria == categoria && c.UltimaActualizacion >= fechaLimite)
                .ToList();

            return criptomonedas;
        }





        public Criptomoneda? GetById(int id)
        {
            return _context.Criptomonedas.Find(id);
        }

        public bool Update(string simbolo, double nuevoValor)
        {
            var criptomoneda = _context.Criptomonedas
                .FirstOrDefault(c => c.Simbolo == simbolo);

            if (criptomoneda == null)
            {
                return false;
            }

            criptomoneda.ValorActual = nuevoValor;
            criptomoneda.UltimaActualizacion = DateTime.UtcNow;


            _context.SaveChanges();

            return true;
        }
    }
}
