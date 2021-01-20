
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

// globalni promenne serveru
// tento skript je vytvoren z duvodu paralelizace timeru kde neni moznost zavolat tridy,funce unity editoru a slouzi jako docastne ulozeni promenych

public static class ServerStats  {
    static public bool ServerActive = false; // server aktivno
    static public bool ClientConnected = false; // klient pripojen
    static public bool ServerReady = false; // server je pripraven
    static public List<TcpClient> clients = new List<TcpClient>(); // list vsech klientu
    static public string PositionOfPlayer1 { get; set; }  //   pozice 1. hrace na serveru
    static public string PositionOfPlayer2 { get; set; }  //   pozice 2. hrace na serveru
    static public string PositionOfBall { get; set; } // pozice mice
    static public TcpListener Listener { get; set; } // naslouchac
    static public GameObject Player1 { get; set; } // object 1. hrace
    public static int[] Score { get; set; } //score
    static public GameObject Player2 { get; set; } // objekt 2. hrace
}
