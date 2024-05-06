using DAL;
using Entity;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EventManagement.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class TaskController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TaskController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult CreateTask([FromBody] TaskDTO input)
        {
            if (ModelState.IsValid)
            {
                if (input.Type == DTO.TaskType.Count)
                {
                    CountTask temp = new CountTask
                    {
                        Type = (Entity.TaskType)input.Type,
                        maxCount = (int)input.MaxCount,
                        Count = 0,
                        Description = input.Description,
                        Deadline = input.Deadline
                    };
                    _unitOfWork.CountTaskRepository.Insert(temp);
                }
                else if (input.Type == DTO.TaskType.Check)
                {
                    CheckTask temp = new CheckTask
                    {
                        Type = (Entity.TaskType)input.Type,
                        IsCompleted = false,
                        Description = input.Description,
                        Deadline = input.Deadline
                    };
                    _unitOfWork.CheckTaskRepository.Insert(temp);
                }
            }
            return Ok(new { message = "Task Created" });
        }
        [HttpPost]
        [Route("delete")]
        public IActionResult RemoveTask(TaskDTO input)
        {
            if (ModelState.IsValid)
            {
                if (input.Type == DTO.TaskType.Count)
                {
                    CountTask finder = _unitOfWork.CountTaskRepository.Get(s => s.maxCount == input.MaxCount && s.Description == input.Description && s.Deadline == input.Deadline).FirstOrDefault();
                    if (finder != null)
                    {
                        _unitOfWork.CountTaskRepository.Delete(ObjectId.Parse(finder.Id));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            return Ok();
        }
        [HttpPost]
        [Route("edit")]
        public IActionResult UpdateTask(TaskDTO input)
        {
            if (ModelState.IsValid)
            {
                CountTask finder = _unitOfWork.CountTaskRepository.Get(s => s.maxCount == input.MaxCount && s.Description == input.Description && s.Deadline == input.Deadline).FirstOrDefault();
                if (finder != null)
                {
                    _unitOfWork.CountTaskRepository.Update(finder);
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }
    }
}
