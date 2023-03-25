using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApipractica.Models;

namespace webApipractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_reservaController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public estados_reservaController(equiposContext estados_reservaContexto)
        {
            _equiposContexto = estados_reservaContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<estados_reserva> listadoestados_reserva = (from e in _equiposContexto.estados_reserva
                                              select e).ToList();
            if (listadoestados_reserva.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoestados_reserva);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] estados_reserva come)
        {
            try
            {

                _equiposContexto.estados_reserva.Add(come);
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
        public IActionResult actualizarcomentario(int id, [FromBody] estados_reserva estados_reservaModificar)
        {
            estados_reserva? carre = (from e in _equiposContexto.estados_reserva
                               where e.estado_res_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            
            carre.estado = estados_reservaModificar.estado;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(estados_reservaModificar);




        }
        // metodo eliminar
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarestados_reserva(int id)
        {
            estados_reserva? carre = (from e in _equiposContexto.estados_reserva
                               where e.estado_res_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.estado = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }

    }
}

