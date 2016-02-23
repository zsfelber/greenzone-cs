using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Submarine.Base
{
    public class SslTcpClient
    {
        public class Session
        {
            internal Session(TcpClient client, SslStream sslStream)
            {
                this.Client = client;
                this.SslStream = sslStream;
            }
            public TcpClient Client { get; set; }
            public SslStream SslStream { get; set; }

            public void Close()
            {
                if (SslStream != null)
                {
                    SslStream.Close();
                }
                if (Client != null)
                {
                    Client.Close();
                }
                SslStream = null;
                Client = null;
            }
        }
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
#if (DEBUG)
                Console.WriteLine("Chain Information");
                Console.WriteLine("Chain revocation flag: {0}", chain.ChainPolicy.RevocationFlag);
                Console.WriteLine("Chain revocation mode: {0}", chain.ChainPolicy.RevocationMode);
                Console.WriteLine("Chain verification flag: {0}", chain.ChainPolicy.VerificationFlags);
                Console.WriteLine("Chain verification time: {0}", chain.ChainPolicy.VerificationTime);
                Console.WriteLine("Chain status length: {0}", chain.ChainStatus.Length);
                Console.WriteLine("Chain application policy count: {0}", chain.ChainPolicy.ApplicationPolicy.Count);
                Console.WriteLine("Chain certificate policy count: {0} {1}", chain.ChainPolicy.CertificatePolicy.Count, Environment.NewLine); 
#endif
                foreach (X509ChainStatus cs in chain.ChainStatus)
                {
#if (DEBUG)
                    Console.WriteLine(cs.Status);
                    Console.WriteLine(cs.StatusInformation);
#endif

                    if (cs.Status == X509ChainStatusFlags.PartialChain)
                    {
                        partial = true;
                    }
                    err++;
                }

#if (DEBUG)
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
#endif

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

        public static Session RunClient(string machineName, string serverName, string certificate, int port)
        {
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.
            TcpClient client = new TcpClient(machineName, port);//443
            Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.
            SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                ValidateServerCertificate,
                SelectLocalCertificate
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
#if (DEBUG)
                // Display the properties and settings for the authenticated stream.
                SslBase.DisplaySecurityLevel(sslStream);
                SslBase.DisplaySecurityServices(sslStream);
                SslBase.DisplayCertificateInformation(sslStream);
                SslBase.DisplayStreamProperties(sslStream);
#endif
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
                return null;
            }
            return new Session(client,sslStream);
        }
   }
}