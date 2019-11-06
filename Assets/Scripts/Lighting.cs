using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] public CreateObject create;
    [SerializeField] private GameObject lighting;
    public void LightingStart()
    {
            int rnd = Random.Range(1, create.objetcCount.Count);
            GameObject LightObj = Instantiate(lighting, new Vector3(create.objetcCount[rnd].transform.position.x - 0.25f, 
                create.objetcCount[rnd].transform.position.y + 3, 
                create.objetcCount[rnd].transform.position.z), Quaternion.identity);
                create.objetcCount[rnd].GetComponent<ObjectConstruction>().Damage(3);

            if (create.objetcCount[rnd].GetComponent<ObjectConstruction>().Lives <= 0)
            {
                Destroy(create.objetcCount[rnd], 0.4f);
                create.objetcCount.RemoveAt(rnd);
            }
            Destroy(LightObj, 0.8f);
            //Debug.Log("objetcCount " + objetcCount.Count);
    }
}
