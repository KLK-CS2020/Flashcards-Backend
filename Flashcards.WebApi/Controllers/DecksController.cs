using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flashcards_backend.Core.Filtering;
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
        public ActionResult<List<GetDeckDto>> GetAllPublic(string search, [FromQuery] Filter filter)
        {
            if (filter == null)
                throw new InvalidDataException("filter cannot be null");
            try
            {
                return Ok(_service.GetAllPublic(search, filter)
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        
        [HttpGet("GetByUserId/{userId}")]
        public ActionResult<List<GetDeckDto>> GetAllByUserId(int userId, string search)
        {
            try
            {
                return Ok(_service.GetByUserId(userId, search)
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("GetById/{deckId}")]
        public ActionResult<GetDeckWithCardsDto> GetById(int deckId, string sortOrder)
        {
            try
            {
                var deck = _service.GetById(deckId, sortOrder);
                return Ok(new GetDeckWithCardsDto
                {
                    Id = deck.Id,
                    Name = deck.Name,
                    Description = deck.Description,
                    IsPublic = deck.isPublic,
                    WasCopied = deck.WasCopied,
                    UserId = deck.User.Id,
                    Cards = deck.Cards.Select(c => new CardInDeckDto
                    {
                        Id = c.Id,
                        Question = c.Question,
                        Answer = c.Answer,
                        Correctness = c.Correctness
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public ActionResult<Deck> Post([FromBody] PostDeckDto postDeckDto)
        {
            if (postDeckDto == null)
                throw new InvalidDataException("deck cannot be null");
          
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

        [HttpPost("CreateCopied")]
        public ActionResult<Deck> CreateCopied([FromQuery] int deckId, int userId)
        {
            try
            {
                return Ok(_service.CreateCopied(deckId, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{deckId}")]
        public ActionResult<Deck> Delete(int deckId)
        {
            try
            {
                return Ok(_service.Delete(deckId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut]
        public ActionResult<Deck> Update([FromBody] PutDeckDto deck)
        {
            if (deck == null) throw new InvalidDataException("deck to update cannot be null");
            try
            {
                return Ok(_service.Update(new Deck
                {
                    Id = deck.Id,
                    Name = deck.Name,
                    Description = deck.Description,
                    isPublic = deck.isPublic
                }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
    
    
}