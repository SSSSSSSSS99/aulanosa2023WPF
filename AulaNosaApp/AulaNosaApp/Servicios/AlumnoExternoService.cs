﻿using AulaNosaApp.DTO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AulaNosaApp.Servicios
{
    public class AlumnoExternoService
    {
        List<AlumnoExternoDTO> listaAlumnos = new List<AlumnoExternoDTO>();

        internal static string AgregarAlumnoExterno(AlumnoExternoDTO alumnoextDTO)
        {
            string resultado = "Se ha producido un error no controlado";
            var client = new RestClient("http://localhost:8080");
            //client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", App.Current.Properties["token"]));
            var request = new RestRequest("/api/alumnoExterno/", Method.Post);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(JsonSerializer.Serialize(alumnoextDTO));
            var response = client.Execute(request);

            if (response != null)
            {
                resultado = "";
            }
            else
            {
                //  Temporal - Falta que WS devuelva un ErrorDTO
                //  ErrorDTO? error = JsonSerializer.Deserialize<ErrorDTO>(response.Content);
                //  if ((error != null) && (error.mensaje != null))
                //  {
                resultado = "Se ha producido un error";
                //  }
            }

            return resultado;
        }
    }
}
