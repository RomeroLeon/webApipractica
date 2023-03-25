using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApipractica.Models;

namespace webApipractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadesController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public facultadesController(equiposContext facultadesContexto)
        {
            _equiposContexto = facultadesContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<facultades> listadofacultades = (from e in _equiposContexto.facultades
                                              select e).ToList();
            if (listadofacultades.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadofacultades);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] facultades come)
        {
            try
            {

                _equiposContexto.facultades.Add(come);
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
        public IActionResult actualizarcomentario(int id, [FromBody] facultades facultadesModificar)
        {
            facultades? carre = (from e in _equiposContexto.facultades
                               where e.facultad_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.nombre_facultad = facultadesModificar.nombre_facultad;
            carre.estado = facultadesModificar.estado;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(facultadesModificar);




        }
        // metodo eliminar
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarfacultades(int id)
        {
            facultades? carre = (from e in _equiposContexto.facultades
                               where e.facultad_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.estado = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }

    }
}

