using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Normas.Web.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Normas.Web.Client;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Normas.Web.Client.Controllers
{
    public class NormasController : Controller
    {
        private readonly HttpClient _httpClient;

        public NormasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        // GET: NormasController
        public async Task<IActionResult> Index()
        {
            var request = await _httpClient.GetAsync($"http://localhost:5000/normas");

            request.EnsureSuccessStatusCode();
            var model = JsonSerializer.Deserialize<List<NormaVM>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return View(model);
        }

        // GET: NormasController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _httpClient.GetAsync($"http://localhost:5000/normas/{id}");
            request.EnsureSuccessStatusCode();

            var modelo = JsonSerializer.Deserialize<NormaVM>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            if (modelo == null)
            {
                return NotFound();
            }


            return View(modelo);
        }

        // GET: NormasController/Create
        public async Task<IActionResult> Create()
        {
            var datos = await GetRequisitos();
            return View();
        }

        // POST: NormasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NormaVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var modelJSON = new StringContent(
                    JsonSerializer.Serialize(model, new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    Encoding.UTF8,
                    "application/json");

                using var httpResponse =
                    await _httpClient.PostAsync("http://localhost:5000/normas", modelJSON);
                httpResponse.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Sucedio un error en la creacion del registro");
                return View(model);
            }
        }

        // GET: NormasController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _httpClient.GetAsync($"http://localhost:5000/normas/{id}");
            request.EnsureSuccessStatusCode();

            var modelo = JsonSerializer.Deserialize<NormaVM>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
           
            var datos = await GetRequisitos(modelo.TipoRequisitoId); /** lista de requisitos***/
            if (modelo == null)
            {
                return NotFound();
            }


            return View(modelo);
        }

        // PUT: NormasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NormaVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var modelJSON = new StringContent(
                    JsonSerializer.Serialize(model, new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }),
                    Encoding.UTF8,
                    "application/json");

                using var httpResponse =
                    await _httpClient.PutAsync($"http://localhost:5000/normas/{id}", modelJSON);
                httpResponse.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Sucedio un error en la creacion del registro");
                return View(model);
            }
    }

        // GET: NormasController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var request = await _httpClient.GetAsync($"http://localhost:5000/normas/{id}");
            request.EnsureSuccessStatusCode();

            var modelo = JsonSerializer.Deserialize<NormaVM>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
            var datos = await GetRequisitos(modelo.TipoRequisitoId); /** lista de requisitos***/
            if (modelo == null)
            {
                return NotFound();
            }


            return View(modelo);
        }

        // POST: NormasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, NormaVM model)
        {
            try
            {
                var request = await _httpClient.DeleteAsync($"http://localhost:5000/normas/{id}");
                request.EnsureSuccessStatusCode(); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        public async Task<IEnumerable<TipoRequisitoVM>> GetRequisitos(object selectedRequisito = null)
        {
            var httpResponse = await _httpClient.GetAsync($"http://localhost:5000/requisitos");
            httpResponse.EnsureSuccessStatusCode();

            var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync();

            var model = await JsonSerializer.DeserializeAsync<List<TipoRequisitoVM>>(httpResponseStream,
                _jsonSerializerOptions);

            ViewBag.TipoRequisitoId = new SelectList(model,
                "TipoRequisitoId",
                "Descripcion",
                selectedRequisito);

            return model;
        }
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
