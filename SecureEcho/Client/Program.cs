using System;
using System.IO;
using System.Net.Sockets;
using Pfz.Remoting;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var client = new TcpClient(args[0], 657))
			{
				var baseStream = client.GetStream();
				var stream = new SecureStream(baseStream);
				
				using(var writer = new StreamWriter(stream))
				{
					var reader = new StreamReader(stream);
					
					while(true)
					{
						string line = Console.ReadLine();
						if (string.IsNullOrEmpty(line))
							return;
						
						writer.WriteLine(line);
						writer.Flush();
						Console.Write("Server response: ");
						Console.WriteLine(reader.ReadLine());
					}
				}
			}
		}
	}
}
