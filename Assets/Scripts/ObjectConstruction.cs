using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectConstruction : MonoBehaviour
{
    public enum Materials
    {
        Wood,
        Metal,
        Glass,
        Stone,
        Explosive
    }
    public Materials materials;

    public int currentHealth;
    public int startHealth;
    public int globalCount = 0;
    [Title("HealthBar stuff")]
    public Image healthBar;
    public Image healthBarBG;
    public float highObj;
    public Canvas canvas;

    [Title("Scratched Object")]
    public Sprite scratchedSprite;
    [Title("Broken Object")]
    public Sprite brokenSprite;

    private Quaternion InitRot;
    private Vector3 InitPos;

    void Start()
    {
        ApplyingMaterials();
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
        InitRot = canvas.transform.localRotation;
        currentHealth = startHealth;
    }
    public void Update()
    {
        healthBar.fillAmount = currentHealth * 100f / (startHealth * 100f);
        if (currentHealth <= 4)
        gameObject.GetComponent<SpriteRenderer>().sprite = scratchedSprite;
        if (currentHealth <= 2)
        gameObject.GetComponent<SpriteRenderer>().sprite = brokenSprite;
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0.1f);
        }
    }
    private void LateUpdate()
    {
        canvas.transform.rotation = Quaternion.Euler(InitRot.x, InitRot.y , InitRot.z);
        canvas.transform.position = new Vector3(transform.position.x  , transform.position.y + highObj , 0);
    }
    public void Damage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.gameObject.SetActive(true);
        healthBarBG.gameObject.SetActive(true);
        StartCoroutine(ShowHealth());
    }
    public void ApplyingMaterials()
    {
                if (materials == Materials.Glass)
        {
            startHealth = 4;
        }
        if (materials == Materials.Wood)
        {
            startHealth = 6;
        }
        if (materials == Materials.Stone)
        {
            startHealth = 8;
        }
        if (materials == Materials.Metal)
        {
            startHealth = 10;
        }
        if (materials == Materials.Explosive)
        {
            startHealth = 10;
        }

    }
    IEnumerator ShowHealth()
    {
        yield return new WaitForSeconds(4f);
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
    }
}

