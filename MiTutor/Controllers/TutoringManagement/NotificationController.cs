using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MiTutor.Models;
using MiTutor.Services;
using MiTutor.Services.TutoringManagement;

namespace MiTutor.Controllers.TutoringManagement
{
	[ApiController]
	[Route("api/notification")]
	public class NotificationController : ControllerBase
	{
		private readonly NotificationService _notificationService;

		public NotificationController(NotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpPost("cancel")]
		public IActionResult CreateCancellationNotification([FromBody] CancellationNotificationRequest request)
		{
			try
			{
				// Verificar si la notificación es para el tutor o el alumno
				if (request.IsTutorCancellation)
				{
					_notificationService.CreateTutorCancellationNotification(request.TutorId, request.Message);
				}
				else
				{
					_notificationService.CreateStudentCancellationNotification(request.StudentIds, request.Message);
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error al crear la notificación: {ex.Message}");
			}
		}

        [HttpPost("crearCancelNotificacionByAppointmentId/{AppointmentId}")]
        public IActionResult CreateNotification(int AppointmentId)
        {
            try
            {
				_notificationService.crearNotificacionDeCancelacionPorAppointmentId(AppointmentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la notificación: {ex.Message}");
            }
        }
    }

	public class CancellationNotificationRequest
	{
		public bool IsTutorCancellation { get; set; }
		public int TutorId { get; set; }
		public List<int> StudentIds { get; set; }
		public string Message { get; set; }
	}
}