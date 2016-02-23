using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneParser.Parsers.Cs
{
    public class MainBlockNode : BlockNode
    {
        public MainBlockNode(CsParser parser)
            : base(parser, null, parser.CsTokenizer.BlockOpenClose, false, null, 0, BlockParseMode.MainBlock)
        {

        }
    }
}
