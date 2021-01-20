using UnityEngine;
using UnityEngine.UI;

// tento skript se stara o funkcnost tlacitek v menu a prirazeni hracu

public class MenuButtons : MonoBehaviour {
    public Button startAsServer;
    public Button startAsClient;
    public Button exit;
    public Button back;
    public Button search;
    public GameObject ServerMenu;
    public GameObject ClientMenu;
    public GameObject MainMenu;
    ServerMain server = new ServerMain();
    Client client = new Client();
   

    void Start() // Spusteni pri startu sceny
    {
        startAsServer.onClick.AddListener(StartAsServerOnClick);
        startAsClient.onClick.AddListener(StartAsClientOnClick);
        exit.onClick.AddListener(Exit);
        back.onClick.AddListener(BackMenu);
        search.onClick.AddListener(SearchIP);
        
        MainMenu.gameObject.SetActive(true);
        back.gameObject.SetActive(false);
        ServerMenu.gameObject.SetActive(false);
        ClientMenu.gameObject.SetActive(false);

    }
     void Update() // Update kazdy snimek
    {
       
        if (ServerStats.ServerReady) {
            Application.LoadLevel(1);
        } 
    }


    void StartAsServerOnClick() // Akce pri kliknuti na tlacitko start jako server
    {
        ClientStats.ActivePlayer = 1;
        ServerMenu.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
        server.StartServer();
        ServerMenu.transform.GetChild(1).gameObject.GetComponent<Text>().text = server.IPAdd.ToString();
        client.SetIPAddress(server.IPAdd.ToString());
        client.StartClient();
        ClientStats.HostClient = true;

    }


    void StartAsClientOnClick() //Akce pri kliknuti na tlacitko spusteni jako client
    {
       
        ClientStats.ActivePlayer = 0;
        ClientMenu.gameObject.SetActive(true);
        ClientMenu.transform.GetChild(0).gameObject.SetActive(false);
        ClientMenu.transform.GetChild(1).gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
        

    }

    //public void CantConnect()
    //{

    //    //ClientMenu.transform.GetChild(1).gameObject.SetActive(true);
    //    ClientMenu.transform.GetChild(0).gameObject.SetActive(false);
    //    ClientMenu.transform.GetChild(2).gameObject.SetActive(false);
    //    ClientMenu.transform.GetChild(3).gameObject.SetActive(false);
    //}

    void SearchIP()
    {
        client.SetIPAddress(ClientMenu.transform.GetChild(2).gameObject.GetComponent<InputField>().text);
        client.StartClient();
    }
    void Exit()
    {
        Application.Quit();
    }


    void BackMenu()
    {
        MainMenu.gameObject.SetActive(true);
        ServerMenu.gameObject.SetActive(false);
        ClientMenu.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }
}
