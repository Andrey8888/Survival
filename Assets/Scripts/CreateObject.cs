using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public List<GameObject> objetcCount = new List<GameObject>();
    public int StartCount = 6;

    public GameObject SpawnPoint;
    public GameObject[] SetItems;
    public void Spawn()
    {
        //Debug.Log(curObj.GetComponent<Rigidbody2D>().gravityScale);
        var p = SpawnPoint.transform.position;
        for (int i = 0; i < StartCount; i++)
        {
            int Type = Random.Range(0, SetItems.Length);
            float xx = Random.Range(-3, 3);
            float yy = Random.Range(3, 5);
            GameObject construction = Instantiate(SetItems[Type], new Vector3(p.x + xx, p.y + yy, p.z), Quaternion.identity);
            construction.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
            //construction.GetComponent<Rigidbody2D>().isKinematic = true;
            objetcCount.Add(construction);
        }
    }
}

