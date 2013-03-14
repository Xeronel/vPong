using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using Lidgren.Network.Xna;

namespace vPong
{
    class NetCode
    {
        public void StartServer()
        {
            //Create server config
            NetPeerConfiguration config = new NetPeerConfiguration("ExampleName");
            config.Port = 14242;

            //Start the server
            NetServer server = new NetServer(config);
            server.Start();

            //Receive a message
            NetIncomingMessage msg;
            while ((msg = server.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(msg.ReadString());
                        break;
                    default:
                        Console.WriteLine("Unhandled type: " + msg.MessageType);
                        break;
                }
                server.Recycle(msg);
            }

            NetOutgoingMessage sendMsg = server.CreateMessage();
            sendMsg.Write("Hello");
            sendMsg.Write(42);
            
            

            server.SendMessage(sendMsg, "192.168.0.15", NetDeliveryMethod.ReliableOrdered);

            NetIncomingMessage incMsg = server.ReadMessage();
            string str = incMsg.ReadString();
            int a = incMsg.ReadInt32();


        }

        public void SendMessage
        {

        }

    }
}
