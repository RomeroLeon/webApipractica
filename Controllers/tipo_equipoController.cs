using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApipractica.Models;

namespace webApipractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipoController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public tipo_equipoController(equiposContext tipo_equipoContexto)
        {
            _equiposContexto = tipo_equipoContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<tipo_equipo> listadotipo_equipo = (from e in _equiposContexto.tipo_equipo
                                              select e).ToList();
            if (listadotipo_equipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadotipo_equipo);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] tipo_equipo come)
        {
            try
            {

                _equiposContexto.tipo_equipo.Add(come);
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
        public IActionResult actualizarcomentario(int id, [FromBody] tipo_equipo tipo_equipoModificar)
        {
            tipo_equipo? carre = (from e in _equiposContexto.tipo_equipo
                               where e.id_tipo_equipo == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            
            carre.descripcion = tipo_equipoModificar.descripcion;
            carre.estado = tipo_equipoModificar.estado;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(tipo_equipoModificar);




        }
        // metodo eliminar
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminartipo_equipo(int id)
        {
            tipo_equipo? carre = (from e in _equiposContexto.tipo_equipo
                               where e.id_tipo_equipo == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.estado = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }
    }
}
