using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerChallenge.Models;

namespace TrueLayerChallenge.Core.Interfaces
{
    public interface IResponse
    {
        public Task<Pokemon> Basic(string name);
        public Task<Pokemon> Translated(string name);
    }
}
