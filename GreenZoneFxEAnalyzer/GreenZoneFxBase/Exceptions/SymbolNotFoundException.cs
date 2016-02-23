using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneFxEngine.Types
{
    public class SymbolNotFoundException : Exception
    {
        public SymbolNotFoundException(string message)
            : base(message)
        {
        }
    }
}
