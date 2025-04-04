using AgenciaViasDeAntioquia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AgenciaViasDeAntioquia.Classes
{
    public class clsCamion
    {

        private DBExamenEntities3 dbExamen = new DBExamenEntities3();

        public Camion camion { get; set; }

        public string Insertar()
        {
            try
            {
                dbExamen.Camion.Add(camion);
                dbExamen.SaveChanges();
                return "Camion registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar el camion" + ex.Message;
            }
        }
        public string Actualizar()
        {
            Camion Camion = Consultar(camion.Placa);
            if (Camion == null)
            {
                return "El ID del camion no es valido";
            }
            dbExamen.Camion.AddOrUpdate(camion);
            dbExamen.SaveChanges();
            return "El camion ha sido actualizado correctamente";
        }

        public Camion Consultar(string Placa)
        {
            Camion camion = dbExamen.Camion.FirstOrDefault(e => e.Placa == Placa);
            return camion;
        }

    }

}
