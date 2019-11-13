using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public List<GameObject> objetcCount;
    public int StartCount = 6;
    public GameObject player;

    public GameObject SpawnPoint;
    public GameObject[] SetItems;
    public LayerMask layermask;
    public void Start()
    {
        objetcCount = new List<GameObject>();
        objetcCount.Add(player);
    }
    public void Spawn()
    {
        var p = SpawnPoint.transform.position;
        for (int i = 0; i < StartCount; i++)
        {
            int Type = Random.Range(0, SetItems.Length);
            float xx = Random.Range(-4, 4);
            float yy = Random.Range(0, 5);
            //Debug.DrawLine(new Vector2(p.x + xx, p.y + yy), new Vector2(p.x + xx, p.y + yy));
            while (Physics2D.OverlapArea(new Vector2(p.x + xx, p.y + yy), new Vector2(p.x + xx, p.y + yy), layermask) == null)
            {
                GameObject construction = Instantiate(SetItems[Type], new Vector3(p.x + xx, p.y + yy, p.z), Quaternion.identity);
                construction.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                construction.GetComponent<Rigidbody2D>().isKinematic = true;
                objetcCount.Add(construction);
            }
        }
    }
    public void UnFreez()
    {
        foreach (var item in objetcCount)
        {
            if(item.GetComponent<DragObject>().MouseOn != true)
                item.GetComponent<DragObject>().enabled = false;
            item.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
