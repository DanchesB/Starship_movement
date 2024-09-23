using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private PlayerShooter player;

    [SerializeField] float range = 10f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] int damage = 10;

    private float nextTimeToFire = 0;
    public bool canShoot = false;

    public float time;

    private void Update()
    {
        if(canShoot == false)
        {
            // Увеличиваем текущий таймер каждый кадр
            time += Time.deltaTime;

            // Проверяем, если таймер достиг значения fireRate
            if (time >= fireRate)
            {
                // Устанавливаем canShoot в true, так как можно стрелять
                canShoot = true;

                // Сбрасываем таймер на 0 для следующего цикла стрельбы
                time = 0f;
            }
        }

        ProcessRaycast();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        Debug.DrawLine(firePoint.transform.position, firePoint.transform.forward, Color.green);

        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, range))
        {
            player = hit.transform.GetComponent<PlayerShooter>();

            if (player != null && canShoot == true)
            {
                GameObject go = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                go.GetComponent<LaserProjectile>().Init(damage);
                Destroy(go, 1f);

                canShoot = false;
                Debug.Log("Player");
                /*CreateHitImpact(hit);
                muzzleFlash.Play();*/

                //playerHealth.takeDamage(damage);
            }
            
        }
    }

    /*private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        GameObject impactSound = Instantiate(impactSoundObject, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(impact, 0.8f);
        Destroy(impactSound, 0.8f);
    }*/
}
