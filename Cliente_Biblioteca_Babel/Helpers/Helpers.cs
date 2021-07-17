using Cliente_Biblioteca_Babel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cliente_Biblioteca_Babel.Helpers
{
    public class Helpers
    {
        

        public static async Task<List<LocalizacionModel>> ObtenerLocalizaciones()
        {
            // URL Base de servicio REST
            string baseUrl = "https://localhost:44388/api/";

            List<LocalizacionModel> localizaciones = new List<LocalizacionModel>();
            ResponseModelLocation responseModelLocation = new ResponseModelLocation();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Llamada al servicio, método GET para obtener todas las localizaciones
                HttpResponseMessage response = await cliente.GetAsync("Localizacion/Get");
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    responseModelLocation = JsonConvert.DeserializeObject<ResponseModelLocation>(data);
                    localizaciones = responseModelLocation.data;
                }
            }

            return localizaciones;
        }
    }
}