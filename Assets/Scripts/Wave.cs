using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waveForce = 4f;
    [SerializeField] private float buoyancyForce = 2f;
    [SerializeField] private float fluctuations;

    private float Y;
    private float X;
    private Vector3 direction;

    void Start()
    {
        speed = Random.Range(2f, 3f);
        fluctuations = Random.Range(0.5f, 1.5f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Y * Time.fixedDeltaTime);
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
        if (transform.position.x < 2)
        {
            Y = Mathf.Sin(0.04f * X);
            X += 1.5f;
        }
        else
        {
            Y = -0.4f;
        }
        if (transform.position.x > 14)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Box") || (col.gameObject.tag == "Player"))
        {
            Debug.Log("Встреча с объектом :" + col.gameObject.name + "Скорость волны " + waveForce);
            waveForce /= 1.7f;
            buoyancyForce /= 1.1f;
            //col.GetComponent<Rigidbody2D>().AddForce(transform.right * waveForce, ForceMode2D.Impulse);
            //col.GetComponent<Rigidbody2D>().velocity = Vector2.up * buoyancyForce;
            //Y = -0.5f;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Box") || (col.gameObject.tag == "Player"))
        {
            col.transform.Translate(Vector3.right * waveForce / (col.GetComponent<Rigidbody2D>().mass) * Time.fixedDeltaTime);
            col.transform.Translate(Vector3.up * waveForce * Time.fixedDeltaTime);
            //col.GetComponent<Rigidbody2D>().velocity = Vector2.right * waveForce / 3;
        }
    }
}
