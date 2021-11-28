using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


        [HttpGet]
        public ActionResult<List<CardInDeckDto>> GetAll([FromQuery] int deckId)
        {
            return Ok(_cardService.GetAllCardsByDeckId(deckId)
                .Select(c => new CardInDeckDto
                {
                    Id = c.Id,
                    Question = c.Question,
                    Answer = c.Answer,
                    Correctness = c.Correctness
                }));
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
        public ActionResult<CardInDeckDto> Update([FromBody] PutCardDto dto)
        {
            if (dto == null)
                throw new InvalidDataException("Card cannot be null");

            try
            {
                var card = _cardService.Update(new Card
                {
                    Id = dto.Id,
                    Question = dto.Question,
                    Answer = dto.Answer,
                    Correctness = dto.Correctness
                });

                return Ok(new CardInDeckDto
                {
                    Id = card.Id,
                    Question = card.Question,
                    Answer = card.Answer,
                    Correctness = card.Correctness
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<CardInDeckDto> Create([FromBody] PostCardDto dto)
        {
            if (dto == null)
                throw new InvalidDataException("Card cannot be null");

            try
            {
                var newCard = _cardService.Create(new Card
                {
                    Question = dto.Question,
                    Answer = dto.Answer,
                    Deck = new Deck {Id = dto.deckId}
                });

                return Ok(new CardInDeckDto
                {
                    Id = newCard.Id,
                    Question = newCard.Question,
                    Answer = newCard.Answer,
                    Correctness = 0
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}