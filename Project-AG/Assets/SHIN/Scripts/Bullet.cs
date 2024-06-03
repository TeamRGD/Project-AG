using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 100f; // �Ѿ��� ���ư� �ִ� �Ÿ�
    private Vector3 initialPosition;
    private Rigidbody rb;

    void Start()
    {
        // �Ѿ��� �ʱ� ��ġ ���
        initialPosition = transform.position;
    }

    void Update()
    {
        // ���� ��ġ�� �ʱ� ��ġ ������ �Ÿ��� ���
        float distanceTravelled = Vector3.Distance(initialPosition, transform.position);
        // �Ÿ��� �ִ� �Ÿ��� �ʰ��ϸ� �Ѿ� �ı�
        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
