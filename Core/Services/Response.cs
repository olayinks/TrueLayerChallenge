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
        public Response(HttpClient client)
        {
            _client = client;
        }
        public async Task<Pokemon> FetchPokemon(string url)
        {
            var response = await _client.GetAsync(url);
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

        public async Task<string> Translation(string url,string description)
        {                     
                var data = new StringContent(JsonConvert
                    .SerializeObject(new { text = description }), Encoding.UTF8, "application/json");
         
                var response = await _client.PostAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    string translation = JsonConvert.DeserializeObject<dynamic>(responseString).contents.translated;
                    return  Utils.RemoveTabs(translation);
           

                }
                return "";
            
        }
    }
}
