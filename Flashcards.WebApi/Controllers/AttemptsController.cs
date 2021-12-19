using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flashcards_backend.Core.Filtering;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Flashcards.WebApi.Dtos.Attempt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AttemptsController : ControllerBase
    {
        private readonly IAttemptService _service;

        public AttemptsController(IAttemptService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Attempt>> Get([FromQuery] int userId, int cardId, int quantity)
        {
            try
            {
                return Ok(_service.Get(userId, cardId, quantity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("{userId}")]
        public ActionResult<List<Activity>> GetActivities(int userId, [FromQuery] Filter filter)
        {
            try
            {
                return Ok(_service.GetForUser(userId, filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Attempt> Create([FromBody] PostAttemptDto attempt)
        {
            if (attempt == null)
                throw new InvalidDataException("attempt cannot be null");
            try
            {
                return Ok(_service.Create(new Attempt
                {
                    CardId = attempt.CardId,
                    UserId = attempt.UserId,
                    Date = attempt.Date,
                    WasCorrect = attempt.WasCorrect
                }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}