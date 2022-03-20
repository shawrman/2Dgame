using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public struct GunStrudct
{
    public GameObject _bullet;//done
    GameObject _gunObject;
    float _bulletSpeed;//done
    int _bulletsPerSeconds;//done
    Sprite _gunSprite;//need to do
    int _magazineSize;
    float _reloadTime;
    float _damage;
    bool canShoot;

   
    
}
public class Gun : NetworkBehaviour
{
    public Gun()
    {

    }
    public void init(GameObject bullet,float bulletSpeed,int bulletsPerSecond,Sprite sprite,int magazineSize,float reloadTime,float damage,GameObject gun)
    {
        _bullet = bullet;
        _bulletSpeed = bulletSpeed;
        _bulletsPerSeconds = bulletsPerSecond;
        _damage = damage;
        _gunSprite = sprite;
        _magazineSize = magazineSize;
        _reloadTime = reloadTime;
        magazine = magazineSize;
        _gunObject = gun;
        canShoot = true;

    }
   
    public GameObject _bullet;//done
    public GameObject _gunObject;
    public float _bulletSpeed;//done
    public int _bulletsPerSeconds;//done
    public Sprite _gunSprite;//need to do
    public int _magazineSize;
    public float _reloadTime;
    public float _damage;
    public bool canShoot = true;

    int magazine;
  
  
    void ableToShoot()
    {
      
       
        canShoot = true;
      
    }
     void ReloadGun()
    {
        
       
        StartCoroutine(rotateObject(_gunObject, _reloadTime));
        

    }

    public void prepShot(Vector3 gunPoint ,Vector3 gunPointUp ,Quaternion ori)
    {
          if(canShoot)
        {  
            //Debug.Log(_bullet);
        
              
            canShoot = false;

            if(magazine > 0)
            {   
                
                notifyAll( gunPoint,gunPointUp, ori,_bulletSpeed);
                
                Invoke("ableToShoot",(float)1 / _bulletsPerSeconds);
                //Debug.Log(1 / _bulletsPerSeconds);
               
                magazine--;
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
        shoot( gunPoint,gunPointUp, ori,bp);

    }
    [ClientRpc]
    public void shoot(Vector3 gunPoint ,Vector3 gunPointUp ,Quaternion ori,float bp)
    {
      
      
       GameObject bullet = Instantiate(_bullet, gunPoint,  ori );
        
        Rigidbody2D BRB = bullet.GetComponent<Rigidbody2D>();
        BRB.AddForce(gunPointUp * bp, ForceMode2D.Impulse);
        //NetworkServer.Spawn(bullet);

    }

    IEnumerator rotateObject(GameObject gameObjectToMove, float duration)
    {
        

       
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.localRotation  =  Quaternion.Euler(new Vector3(0, 0, 1080 *(counter / duration)));
            yield return null;
        }
        gameObjectToMove.transform.localRotation  =  Quaternion.Euler(new Vector3(0, 0, 0));

        magazine = _magazineSize;
        canShoot = true;

    }  
    //void cmdRotate(GameObject)



}
