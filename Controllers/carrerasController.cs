using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApipractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApipractica.Models;
using Microsoft.EntityFrameworkCore;

namespace webApipractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carrerasController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public carrerasController(equiposContext carrerasContexto)
        {
            _equiposContexto = carrerasContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var listadocarreras = (from c in _equiposContexto.carreras
                                              join f in _equiposContexto.facultades on c.facultad_id equals f.facultad_id
                                              
                                              select new
                                              {
                                                  c.carrera_id,
                                                  c.nombre_carrera,
                                                  c.facultad_id,
                                                  f.nombre_facultad,
                                                  c.estado
                                              }).ToList();
            if (listadocarreras.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadocarreras);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] carreras come)
        {
            try
            {

                _equiposContexto.carreras.Add(come);
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
        public IActionResult actualizarcomentario(int id, [FromBody] carreras carrerasModificar)
        {
            carreras? carre = (from e in _equiposContexto.carreras
                                 where e.carrera_id == id
                                 select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.nombre_carrera = carrerasModificar.nombre_carrera;
            carre.facultad_id = carrerasModificar.facultad_id;
            carre.estado = carrerasModificar.estado;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carrerasModificar);




        }
        // metodo eliminar
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarcarreras(int id)
        {
            carreras? carre = (from e in _equiposContexto.carreras
                                 where e.carrera_id == id
                                 select e).FirstOrDefault();
            if (carre == null) return NotFound();
            
            carre.estado = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }

    }
}
