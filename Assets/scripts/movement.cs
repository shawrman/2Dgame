using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class movement : NetworkBehaviour
{
    [Header("----Movement----")]

    public Rigidbody2D rb;
    public float jetPackForce;
    public float speed;
    public Joystick moveStick;
    public float limitY = 10;

    
    void start()
    {
        if(isLocalPlayer)
        {
            rb = GetComponent<Rigidbody2D>();
        }
     
    }

    // Update is called once per frame
    void Update()
    {
       if(isLocalPlayer)
       {
           if(!moveStick)
           {
            moveStick = GameObject.Find("moving").GetComponent<FloatingJoystick>();

           }
            

           handleMovement();
       }
    }
    void handleMovement()
    {
        //rb.velocity = new Vector2(moveStick.Horizontal * speed,rb.velocity.y);
        rb.AddForce(new Vector2(moveStick.Horizontal * speed ,0 ));
        if (rb.velocity.y < limitY)
        {
            rb.AddForce(new Vector2(0, jetPackForce * moveStick.Vertical), ForceMode2D.Force);

        }
    }
}

