using DAL;
using Entity.Chat;
using EventManagement.DTO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EventManagement.Controllers
{
    [Route("api/phases")]
    [ApiController]
    public class PhaseController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PhaseController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePhase([FromBody] PhaseDTO phaseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity
            var phase = new Phase
            {
                Name = phaseDto.Name,
                EventId = phaseDto.EventId
            };

            try
            {
                _unitOfWork.PhaseRepository.Insert(phase);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log error
                return BadRequest(new
                {
                    stacktrace = ex.StackTrace,
                    message = ex.Message,
                });
            }
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditPhase(string id, [FromBody] PhaseDTO phaseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPhase = GetPhase(id);
            if (existingPhase == null)
            {
                return NotFound();
            }

            // Update properties
            existingPhase.Name = phaseDto.Name;
            existingPhase.EventId = phaseDto.EventId;

            try
            {
                _unitOfWork.PhaseRepository.Update(existingPhase);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log error
                return BadRequest(new
                {
                    stacktrace = ex.StackTrace,
                    message = ex.Message,
                });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeletePhase(string id)
        {
            Phase existingPhase = GetPhase(id);
            if (existingPhase == null)
            {
                return NotFound();
            }

            try
            {
                _unitOfWork.PhaseRepository.Delete(existingPhase.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log error
                return BadRequest(new
                {
                    ex.StackTrace,
                    ex.Message,
                });
            }
        }

        private Phase GetPhase(string id)
        {
            var idfinder = ObjectId.Parse(id);
            var existingPhase = _unitOfWork.PhaseRepository.Get(s => s.Id == idfinder).FirstOrDefault();
            return existingPhase;
        }


    }
}
