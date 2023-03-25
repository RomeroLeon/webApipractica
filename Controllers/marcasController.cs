using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApipractica.Models;

namespace webApipractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public marcasController(equiposContext marcasContexto)
        {
            _equiposContexto = marcasContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<marcas> listadomarcas = (from e in _equiposContexto.marcas
                                              select e).ToList();
            if (listadomarcas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadomarcas);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] marcas come)
        {
            try
            {

                _equiposContexto.marcas.Add(come);
                _equiposContexto.SaveChanges();
                return Ok(come);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // metodo actualizar
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarcomentario(int id, [FromBody] marcas marcasModificar)
        {
            marcas? carre = (from e in _equiposContexto.marcas
                               where e.id_marcas == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.nombre_marca = marcasModificar.nombre_marca;
            carre.estados = marcasModificar.estados;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(marcasModificar);




        }
        // metodo eliminar
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarmarcas(int id)
        {
            marcas? carre = (from e in _equiposContexto.marcas
                               where e.id_marcas == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.estados = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }

    }

}

