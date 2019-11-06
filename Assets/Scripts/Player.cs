using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int Lives = 3;

    private float groundAngle = 0;
    private Transform curObj;

    public void Awake()
    {
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
            createobject.Spawn();
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
        if (isGrounded == true ||  Input.GetKeyDown(KeyCode.Space))
        {
            curObj.GetComponent<Rigidbody2D>().gravityScale = 1;
            isParashuteOff = false;
        }
    }
    public void Damage(int damage)
    {
        Lives = Lives - damage;
    }

}
