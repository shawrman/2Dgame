using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct simpleGun
{   
    public int magazine;
    public bool canShoot;
    public simpleGun(int magazineSize)
    {
        canShoot = true;
        magazine = magazineSize;
    }

}

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

    public Transform rbTarget;
    public GameObject ori;
    public Transform startingLine;
   
    public int GunId = 0; 
    public float trailDis = 100;
 
    RaycastHit2D hit;

    simpleGun gun;
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
        gun = new simpleGun(magazineSize[id]);

        //new GunStruct(Bullet[id],BulletSpeed[id],BulletsPerSeconds[id],gunSprite[id],magazineSize[id],reloadTime[id],damage[id],spriteHolder);
        //gunHolder.init(Bullet[id],BulletSpeed[id],BulletsPerSeconds[id],gunSprite[id],magazineSize[id],reloadTime[id],damage[id],spriteHolder);
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

        if (Mathf.Abs(shootStick.Vertical) > 0.1f || Mathf.Abs(shootStick.Horizontal) > 0.1f)
        {      
            if (Mathf.Abs(shootStick.Vertical) > 0.5f || Mathf.Abs(shootStick.Horizontal) > 0.5f)
            {
                shoot();
            }       

        }
           
    }

    [Command]
    void CmdOriPos(Vector2 dir)
    {
        oriPos(dir);

        rbTarget.localRotation = Quaternion.Euler(rbTarget.localRotation.x,rbTarget.localRotation.y,Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)) ;
    }
    [ClientRpc]
    void oriPos(Vector2 dir)
    {  
        rbTarget.localRotation = Quaternion.Euler(rbTarget.localRotation.x,rbTarget.localRotation.y,Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)) ;

        if(rbTarget.localRotation.z > 90 || rbTarget.localRotation.z < -90)
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
    }
    void ableToShoot()
    {
        gun.canShoot = true;
    }
    void ReloadGun()
    {
        StartCoroutine(rotateObject());
    }
    
    void shoot()
    {     
        if(gun.canShoot)
        {  
            //Debug.Log(_bullet);
        
              
            gun.canShoot = false;

            if(gun.magazine > 0)
            {   
                
                notifyAll( startingLine.position,startingLine.up, ori.transform.rotation,BulletSpeed[GunId]);
                
                Invoke("ableToShoot",(float)1 / BulletsPerSeconds[GunId]);
                //Debug.Log(1 / _bulletsPerSeconds);
               
                gun.magazine--;
            }
            else
            {
                ReloadGun();                

            }
             
        }
    }
        
    [Command]
    void notifyAll(Vector3 gunPoint ,Vector3 gunPointUp ,Quaternion ori,float bp)
    {

        shootRPC( gunPoint,gunPointUp, ori,bp);

    }
    [ClientRpc]
    public void shootRPC(Vector3 gunPoint ,Vector3 gunPointUp ,Quaternion ori,float bp)
    {
      
      
       GameObject bullet = Instantiate(Bullet[GunId], gunPoint,  ori );
        
        Rigidbody2D BRB = bullet.GetComponent<Rigidbody2D>();
        BRB.AddForce(gunPointUp * bp, ForceMode2D.Impulse);
        //NetworkServer.Spawn(bullet);

    }
    IEnumerator rotateObject()
    {
        

       
        float counter = 0;
        while (counter < reloadTime[GunId])
        {
            counter += Time.deltaTime;
            ori.transform.localRotation  =  Quaternion.Euler(new Vector3(0, 0, 1080 *(counter / reloadTime[GunId])));
            yield return null;
        }
       ori.transform.localRotation  =  Quaternion.Euler(new Vector3(0, 0, 0));

        gun.magazine = magazineSize[GunId];
        gun.canShoot = true;

    }  








    
    
}
