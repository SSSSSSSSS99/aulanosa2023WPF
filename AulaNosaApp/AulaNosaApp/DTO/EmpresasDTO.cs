using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AulaNosaApp.DTO
{
    public class EmpresasDTO
    {
        public int id { get; set; }
        public int idCurso { get; set; }
        public int idEstudios { get; set; }
        public string nombre { get; set; }
        public string direccionSocial { get; set; }
        public string direccionTrabajo { get; set; }
        public string cif { get; set; }
        public string representante { get; set; }
        public string contacto { get; set; }
        public string tutor1 { get; set; }
        public string tutor2 { get; set; }
        public string tutor3 { get; set; }
        public char convenio { get; set; }
        public char planIndividual { get; set; }
        public char hojaActividades { get; set; }
    }
}
