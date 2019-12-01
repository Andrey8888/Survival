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

    public Material noAlpha;
    private Material standart;
    private HashSet<GameObject> electrifiedObjects;
    private Rigidbody2D rb2d;

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
    public bool IsElectrified;
    public bool IsElectrifiedPlayer;

    private Player player;
    private SceneController controller;
    private float Speed;

    private float startDelayFallingDamage = 1f;
    private float delayFallingDamage = 1f;

    void Start()
    {
        ApplyingMaterials();
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
        InitRot = canvas.transform.localRotation;
        currentHealth = startHealth;

        standart = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = noAlpha;

        IsElectrified = false;
        IsElectrifiedPlayer = false;

        electrifiedObjects = new HashSet<GameObject>();
        player = FindObjectOfType<Player>();
        controller = FindObjectOfType<SceneController>();
        rb2d = GetComponent<Rigidbody2D>();

        delayFallingDamage = startDelayFallingDamage;
    }
    public void Update()
    {
        Speed = rb2d.velocity.magnitude;

        healthBar.fillAmount = currentHealth * 100f / (startHealth * 100f);
        if (currentHealth <= 4)
            gameObject.GetComponent<SpriteRenderer>().sprite = scratchedSprite;
        if (currentHealth <= 2)
            gameObject.GetComponent<SpriteRenderer>().sprite = brokenSprite;
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0.1f);
        }

        delayFallingDamage -= Time.deltaTime;
    }
    void RefreshDelay()
    {
        this.delayFallingDamage = this.startDelayFallingDamage;
    }
    private void LateUpdate()
    {
        canvas.transform.rotation = Quaternion.Euler(InitRot.x, InitRot.y, InitRot.z);
        canvas.transform.position = new Vector3(transform.position.x, transform.position.y + highObj, 0);
    }
    public void Damage(int damage)
    {
        IsElectrified = false;
        currentHealth = currentHealth - damage;
        healthBar.gameObject.SetActive(true);
        healthBarBG.gameObject.SetActive(true);
        StartCoroutine(ShowHealth());

        if (this.materials == Materials.Metal)
        {
            StartCoroutine(ShowLight());
        }
        GetComponent<SpriteRenderer>().material = standart;
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
    IEnumerator ShowLight()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().material = noAlpha;
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{

    //    float power = collision.relativeVelocity.magnitude;
    //    if (power >= dangerPower)
    //    {
    //        state.SetPower(power);
    //    }
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Box") || (collision.gameObject.tag == "Player"))
        {
            if (collision)
            {
                if (!electrifiedObjects.Contains(collision.gameObject))
                {
                    if ((collision.GetComponent<ObjectConstruction>() != null) && collision.GetComponent<ObjectConstruction>().materials == Materials.Metal)
                    {
                        electrifiedObjects.Add(collision.gameObject);
                    }
                }
                //col.gameObject.GetComponent<ObjectConstruction>().IsElectrified = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            if (collision)
            {
                if (!electrifiedObjects.Contains(collision.gameObject))
                {
                    electrifiedObjects.Add(collision.gameObject);
                }
                //col.gameObject.GetComponent<ObjectConstruction>().IsElectrified = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (!controller.isSpacePressed)
        {
            if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
            {
                if (Speed > 3)
                {
                    if (delayFallingDamage > 0) return;
                    {
                        RefreshDelay();
                        Debug.Log(Speed);
                        int damageFalling = (int)Speed / 3;
                        if (collision.GetComponent<ObjectConstruction>() != null)
                        {
                            Debug.Log("Сила удара " + collision.gameObject.name + " : " + damageFalling);
                            collision.GetComponent<ObjectConstruction>().Damage(damageFalling);
                        }
                        if (collision.GetComponent<Player>() != null)
                        {
                            Debug.Log("Сила удара " + collision.gameObject.name + " : " + damageFalling);
                            collision.gameObject.GetComponent<Player>().Damage(damageFalling);
                        }
                        Damage(damageFalling);
                        Debug.Log("Собственая сила удара " + this.gameObject.name + " : " + damageFalling);
                    }
                }
            }
        }


        if ((collision.gameObject.tag == "Box") && (IsElectrified == true) )
        {
            IsElectrified = false;
            foreach (var item in electrifiedObjects)
            {
                if(item != null)
                    { 
                    if (item.GetComponent<ObjectConstruction>() != null)
                        item.GetComponent<ObjectConstruction>().Damage(1);
                    }
            }
        }
        if ((collision.gameObject.tag == "Player") && (IsElectrifiedPlayer == true))
        {
            IsElectrifiedPlayer = false;
            Debug.Log(collision.gameObject.name + ": ударило молнией");
            player.gameObject.GetComponent<Player>().Damage(1);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
        }
    }
}

