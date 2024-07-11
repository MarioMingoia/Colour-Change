using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMovement : MonoBehaviour
{
    //movement
    public float speed;

    //jump
    public float jumpForce;
    public float airMovement;
    public bool grounded;
    public Rigidbody rb;
    public Vector3 jump;
    public bool readytoJump;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        airMovement = speed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            //local movement
            transform.Translate(Vector3.right * (Input.GetAxis("Horizontal") * speed) * Time.deltaTime);
            transform.Translate(Vector3.forward * (Input.GetAxis("Vertical") * speed) * Time.deltaTime);

            //global movement
            //float moveX = (Input.GetAxis("Horizontal") * speed) * Time.deltaTime;
            //float moveZ = (Input.GetAxis("Vertical") * speed) * Time.deltaTime;
            //transform.Translate(moveX, transform.position.y, moveZ);
        }

        else if (!grounded)
        {
            //local movement
            transform.Translate(Vector3.right * (Input.GetAxis("Horizontal") * airMovement) * Time.deltaTime);
            transform.Translate(Vector3.forward * (Input.GetAxis("Vertical") * airMovement) * Time.deltaTime);

            //global movement
            //float moveX = (Input.GetAxis("Horizontal") * airMovement) * Time.deltaTime;
            //float moveZ = (Input.GetAxis("Vertical") * airMovement) * Time.deltaTime;
            //transform.Translate(moveX, transform.position.y, moveZ);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            readytoJump = true;
        }

    }
    private void FixedUpdate()
    {
        if (grounded && readytoJump)
        {
            rb.velocity = Vector3.up * jumpForce * Time.fixedDeltaTime;
            grounded = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor") && Mathf.Approximately(rb.velocity.y, 0))
        {
            grounded = true;
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("Crusher"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.CompareTag("floor"))
        {
            readytoJump = false;
        }
    }

}
