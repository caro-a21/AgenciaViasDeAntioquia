using AgenciaViasDeAntioquia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgenciaViasDeAntioquia.Controllers
{
    public class PesajeController : ApiController
    {
        private readonly DBExamenEntities3 dbExamen = new DBExamenEntities3();

        public class PesajeRequest
        {
            public Pesaje Pesaje { get; set; }
            public Camion Camion { get; set; }
        }

        // POST: api/Pesaje
        [HttpPost]
        public IHttpActionResult RegistrarFotomulta([FromBody] PesajeRequest request)
        {
            if (request == null)
                return Content(HttpStatusCode.BadRequest, "El objeto enviado es inválido.");

            try
            {
                using (var transaction = dbExamen.Database.BeginTransaction())
                {

                    var vehiculoExistente = dbExamen.Camion.FirstOrDefault(v => v.Placa == request.Camion.Placa);
                    if (vehiculoExistente == null)
                    {
                        dbExamen.Camion.Add(request.Camion);
                        dbExamen.SaveChanges();
                    }

                    request.Pesaje.PlacaCamion = request.Camion.Placa;
                    dbExamen.Pesaje.Add(request.Pesaje);
                    dbExamen.SaveChanges();

                    transaction.Commit();
                    return Content(HttpStatusCode.Created, "Pesaje registrado correctamente.");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }


        // GET: api/Pesaje/{placa}
        [HttpGet]
        [Route("api/Pesaje/{placa}")]
        public IHttpActionResult ObtenerFoPesajePorPlaca(string placa)
        {
            try
            {
                var resultado = dbExamen.Pesaje
                    .Where(i => i.PlacaCamion == placa)
                    .Select(i => new
                    {
                        i.PlacaCamion,
                        Camion = dbExamen.Camion
                            .Where(v => v.Placa == i.PlacaCamion)
                            .Select(v => new { v.Placa, v.Marca, v.NumeroEjes })
                            .FirstOrDefault(),
                        i.FechaPesaje,
                        i.Peso,
                        i.Estacion,
                        Fotos = dbExamen.FotoPesaje
                            .Where(f => f.idPesaje == i.id)
                            .Select(f => f.idFotoPesaje)
                            .ToList()
                    })
                    .ToList();

                if (resultado.Count == 0)
                    return Content(HttpStatusCode.NotFound, "No se encontraron pesajes para esta placa.");

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }

    }
}
