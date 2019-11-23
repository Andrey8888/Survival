using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] public CreateObject create;
    [SerializeField] private GameObject lighting;
    public bool IsLight;
    public void Start()
    {
        IsLight = false;
    }
    public void LightingStart()
    {
        if (create.objetcCount.FirstOrDefault(x => x != null))
            {
            while (!IsLight)
            {
                int rnd = Random.Range(0, create.objetcCount.Count);

                if (create.objetcCount[rnd] != null)
                {
                    //if (create.objetcCount[rnd] == create.objetcCount[0])
                    //{
                    //    //float maxPosY = -100f;
                    //    //var obj = create.objetcCount[0];
                    //    //foreach (var item in create.objetcCount)
                    //    //{
                    //    //    if (item != null)
                    //    //        {
                    //    //        var temp = item.gameObject.transform.position.y;
                    //    //        if (maxPosY < temp)
                    //    //        {
                    //    //            maxPosY = temp;
                    //    //            obj = item;
                    //    //        }
                    //    //    }
                    //    //}
                    //}
                    GameObject LightObj = Instantiate(lighting, new Vector3(create.objetcCount[rnd].transform.position.x - 0.25f,
                    create.objetcCount[rnd].transform.position.y + 3,
                    create.objetcCount[rnd].transform.position.z), Quaternion.identity);
                    create.objetcCount[rnd].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                    //if (obj.GetComponent<ObjectConstruction>().materials == ObjectConstruction.Materials.Glass)
                    //{
                    //    obj.GetComponent<ObjectConstruction>().Damage(0);
                    //}
                    //else
                    if (create.objetcCount[rnd].gameObject.GetComponent<Player>() == true)
                    {
                        create.objetcCount[rnd].gameObject.GetComponent<Player>().Damage(2);
                        create.objetcCount[rnd].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                    }
                    else
                        create.objetcCount[rnd].GetComponent<ObjectConstruction>().Damage(4);
                    Destroy(LightObj, 0.8f);
                    IsLight = true;
                }
            }
            IsLight = false;
        }
    }
}
