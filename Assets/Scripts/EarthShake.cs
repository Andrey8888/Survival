using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShake : MonoBehaviour
{
    private bool i;
    public bool isActive;
    private GameObject Up;
    private GameObject Down;

    private void Start()
    {
        i = true;
        Up = GameObject.FindGameObjectWithTag("Up");
        Down = GameObject.FindGameObjectWithTag("Down");
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        float rnd = Random.Range(-0.03f, 0.03f);
        if (isActive)
        {
            if (i == true)
            {
                transform.localPosition = Vector3.MoveTowards(new Vector2 (transform.position.x + rnd, transform.position.y + rnd), Up.transform.position, Time.fixedDeltaTime);
                if (transform.position == Up.transform.position)
                {
                    i = false;
                }
            }
            if (i == false)
            {
                transform.localPosition = Vector3.MoveTowards(transform.position, Down.transform.position, Time.fixedDeltaTime);
                if (transform.position == Down.transform.position)
                {
                    i = true;
                }
            }
        }
    }
}
