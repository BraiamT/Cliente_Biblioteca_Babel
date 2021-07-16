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
    public class LocalizacionController : Controller
    {
        // URL Base de servicio REST
        string baseUrl = "https://localhost:44388/api/";

        public async Task<ActionResult> Index()
        {
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

            ViewBag.locations = localizaciones;
            return View();
        }

        [HttpPost]
        public ActionResult Crear(LocalizacionModel localizacion)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                var postTask = cliente.PostAsJsonAsync<LocalizacionModel>("Localizacion/Add", localizacion);
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
            List<LocalizacionModel> localizaciones = new List<LocalizacionModel>();
            ResponseModelLocation responseModelLocation = new ResponseModelLocation();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync("Localizacion/Get/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    responseModelLocation = JsonConvert.DeserializeObject<ResponseModelLocation>(data);
                    localizaciones = responseModelLocation.data;
                }
            }
            return View(localizaciones[0]);
        }

        [HttpPost]
        public ActionResult Edit(LocalizacionModel localizacion)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                var putTask = cliente.PutAsJsonAsync<LocalizacionModel>("Localizacion/Edit", localizacion);
                putTask.Wait();
                var response = putTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View(localizacion);
        }

        public async Task<ActionResult> Delete(int Id)
        {
            List<LocalizacionModel> localizaciones = new List<LocalizacionModel>();
            ResponseModelLocation responseModelLocation = new ResponseModelLocation();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync("Localizacion/Get/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    responseModelLocation = JsonConvert.DeserializeObject<ResponseModelLocation>(data);
                    localizaciones = responseModelLocation.data;
                }
            }
            ViewBag.ID = localizaciones[0].Id;
            return View(localizaciones[0]);
        }

        [HttpPost]
        public ActionResult Delete(short Id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                var deleteTask = cliente.DeleteAsync("Localizacion/Delete/" + Id);
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