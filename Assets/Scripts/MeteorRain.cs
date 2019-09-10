using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRain : MonoBehaviour
{  

    private GameObject player;

    private Rigidbody2D componentRigidbody;
    public bool MeterorTouch;

    private void Start()
    {
        int dir = Random.Range(-20, -10);
        int force = Random.Range(-5, -15);

        MeterorTouch = true;

        componentRigidbody = GetComponent<Rigidbody2D>();
        componentRigidbody.AddForce(new Vector2(dir, force), ForceMode2D.Impulse);
    }
    void FixedUpdate()
    {
 

  

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Box") 

        {
            col.gameObject.GetComponent<ObjectConstruction>().Damage(2);
            Destroy(gameObject, 0.4f);
            if (col.gameObject.gameObject.GetComponent<ObjectConstruction>().Lives <= 0)
            {
                Destroy(col.gameObject, 0.1f);
            }
        }

        if (col.gameObject.tag == "Ground")
        {   
                Destroy(gameObject, 0.1f);
        }

        if (col.gameObject.tag == "Player")

        {
            if (MeterorTouch)
                {
                col.rigidbody.AddForce(new Vector2(0, 1000));
                MeterorTouch = false;
                }
        }
    }
	
}
