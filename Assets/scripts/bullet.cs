using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float _damage  = 30;
    void Start()
    {
        Destroy(this.gameObject,1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<health>().damageTaken(_damage);

        }
        Destroy(this.gameObject);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
