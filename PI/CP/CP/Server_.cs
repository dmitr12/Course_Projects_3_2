using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CP
{
    class Server_
    {
        IPAddress serverIp;
        int port;
        TcpListener server;

        public Server_(string ip, int port)
        {
            try
            {
                serverIp = IPAddress.Parse(ip);
                this.port = port;
                server = new TcpListener(serverIp, port);
                server.Start();
            }
            catch(SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }


    }
}
