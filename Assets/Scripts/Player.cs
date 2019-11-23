using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour {

    public float speed;
    [SerializeField]
    private Transform groundCheck;
    public CreateObject createobject;

    public bool isGrounded;
    public bool isParashuteOff;

    public LayerMask whatIsGround;
    public bool isSpawn;

    private Rigidbody2D rb2d;
    private Animator anim;

    public float currentHealth;
    private float startHealth  = 6;

    [Title("HealthBar stuff")]
    public Image healthBar;
    public Image healthBarBG;
    public Canvas canvas;

    private float groundAngle = 0;
    private Transform curObj;

    public void Awake()
    {
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
        isSpawn = true;
        isParashuteOff = true;
        isGrounded = true;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        curObj = transform;
        curObj.GetComponent<Rigidbody2D>().gravityScale = 0.08f;
    }

    void FixedUpdate () 
        {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(System.Convert.ToSingle(0.1), System.Convert.ToSingle(0.25)), groundAngle, whatIsGround);
        anim.SetBool("Parashute", isParashuteOff);

        if (!isGrounded )
            {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        }
        if (isGrounded == true && isSpawn == true)
        {
            createobject.StartCoroutine("Spawn");
            isSpawn = false;
        }
    }
void Update()
    {
        //if (curObj.GetComponent<BoxCollider2D>() == null || objetcCount.Count == 0)
        //{
        //    isParashuteOff = true;
        //    Application.LoadLevel(0);
        //}
        healthBar.fillAmount = currentHealth * 100f / (startHealth * 100f);
        if (isGrounded == true ||  Input.GetKeyDown(KeyCode.Space))
        {
            curObj.GetComponent<Rigidbody2D>().gravityScale = 1;
            isParashuteOff = false;
        }
    }
    public void Damage(int damage)
    {
        currentHealth = currentHealth - damage;
        StartCoroutine(ShowHealth());
    }
    IEnumerator ShowHealth()
    {
        yield return new WaitForSeconds(4f);
        healthBar.gameObject.SetActive(false);
        healthBarBG.gameObject.SetActive(false);
    }
}
