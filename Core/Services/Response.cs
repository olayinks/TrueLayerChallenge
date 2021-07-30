using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrueLayerChallenge.Core.Interfaces;
using TrueLayerChallenge.Helper;
using TrueLayerChallenge.Models;

namespace TrueLayerChallenge.Core.Services
{
    public class Response : IResponse
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        public Response(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }
        public async Task<Pokemon> Basic(string name)
        {


            var response = await _client.GetAsync($"{_config["PokemonURL"]}{name}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var pokemonDto = JsonConvert.DeserializeObject<PokemonDTO>(responseString);
            var res = pokemonDto.Flavor_Text_Entries.Where(x => x.Language.Name == "en"); // get only english translation
            var desc = res?.ElementAt(new Random().Next(0, res.Count() - 1)).Flavor_Text; //random description
            var pokemon = new Pokemon
            {
                Name = pokemonDto.Name,
                Description = Utils.RemoveTabs(desc),
                Habitat = pokemonDto.Habitat?.Name,
                IsLegendary = pokemonDto.Is_Legendary == "true"
            };
            return pokemon;

        }

        public async Task<Pokemon> Translated(string name)
        {
            var pokemon = await Basic(name);

            if (pokemon != null)
            {
                var data = new StringContent(JsonConvert
                    .SerializeObject(new { text = pokemon.Description }), Encoding.UTF8, "application/json");
                var language = pokemon.IsLegendary || pokemon.Habitat == "cave" ? Utils.YODA : Utils.SHAKESPEARE;
                var response = await _client.PostAsync($"{_config["translationURL"]}{language}", data);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    string translation = JsonConvert.DeserializeObject<dynamic>(responseString).contents.translated;
                    pokemon.Description = Utils.RemoveTabs(translation);
                    return pokemon;

                }
                return pokemon;

            }
            return null;
        }
    }
}
