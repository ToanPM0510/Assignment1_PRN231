using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.GetAsync("Order");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<List<Order>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(orders);
            }

            return View(new List<Order>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PostAsJsonAsync("Order", order);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.GetAsync($"Order/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var order = JsonSerializer.Deserialize<Order>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(order);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PutAsJsonAsync($"Order/{order.OrderId}", order);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.DeleteAsync($"Order/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}