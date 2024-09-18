using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platfforrm : MonoBehaviour
{
    public Vector3 size;
    Transform player;
         
    void Start()
    {
        player = GameObject.Find("runner").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z >= (transform.position.z + 32))
        {
            Destroy(gameObject);
        }
    }

    public float Gettenght()
    {
        return size.z;
    }

}
