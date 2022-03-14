using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class shooting : NetworkBehaviour 
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
    public SpriteRenderer SR;



    [Header("other")]

    // Start is called before the first frame update
    public Joystick shootStick;
    public LineRenderer TR;
    public Rigidbody2D rb;
    public Transform rbTarget;
    public GameObject ori;
    public Transform startingLine;
    Gun gunHolder;
    
   
    public int GunId = 0;
    //public Transform ori;




 
    public float trailDis = 100;
 
    RaycastHit2D hit;
    void Start()
    {
        
        if(isLocalPlayer)
        {
            shootStick = GameObject.Find("shoting").GetComponent<FloatingJoystick>();      
            //rb = GetComponent<Rigidbody2D>();
            reloadGunId();

        }
      
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
        if(isLocalPlayer)
        {
            handleLine();
        }
       
    }
    void Update()
    {
        if(isLocalPlayer)
        {
            handleShoting();
        }
  
        //Instantiate(bulletPreFab,transform,)
    }
    void handleLine()
    {
        TR.SetPosition(0, startingLine.position);

            if (Mathf.Abs(shootStick.Vertical) > 0.1f || Mathf.Abs(shootStick.Horizontal) > 0.1f)
            {
                hit = Physics2D.Linecast(spriteHolder.transform.position,new Vector2(spriteHolder.transform.position.x + (shootStick.Horizontal * 10) ,spriteHolder.transform.position.y+ (shootStick.Vertical * 10)), 1 << LayerMask.NameToLayer("ground") );

                
                if (hit)
                {
                    TR.SetPosition(1, hit.point);
                }
                else
                {
                

                TR.SetPosition(1,new Vector2( startingLine.position.x + 10*Mathf.Cos(Mathf.Deg2Rad * rb.rotation ),  startingLine.position.y + 10*Mathf.Sin(Mathf.Deg2Rad *rb.rotation)));

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

    [Command]
    void CmdOriPos(Vector2 dir)
    {
        oriPos(dir);

        
        ori.transform.position = rbTarget.position;
        rb.rotation = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
    }
    [ClientRpc]
    void oriPos(Vector2 dir)
    {
        ori.transform.position = rbTarget.position;
        rb.rotation = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        if(rb.rotation > 90 || rb.rotation < -90)
        {
            SR.flipY = true;
        }
        else
        { 
            SR.flipY = false;

        }
    }


    [ClientCallback]
    void handleShoting()
    {
        CmdOriPos(shootStick.Direction);
        TR.positionCount = 2;
        
   
    }

    
    void shoot()
    {
      
        gunHolder.prepShot( startingLine.position ,startingLine.up, ori.transform.rotation);

            

 
      
        //GameObject bullet = Instantiate(bulletPreFab, startingLine.position, transform.rotation);
        //Rigidbody2D BRB = bullet.GetComponent<Rigidbody2D>();
        //BRB.AddForce(startingLine.up * BulletForce,ForceMode2D.Impulse);

    }

    
}
