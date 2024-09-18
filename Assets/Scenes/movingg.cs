using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingg : MonoBehaviour
{
    public float movespeed;
    float vertical =1,horizontal;

    public Transform orientation;

    Vector3 movdirection;
    Rigidbody rb;
    [Header("chek ground")]
    public float playerheigh;
    public float grounDrag;
    public LayerMask whatIsGround;
    public bool grouned;

    [Header("JUMP")]
    public float jumpForce;
    public float jumpcooldown,airmulty;
    public bool redetojump;
    public float jadibya;

    [Header("slope")]
    public float maxslope;
    private RaycastHit slopehit;
  
    public CapsuleCollider cap1, cap2;

    [Header("mov left and right")]
    public float line;
    public Animator an;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        redetojump = true;

    }

    // Update is called once per frame
    void Update()
    {
        grouned = Physics.Raycast(transform.position, Vector3.down, playerheigh * 0.5f+0.2f, whatIsGround);
       
        if (Input.GetKeyDown(KeyCode.UpArrow) && redetojump && grouned)
        {
            redetojump = false;
            jump();
            Invoke("rasetjump", jumpcooldown);

        }



        if (grouned)
        {
            rb.drag = grounDrag;
            an.SetBool("isground",true);
        }
        else
        {
            rb.drag = 0;
            an.SetBool("isground", false);
        }

        ///slide
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopehit, 5f) && !redetojump)
            {

                rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
                transform.position = new Vector3(transform.position.x, slopehit.point.y+1f, transform.position.z);
            }
            
            cap2.enabled = true;
            cap1.enabled = false;
            an.SetTrigger("slide");
        }
       

        ///
        leftAndRight();

    }
    private void FixedUpdate()
    {
        movdirection = orientation.forward * vertical + orientation.right * horizontal;
        if(grouned)
            rb.AddForce(movdirection.normalized * movespeed * 10f, ForceMode.Force);
        else
        {
            rb.AddForce(movdirection.normalized * movespeed * 1.5f * airmulty, ForceMode.Force);
            rb.AddForce(Vector3.down * jadibya, ForceMode.Impulse);
        }

        if (onslope())
        {
            rb.AddForce(getslopemovdirection() * movespeed * 5f, ForceMode.Force);
           
        }
       
        rb.useGravity = !onslope();


        if(rb.velocity.magnitude> movespeed)
        {
          rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, movespeed) ;
        }


       
    }

    private void jump()
    {
        
            faneshSlide();
            an.SetTrigger("jump");
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
      
    }
    private void rasetjump()
    {
        redetojump = true;
      
    }
    private bool onslope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopehit, playerheigh * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle < maxslope && angle != 0;
        }
        return false;
    }
    private Vector3 getslopemovdirection()
    {
        return Vector3.ProjectOnPlane(movdirection, slopehit.normal).normalized;

    }









    private void leftAndRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && line == 1)
        {
            line = 2;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && line == 2 && transform.position.x <= 1.5f + 0.2f)
        {
            line = 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && line == 2 && transform.position.x >= 1.5f - 0.2f)
        {
            line = 3;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && line == 3)
        {
            line = 2;
        }

        if (line  == 3 && transform.position.x < 3f)
        {
            transform.position += new Vector3(10.5f * Time.deltaTime, 0, 0);
        }
        else if (line == 1 && transform.position.x > 0)
        {
            transform.position += new Vector3(-10.5f * Time.deltaTime, 0, 0);
        }
        else if (line == 2 && transform.position.x <= 1.5 - 0.1f)
        {
            transform.position += new Vector3(10.5f * Time.deltaTime, 0, 0);
        }
        else if (line == 2 && transform.position.x >= 1.5 + 0.1f)
        {
            transform.position += new Vector3(-10.5f * Time.deltaTime, 0, 0);
        }

    }
    public void faneshSlide()
    {
        cap1.enabled = true;
        cap2.enabled = false;

    }
}
