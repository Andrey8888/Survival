using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWindEvent : MonoBehaviour
{
    [SerializeField] private Transform windPref;
    private GameObject windPos;
    private void Start()
    {
        windPos = GameObject.FindGameObjectWithTag("Wind");
    }
    IEnumerator WindCoroutine()
    {
        while (true)
        {
            //int rnd = Random.Range(0, windPos.Length);
            Instantiate(windPref, new Vector2(windPos.transform.position.x, windPos.transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(30);
        }
    }
}
