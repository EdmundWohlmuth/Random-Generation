using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localPosition += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localPosition -= Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition -= Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition -= Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            // rotate
        }
        if (Input.GetKey(KeyCode.E))
        {
            // rotate
        }
    }
}
