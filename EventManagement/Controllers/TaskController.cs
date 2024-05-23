using DAL;
using Entity.Task;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace EventManagement.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class TaskController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public TaskController(UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _HttpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("tasktestadd")]
        [Authorize]
        public async Task<IActionResult> AddTestTasks()
        {
            try
            {
                string json = @"[
                    {""Description"":""Write an article about the event"",""Type"":""ContentCreation"",""Deadline"":""2024-06-01T00:00:00Z"",""EventId"":""60d5ec49f92d2b398c5d4b29"",""PhaseId"":""60d5ec49f92d2b398c5d4b2a"",""ContentType"":""Article"",""AssignedTo"":""John Doe""},
                    {""Description"":""Arrange transportation for the VIP guests"",""Type"":""Logistics"",""Deadline"":""2024-06-01T00:00:00Z"",""EventId"":null,""PhaseId"":""60d5ec49f92d2b398c5d4b2b"",""TransportationDetails"":""5 luxury cars""},
                    {""Description"":""Launch a social media campaign"",""Type"":""Marketing"",""Deadline"":""2024-06-01T00:00:00Z"",""EventId"":""60d5ec49f92d2b398c5d4b2c"",""PhaseId"":""60d5ec49f92d2b398c5d4b2d"",""CampaignType"":""Social Media""},
                    {""Description"":""Book the main hall for the event"",""Type"":""Planning"",""Deadline"":""2024-06-01T00:00:00Z"",""EventId"":""60d5ec49f92d2b398c5d4b2e"",""PhaseId"":""60d5ec49f92d2b398c5d4b2f"",""Venue"":""Main Hall""},
                    {""Description"":""Collect 100 survey responses"",""Type"":""Quota"",""Deadline"":""2024-06-01T00:00:00Z"",""EventId"":null,""PhaseId"":""60d5ec49f92d2b398c5d4b30"",""MaxCount"":100},
                    {""Description"":""Register expected attendees"",""Type"":""Registration"",""Deadline"":""2024-06-01T00:00:00Z"",""EventId"":""60d5ec49f92d2b398c5d4b31"",""PhaseId"":""60d5ec49f92d2b398c5d4b32"",""ExpectedAttendees"":500},
                    {""Description"":""Coordinate with the catering vendor"",""Type"":""VendorCoordination"",""Deadline"":""2024-06-01T00:00:00Z"",""EventId"":""60d5ec49f92d2b398c5d4b33"",""PhaseId"":""60d5ec49f92d2b398c5d4b34"",""VendorName"":""Gourmet Catering"",""VendorService"":""Catering""}
                ]";

                var tasks = JsonConvert.DeserializeObject<List<TaskDTO>>(json);
                foreach (var item in tasks)
                {
                    item.CreatedBy = GetUserId();
                    var task = CreateTaskOnType(item);
                    _unitOfWork.BaseTaskRepository.Insert(task);
                }
                return Ok(new
                {
                    response = "add succesfully",
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    stacktrace = ex.StackTrace,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    input.CreatedBy = GetUserId();
                    var task = CreateTaskOnType(input);
                    _unitOfWork.BaseTaskRepository.Insert(task);
                    return Ok(new { message = "Task Created" });
                }
                else
                {
                    return BadRequest(new
                    {
                        response = "failed to create"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    stackTrace = ex.StackTrace,
                    error = ex.Message,
                });
            }

        }

        #region userid
        private string GetUserId()
        {
            var user = _HttpContextAccessor.HttpContext.User;
            string userid = user.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            return userid;
        }
        #endregion

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveTask(string id)
        {
            var idfinder = ObjectId.Parse(id);
            var result = _unitOfWork.BaseTaskRepository.Get(s => s.Id == idfinder).FirstOrDefault();

            if (result is null)
            {
                return NotFound(new
                {
                    response = "Task was not found"
                });
            }

            if (GetUserId() is null)
            {
                _unitOfWork.BaseTaskRepository.Delete(result.Id);
                return Ok(new
                {
                    response = "delete successfull"
                });
            }

            return Unauthorized(new
            {
                message = "you cannot delete this"
            });
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> UpdateTask(TaskDTO input)
        {
            if (ModelState.IsValid)
            {
                QuotaTask finder = _unitOfWork.QuotaTaskRepository.Get(s => s.maxCount == input.MaxCount && s.Description == input.Description && s.Deadline == input.Deadline).FirstOrDefault();
                if (finder != null)
                {
                    _unitOfWork.QuotaTaskRepository.Update(finder);
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public ActionResult<IEnumerable<BaseTask>> GetAllTask()
        {
            try
            {
                var tasks = _unitOfWork.BaseTaskRepository.Get(_ => true).ToList();

                var countList = tasks.OfType<QuotaTask>().ToList();

                /*var allTasks = checkTasks.Cast<BaseTask>().Concat(countTasks.Cast<BaseTask>());*/
                return Ok(new
                {
                    message = "api is called",
                    count = countList,
                });
            }
            catch (InvalidCastException ex)
            {
                // Log the error
                Debug.WriteLine("InvalidCastException: " + ex.Message);
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);

                // Handle the error
                // For example, return an error response
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log other types of exceptions
                Debug.WriteLine("Exception: " + ex.Message);
                Debug.WriteLine("Stack Trace: " + ex.StackTrace);

                // Handle other types of exceptions
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        #region task creation

        private BaseTask CreateTaskOnType(TaskDTO dto)
        {
            switch (dto.Type)
            {
                case TaskType.ContentCreation:
                    return ConvertToContentCreationTask(dto);
                case TaskType.Logistics:
                    return ConvertToLogisticsTask(dto);
                case TaskType.Marketing:
                    return ConvertToMarketingTask(dto);
                case TaskType.Planning:
                    return ConvertToPlanningTask(dto);
                case TaskType.Quota:
                    return ConvertToQuotaTask(dto);
                case TaskType.Registration:
                    return ConvertToRegistrationTask(dto);
                case TaskType.VendorCoordination:
                    return ConvertToVendorCoordinationTask(dto);
                default:
                    throw new ArgumentException("Invalid TaskType", nameof(dto.Type));
            }
        }

        private ContentCreationTask ConvertToContentCreationTask(TaskDTO dto)
        {
            return new ContentCreationTask
            {
                Description = dto.Description,
                Deadline = dto.Deadline,
                ContentType = dto.ContentType ?? string.Empty,
                AssignedTo = dto.AssignedTo ?? string.Empty,
                EventId = string.IsNullOrEmpty(dto.EventId) ? ObjectId.Empty : ObjectId.Parse(dto.EventId),
                PhaseId = string.IsNullOrEmpty(dto.PhaseId) ? ObjectId.Empty : ObjectId.Parse(dto.PhaseId),
                CreatedBy = Guid.Parse(dto.CreatedBy)
            };
        }

        private LogisticsTask ConvertToLogisticsTask(TaskDTO dto)
        {
            return new LogisticsTask
            {
                Description = dto.Description,
                Deadline = dto.Deadline,
                TransportationDetails = dto.TransportationDetails ?? string.Empty,
                EventId = string.IsNullOrEmpty(dto.EventId) ? ObjectId.Empty : ObjectId.Parse(dto.EventId),
                PhaseId = string.IsNullOrEmpty(dto.PhaseId) ? ObjectId.Empty : ObjectId.Parse(dto.PhaseId),
                CreatedBy = Guid.Parse(dto.CreatedBy)
            };
        }

        private MarketingTask ConvertToMarketingTask(TaskDTO dto)
        {
            return new MarketingTask
            {
                Description = dto.Description,
                Deadline = dto.Deadline,
                CampaignType = dto.CampaignType ?? string.Empty,
                EventId = string.IsNullOrEmpty(dto.EventId) ? ObjectId.Empty : ObjectId.Parse(dto.EventId),
                PhaseId = string.IsNullOrEmpty(dto.PhaseId) ? ObjectId.Empty : ObjectId.Parse(dto.PhaseId),
                CreatedBy = Guid.Parse(dto.CreatedBy)
            };
        }

        private PlanningTask ConvertToPlanningTask(TaskDTO dto)
        {
            return new PlanningTask
            {
                Description = dto.Description,
                Deadline = dto.Deadline,
                Venue = dto.Venue ?? string.Empty,
                EventId = string.IsNullOrEmpty(dto.EventId) ? ObjectId.Empty : ObjectId.Parse(dto.EventId),
                PhaseId = string.IsNullOrEmpty(dto.PhaseId) ? ObjectId.Empty : ObjectId.Parse(dto.PhaseId),
                CreatedBy = Guid.Parse(dto.CreatedBy)
            };
        }

        private QuotaTask ConvertToQuotaTask(TaskDTO dto)
        {
            return new QuotaTask
            {
                Description = dto.Description,
                Deadline = dto.Deadline,
                maxCount = dto.MaxCount ?? 0,
                Count = 0, // Initialize Count to 0 or any other appropriate logic
                EventId = string.IsNullOrEmpty(dto.EventId) ? ObjectId.Empty : ObjectId.Parse(dto.EventId),
                PhaseId = string.IsNullOrEmpty(dto.PhaseId) ? ObjectId.Empty : ObjectId.Parse(dto.PhaseId),
                CreatedBy = Guid.Parse(dto.CreatedBy)
            };
        }

        private RegistrationTask ConvertToRegistrationTask(TaskDTO dto)
        {
            return new RegistrationTask
            {
                Description = dto.Description,
                Deadline = dto.Deadline,
                ExpectedAttendees = dto.ExpectedAttendees ?? 0,
                RegisteredAttendees = 0, // Initialize RegisteredAttendees to 0 or any other appropriate logic
                EventId = string.IsNullOrEmpty(dto.EventId) ? ObjectId.Empty : ObjectId.Parse(dto.EventId),
                PhaseId = string.IsNullOrEmpty(dto.PhaseId) ? ObjectId.Empty : ObjectId.Parse(dto.PhaseId),
                CreatedBy = Guid.Parse(dto.CreatedBy)
            };
        }

        private VendorCoordinationTask ConvertToVendorCoordinationTask(TaskDTO dto)
        {
            return new VendorCoordinationTask
            {
                Description = dto.Description,
                Deadline = dto.Deadline,
                VendorName = dto.VendorName ?? string.Empty,
                VendorService = dto.VendorService ?? string.Empty,
                EventId = string.IsNullOrEmpty(dto.EventId) ? ObjectId.Empty : ObjectId.Parse(dto.EventId),
                PhaseId = string.IsNullOrEmpty(dto.PhaseId) ? ObjectId.Empty : ObjectId.Parse(dto.PhaseId),
                CreatedBy = Guid.Parse(dto.CreatedBy)
            };
        }

        #endregion


    }
}
