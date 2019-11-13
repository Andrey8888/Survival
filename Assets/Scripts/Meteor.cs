using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{  

    private GameObject player;
    [SerializeField] private CreateObject create;

    private Rigidbody2D componentRigidbody;
    public bool MeterorTouch;

    private void Start()
    {
        int dir = Random.Range(-20, -10);
        int force = Random.Range(-5, -15);

        MeterorTouch = true;

        componentRigidbody = GetComponent<Rigidbody2D>();
        componentRigidbody.AddForce(new Vector2(dir, force), ForceMode2D.Impulse);
        create = FindObjectOfType<CreateObject>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Box") 
        {
            //int obj = col.gameObject.GetComponent<ObjectConstruction>().globalCount;
            col.gameObject.GetComponent<ObjectConstruction>().Damage(2);
            Destroy(gameObject, 0.4f);
        }
        if (col.gameObject.tag == "Ground")
        {   
                Destroy(gameObject, 0.4f);
            //this.componentRigidbody.freezeRotation = true;
        }
        if (col.gameObject.tag == "Player")
        {
            if (MeterorTouch)
                {
                col.gameObject.GetComponent<Player>().Damage(2);
                col.rigidbody.AddForce(new Vector2(0, 1000));
                Destroy(gameObject, 0.4f);
                MeterorTouch = false;
                if (col.gameObject.GetComponent<Player>().currentHealth <= 0)
                {
                    Destroy(col.gameObject, 0.1f);
                }
            }
        }
    }	
}
