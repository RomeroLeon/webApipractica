using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApipractica.Models;

namespace webApipractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public reservasController(equiposContext reservasContexto)
        {
            _equiposContexto = reservasContexto;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
           var listadoreservas = (from e in _equiposContexto.reservas
               join eq in _equiposContexto.equipos on e.equipo_id equals eq.id_equipos
               join us in _equiposContexto.usuarios on e.usuario_id equals us.usuario_id
               join er in _equiposContexto.estados_reserva on e.estado_reserva_id equals er.estado_res_id
               select new
               {
                   e.reserva_id,
                   e.equipo_id,
                   eq.nombre,
                   eq.descripcion,
                   eq.costo,
                   e.usuario_id,
                   nombreuser=us.nombre,
                   us.documento,
                   us.carnet,
                   e.fecha_salida,
                   e.fecha_retorno,
                   e.tiempo_reserva,
                   e.estado_reserva_id,
                   estado_reserva=er.estado,
                   e.estado
               }).ToList();
            if (listadoreservas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoreservas);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] reservas come)
        {
            try
            {

                _equiposContexto.reservas.Add(come);
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
        public IActionResult actualizarcomentario(int id, [FromBody] reservas reservasModificar)
        {
            reservas? carre = (from e in _equiposContexto.reservas
                               where e.reserva_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.equipo_id = reservasModificar.equipo_id;
            carre.usuario_id = reservasModificar.usuario_id;
            carre.fecha_salida = reservasModificar.fecha_salida;
            carre.hora_saldia = reservasModificar.hora_saldia;
            carre.tiempo_reserva = reservasModificar.tiempo_reserva;
            carre.estado_reserva_id = reservasModificar.estado_reserva_id;
            carre.fecha_retorno = reservasModificar.fecha_retorno;
            carre.hora_retorno = reservasModificar.hora_retorno;
            carre.estado = reservasModificar.estado;


            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(reservasModificar);




        }
        // metodo eliminar
        [HttpPut]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarreservas(int id)
        {
            reservas? carre = (from e in _equiposContexto.reservas
                               where e.reserva_id == id
                               select e).FirstOrDefault();
            if (carre == null) return NotFound();

            carre.estado = "I";
            _equiposContexto.Entry(carre).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(carre);

        }

    }
}

