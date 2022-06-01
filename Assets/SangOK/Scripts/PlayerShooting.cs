using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject grenadeFactory;
    public Transform ThrowPosition;

   

    public enum GunState
    {
        defaultGun,
        heavyMachine,
        flameGun,
        shotGun,
        rocketLauncher
    }
    GunState gunState;

    private void Start()
    {
        gunState = GunState.defaultGun;
        BulletManager.instance.defaultBombBullet = 10;
      
    }

    
    void FixedUpdate()
    {
       

        if (gunState != GunState.heavyMachine)
        {
            BulletManager.instance.defaultHevyMachinGunBullet = 0;
        }
        if(gunState != GunState.flameGun)
        {
            BulletManager.instance.defaultFlameGunBullet = 0;
        }
        if(gunState != GunState.shotGun)
        {
            BulletManager.instance.defaultShotGunBullet = 0;
        }
        if(gunState != GunState.rocketLauncher)
        {
            BulletManager.instance.defaultRocketLauncherBullet = 0;
        }

        
        

        if (Input.GetMouseButtonDown(0))
        {
            
                switch (gunState)
                {
                    case GunState.defaultGun:
                        GameObject dgBUllet = BulletManager.instance.MakeObj("DefaultGun");
                        break;

                    case GunState.heavyMachine:
                        BulletManager.instance.defaultHevyMachinGunBullet--;
                        GameObject hmBullet = BulletManager.instance.MakeObj("HeavyMachinGun");
                        if (BulletManager.instance.defaultHevyMachinGunBullet == 0)
                        {
                            gunState = GunState.defaultGun;
                        }

                        break;

                    case GunState.flameGun:
                        BulletManager.instance.defaultFlameGunBullet--;
                        if (BulletManager.instance.defaultFlameGunBullet == 0)
                        {
                            gunState = GunState.defaultGun;
                        }

                        break;

                    case GunState.shotGun:
                        BulletManager.instance.defaultShotGunBullet--;
                        if (BulletManager.instance.defaultShotGunBullet == 0)
                        {
                            gunState = GunState.defaultGun;
                        }
                        break;

                    case GunState.rocketLauncher:
                        BulletManager.instance.defaultRocketLauncherBullet--;
                        if (BulletManager.instance.defaultRocketLauncherBullet == 0)
                        {
                            gunState = GunState.defaultGun;
                        }
                        break;

                }
            for (int i = 0; i < BulletManager.instance.targetPool.Length; i++)
            {
                BulletManager.instance.targetPool[i].gameObject.transform.position += transform.forward * 20f * Time.fixedDeltaTime;
            }




        }

        if (Input.GetMouseButtonDown(1))
        {
            if (BulletManager.instance.defaultBombBullet > 0)
            {

                GameObject grenade = Instantiate(grenadeFactory);

                grenade.transform.position = ThrowPosition.position;

                BulletManager.instance.defaultBombBullet--;
            }


        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "HeavyMachinGun")
        {                     
            
            BulletManager.instance.defaultHevyMachinGunBullet += 200;            
            gunState = GunState.heavyMachine;
        }

        if(other.name == "FlameGun")
        {
           
            BulletManager.instance.defaultFlameGunBullet += 30;            
            gunState= GunState.flameGun;
        }
        if(other.name == "ShotGun") 
        {
            
            BulletManager.instance.defaultShotGunBullet += 30;            
            gunState = GunState.shotGun;
        }
        if(other.name == "Rocket")
        {
            
            BulletManager.instance.defaultRocketLauncherBullet += 30;           
            gunState = GunState.rocketLauncher;
        }
        if(other.name == "Bomb")
        {
            BulletManager.instance.defaultBombBullet += 10;
        }

        Destroy(other.gameObject);

    }


}
