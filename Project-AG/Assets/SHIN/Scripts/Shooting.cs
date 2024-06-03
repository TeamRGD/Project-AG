using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ��� �߻�Ǵ� ��ġ

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �Ѿ� ����
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawnPoint.forward * 100f;
    }
}
