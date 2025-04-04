using AgenciaViasDeAntioquia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AgenciaViasDeAntioquia.Classes
{
    public class clsFotoPesaje
    {
        private DBExamenEntities3 dbExamen = new DBExamenEntities3();

        public FotoPesaje fotopesaje { get; set; }
        public string idFotoPesaje { get; set; }
        public List<string> Archivos { get; set; }
        public string GrabarImagenes()
        {
            try
            {
                if (Archivos.Count > 0)
                {
                    foreach (string Archivo in Archivos)
                    {
                        FotoPesaje Imagen = new FotoPesaje();
                        Imagen.idFotoPesaje = Convert.ToInt32(idFotoPesaje);
                        Imagen.ImagenVehiculo = Archivo;
                        dbExamen.FotoPesaje.Add(Imagen);
                        dbExamen.SaveChanges();
                    }
                    return "Imagenes guardadas correctamente";
                }
                else
                {
                    return "No se enviaron archivos para guardar";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
