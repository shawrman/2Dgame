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
        
        rb = GetComponent<Rigidbody2D>();
      
     
    }

 
    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
       
        if(!moveStick)
        {
        moveStick = GameObject.Find("moving").GetComponent<FloatingJoystick>();

        }
        

        Validate(moveStick.Direction);
      
    }

    [Command]
    void Validate(Vector2 move)
    {
        handleMovement(move);

        if(isServerOnly)
        {
            rb.AddForce(new Vector2(move.x * speed ,0 ));
            if (rb.velocity.y < limitY)
            {
                rb.AddForce(new Vector2(0, jetPackForce * move.y), ForceMode2D.Force);

            }
        }
   
    }

    [ClientRpc]
    void handleMovement(Vector2 moveStick)
    {
        
        //rb.velocity = new Vector2(moveStick.Horizontal * speed,rb.velocity.y);
        
        rb.AddForce(new Vector2(moveStick.x * speed ,0 ));
        if (rb.velocity.y < limitY)
        {
            rb.AddForce(new Vector2(0, jetPackForce * moveStick.y), ForceMode2D.Force);

        }
    }
}

