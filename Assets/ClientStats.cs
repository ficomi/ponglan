
using System.Net.Sockets;

// globalni promenne clienta
// tento skript je vytvoren z duvodu paralelizace timeru kde neni moznost zavolat tridy,funce unity editoru a slouzi jako docastne ulozeni promenych

public class ClientStats  {
    static public int ActivePlayer { get; set; } // jaky hrac je client (0,1)
    static public TcpClient Server { get; set; } // TCP server
    static public bool HostClient = false;  // jestli client je hostujici klient
    public static string Player1Position { get; set; } // pozice 1. hrace
    public static string Player2Position { get; set; } // pozice 2. hrace
    public static string PositionOfBall { get; set; }  // pozice mice
    public static bool SetPlayer1 { get; set; } // jestli ma prepsat 1. hrace
    public static bool SetPlayer2 { get; set; } // jestli ma prepsat 2. hrace
    public static bool SetBall { get; set; } // jestli ma mic
    public static bool QuitGame { get; set; } // jestli ma vypnout hru
    public static int[] Score { get; set; } // score

}
