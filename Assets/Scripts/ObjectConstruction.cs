using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConstruction : MonoBehaviour
{
    public int Lives = 6;
    public Sprite sprites;

    //private Player player;
    void Start()
    {
        Lives = 6;
    }
    public void Update()
    {
        if (Lives <= 3)
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites;
    }
    public void Damage(int damage)
    {
        Lives = Lives - damage;
    }

}
