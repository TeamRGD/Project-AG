using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // ÃÑ¾Ë ÇÁ¸®ÆÕ
    public Transform bulletSpawnPoint; // ÃÑ¾ËÀÌ ¹ß»çµÇ´Â À§Ä¡

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // ÃÑ¾Ë »ý¼º
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * 100f;
    }
}
