using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public Player player;

    public GameObject lighting;
    void Start()
    {
        
    }

    public void LightingStart()
    {
        if (player.objetcCount.Count > 1)
        {

            int rnd = Random.Range(1, player.objetcCount.Count);
            GameObject LightObj = Instantiate(lighting, new Vector3(player.objetcCount[rnd].transform.position.x - 0.25f, 
                player.objetcCount[rnd].transform.position.y + 3, 
                player.objetcCount[rnd].transform.position.z), Quaternion.identity);

                player.objetcCount[rnd].GetComponent<ObjectConstruction>().Damage(3);

            if (player.objetcCount[rnd].GetComponent<ObjectConstruction>().Lives <= 0)
            {
                Destroy(player.objetcCount[rnd], 0.4f);
                player.objetcCount.RemoveAt(rnd);
            }

            Destroy(LightObj, 0.8f);
            //Debug.Log("objetcCount " + objetcCount.Count);
        }
    }
}
