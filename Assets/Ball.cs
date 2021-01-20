using UnityEngine;

// tento skript se stara o mic

public class Ball : MonoBehaviour
{
    static GameObject ball;
    Rigidbody2D BallRigBody { get; set; }
    static Vector3 Position { get; set; }


    
    void Start() // na zacatku zjisti kde je mic a pokud je hostujici client tak ho vypusti
    {
        ball = GameObject.FindGameObjectWithTag("ball");
        if (ClientStats.HostClient)
        {
            StartBall();

        }
    }

    public void StartBall()
    {
        Vector2 Movement = Vector2.zero; ;
        float speed = 0.1f;
        ball.gameObject.transform.SetPositionAndRotation(new Vector2(0, 0), ball.transform.rotation);  // priradi mic na zacatecni pozici
        BallRigBody = ball.gameObject.GetComponent<Rigidbody2D>();
        Movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); // vytvori nahodny vektor
        BallRigBody.AddForce(Movement * speed); // vypusti mic v nahodnem smeru
    }

    void Update()
    {
        if (ClientStats.HostClient) // pokud je hostujici klient tak ze zapisuje kazdy snimek pozice mice
        {
            Position = ball.transform.position;

        }
    }

    public string GetActualPositionOfBall() // zjisti aktualni pozici mice
    {
        return Position.x + "/" + Position.y + "/";
    }

    public void SetBallPosition(string positions) // nastavi mic na pozici
    {
        string[] StringField = positions.Split('/');
        
        ball.transform.SetPositionAndRotation(new Vector2(float.Parse(StringField[0]), float.Parse(StringField[1])), ball.gameObject.transform.rotation);

    }
}
