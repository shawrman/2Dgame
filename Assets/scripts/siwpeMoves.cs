using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siwpeMoves : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float TapRange;



    // Update is called once per frame
    void Update()
    {
        Swipe();   
    }
    public void Swipe()
    {
        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began )
        {
            startTouchPosition = Input.GetTouch(0).position;

        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPosition - startTouchPosition;
            if(!stopTouch)
            {
                if(Distance.x < -swipeRange)
                {
                   left();
                   stopTouch = true;
                }
                else if(Distance.x > swipeRange)
                {
                   right();
                   stopTouch = true;
                }
                else if(Distance.y >swipeRange)
                {
                   up();
                   stopTouch = true;
                } 
                else if(Distance.y < -swipeRange)
                {
                   down();
                   stopTouch = true;
                }
            }  
        }
        if(Input.touchCount> 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }
    }

    private void down()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        RaycastHit2D hit = Physics2D.Linecast(transform.position,new Vector2(transform.position.x,transform.position.y - 3), 1 << LayerMask.NameToLayer("ground") );
     
      
        Debug.Log(hit.point);
        if(!hit)
        {

            rb.velocity = new Vector2(0f,-20f);
        }
    }

    private void up()
    {
    }

    private void right()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        RaycastHit2D hit = Physics2D.Linecast(transform.position,new Vector2(transform.position.x + 3,transform.position.y), 1 << LayerMask.NameToLayer("ground") );
     
      
        Debug.Log(hit.point);
        if(!hit)
        {

            rb.velocity = new Vector2(20f,0f);

        }
    }

    private void left()
    {
    }
}
