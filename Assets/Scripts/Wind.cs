using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float speed = 1f;
    public float windForce = 2f;
    private Rigidbody2D componentRigidbody;
    private Vector3 pos;
    void Awake()
    {
        pos = gameObject.transform.position;
        componentRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);


        if (transform.position.x < (pos.x - 6))
        {
            speed = speed * -1;
        }

        if (transform.position.x > (pos.x + 1))
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerStay2D(Collider2D col)
    {

        if ((col.gameObject.tag == "Box") || (col.gameObject.tag == "Player"))

        {
            col.transform.Translate(Vector3.up * windForce * Time.deltaTime);
            col.transform.Translate(Vector3.left * windForce / 4 * speed * Time.deltaTime);
            Debug.Log("OnCollisionEnter2D");
        }
    }

		  //  void OnTriggerExit2D(Collider2D col)
    //{

    //    if ((col.gameObject.tag == "Box") || (col.gameObject.tag == "Player"))

    //    {
    //        col.transform.Translate(Vector3.down * 0);
    //        col.transform.Translate(Vector3.down * 0);
    //        Debug.Log("OnCollisionExit2D");
    //    }
    //}
	
}
