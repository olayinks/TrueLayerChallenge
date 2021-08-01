using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerChallenge.Core.Interfaces;
using TrueLayerChallenge.Helper;

namespace TrueLayerChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IResponse _response;
        private readonly IConfiguration _config;
        public PokemonController(ILogger<PokemonController> logger, IResponse response,IConfiguration config)
        {
            _logger = logger;
            _response = response;
            _config = config;
        }
        // GET api/<PokemonController>/mewtwo
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    return NotFound();

                var pokemon = await _response.FetchPokemon($"{_config["PokemonURL"]}{name}");
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

        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> Translated(string name)
        {

            try
            {

                if (String.IsNullOrEmpty(name))
                    return NotFound();
                var pokemon = await _response.FetchPokemon($"{_config["PokemonURL"]}{name}");
                var language = pokemon.IsLegendary || pokemon.Habitat == "cave" ? Utils.YODA : Utils.SHAKESPEARE;                

                var pokemonTranslated = await _response.Translation($"{_config["translationURL"]}{language}",pokemon.Description);
                
                if (!string.IsNullOrEmpty(pokemonTranslated))
                {
                    pokemon.Description = pokemonTranslated;
                    return Ok(pokemon);
                }
                else
                     return Ok(pokemon);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }
    }
}
