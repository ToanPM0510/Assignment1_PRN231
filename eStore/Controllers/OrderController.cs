﻿using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _unitOfWork.Orders.GetAll();
            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return Ok(orderDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null) return NotFound();

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return Ok(orderDTO);
        }

        [HttpPost]
        public IActionResult Create(OrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDTO);
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, orderDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingOrder = _unitOfWork.Orders.GetById(id);
            if (existingOrder == null) return NotFound();

            _mapper.Map(orderDTO, existingOrder);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null) return NotFound();

            _unitOfWork.Orders.Delete(order);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}