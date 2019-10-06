using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LP2PracticaGrupal.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}