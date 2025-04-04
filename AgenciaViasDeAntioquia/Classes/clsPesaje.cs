using AgenciaViasDeAntioquia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AgenciaViasDeAntioquia.Classes
{
    public class clsPesaje
    {

        private DBExamenEntities3 dbExamen = new DBExamenEntities3();

        public Pesaje pesaje {  get; set; }

        public string Insertar()
        {
            try
            {
                dbExamen.Pesaje.Add(pesaje);
                dbExamen.SaveChanges();
                return "Pesaje registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar el pesaje" + ex.Message;
            }
        }
        public string Actualizar()
        {
            Pesaje Pesaje = Consultar(pesaje.id);
            if (Pesaje == null)
            {
                return "El ID del pesaje no es valido";
            }
            dbExamen.Pesaje.AddOrUpdate(pesaje);
            dbExamen.SaveChanges();
            return "El pesaje ha sido actualizado correctamente";
        }

        public Pesaje Consultar(int id)
        {
            Pesaje pesaje = dbExamen.Pesaje.FirstOrDefault(e => e.id == id);
            return pesaje;
        }

        public List<Pesaje> ConsultarTodos()
        {
            return dbExamen.Pesaje.OrderBy(e => e.id).ToList();
        }

        public string Eliminar(int id)
        {
            try
            {
                Pesaje Pesaje = Consultar(id);
                if (Pesaje == null)
                {
                    return "El ID del pesaje no es valido";
                }

                dbExamen.Pesaje.Remove(Pesaje);
                dbExamen.SaveChanges();
                return "El pesaje ha sido eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el pesaje:" + ex.Message;
            }
        }
    }
}
