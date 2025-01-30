using MessagerieApp.Business.Interfaces;
using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Notification>> GetAllNotifications()
        {
            return Ok(_service.GetAllNotifications());
        }

        [HttpGet("{id}")]
        public ActionResult<Notification> GetNotificationById(int id)
        {
            var notification = _service.GetNotificationById(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost]
        public ActionResult AddNotification([FromBody] Notification notification)
        {
            _service.AddNotification(notification);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
        }

        [HttpPut("mark-as-read/{id}")]
        public ActionResult MarkAsRead(int id)
        {
            _service.MarkAsRead(id);
            return NoContent();
        }
    }
}
