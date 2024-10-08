using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialWebApi.Models;
using ParcialWebApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParcialWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptomonedasController : ControllerBase
    {

        private ICriptoRepository _repository;
        public CriptomonedasController(ICriptoRepository repository)
        {
            _repository = repository;
        }


        // GET: api/criptomoneda/category/{categoria}
        [HttpGet("cripto")]
        public ActionResult<List<Criptomoneda>> GetCriptomonedasByCategoria(int categoria)
        {
            if (categoria <= 0)
            {
                return BadRequest("El ID de la categoría debe ser un valor positivo.");
            }

            var criptomonedas = _repository.GetByCategory(categoria);
            if (criptomonedas == null || criptomonedas.Count == 0)
            {
                return NotFound($"No se encontraron criptomonedas en la categoría con ID '{categoria}' o no han sido actualizadas recientemente.");
            }

            return Ok(criptomonedas); 
        }
 

    // DELETE api/<Cripto>/1
    [HttpDelete("cripto")]
        public IActionResult Delete(int id)
        {
            try
            {
                var criptoDeleted = _repository.Delete(id);
                if (criptoDeleted)
                {
                    return Ok("Criptomoneda eliminada correctamente");
                }
                else
                {
                    return NotFound($"No existe criptomoneda con el id: [{id}] o no está habilitada.");
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }


    // PUT api/<PeliculasController>/5
    [HttpPut("cripto")]
        public IActionResult UpdateCriptomoneda(string simbolo, double nuevoValor)
        {
            if (string.IsNullOrEmpty(simbolo) || nuevoValor <= 0)
            {
                return BadRequest("Los datos ingresados no son inválidos.");
            }

            var resultado = _repository.Update(simbolo, nuevoValor);
            if (!resultado)
            {
                return NotFound($"Criptomoneda con símbolo {simbolo} no encontrada.");
            }

            return Ok("Criptomoneda actualizada con éxito");
        }
    }
}
