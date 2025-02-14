using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class MemberController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MemberController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.GetAsync("Members");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var members = JsonSerializer.Deserialize<List<Member>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(members);
            }

            return View(new List<Member>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Member member)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PostAsJsonAsync("Members", member);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.GetAsync($"Members/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var member = JsonSerializer.Deserialize<Member>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(member);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Member member)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PutAsJsonAsync($"Members/{member.MemberId}", member);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.DeleteAsync($"Members/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}