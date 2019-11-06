using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConstruction : MonoBehaviour
{
    [SerializeField] private enum Materials
    {
        Wood,
        Metal,
        Glass,
        Stone,
        Explosive
    }
    public int Lives = 6;
    [Title("Scratched Object")]
    public Sprite scratchedSprite;
    [Title("Broken Object")]
    public Sprite brokenSprite;
    void Start()
    {
        Lives = 6;
    }
    public void Update()
    {
        if (Lives <= 4)
        gameObject.GetComponent<SpriteRenderer>().sprite = scratchedSprite;
        if (Lives <= 2)
        gameObject.GetComponent<SpriteRenderer>().sprite = brokenSprite;
    }
    public void Damage(int damage)
    {
        Lives = Lives - damage;
    }
}
