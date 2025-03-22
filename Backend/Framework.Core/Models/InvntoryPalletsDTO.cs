using System.Collections.Generic;

namespace Framework.Core.Models
{
    public class InvntoryPalletsDTO<T> where T : class
    {
        public IList<T> Pallets { get; set; }
        public IList<string> UnknownPallets { get; set; }
    }
}
