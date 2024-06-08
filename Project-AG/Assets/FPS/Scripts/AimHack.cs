using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class AimHack : MonoBehaviour
{
    public float detectionRadius;
    public string targetTag;
    public Transform playerCamera; // 플레이어의 카메라 트랜스폼

    public Text mode;
    void Start()
    {
        targetTag = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetTag = "HoverBot";
            mode.text = "HoverBot";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            targetTag = "Drone";
            mode.text = "Drone";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            targetTag = "Diver";
            mode.text = "Diver";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            targetTag = "Submarine";
            mode.text = "Submarine";
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(targetTag))
            {
                transform.LookAt(hitCollider.transform);
            }
        }

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

    }
}
