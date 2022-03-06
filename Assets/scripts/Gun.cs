using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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
    public Gun(GameObject bullet,float bulletSpeed,int bulletsPerSecond,Sprite sprite,int magazineSize,float reloadTime,float damage)
    {
        _bullet = bullet;
        _bulletSpeed = bulletSpeed;
        _bulletsPerSeconds = bulletsPerSecond;
        _damage = damage;
        _gunSprite = sprite;
        _magazineSize = magazineSize;
        _reloadTime = reloadTime;
        magazine = magazineSize;
        
    }
    GameObject _bullet;//done
    GameObject _gunObject;
    float _bulletSpeed;//done
    int _bulletsPerSeconds;//done
    Sprite _gunSprite;//need to do
    int _magazineSize;
    float _reloadTime;
    float _damage;
    bool canShoot = true;

    int magazine;
  
  
    void ableToShoot()
    {
      
       
        canShoot = true;
      
    }
     void ReloadGun()
    {
        
       
        StartCoroutine(rotateObject(_gunObject, _reloadTime));
        

    }
    public void shoot(Transform gunPoint,Transform ori)
    {
        
        if(canShoot)
        {   
            canShoot = false;

            if(magazine > 0)
            {
                GameObject bullet = Instantiate(_bullet, gunPoint.position, ori.rotation);
                Rigidbody2D BRB = bullet.GetComponent<Rigidbody2D>();
                BRB.AddForce(gunPoint.up * _bulletSpeed, ForceMode2D.Impulse);
               
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

    IEnumerator rotateObject(GameObject gameObjectToMove, float duration)
    {


        Quaternion currentRot = gameObjectToMove.transform.localRotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.localRotation  =  Quaternion.Euler(new Vector3(0, 0, 1080 *(counter / duration)));
            yield return null;
        }
        magazine = _magazineSize;
        canShoot = true;

    }  



}
