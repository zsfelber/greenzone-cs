using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using GreenZoneParser.Parsers.Cs;
using System.Windows.Forms;
using GreenZoneParser.Parsers;
using System.Threading;
using GreenZoneParser.Lexer;
using GreenZoneUtil.Util;


namespace GreenZoneParser
{

    public class CsParser : Parser
    {
        public CsParser(string fileName, string fileContent)
            : base(fileName, fileContent)
        {
            this.tokenizer = new CsTokenizer(this, fileContent);
        }

        readonly CsTokenizer tokenizer;
        public override Tokenizer Tokenizer
        {
            get
            {
                return tokenizer;
            }
        }

        public CsTokenizer CsTokenizer
        {
            get
            {
                return tokenizer;
            }
        }

        public override BaseNode Parse()
        {
            Console.WriteLine(dt()+" parser.Parse()");
            tokenizer.Tokenize();
            Console.WriteLine(dt() + " Tokenizer finished");
            MainBlockNode result = new MainBlockNode(this);
            Console.WriteLine(dt()+" MainBlockNode parsed");
            return result;
        }

        string dt()
        {
            DateTime d = DateTime.Now;
            return d.ToShortTimeString() + ":" + d.Second + "." + d.Millisecond;
        }



    }
}