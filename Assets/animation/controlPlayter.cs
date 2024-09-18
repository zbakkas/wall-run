using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPlayter : MonoBehaviour
{
    public float sped;



    private Rigidbody rb;
    public float jumpForce = 5f;
    public float playerhight;
    public LayerMask groundLayer;
    public bool graounded;

    public int lane = 2;
    public float graound;
    float startpositin;

    public CapsuleCollider cps;
    float capsstart;
    public float cpsOnDown, timeUP;

    public Animator an;
    public Transform childAnim;
  
    // Start is called before the first frame update
    void Start()
    {
        startpositin = transform.position.x;
        rb = GetComponent<Rigidbody>();
        capsstart = cps.height;
      

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.forward * Time.deltaTime * sped;
        graounded = Physics.Raycast(transform.position, Vector3.down, playerhight * 0.5f, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space)&& (graounded|| rb.isKinematic))
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cps.height = cpsOnDown;
            childAnim.position = new Vector3(childAnim.position.x, 0.62f, childAnim.position.z);
            an.SetTrigger("slide");
            Invoke("sidown", timeUP) ;
        }

        


        if(lane ==3 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane = 4;
            transform.position += new Vector3(0.5f, 0, 0);
            rb.isKinematic = true;
            an.SetFloat("x",1);
        }
         else if (lane == 1 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane = 0;
            transform.position -= new Vector3(0.5f, 0, 0);
            rb.isKinematic = true;
            an.SetFloat("x", 0.75f);
        }

        leftAndRight();

        if (lane == 0 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            lane = 1;
            transform.position += new Vector3(0.5f, 0, 0);
            rb.isKinematic = false;
            an.SetFloat("x", 0);
        }
        else if (lane == 4 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lane = 3;
            transform.position -= new Vector3(0.5f, 0, 0);
            rb.isKinematic = false;
            an.SetFloat("x", 0);
        }


    }
    private void leftAndRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && lane == 1)
        {
            lane = 2;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && lane == 2 && transform.position.x <= startpositin+ 0.2f)
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

        if (lane == 3 && transform.position.x < startpositin+graound)
        {
            
            if (transform.position.x >= 2.5f)
            {
                transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(10.5f * Time.deltaTime, 0, 0);
            }
        }
        else if (lane == 1 && transform.position.x > startpositin - graound)
        {
            
            if (transform.position.x <= -0.5f)
            {
                transform.position = new Vector3(-0.5f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(-10.5f * Time.deltaTime, 0, 0);
            }
        }
        else if (lane == 2 && transform.position.x <= startpositin - 0.1f)
        {
            
            if (transform.position.x >= startpositin)
            {
                transform.position = new Vector3(startpositin, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(10.5f * Time.deltaTime, 0, 0);
            }
        }
        else if (lane == 2 && transform.position.x >= startpositin+0.1f)
        {
            
            if (transform.position.x <= startpositin)
            {
                transform.position = new Vector3(startpositin, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position += new Vector3(-10.5f * Time.deltaTime, 0, 0);
            }
        }

    }


    private void sidown()
    {   
        childAnim.localPosition = new Vector3(childAnim.localPosition.x, 0, childAnim.localPosition.z);
        cps.height = capsstart;
    }
   
}
