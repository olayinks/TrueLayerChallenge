using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerChallenge.Core.Interfaces;

namespace TrueLayerChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IResponse _response;
        public PokemonController(ILogger<PokemonController> logger, IResponse response)
        {
            _logger = logger;
            _response = response;
        }
        // GET api/<PokemonController>/mewtwo
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    return NotFound();

                var pokemon = await _response.Basic(name);
                if (pokemon != null)
                {
                    return Ok(pokemon);
                }
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest("An error occured");
            }

        }
    }
}
