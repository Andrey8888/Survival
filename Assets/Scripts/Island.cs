using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    private bool i;
    public bool isActive;
    private GameObject Up;
    private GameObject Down;
    public float speed;
    private void Start()
    {
        i = true;
        Up = GameObject.FindGameObjectWithTag("Up");
        Down = GameObject.FindGameObjectWithTag("Down");
    }
    // Update is called once per frame
    private void Update()
    {
        if (isActive)
        {
            if (i == true)
            {
                transform.localPosition = Vector3.MoveTowards(transform.position, Up.transform.position, speed * Time.deltaTime);
                if (transform.position == Up.transform.position)
                {
                    i = false;
                }
            }

            if (i == false)
            {
                transform.localPosition = Vector3.MoveTowards(transform.position, Down.transform.position, speed * Time.deltaTime);
                if (transform.position == Down.transform.position)
                {
                    i = true;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        if ((collider.gameObject.tag == "Box") || (collider.gameObject.tag == "Player"))

        {
            collider.transform.parent = transform;
        }

    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if ((collider.gameObject.tag == "Box") || (collider.gameObject.tag == "Player"))
        {
            collider.transform.parent = null;
        }
    }
}