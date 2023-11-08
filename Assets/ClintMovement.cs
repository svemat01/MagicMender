using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClintMovement : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float moveSpeed = 2.0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            animator.SetFloat("Speed4", moveSpeed);
            Vector3 v = transform.up * moveSpeed;  // -transform.right = left
            GetComponent<Rigidbody2D>().velocity = v;  // Sets velocity to left movement
        }
        else if (Input.GetKey("down"))
        {
            animator.SetFloat("Speed2", moveSpeed);
            Vector3 v = -transform.up * moveSpeed;  // -transform.right = left
            GetComponent<Rigidbody2D>().velocity = v;  // Sets velocity to left movement
        }
        else if (Input.GetKey("left"))
        {
            animator.SetFloat("Speed3", moveSpeed);
            Vector3 v = -transform.right * moveSpeed;  // -transform.right = left
            GetComponent<Rigidbody2D>().velocity = v;  // Sets velocity to left movement
        }
        else if (Input.GetKey("right"))
        {
            animator.SetFloat("Speed", moveSpeed);
            Vector3 v = transform.right * moveSpeed;  // -transform.right = left
            GetComponent<Rigidbody2D>().velocity = v;  // Sets velocity to left movement
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Hammer", true);
            moveSpeed = 0f;
        }
        else
        {
            moveSpeed = 2.0f;
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Speed2", 0);
            animator.SetFloat("Speed3", 0);
            animator.SetFloat("Speed4", 0);
            animator.SetBool("Hammer", false);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}
