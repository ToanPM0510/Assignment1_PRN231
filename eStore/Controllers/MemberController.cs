using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MembersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var members = _unitOfWork.Members.GetAll();
            var memberDTOs = _mapper.Map<IEnumerable<MemberDTO>>(members);
            return Ok(memberDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var member = _unitOfWork.Members.GetById(id);
            if (member == null) return NotFound();

            var memberDTO = _mapper.Map<MemberDTO>(member);
            return Ok(memberDTO);
        }

        [HttpPost]
        public IActionResult Create(MemberDTO memberDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = _mapper.Map<Member>(memberDTO);
            _unitOfWork.Members.Add(member);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = member.MemberId }, memberDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, MemberDTO memberDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMember = _unitOfWork.Members.GetById(id);
            if (existingMember == null) return NotFound();

            _mapper.Map(memberDTO, existingMember);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = _unitOfWork.Members.GetById(id);
            if (member == null) return NotFound();

            _unitOfWork.Members.Delete(member);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}