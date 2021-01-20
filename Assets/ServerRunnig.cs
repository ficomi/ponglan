using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net.Sockets;
using System.Linq;
using System.Timers;

// tento skript se stara o bezici server

public class ServerRunnig : MonoBehaviour
{
    Timer aTimer = new Timer(); // vytvori timer
    public void StartRunningServer()
    {
        ServerStats.Score = new int[2];
        aTimer.Elapsed += new ElapsedEventHandler(UpdateServer_tick); //definuje jakou fumkci ma timer zpoustet
        aTimer.Interval = 5; // refresh odesilani každých .005s
        aTimer.Enabled = true; 
    }

    private void UpdateServer_tick(object source, ElapsedEventArgs e)
    {
        try
        {
            if (ServerStats.clients.Count == 2 ) // overi jestli jsou opravdu 2 clienti
            {



                if (ServerStats.clients[0].GetStream().DataAvailable) // pokud prijde od hostujiciho klienta zprava
                {
                    string msg = ReciveString(ServerStats.clients[0]);

                    if (!String.IsNullOrEmpty(msg)) // jestli je zprava prazdna nebo nulova
                    {
                        //Debug.Log("Zprava prijata (Host client): " + msg);
                        string[] msgStringField = msg.Split('/'); 
                        ServerStats.PositionOfPlayer1 = msgStringField[0] + "/" + msgStringField[1] + "/"; // priradi pozici 1. hrace na globalni promenou serveru
                        ServerStats.PositionOfBall = msgStringField[2] + "/" + msgStringField[3] + "/";
                        ServerStats.Score[0] = Int32.Parse(msgStringField[4]); // priradi score na globalni promenou serveru
                        ServerStats.Score[1] = Int32.Parse(msgStringField[5]);
                        try
                        {
                            SendString(ServerStats.clients[1], ServerStats.PositionOfPlayer1 + ServerStats.PositionOfBall + ServerStats.Score[0]+"/"+ ServerStats.Score[1]+ "/"); // odesle vsechny data 2. clientovy
                        }
                        catch (Exception i)
                        {
                            //Debug.Log(" Nepodarilo se odeslat na 1 klienta " + i);
                            ServerStats.Listener.Stop();
                            aTimer.Stop();
                        }

                    }

                }
                if (ServerStats.clients[1].GetStream().DataAvailable) // pokud prijde od nehostujiciho klienta
                {

                    string msg = ReciveString(ServerStats.clients[1]);

                    if (!String.IsNullOrEmpty(msg))
                    {
                        //Debug.Log("Zprava prijata: " + msg);
                        string[] msgStringField = msg.Split('/');
                        ServerStats.PositionOfPlayer2 = "";
                        for (int i = 0; i < msgStringField.Length - 1; i++)
                        {
                            ServerStats.PositionOfPlayer2 += msgStringField[i] + "/";
                        }
                        try
                        {
                            SendString(ServerStats.clients[0], ServerStats.PositionOfPlayer2); // posle zpravu hostujicimu clientovi o pozici 2. hrace
                        }
                        catch (Exception i)
                        {
                            Debug.Log(" Nepodarilo se odeslat na 2 klienta " + i);
                            ServerStats.Listener.Stop();
                            aTimer.Stop();
                        }
                    }

                }
                //Debug.Log(ServerStats.PositionOfPlayer1+ ServerStats.PositionOfPlayer2 + ServerStats.PositionOfBall);


            }
        }
        catch (Exception a)
        {
            Debug.Log("Neco se nepovedlo pri behu serveru " + a);
           
          
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

