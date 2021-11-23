using System;
using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        [HttpDelete("{id}")]
        public ActionResult<Card> DeleteProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        [HttpPut]
        public ActionResult<Card> Update()
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public ActionResult<Card> Create()
        {
            throw new System.NotImplementedException();
        }

    }
}