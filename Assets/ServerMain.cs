using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Timers;

// Tento skript je o vytvoreni serveru na ip adrese a priprava na bezici server

public class ServerMain : MonoBehaviour
{
    Timer aTimer = new Timer();
    public IPAddress IPAdd { get; private set; }
    Ball ball = new Ball();
    ServerRunnig SRunning = new ServerRunnig();
    ClientRunning Crunning = new ClientRunning();


    public void StartServer()
    {
        IPAdd = GetLocalIPAddress();
        ServerStats.Listener = new TcpListener(IPAdd, 8080); // vytvori listener na pocitaci
        ServerStats.Listener.Start();
        ServerStats.ServerActive = true; // srver je aktivni
        ClientStats.HostClient = true; 
        aTimer.Elapsed += new ElapsedEventHandler(LookingForClients_tick); //definuje jakou funkci ma timer zpoustet
        aTimer.Interval = 10; // refresh odesilani každých .01s
        aTimer.Enabled = true;
        //Debug.Log("Server běží, čekám na 2 klienty");
    }


    //Připojení klientů

    private void LookingForClients_tick(object source, ElapsedEventArgs a)
    {
        if (ServerStats.ServerActive) // pokud je server aktivni
        { 
            if (ServerStats.clients.Count < 2) // pokud nejsou clienti pripojeni
            {
                if (!ServerStats.Listener.Pending())
                {
                    Debug.Log("Neni zadne pripojeni");
                }
                else
                {
                    // Vrátí klienta, co se připojuje
                    TcpClient klient = ServerStats.Listener.AcceptTcpClient();
                    ServerStats.clients.Add(klient);
                }
            }
          
            if (ServerStats.clients.Count == 2) // pokud jsou 2 clienti pripojeni
            {             
                ServerStats.ServerReady = true;// server je ready      
                aTimer.Stop();
                SRunning.StartRunningServer(); // spusti  bezici cast server
                Crunning.StartRunningClient(); // spusti bezici cast clienta        
            }
        }
    }


    public static IPAddress GetLocalIPAddress() // ziska lokalni IP adresu
    {
        var host = Dns.GetHostEntry(Dns.GetHostName()); // ziska vsechny adresy pocitace
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork) // hledá IPv4 adresu zařízení
            {
                return ip;
            }
        }
        return null;  //nenalezeno žádná IPv4 adressa
    }
}

