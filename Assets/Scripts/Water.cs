using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private Player player;
    public float speed = 0.4f;
    public GameObject WaterPref;


    void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);


        if (transform.position.x > 22)
        {
            Instantiate(WaterPref, new Vector2(-23, -1.25f), Quaternion.identity);
            Destroy(gameObject);

        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if ( (col.gameObject.tag == "Player"))

        {
            Destroy(col);
        }
    }


}
