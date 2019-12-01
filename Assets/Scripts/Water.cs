using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Player player;
    public float speed = 0.4f;
    public GameObject WaterPref;

    public SceneController controller;
    private bool IsFflod;

    public void Start()
    {
        controller = FindObjectOfType<SceneController>();
        controller.IsWaves = false;
        IsFflod = false;
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > 22)
        {
            Instantiate(WaterPref, new Vector2(-23, -1.25f), Quaternion.identity);
            Destroy(gameObject);
        }
        if(controller.IsWaves && transform.position.y <= -0.1)
            {
            transform.Translate(Vector3.up * 0.05f * Time.deltaTime);
            if (transform.position.y >= -0.27)
            IsFflod = true;
            }
        if ( IsFflod )
        {
            transform.Translate(Vector3.down * 0.2f * Time.deltaTime);
            if (transform.position.y <= -1.25)
            IsFflod = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col);
            Debug.Log("player destroed");
        }
    }
}
