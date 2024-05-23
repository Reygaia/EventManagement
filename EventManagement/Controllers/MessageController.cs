using DAL;
using Entity.Chat;
using EventManagement.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace EventManagement.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UnitOfWork _unitOfWork;

        public MessageController(IHttpContextAccessor contextAccessor, UnitOfWork UnitOfWork)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = UnitOfWork;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO message)
        {
            if (ModelState.IsValid)
            {
                var newMessage = new ApplicationMessage
                {
                    Message = message.ContentMessage,
                    MessageBy = ObjectId.Parse(GetUserId()),
                    MessageCreated = DateTime.Now,
                };
                _unitOfWork.MessageRepository.Insert(newMessage);
            }
            return Ok(new
            {
                Response = "message successfully sent"
            });
        }

        [HttpPut]
        [Route("edit/{id}")]
        [Authorize]
        public async Task<IActionResult> EditMessage(string id, [FromBody] MessageDTO message)
        {
            if (ModelState.IsValid)
            {
                var resultMessage = GetMessage(id);
                if (resultMessage.MessageBy == ObjectId.Parse(GetUserId()))
                {
                    resultMessage.Message = message.ContentMessage;
                    resultMessage.IsEdited = true;
                    _unitOfWork.MessageRepository.Update(resultMessage);

                    return Ok(new
                    {
                        message = "edit successfully",
                        newMessage = message.ContentMessage,
                    });
                }
                return Unauthorized(new
                {
                    message = "you don't own this message"
                });
            }
            return NotFound(new
            {
                message = "message not found",
            });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(string id)
        {
            var resultMessage = GetMessage(id);
            if(resultMessage.MessageBy == ObjectId.Parse(GetUserId()))
            {
                _unitOfWork.MessageRepository.Delete(resultMessage.Id);
                return Ok(new
                {
                    response = "Message removed"
                });
            }

            return Unauthorized(new
            {
                result = "this is not your message"
            });
        }

        private ApplicationMessage GetMessage(string id)
        {
            var idfinder = ObjectId.Parse(id);
            var resultMessage = _unitOfWork.MessageRepository.Get(s => s.Id == idfinder).FirstOrDefault();
            return resultMessage;
        }

        private string GetUserId()
        {
            var user = _contextAccessor.HttpContext.User;
            var id = user.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            return id;
        }
    }
}
