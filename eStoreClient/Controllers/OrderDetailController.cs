using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Hiển thị danh sách OrderDetail
        public async Task<IActionResult> Index(int orderId)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.GetAsync($"OrderDetail?orderId={orderId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orderDetails = JsonSerializer.Deserialize<List<OrderDetail>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                ViewBag.OrderId = orderId;
                return View(orderDetails);
            }

            return View(new List<OrderDetail>());
        }

        // Hiển thị form tạo OrderDetail
        public IActionResult Create(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        // Xử lý tạo OrderDetail
        [HttpPost]
        public async Task<IActionResult> Create(OrderDetail orderDetail)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PostAsJsonAsync("OrderDetail", orderDetail);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), new { orderId = orderDetail.OrderId });
            }

            ViewBag.OrderId = orderDetail.OrderId;
            return View(orderDetail);
        }

        // Hiển thị form chỉnh sửa OrderDetail
        public async Task<IActionResult> Edit(int orderId, int productId)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.GetAsync($"OrderDetail/{orderId}/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orderDetail = JsonSerializer.Deserialize<OrderDetail>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(orderDetail);
            }

            return NotFound();
        }

        // Xử lý chỉnh sửa OrderDetail
        [HttpPost]
        public async Task<IActionResult> Edit(OrderDetail orderDetail)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.PutAsJsonAsync($"OrderDetail/{orderDetail.OrderId}/{orderDetail.ProductId}", orderDetail);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), new { orderId = orderDetail.OrderId });
            }

            return View(orderDetail);
        }

        // Xóa OrderDetail
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var client = _httpClientFactory.CreateClient("eStore");
            var response = await client.DeleteAsync($"OrderDetail/{orderId}/{productId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), new { orderId });
            }

            return NotFound();
        }
    }
}