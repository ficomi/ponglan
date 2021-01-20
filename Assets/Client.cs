using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

// vytvoreni TCP clienta na server a priprava na bezici server

public class Client : MonoBehaviour
{

    IPAddress IPAdd { get; set; }
    ClientRunning CRunning = new ClientRunning();

    public void StartClient()
    {
        try
        {
            // Vyvolá připojovací dotaz na server
            ClientStats.Server = new TcpClient(IPAdd.ToString(), 8080); // Vytvoreni TCP Clienta a pripojeni na IP
            ServerStats.ClientConnected = true;
           
            if (!ClientStats.HostClient) // pokud to neni hostujici klient
            {
                Application.LoadLevel(1);
                ClientStats.Score = new int[2];
                CRunning.StartRunningClient();
               
            }
            ClientStats.QuitGame = false;
        }
        catch (Exception e) // Vyvola exception pokud se nenapoji
        {
            Debug.Log("Nepodarilo se pripojit k " + IPAdd.ToString()+" "+e);
        }
    }


    public void SetIPAddress(string ip) // Nastavi IP Adresu
    {
        IPAdd = IPAddress.Parse(ip);
    }

}
