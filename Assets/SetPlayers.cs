using UnityEngine;

// tento skript se stara o prepisovani dat ze ClientStats na scenu

public class SetPlayers : MonoBehaviour
{
    Ball ball = new Ball();


    void Start()
    {
        if (ClientStats.HostClient)
        {
            ServerStats.Player1 = GameObject.FindGameObjectsWithTag("player")[1];
            ServerStats.Player2 = GameObject.FindGameObjectsWithTag("player")[0];
        }
    }
    void Update()
    {
        if (!ClientStats.HostClient)
        {

            ClientStats.Player2Position = Player.GetActualPositionOfPlayer(GameObject.FindGameObjectsWithTag("player")[0]);

        }
        else
        {
            ClientStats.Player1Position = Player.GetActualPositionOfPlayer(GameObject.FindGameObjectsWithTag("player")[1]);
            ClientStats.PositionOfBall = ball.GetActualPositionOfBall();
        }

        if (ClientStats.SetPlayer1)
        {
            Player.SetActualPosition(GameObject.FindGameObjectsWithTag("player")[1], ClientStats.Player1Position);
            ClientStats.SetPlayer1 = false;
        }

        if (ClientStats.SetPlayer2)
        {
            Player.SetActualPosition(GameObject.FindGameObjectsWithTag("player")[0],ClientStats.Player2Position);
            ClientStats.SetPlayer2 = false;
        }

        if (ClientStats.SetBall)
        {
            ball.SetBallPosition(ClientStats.PositionOfBall);
            ClientStats.SetBall = false;
        }
        if (ClientStats.QuitGame) {
            Application.Quit();
        }


    }


}
