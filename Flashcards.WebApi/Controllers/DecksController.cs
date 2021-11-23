using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flashcards_backend.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecksController: ControllerBase
    {
        private readonly IDeckService _service;
        public DecksController(IDeckService service)
        {
            if (service == null) throw new InvalidDataException("Deck service cannot be null");
            _service = service;
        }
    }
}