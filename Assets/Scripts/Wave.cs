using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waveForce = 6f;
    [SerializeField] private float fluctuations = 1f;

    private float Y;
    private float X;
    void Start()
    {
        speed = Random.Range(2f, 3f);
        fluctuations = Random.Range(0.5f, 1f);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Y * Time.deltaTime);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        Y = Mathf.Sin(0.04f * X);
        X += 1f;

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
            waveForce /= 2f;
            //Y = -1;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Box") || (col.gameObject.tag == "Player"))
        {
            col.transform.Translate(Vector3.right * waveForce * Time.deltaTime);
            //col.transform.Translate(Vector3.up * waveForce / 1.4f * Time.deltaTime);
        }
    }
}
