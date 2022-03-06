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
    }

    private void down()
    {
        Debug.Log(1);
    }

    private void up()
    {
    }

    private void right()
    {
    }

    private void left()
    {
    }
}
