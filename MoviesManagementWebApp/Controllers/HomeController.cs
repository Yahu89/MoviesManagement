using Microsoft.AspNetCore.Mvc;
using MoviesManagementApi.Dto;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace MoviesManagementWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var api = "https://localhost:7106/api/Movie/list";

            var response = await _httpClient.GetAsync(api);

            if (!response.IsSuccessStatusCode)
            {
                return View(nameof(Error));
            }

            var movies = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<MovieDto>>(movies);

            return View(list);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var api = "https://localhost:7106/api/Movie/create";

            var dataToSend = JsonConvert.SerializeObject(dto);
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(api, content);

            if (!response.IsSuccessStatusCode)
            {
                return Json(new { redirectToUrl = Url.Action(nameof(Error)) });
            }

            return Json(new {redirectToUrl = Url.Action(nameof(Index))});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var api = $"https://localhost:7106/api/Movie/{id}";
            var response = await _httpClient.GetAsync(api);

            if (!response.IsSuccessStatusCode)
            {
                return View(nameof(Error));
            }

            var movie = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MovieDto>(movie);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), dto);
            }

            var api = $"https://localhost:7106/api/Movie/edit/{dto.Id}";

            var dataToSend = JsonConvert.SerializeObject(dto);
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(api, content);

            if (!response.IsSuccessStatusCode)
            {
                return View(nameof(Error));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var api = $"https://localhost:7106/api/Movie/{id}";
            var response = await _httpClient.GetAsync(api);

            if (!response.IsSuccessStatusCode)
            {
                return View(nameof(Error));
            }

            var movie = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MovieDto>(movie);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var api = $"https://localhost:7106/api/Movie/delete/{id}";
            var dataToSend = JsonConvert.SerializeObject(id);
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.DeleteAsync(api);

            if (!response.IsSuccessStatusCode)
            {
                return View(nameof(Error));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetExternalMovies()
        {
            var apiGet = "https://filmy.programdemo.pl/MyMovies";
            var response = await _httpClient.GetAsync(apiGet);

            var movies = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<MovieDto>>(movies);

            var apiPost = "https://localhost:7106/api/Movie/createrange";

            var dataToSend = JsonConvert.SerializeObject(list);
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            HttpResponseMessage responsePost = await _httpClient.PostAsync(apiPost, content);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
