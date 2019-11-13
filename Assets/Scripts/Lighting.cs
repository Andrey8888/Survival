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
                float maxPosY = -100f;
                var obj = create.objetcCount[0];
                foreach (var item in create.objetcCount)
                {
                    if (item != null)
                        {
                        var temp = item.gameObject.transform.position.y;
                        if (maxPosY < temp)
                        {
                            maxPosY = temp;
                            obj = item;
                        }
                    }
                }
                if (obj != null)
                {
                    GameObject LightObj = Instantiate(lighting, new Vector3(obj.transform.position.x - 0.25f,
                    obj.transform.position.y + 3,
                    obj.transform.position.z), Quaternion.identity);
                    obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                    //if (obj.GetComponent<ObjectConstruction>().materials == ObjectConstruction.Materials.Glass)
                    //{
                    //    obj.GetComponent<ObjectConstruction>().Damage(0);
                    //}
                    //else
                    if (obj.gameObject.GetComponent<Player>() == true)
                    {
                        obj.gameObject.GetComponent<Player>().Damage(2);
                        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                    }
                    else
                        obj.GetComponent<ObjectConstruction>().Damage(4);
                    Destroy(LightObj, 0.8f);
                    IsLight = true;
                }
            }
            IsLight = false;
        }
    }
}
