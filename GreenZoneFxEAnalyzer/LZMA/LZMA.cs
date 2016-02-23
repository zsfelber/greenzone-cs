using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SevenZip
{
    public static class LZMA
    {
        public static void Decompress(Stream inStream, Stream outStream)
        {
            byte[] properties = new byte[5];
            if (inStream.Read(properties, 0, 5) != 5)
                throw (new LZMAException("input .lzma is too short"));
            Compression.LZMA.Decoder decoder = new Compression.LZMA.Decoder();
            decoder.SetDecoderProperties(properties);

            long outSize = 0;
            for (int i = 0; i < 8; i++)
            {
                int v = inStream.ReadByte();
                if (v < 0)
                    throw (new LZMAException("Can't Read 1"));
                outSize |= ((long)(byte)v) << (8 * i);
            }
            long compressedSize = inStream.Length - inStream.Position;
            decoder.Code(inStream, outStream, compressedSize, outSize, null);
        }
    }
}
