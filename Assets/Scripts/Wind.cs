using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float speed = 1f;
    public float windForce = 6f;
    private Rigidbody2D componentRigidbody;
    private Vector3 pos;
    private float direction;

    void Awake()
    {
        pos = gameObject.transform.position;
        componentRigidbody = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * speed * direction * Time.fixedDeltaTime);
        if (transform.position.x < (pos.x - 6))

        {
            direction = -1;
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
            col.GetComponent<Rigidbody2D>().velocity = new Vector2 (direction * windForce/(col.GetComponent<Rigidbody2D>().mass * 2) , windForce / col.GetComponent<Rigidbody2D>().mass);
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
