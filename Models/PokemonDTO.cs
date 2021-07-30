using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueLayerChallenge.Models
{
    public class PokemonDTO
    {
        public string Name { get; set; }
        public IEnumerable<Description> Flavor_Text_Entries { get; set; }
        public Label Habitat { get; set; }
        public string Is_Legendary { get; set; }
    }
}
