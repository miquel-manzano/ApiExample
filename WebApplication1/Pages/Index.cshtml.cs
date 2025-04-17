using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApplication1.Model;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public List<FilmGetDTO> Films { get; set; } = new List<FilmGetDTO>();

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("ApiFilms");

            try
            {
                //var response = await client.GetAsync("api/Films/hi");
                var response = await client.GetAsync("api/Films");
                //var response = await client.GetFromJsonAsAsyncEnumerable<List<Film>>("Films",);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error de carrega de dades de la llista Films");
                }
                else
                {
                    //_logger.LogError(await response.Content.ReadAsStringAsync());
                    var json = await response.Content.ReadAsStringAsync();
                    Films = JsonSerializer.Deserialize<List<FilmGetDTO>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //Agafem el token
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token)) return RedirectToPage("/Login");

            //Obliguem al HttpClient a enviar el token en el Header:
            var client = _httpClientFactory.CreateClient("ApiFilms");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"/api/Films/delete/{id}");

            //En aquest cas comprobem si la resposta es BadRequest
            if (response == null || !(response.StatusCode == HttpStatusCode.BadRequest))
            {
                _logger.LogError("No s'ha eliminat l'element. BadRequest response");
            }

            return RedirectToPage();
        }
    }
}
