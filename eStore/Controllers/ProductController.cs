using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Products.GetAll();
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null) return NotFound();

            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            _unitOfWork.Products.Add(product);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, productDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDTO productDTO)
        {
            if (id != productDTO.ProductId) return BadRequest();

            var product = _unitOfWork.Products.GetById(id);
            if (product == null) return NotFound();

            _mapper.Map(productDTO, product);
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null) return NotFound();

            _unitOfWork.Products.Delete(product);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}