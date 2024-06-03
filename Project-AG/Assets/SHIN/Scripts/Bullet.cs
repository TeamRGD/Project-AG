using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 100f; // 총알이 날아갈 최대 거리
    private Vector3 initialPosition;
    private Rigidbody rb;

    void Start()
    {
        // 총알의 초기 위치 기록
        initialPosition = transform.position;
    }

    void Update()
    {
        // 현재 위치와 초기 위치 사이의 거리를 계산
        float distanceTravelled = Vector3.Distance(initialPosition, transform.position);
        // 거리가 최대 거리를 초과하면 총알 파괴
        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
