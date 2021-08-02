using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerChallenge.Models;

namespace TrueLayerChallenge.Core.Interfaces
{
    public interface IResponse
    {
        public Task<Pokemon> FetchPokemon(string url);
        public Task<Pokemon> Translation(string url,Pokemon pokemon);
    }
}
