﻿using AulaNosaApp.DTO;
using AulaNosaApp.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AulaNosaApp.Servicios
{
    public class AlumnoApi
    {
        public static List<AlumnoDTO> ListarAlumnos()
        {
            var cliente = new RestClient(Constantes.client);
            //cliente.AddDefaultHeader("Authorization", string.Format("Bearer {0}", App.Current.Properties["token"]));
            var request = new RestRequest("api/alumno/all", Method.Get);
            var response = cliente.Execute<List<AlumnoDTO>>(request);
            var apiResponse = response.Data;
            return apiResponse;
        }
    }
}
