using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafolowing : MonoBehaviour
{
    public Transform player;
    public float z,y,timm;
    public float speed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       transform.position = new Vector3(transform.position.x , player.position.y+y, player.position.z+z);

    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, 0f, 3f);
        transform.position = smoothedPosition;


    }
}
