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

namespace Cliente_Biblioteca_Babel.Controllers
{
    public class VolumenController : Controller
    {
        // URL Base de servicio REST
        string baseUrl = "https://localhost:44388/api/";

        public async Task<ActionResult> Index()
        {
            List<VolumenModel> volumenes = new List<VolumenModel>();
            ResponseModelVols responseModelVols = new ResponseModelVols();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Llamada al servicio, método GET para obtener todos los volúmenes
                HttpResponseMessage response = await cliente.GetAsync("Volumen/Get");
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    responseModelVols = JsonConvert.DeserializeObject<ResponseModelVols>(data);
                    volumenes = responseModelVols.data;
                }
            }

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

            ViewBag.localizaciones = localizaciones;
            ViewBag.volumenes = volumenes;
            return View();
        }

        [HttpPost]
        public ActionResult Crear(VolumenModel volumen)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                var postTask = cliente.PostAsJsonAsync<VolumenModel>("Volumen/Add", volumen);
                postTask.Wait();
                var response = postTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error");
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            List<VolumenModel> vols = new List<VolumenModel>();
            ResponseModelVols responseModelVols = new ResponseModelVols();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync("Volumen/Get/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    responseModelVols = JsonConvert.DeserializeObject<ResponseModelVols>(data);
                    vols = responseModelVols.data;
                }
            }

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

            ViewBag.localizaciones = localizaciones;

            return View(vols[0]);
        }

        [HttpPost]
        public ActionResult Edit(VolumenModel volumen)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                var putTask = cliente.PutAsJsonAsync<VolumenModel>("Volumen/Edit", volumen);
                putTask.Wait();
                var response = putTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View(volumen);
        }

        public async Task<ActionResult> Delete(int Id)
        {
            List<VolumenModel> vols = new List<VolumenModel>();
            ResponseModelVols responseModelVols = new ResponseModelVols();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync("Volumen/Get/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    responseModelVols = JsonConvert.DeserializeObject<ResponseModelVols>(data);
                    vols = responseModelVols.data;
                }
            }
            ViewBag.ID = vols[0].Id;
            return View(vols[0]);
        }

        [HttpPost]
        public ActionResult Delete(short Id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                var deleteTask = cliente.DeleteAsync("Volumen/Delete/" + Id);
                deleteTask.Wait();
                var response = deleteTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error");
            return RedirectToAction("Index");
        }
    }
}