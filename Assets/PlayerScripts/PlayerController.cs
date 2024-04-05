using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float x;
    public float speed = 1f;
    public float jumpHeight;
    Vector3 Move;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        Move = new Vector3(x, 0, 0);
        Move = Move * speed * Time.deltaTime;
        transform.position += Move;

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode.Impulse);
        }
    }

}
