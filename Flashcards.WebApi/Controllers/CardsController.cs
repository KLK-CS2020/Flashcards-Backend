using System;
using System.Collections.Generic;
using System.IO;
using Flashcards.WebApi.Dtos.Card;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService ?? throw new InvalidOperationException("LOL");
        }


        [HttpGet("{id}")]
        public ActionResult<List<Card>> GetAll([FromQuery] int deckId)
        {
            return Ok(_cardService.GetAllCardsByDeckId(deckId));
        }

        [HttpDelete("{id}")]
        public ActionResult<CardInDeckDto> Delete(int id)
        {
            var card = _cardService.Delete(id);
            var dto = new CardInDeckDto
            {
                Id = card.Id,
                Question = card.Question,
                Answer = card.Answer,
                Correctness = card.Correctness
            };
            return Ok(dto);
        }

        [HttpPut]
        public ActionResult<Card> Update([FromBody] PostCardDto dto)
        {
            if (dto == null)
                throw new InvalidDataException("Card cannot be null");
            if (dto.Question is null or "")
                return BadRequest("Question cannot be empty");
            if (dto.Answer is null or "")
                return BadRequest("Answer cannot be empty");
            
            return Ok(_cardService.Update(new Card
            {
                Question = dto.Question,
                Answer = dto.Answer
            }));
        }

        [HttpPost]
        public ActionResult<Card> Create([FromBody] PostCardDto dto)
        {
            if (dto == null)
                throw new InvalidDataException("Card cannot be null");
            if (dto.Question is null or "")
                return BadRequest("Question cannot be empty");
            if (dto.Answer is null or "")
                return BadRequest("Answer cannot be empty");
            
            return Ok(_cardService.Create(new Card
            {
                Id = dto.Id,
                Question = dto.Question,
                Answer = dto.Answer
            }));
        }
        
    }
}