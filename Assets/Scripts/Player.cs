using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    public float speed;
    [SerializeField]
    private Transform groundCheck;
    public CreateObject createobject;

    public bool isGrounded;
    public bool isParashuteOff;
    public bool IsHitPlayer;

    public LayerMask whatIsGround;
    public bool isSpawn;

    public Material noAlpha;
    private Material standart;

    private Rigidbody2D rb2d;
    private Animator anim;

    public float currentHealth;
    private float startHealth = 10;

    [Title("HealthBar stuff")]
    public Image healthBar;
    public Image healthBarBG;
    public Canvas canvas;

    private float groundAngle = 0;
    private Transform curObj;

    public void Start() // поменял с Awake
    {
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
        isSpawn = true;
        isParashuteOff = true;
        isGrounded = true;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        IsHitPlayer = false;

        curObj = transform;
        curObj.GetComponent<Rigidbody2D>().gravityScale = 0.08f;

        standart = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = noAlpha;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(System.Convert.ToSingle(0.1), System.Convert.ToSingle(0.25)), groundAngle, whatIsGround);
        anim.SetBool("Parashute", isParashuteOff);

        if (!isGrounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        }
        if (isGrounded == true && isSpawn == true)
        {
            createobject.StartCoroutine("Spawn");
            healthBar.gameObject.SetActive(true);
            healthBarBG.gameObject.SetActive(true);
            isSpawn = false;
        }
    }
    void Update()
    {
        if (curObj.GetComponent<BoxCollider2D>() == null || currentHealth <= 0)
        {
            isParashuteOff = true;
            Application.LoadLevel(0);
        }
        healthBar.fillAmount = currentHealth * 100f / (startHealth * 100f);
        if (isGrounded == true || Input.GetKeyDown(KeyCode.Space))
        {
            curObj.GetComponent<Rigidbody2D>().gravityScale = 1;
            isParashuteOff = false;
        }
    }
    public void Damage(int damage)
    {
        currentHealth = currentHealth - damage;
        StartCoroutine(ShowHealth());
        StartCoroutine(ShowLight());
        GetComponent<SpriteRenderer>().material = standart;
    }
    IEnumerator ShowLight()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().material = noAlpha;
    }
    IEnumerator ShowHealth()
    {
        yield return new WaitForSeconds(4f);
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
    }
}
