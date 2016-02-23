using System.IO;
using System.Net.Sockets;
using System.Threading;
using Pfz.Remoting;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			var listener = new TcpListener(657);
			listener.Start();
			
			while(true)
			{
				TcpClient client = listener.AcceptTcpClient();
				Thread thread = new Thread(p_ClientConnected);
				thread.Start(client);
			}
		}
		private static void p_ClientConnected(object data)
		{
			try
			{
				using(TcpClient client = (TcpClient)data)
				{
					var baseStream = client.GetStream();
					var stream = new SecureStream(baseStream, true);
					using(var reader = new StreamReader(stream))
					{
						var writer = new StreamWriter(stream);
						
						while(true)
						{
							string line = reader.ReadLine();
							if (line == null)
								return;
							
							writer.WriteLine(line);
							writer.Flush();
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
