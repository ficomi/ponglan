using UnityEngine;

// tento skript se stara o detekci pricteni skore

public class TriggerGoal : MonoBehaviour {
    public GameObject goalGate;
    Score score = new Score();


    private void Update()
    {
        if (!ClientStats.HostClient) // pokud neni hostujici klient berou se score z ClientStats
        {
            score.ShowScore(1, ClientStats.Score[1]);
            score.ShowScore(0, ClientStats.Score[0]);
        }
    }
    void OnTriggerEnter2D(Collider2D other) // pokud se mic dostane do colladeru za hracem 
    {
        if (ClientStats.HostClient) // pokud je to na hostujicim clientu
        {
            if (other.gameObject.name == "Ball")
            {
                if (goalGate.name == "GoalTrigger1")
                {
                    score.AddScore(0); // pricte skore 1. hraci
                }
                if (goalGate.name == "GoalTrigger2")
                {
                    score.AddScore(1); // pricte score 2. hraci
                }
            }

        }
       
    }
}
