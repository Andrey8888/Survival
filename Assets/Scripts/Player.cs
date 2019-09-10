using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    [SerializeField]
    private Transform groundCheck;

    public bool isGrounded;
    public bool isSpawn;
    public bool isParashuteOff;

    public LayerMask whatIsGround;
    public int StartCount;

    public GameObject Box;
    public GameObject Plank;
    public GameObject Spawn;

    private Rigidbody2D rb2d;
    private Animator anim;

    private float groundAngle = 0;
    private Transform curObj;

    public List<GameObject> objetcCount = new List<GameObject>();

    public void Awake()
    {
        isParashuteOff = true;
        isGrounded = true;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCount = 6;

        isSpawn = true;

        curObj = transform;
        curObj.GetComponent<Rigidbody2D>().gravityScale = 0.08f;
        objetcCount.Add(curObj.gameObject);
    }

    void FixedUpdate () {


        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(System.Convert.ToSingle(0.1), System.Convert.ToSingle(0.25)), groundAngle, whatIsGround);
        anim.SetBool("Parashute", isParashuteOff);

        if (!isGrounded )
            {
            float moveHorizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        }
        }
void Update()
    {

        //if (curObj.GetComponent<BoxCollider2D>() == null || objetcCount.Count == 0)
        //{
        //    isParashuteOff = true;
        //    Application.LoadLevel(0);

        //}

        if (isGrounded == true && isSpawn == true ||  Input.GetKeyDown(KeyCode.Space) && isSpawn == true)
        {
            curObj.GetComponent<Rigidbody2D>().gravityScale = 1;
            isParashuteOff = false;
        }

        if (isGrounded == true && isSpawn == true )
        {

            //Debug.Log(curObj.GetComponent<Rigidbody2D>().gravityScale);
            var p = Spawn.transform.position;
            for (int i = 0; i < StartCount; i++)
            {
                int Type = Random.Range(1, 3);
                float xx = Random.Range(-3, 3);
                float yy = Random.Range(3, 5);
                switch (Type)
                {
                    case 1:
                        GameObject objBox =  Instantiate(Box, new Vector3(p.x + xx, p.y + yy, p.z), Quaternion.identity);
                        objBox.GetComponent<Rigidbody2D>().AddForce(new Vector2 (0, 300));
                        objetcCount.Add(objBox);

                        break;
                    case 2:
                        GameObject objPlank = Instantiate(Plank, new Vector3(p.x + xx, p.y + yy, p.z), Quaternion.identity);
                        objPlank.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                        objetcCount.Add(objPlank);
                        break;

                }
                isSpawn = false;
            }
        }
    }


}
