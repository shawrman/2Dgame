using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called before the first frame update
    public float _health = 100;

    public void damageTaken(float damgae)
    {
        _health -= damgae;
        if(_health < 0)
        {
            this.gameObject.transform.position = new Vector3(2,2,2);
        }
    } 
}
