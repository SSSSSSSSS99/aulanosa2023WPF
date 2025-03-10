﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaNosaApp.DTO
{
    public class MatriculaDTO
    {
        public int id {  get; set; }
        public String factura { get; set; }
        public String nombre { get; set; }
        public String nif { get; set; }
        public int cuota { get; set; }
        public int matricula { get; set; }
        public int idCurso { get; set; }
        public String observaciones { get; set; }
        public DateTime? fechaInicio { get; set; }
        public int idUsuario { get; set; }
        public DateTime? fecha { get; set; }
    }
}
