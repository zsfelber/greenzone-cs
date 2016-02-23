﻿//Generated by the GOLD Parser Builder

using System.IO;

class MyParserClass
{
    private Parser parser = new Parser();

    public Reduction Root;     //Store the top of the tree
    public string FailMessage;

    public bool Setup(string FilePath)
    {
        return parser.LoadTables(FilePath);
    }

    public bool Parse(TextReader reader)
    {
        //This procedure starts the GOLD Parser Engine and handles each of the
        //messages it returns. Each time a reduction is made, you can create new
        //custom object and reassign the .CurrentReduction property. Otherwise, 
        //the system will use the Reduction object that was returned.
        //
        //The resulting tree will be a pure representation of the language 
        //and will be ready to implement.

        ParseMessage response;
        bool done;                      //Controls when we leave the loop
        bool accepted = false;          //Was the parse successful?
        
        parser.Open(reader);
        parser.TrimReductions = false;  //Please read about this feature before enabling  

        done = false;
        while (!done)
        {
            response = parser.Parse();

            switch (response)
            {
                case ParseMessage.LexicalError:
                    //Cannot recognize token
                    FailMessage = "Lexical Error:\n" +
                                  "Position: " + parser.CurrentPosition().Line + ", " + parser.CurrentPosition().Column + "\n" +
                                  "Read: " + parser.CurrentToken().Data;
                    done = true;
                    break;

                case ParseMessage.SyntaxError:
                    //Expecting a different token
                    FailMessage = "Syntax Error:\n" +
                                  "Position: " + parser.CurrentPosition().Line + ", " + parser.CurrentPosition().Column + "\n" +
                                  "Read: " + parser.CurrentToken().Data + "\n" +
                                  "Expecting: " + parser.ExpectedSymbols().Text();
                    done = true;
                    break;

                case ParseMessage.Reduction:
                    //For this project, we will let the parser build a tree of Reduction objects
                    //parser.CurrentReduction = CreateNewObject(parser.CurrentReduction);
                    break;

                case ParseMessage.Accept:
                    //Accepted!
                    Root = (Reduction) parser.CurrentReduction;    //The root node!                                  
                    done = true;
                    accepted = true;
                    break;

                case ParseMessage.TokenRead:
                    //You don't have to do anything here.
                    break;

                case ParseMessage.InternalError:
                    //INTERNAL ERROR! Something is horribly wrong.
                    done = true;
                    break;

                case ParseMessage.NotLoadedError:
                    //This error occurs if the CGT was not loaded.                   
                    FailMessage = "Tables not loaded";
                    done = true;
                    break;

                case ParseMessage.GroupError:
                    //GROUP ERROR! Unexpected end of file
                    FailMessage = "Runaway group";
                    done = true;
                    break;
            }
        } //while

        return accepted;
    }

}; //MyParser