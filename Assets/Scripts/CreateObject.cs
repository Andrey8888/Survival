using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public List<GameObject> objetcCount;
    public int StartCount = 7;
    public GameObject player;

    public GameObject SpawnPoint;
    public GameObject[] SetItems;
    public LayerMask layermask;
    public void Start()
    {
        objetcCount = new List<GameObject>();
        objetcCount.Add(player);
    }
    IEnumerator Spawn()
    {
        var p = SpawnPoint.transform.position;
        for (int i = 0; i < StartCount; )
        {
            int Type = Random.Range(0, SetItems.Length);
            float xx = Random.Range(-4, 4);
            float yy = Random.Range(2, 5);
            if (Physics2D.OverlapArea(new Vector2(p.x + xx, p.y + yy), new Vector2(p.x + xx - 2f, p.y + yy + 2f), layermask) == null)
            {
                GameObject construction = Instantiate(SetItems[Type], new Vector3(p.x + xx, p.y + yy, p.z), Quaternion.identity);
                construction.GetComponent<Rigidbody2D>().isKinematic = true;
                construction.GetComponent<DragObject>().enabled = true;
                objetcCount.Add(construction);
                i++;           
            }
            yield return new WaitForSeconds(0);
        }
    }
    IEnumerator Freezing()
    {
        foreach (var item in objetcCount)
        {
            if (item != null)
            {
                if (item.GetComponent<DragObject>() != null)
                {
                    if (item.GetComponent<DragObject>().MouseOn != true)
                    {
                        item.GetComponent<Rigidbody2D>().isKinematic = false;
                        item.GetComponent<DragObject>().enabled = false;
                    }
                    item.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }
        }
        yield return new WaitForSeconds(0);
    }
    IEnumerator Defrosting()
    {
        foreach (var item in objetcCount)
        {
            if (item != null)
            {
                if (item.GetComponent<DragObject>() != null)
                {
                    item.GetComponent<DragObject>().enabled = true;
                }
            }
        }
        yield return new WaitForSeconds(0);
    }
}
