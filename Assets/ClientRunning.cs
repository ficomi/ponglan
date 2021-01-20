
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net.Sockets;
using System.Linq;
using System.Timers;

// tento skript se stara o beziciho clienta

public class ClientRunning 
{
    Timer aTimer = new Timer();
   
    Ball ball = new Ball();
    
   public void StartRunningClient()
    {
        ClientStats.Score = new int[2];
        aTimer.Elapsed += new ElapsedEventHandler(UpdateClient_tick);
        aTimer.Interval = 10; // refresh odesilani každých .01s
        aTimer.Enabled = true;
    }

    // Update is called once per frame
    private void UpdateClient_tick(object source, ElapsedEventArgs e)
    {
       
            if (ServerStats.ClientConnected) // pokus je client pripojen
            {
                try
                {
                    if (ClientStats.HostClient) // pokud je to hostujici klient
                    {
                         
                         SendString(ClientStats.Server, ClientStats.Player1Position + ClientStats.PositionOfBall+ClientStats.Score[0]+"/"+ ClientStats.Score[1]+"/"); // Posle svoje data o vsech objektech na server
                        if (ClientStats.Server.GetStream().DataAvailable) // Pokud Prijde informace od serveru
                        {

                            ClientStats.Player2Position = ReciveString(ClientStats.Server); // zapise data o 2. hraci to globalni promenne
                            //Debug.Log("Hostujici klient prijal zpravu od serveru " + ClientStats.Player2Position);
                            ClientStats.SetPlayer2 = true;  // rika jestli se maji propsat globalni promenne o objektu do sceny

                    }
                        else
                        {
                            Debug.Log("Client 1 nic neprijal");
                            
                        }
                    }
                    else // pokus to neni hostujici klient
                    {


                        SendString(ClientStats.Server, ClientStats.Player2Position); // posle svoje data
                        if (ClientStats.Server.GetStream().DataAvailable) // pokud prichazi data od serveru
                        {
                            string msgString = ReciveString(ClientStats.Server);
                            string[] msgStringField = msgString.Split('/');
                            ClientStats.Player1Position = msgStringField[0] + "/" + msgStringField[1] + "/";
                            ClientStats.PositionOfBall = msgStringField[2] + "/" + msgStringField[3] + "/";
                            ClientStats.Score[0] = Int32.Parse(msgStringField[4]); // zapisuje score do globalni promene
                            ClientStats.Score[1] = Int32.Parse(msgStringField[5]);
                            //Debug.Log("Druhy klient prijal zpravu od serveru " + ClientStats.Player1Position + ClientStats.PositionOfBall);
                            ClientStats.SetPlayer1 = true; // rika jestli se maji propsat globalni promenne do sceny
                            ClientStats.SetBall = true;  // rika jestli se maji propsat globalni promenne o objektu do sceny
                    }
                        else
                        {
                            Debug.Log("Client 2 nic neprijal");
                            
                        }
                    }

                }
                catch (Exception a)
                {
                    Debug.Log("NEco neni v poradku " + a );
                    

            }
          

        }


    }

    public static void SendString(TcpClient klient, string zprava) // posila string
    {
        byte[] byteBuffer = Encoding.ASCII.GetBytes(zprava); // vytvori binarni buffer
        NetworkStream netStream = klient.GetStream();  // vytvori stream ke clientovi
        netStream.Write(byteBuffer, 0, byteBuffer.Length); // posle data
        netStream.Write(new byte[] { 0 }, 0, sizeof(byte)); // posle prazdne data
        netStream.Flush(); // flushne stream
    }



    public static string ReciveString(TcpClient klient)
    {
        List<int> buffer = new List<int>(); // vytvori binarni list (nevime jak dlouha bude zprava)
        NetworkStream stream = klient.GetStream();
        int readByte;
        while ((readByte = stream.ReadByte()) != 0) //dokud prichazeji informace a nejsou nulova ukladaji se do readbyte a nasledne do listu
        {
            buffer.Add(readByte);
        }
        return Encoding.ASCII.GetString(buffer.Select<int, byte>(b => (byte)b).ToArray(), 0, buffer.Count);// vrati obsah listu do pole, 0, delku pole a z binarniho pole udela text
    }






}
