using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Examples.System.Net
{
    public class SslTcpClient
    {
        private static Hashtable certificateErrors = new Hashtable();

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors)
            {
                bool partial = false;
                int err = 0;

                Console.WriteLine("Chain Information");
                Console.WriteLine("Chain revocation flag: {0}", chain.ChainPolicy.RevocationFlag);
                Console.WriteLine("Chain revocation mode: {0}", chain.ChainPolicy.RevocationMode);
                Console.WriteLine("Chain verification flag: {0}", chain.ChainPolicy.VerificationFlags);
                Console.WriteLine("Chain verification time: {0}", chain.ChainPolicy.VerificationTime);
                Console.WriteLine("Chain status length: {0}", chain.ChainStatus.Length);
                Console.WriteLine("Chain application policy count: {0}", chain.ChainPolicy.ApplicationPolicy.Count);
                Console.WriteLine("Chain certificate policy count: {0} {1}", chain.ChainPolicy.CertificatePolicy.Count, Environment.NewLine); 

                foreach (X509ChainStatus cs in chain.ChainStatus)
                {
                    Console.WriteLine(cs.Status);
                    Console.WriteLine(cs.StatusInformation);

                    if (cs.Status == X509ChainStatusFlags.PartialChain)
                    {
                        partial = true;
                    }
                    err++;
                }

                Console.WriteLine("Chain Element Information");
                Console.WriteLine("Number of chain elements: {0}", chain.ChainElements.Count);
                Console.WriteLine("Chain elements synchronized? {0} {1}", chain.ChainElements.IsSynchronized, Environment.NewLine);

                foreach (X509ChainElement element in chain.ChainElements)
                {
                    Console.WriteLine("Element issuer name: {0}", element.Certificate.Issuer);
                    Console.WriteLine("Element certificate valid until: {0}", element.Certificate.NotAfter);
                    Console.WriteLine("Element certificate is valid: {0}", element.Certificate.Verify());
                    Console.WriteLine("Element error status length: {0}", element.ChainElementStatus.Length);
                    Console.WriteLine("Element information: {0}", element.Information);
                    Console.WriteLine("Number of element extensions: {0}{1}", element.Certificate.Extensions.Count, Environment.NewLine);

                    if (chain.ChainStatus.Length > 1)
                    {
                        foreach (var cs in element.ChainElementStatus) {
                            Console.WriteLine(cs.Status);
                            Console.WriteLine(cs.StatusInformation);
                        }
                    }
                }

                if (err == 1 && partial)
                {
                    return true;
                }
            }
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        public static X509Certificate SelectLocalCertificate(
	        Object sender,
	        string targetHost,
	        X509CertificateCollection localCertificates,
	        X509Certificate remoteCertificate,
	        string[] acceptableIssuers)
        {
            if (localCertificates.Count > 0)
                return localCertificates[0];
            else
                return null;
        }

        public static void RunClient(string machineName, string serverName, string certificate)
        {
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.
            TcpClient client = new TcpClient(machineName, 8081);//443
            Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.
            SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                new LocalCertificateSelectionCallback(SelectLocalCertificate)
                );
            // The server name must match the name on the server certificate.
            try
            {
                if (certificate == null) 
                {
                    sslStream.AuthenticateAsClient(serverName);
                } 
                else 
                {
                    X509Certificate clientCertificate = null;
                    clientCertificate = X509Certificate.CreateFromCertFile(certificate);
                    X509CertificateCollection clientCertificates = new X509CertificateCollection();
                    clientCertificates.Add(clientCertificate);
                    sslStream.AuthenticateAsClient(serverName, clientCertificates, SslProtocols.Default, false);
                }
                // Display the properties and settings for the authenticated stream.
                DisplaySecurityLevel(sslStream);
                DisplaySecurityServices(sslStream);
                DisplayCertificateInformation(sslStream);
                DisplayStreamProperties(sslStream);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                client.Close();
                return;
            }
            // Encode a test message into a byte array.
            // Signal the end of the message using the "<EOF>".
            byte[] messsage = Encoding.UTF8.GetBytes("Hello from the client.<EOF>");
            // Send hello message to the server. 
            sslStream.Write(messsage);
            sslStream.Flush();
            // Read message from the server.
            string serverMessage = ReadMessage(sslStream);
            Console.WriteLine("Server says: {0}", serverMessage);
            // Close the client connection.
            client.Close();
            Console.WriteLine("Client closed.");
        }
        static string ReadMessage(SslStream sslStream)
        {
            // Read the  message sent by the server.
            // The end of the message is signaled using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }
        static void DisplaySecurityLevel(SslStream stream)
        {
            Console.WriteLine("Cipher: {0} strength {1}", stream.CipherAlgorithm, stream.CipherStrength);
            Console.WriteLine("Hash: {0} strength {1}", stream.HashAlgorithm, stream.HashStrength);
            Console.WriteLine("Key exchange: {0} strength {1}", stream.KeyExchangeAlgorithm, stream.KeyExchangeStrength);
            Console.WriteLine("Protocol: {0}", stream.SslProtocol);
        }
        static void DisplaySecurityServices(SslStream stream)
        {
            Console.WriteLine("Is authenticated: {0} as server? {1}", stream.IsAuthenticated, stream.IsServer);
            Console.WriteLine("IsSigned: {0}", stream.IsSigned);
            Console.WriteLine("Is Encrypted: {0}", stream.IsEncrypted);
        }
        static void DisplayStreamProperties(SslStream stream)
        {
            Console.WriteLine("Can read: {0}, write {1}", stream.CanRead, stream.CanWrite);
            Console.WriteLine("Can timeout: {0}", stream.CanTimeout);
        }
        static void DisplayCertificateInformation(SslStream stream)
        {
            Console.WriteLine("Certificate revocation list checked: {0}", stream.CheckCertRevocationStatus);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                Console.WriteLine("Local cert was issued to {0} and is valid from {1} until {2}.",
                    localCertificate.Subject,
                    localCertificate.GetEffectiveDateString(),
                    localCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Local certificate is null.");
            }
            // Display the properties of the client's certificate.
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                Console.WriteLine("Remote cert was issued to {0} and is valid from {1} until {2}.",
                    remoteCertificate.Subject,
                    remoteCertificate.GetEffectiveDateString(),
                    remoteCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Remote certificate is null.");
            }
        }
        private static void DisplayUsage()
        {
            Console.WriteLine("To start the client specify:");
            Console.WriteLine("SslTcpClient machineName [serverName] [certificateFile.cer]");
            Environment.Exit(1);
        }
        public static int Main(string[] args)
        {
            string certificate = null;
            string serverCertificateName = null;
            string machineName = null;
            if (args == null || args.Length < 1)
            {
                DisplayUsage();
            }
            // User can specify the machine name and server name.
            // Server name must match the name on the server's certificate. 
            machineName = args[0];
            if (args.Length < 2)
            {
                serverCertificateName = machineName;
            }
            else
            {
                serverCertificateName = args[1];
            }
            if (args.Length < 3)
            {
                certificate = null;
            }
            else
            {
                certificate = args[2];
            }
            SslTcpClient.RunClient(machineName, serverCertificateName, certificate);
            return 0;
        }
    }
}