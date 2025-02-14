using BusinessObject.Models;
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
            var response = await client.GetAsync("members");

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
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PostAsJsonAsync("members", member);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Failed to create member: {errorContent}");
            return View(member);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.GetAsync($"members/{id}");

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
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PutAsJsonAsync($"members/{member.MemberId}", member);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Failed to update member: {errorContent}");
            return View(member);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.DeleteAsync($"members/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}