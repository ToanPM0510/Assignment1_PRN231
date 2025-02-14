using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orderDetails = _unitOfWork.OrderDetails.GetAll();
            var orderDetailDTOs = _mapper.Map<IEnumerable<OrderDetailDTO>>(orderDetails);
            return Ok(orderDetailDTOs);
        }

        [HttpGet("{orderId}/{productId}")]
        public IActionResult GetById(int orderId, int productId)
        {
            var orderDetail = _unitOfWork.OrderDetails.Find(od => od.OrderId == orderId && od.ProductId == productId).FirstOrDefault();
            if (orderDetail == null) return NotFound();

            var orderDetailDTO = _mapper.Map<OrderDetailDTO>(orderDetail);
            return Ok(orderDetailDTO);
        }

        [HttpPost]
        public IActionResult Create(OrderDetailDTO orderDetailDTO)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDTO);
            _unitOfWork.OrderDetails.Add(orderDetail);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { orderId = orderDetail.OrderId, productId = orderDetail.ProductId }, orderDetailDTO);
        }
    }
}