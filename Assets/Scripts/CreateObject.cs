using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public List<GameObject> objetcCount;
    public int StartCount = 6;
    //public GameObject player;

    public GameObject SpawnPoint;
    public GameObject[] SetItems;
    public LayerMask layermask;

    public void Start()
    {
        objetcCount = new List<GameObject>();
        //objetcCount.Add(player);
    }

    IEnumerator Spawn()
    {
        var pos = SpawnPoint.transform.position;
        for (int i = 0; i < StartCount;)
        {
                int type = Random.Range(0, SetItems.Length);
                float spawnPosX = Random.Range(-3, 6);
                float spawnPosY = Random.Range(2, 6);
                Vector3 spawnPos = new Vector3(pos.x + spawnPosX, pos.y + spawnPosY, 0);
                Vector3 spawnPos2 = new Vector3(pos.x + spawnPosX -2f , pos.y + spawnPosY - 2f, 0);
                while (!Physics2D.OverlapArea(spawnPos, spawnPos2, layermask))
                {
                    GameObject construction = Instantiate(SetItems[type], (spawnPos + spawnPos2)/2, Quaternion.identity);
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
