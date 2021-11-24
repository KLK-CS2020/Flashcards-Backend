using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flashcards.WebApi.Dtos;
using Flashcards_backend.Core.IServices;
using Flashcards_backend.Core.Models;
using Flashcards.WebApi.Dtos.Card;
using Flashcards.WebApi.Dtos.Deck;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DecksController: ControllerBase
    {
        private readonly IDeckService _service;
        public DecksController(IDeckService service)
        {
            if (service == null) throw new InvalidDataException("Deck service cannot be null");
            _service = service;
        }
        
        [HttpGet("GetAllPublic")]
        public ActionResult<List<GetDeckDto>> GetAllPublic()
        {
            return Ok(_service.GetAllPublic()
                .Select(d => new GetDeckDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    isPublic = d.isPublic,
                    UserId = d.User.Id,
                    NumberOfCards = d.Cards.Count
                }));
        }
        
        [HttpGet("GetByUserId/{userId}")]
        public ActionResult<List<GetDeckDto>> GetAllByUserId(int userId)
        {
            if (userId < 0) return BadRequest("userId cannot be less than 0");
            return Ok(_service.GetByUserId(userId)
                .Select(d => new GetDeckDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    isPublic = d.isPublic,
                    UserId = d.User.Id,
                    NumberOfCards = d.Cards.Count
                }));
        }

       // [AllowAnonymous]
        [HttpPost]
        public ActionResult<Deck> Post([FromBody] PostDeckDto postDeckDto)
        {
            if (postDeckDto == null)
                throw new InvalidDataException("deck cannot be null");
            if (postDeckDto.Name is null or "")
                return BadRequest("Name cannot be empty");
            if (postDeckDto.Description is null or "")
                return BadRequest("Description cannot be empty");
            if (postDeckDto.UserId ==0)
                return BadRequest("User ID must be specified");
            try
            {
                return Ok(_service.Create(new Deck()
                {
                    Name = postDeckDto.Name,
                   Description = postDeckDto.Description,
                   isPublic = postDeckDto.isPublic,
                   User = new User{Id = postDeckDto.UserId}
                }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }


        [HttpGet("GetById/{deckId}")]
        public ActionResult<GetDeckWithCardsDto> GetById(int deckId)
        {
            if (deckId < 0) return BadRequest("deckId cannot be less than 0");
            var deck = _service.GetById(deckId);
            return Ok(new GetDeckWithCardsDto
            {
                Id = deck.Id,
                Name = deck.Name,
                Description = deck.Description,
                IsPublic = deck.isPublic,
                UserId = deck.User.Id,
                Cards = deck.Cards.Select(c=>new CardInDeckDto
                {
                    Id = c.Id,
                    Question = c.Question,
                    Answer = c.Answer,
                    Correctness = c.Correctness
                }).ToList()
            });
        }
        
        

        [HttpDelete("{deckId}")]
        public ActionResult<Deck> Delete(int deckId)
        {
            if (deckId < 0) return BadRequest("deckId cannot be less than 0");
            return Ok(_service.Delete(deckId));
        }
        
        [HttpPut]
        public ActionResult<Deck> Update([FromBody] PutDeckDto deck)
        {
            if (deck == null) throw new InvalidDataException("deck to update cannot be null");
            if (deck.Id < 0) return BadRequest("deckId cannot be less than 0");
            if (deck.Name.Length==0) return BadRequest("name cannot be empty");
            if (deck.Description.Length > 250) return BadRequest("description cannot be longer than 250 characters");

            return Ok(_service.Update(new Deck
            {
                Id = deck.Id,
                Name = deck.Name,
                Description = deck.Description,
                isPublic = deck.isPublic
            }));
        }
    }
    
    
}