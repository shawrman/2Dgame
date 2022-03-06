using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour 
{
    [Header("Guns")]
    public GameObject[] Bullet;
    public float[] BulletSpeed;
    public int[] BulletsPerSeconds;
    public Sprite[] gunSprite;
    public int[] magazineSize;
    public float[] reloadTime;
    public float[] damage;
    public GameObject spriteHolder;
    SpriteRenderer SR;



    [Header("other")]

    // Start is called before the first frame update
    public Joystick shootStick;
    public LineRenderer TR;
    public Rigidbody2D rb;
    public Transform rbTarget;
    public Transform startingLine;
    Gun gunHolder;
    
   
    public int GunId = 0;
    //public Transform ori;




 
    public float trailDis = 100;
    public LayerMask lay;
    RaycastHit2D hit;
    void Start()
    {
        
      
        rb = GetComponent<Rigidbody2D>();
        reloadGunId();

    }
    void reloadGunId(int id = 0)
    {
        gunHolder = this.GetComponent<Gun>();
        gunHolder.init(Bullet[id],BulletSpeed[id],BulletsPerSeconds[id],gunSprite[id],magazineSize[id],reloadTime[id],damage[id],spriteHolder);
        SR = spriteHolder.GetComponent<SpriteRenderer>();
        SR.sprite = gunSprite[id];
        
        //gunHolder = new Gun(Bullet[id],BulletSpeed[id],BulletsPerSeconds[id],gunSprite[id],magazineSize[id],reloadTime[id],damage[id]);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        TR.SetPosition(0, startingLine.position);

        if (Mathf.Abs(shootStick.Vertical) > 0.1f || Mathf.Abs(shootStick.Horizontal) > 0.1f)
        {
            hit = Physics2D.Raycast(spriteHolder.transform.position, startingLine.forward,trailDis,lay);
            if (hit)
            {
                TR.SetPosition(1, hit.point);
            }
            else
            {
                Debug.Log(startingLine.forward);

             TR.SetPosition(1,startingLine.position + startingLine.forward * 100);

            }
            //TR.SetPosition(1, hit.point);
            if (Mathf.Abs(shootStick.Vertical) > 0.5f || Mathf.Abs(shootStick.Horizontal) > 0.5f)
            {
                shoot();
            }
           

        }
        else
        {
            TR.SetPosition(1, startingLine.position);

        }
    }
    void Update()
    {
        TR.positionCount = 2;

        rb.rotation = Mathf.Rad2Deg * Mathf.Atan2(shootStick.Vertical, shootStick.Horizontal);
        transform.position = rbTarget.position;
        if(rb.rotation > 90 || rb.rotation < -90)
        {
            SR.flipY = true;
        }
        else
        {
            SR.flipY = false;

        }
  
        //Instantiate(bulletPreFab,transform,)
    }
    void shoot()
    {
      
        gunHolder.shoot( startingLine, transform);

            

 
      
        //GameObject bullet = Instantiate(bulletPreFab, startingLine.position, transform.rotation);
        //Rigidbody2D BRB = bullet.GetComponent<Rigidbody2D>();
        //BRB.AddForce(startingLine.up * BulletForce,ForceMode2D.Impulse);

    }

    
}
