using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TcpClientConnection
{
        private const int portNumber = 2015;  
        private const string hostName = "localhost";
        private NetworkStream ns;


        public TcpClientConnection()
        {
            setConnection();
        }




        void setConnection()
        {
                      
                try
                {
                    //open tcp host 
                    TcpClient client = new TcpClient(hostName, portNumber);

                    string hello = "hello from unity";
                    
                    // Translate the passed message into ASCII and store it as a Byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(hello);  
                    
                    ns = client.GetStream();
                    
                    // Send the message to the connected TcpServer. 
                    ns.Write(data, 0, data.Length);


                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    throw;
                }

            

        }

        public void sendMessage(string messageToSend ="hello from unity")
        {
            
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(messageToSend);
            ns.Write(data,0,data.Length);
        }

        void readIncommingRequest()
        {            
            
            
            // Buffer to store the response bytes.
            Byte[] data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            
            Int32 bytes = ns.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
       
        }

        void startReadingBackgroundTask()
        {
            //Thread
            
        }
}
