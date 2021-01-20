using UnityEngine;

// tento skript se stara o kontrolu krace

public class PlayerControl : MonoBehaviour
{
  GameObject PlayerPlane;
    
      void Start() // na zacatku najde objekty hracu a priradi aktivnoho hrace
    {
        PlayerPlane = GameObject.FindGameObjectsWithTag("player")[ClientStats.ActivePlayer]; 
        //Debug.Log("Hrac je nastaven na: "+ ServerStats.ActivePlayer);
    }

   
    void Update()
    {
        //if (PlayerPlane != null)
        //{
        if (Input.GetButton("Vertical")) // pokud stisknem sipku nahoru
        {

            PlayerPlane.GetComponent<Rigidbody2D>().velocity = new Vector2(0, /*GetComponent<Rigidbody2D>().velocity.y **/ (Input.GetAxis("Vertical") * 60)); // pohne s objekten v ose y

        }
        else {
            PlayerPlane.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // nic se nedeje
        }
    }
}



