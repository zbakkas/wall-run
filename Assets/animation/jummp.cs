using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jummp : MonoBehaviour
{

    public Animator an;
    public float moveSpeed = 5f;

    public Rigidbody rb;

    public int lane = 2;
    public float graound;
    float startpositin;

     public bool jump,isjump,isslid;
    public float Speed;
    public bool bar;

    public float playerhight;
    public LayerMask groundLayer;
    public bool graounded;

    // Start is called before the first frame update
    void Start()
    {
        startpositin = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        graounded = Physics.Raycast(transform.position, Vector3.down, playerhight * 0.5f, groundLayer);


        if (!graounded )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1 * Time.deltaTime * Speed, transform.position.z);
        }



        if (Input.GetKeyDown(KeyCode.UpArrow)&&!isjump)
        {
            an.SetTrigger("jump");

            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)&&!isslid)
        {
            an.SetTrigger("slide");
            
        }

        



        if (lane == 3 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane = 4;
            transform.position += new Vector3(0.5f, 0, 0);
     
            an.SetFloat("x", 1);
        }
        else if (lane == 1 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane = 0;
            transform.position -= new Vector3(0.5f, 0, 0);
         
            an.SetFloat("x", 0.75f);
        }

        leftAndRight();

        if (lane == 0 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane = 1;
            transform.position += new Vector3(0.5f, 0, 0);
      
            an.SetFloat("x", 0);
        }
        else if (lane == 4 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane = 3;
            transform.position -= new Vector3(0.5f, 0, 0);
          
            an.SetFloat("x", 0);
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
    }

    private void leftAndRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && lane == 1)
        {
            lane = 2;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lane == 2 && transform.position.x <= startpositin + 0.2f)
        {
            lane = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lane == 2 && transform.position.x >= startpositin - 0.2f)
        {
            lane = 3;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lane == 3)
        {
            lane = 2;
        }
        //////////////////
        if (lane == 3 && transform.position.x < startpositin + graound&&!bar)
        {

            if (transform.position.x >= 2.5f)
            {
               // transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(10.5f * Time.deltaTime, 0.01f, 0);
            }
        }
        else if (lane == 1 && transform.position.x > startpositin - graound && !bar)
        {

            if (transform.position.x <= -0.5f)
            {
               // transform.position = new Vector3(-0.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(-10.5f * Time.deltaTime, 0.01f, 0);
            }
        }
        else if (lane == 2 && transform.position.x <= startpositin - 0.1f && !bar)
        {

            if (transform.position.x >= startpositin)
            {
               // transform.position = new Vector3(startpositin, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(10.5f * Time.deltaTime, 0.01f, 0);
            }
        }
        else if (lane == 2 && transform.position.x >= startpositin + 0.1f && !bar)
        {

            if (transform.position.x <= startpositin)
            {
               // transform.position = new Vector3(startpositin, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(-10.5f * Time.deltaTime, 0.01f, 0);
            }
        }

    }
    public void truee()
    {
        isjump = false;
        //rb.useGravity = true;
    }

    public void truee1()
    {
        isjump = true;
        rb.useGravity = false;
        isslid = false;
    }
    public void issliding()
    {
        isslid = true;
       // rb.useGravity = true;
        isjump = false;

    }

    public void isslidingEnd()
    {
        isslid = false;
        

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jump = false;
        }
        if (collision.gameObject.tag == "bar")
        {
            bar = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jump = true;
        }
    }
    

}
