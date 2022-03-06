using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("----Movement----")]

    public Rigidbody2D rb;
    public float jetPackForce;
    public float speed;
    public Joystick moveStick;
    public float moveX;
    public float moveY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveStick.Horizontal * speed,rb.velocity.y);
        rb.AddForce(new Vector2(0, jetPackForce * moveStick.Vertical), ForceMode2D.Force);

        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed,rb.velocity.y);
        if (Input.GetKey(KeyCode.Space) || (moveStick.Vertical > 0.3f))
        {
        }
        if (Input.GetKey(KeyCode.DownArrow) || (moveStick.Vertical < -0.3f))
        {
            rb.velocity = new Vector2(rb.velocity.x, -jetPackForce);
        }
    }
}
