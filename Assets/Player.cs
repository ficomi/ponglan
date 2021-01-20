using UnityEngine;

// z tohoto skriptu se tvori list hracu

public class Player : MonoBehaviour
{
    
    public GameObject Plane { get; private set; }

    public int score { get; set; }


    public Player( GameObject plane)
    {
        Plane = plane;
        score = 0;
    }
    static public string GetActualPositionOfPlayer(GameObject Plane) // aktualni pozice hrace
    {
        return Plane.gameObject.transform.position.x.ToString() + "/" + Plane.gameObject.transform.position.y.ToString() + "/";
    }

    static public void SetActualPosition(GameObject Plane,string position) { // nastavit pozici hrace

        string[] StringField = position.Split('/');
        Plane.transform.SetPositionAndRotation(new Vector2(float.Parse(StringField[0]), float.Parse(StringField[1])), Plane.gameObject.transform.rotation);
    }

}
