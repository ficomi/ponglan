using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// tento skript se stara o score a vytvori list hracu

public class Score : MonoBehaviour
{
    public static List<Player> players = new List<Player>();
    GameObject[] planes;
    public static GameObject[] scoreText;
    Ball ball = new Ball();

    void Start()
    {
        
        planes = GameObject.FindGameObjectsWithTag("player"); // najde vsechy objekty na scene s tagem "player"
        FillPlayers(planes); // naplni list
        scoreText = GameObject.FindGameObjectsWithTag("scoreText"); // najde vsechy objekty na scene s tagem "scoreText"

        ;
    }

    void Update() // vola se kazdy snimek
    {
        if (ClientStats.HostClient) { // pokud jde o hostujiciho klienta tak se score prepise do globalni promenne ulozenou v ClientStats
           for(int i=0;i< ClientStats.Score.Length;i++) {
                ClientStats.Score[i] = players[i].score;
            }
        }
    }

    private void FillPlayers(GameObject[] planes) // naplni list
    {
        for (int i = 0; i < planes.Length; i++) {
            players.Add(new Player( planes[i]));
        }
    }


    public void AddScore(int player) // prida score
    {
        players[player].score++;
        ShowScore(player, players[player].score);
        ball.StartBall();
    }

    public void ShowScore(int player, int score) // ukaze score na scene
    {    
        scoreText[player].GetComponent<Text>().text = score.ToString();
    }
}
