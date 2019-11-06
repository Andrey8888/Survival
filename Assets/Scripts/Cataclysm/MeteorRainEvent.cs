using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRainEvent : MonoBehaviour
{
    [SerializeField] private Transform meteorPref;
    public Transform[] meteorPos;
    IEnumerator MeteorCoroutine()
    {
        while (true)
        {
            int rnd = Random.Range(0, meteorPos.Length);
            Instantiate(meteorPref, new Vector2(meteorPos[rnd].position.x, meteorPos[rnd].position.y), Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}
