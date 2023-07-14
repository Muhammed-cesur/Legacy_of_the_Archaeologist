using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : MonoBehaviour
{

    public Transform[] patrolPoints;   // NPC'nin gideceği noktaların listesi
    public float moveSpeed = 3f;       // NPC'nin hareket hızı

    private int currentPatrolIndex;    // Mevcut hedef noktanın dizinini tutar

    private void Start()
    {
        // Başlangıçta ilk noktaya gitmesi için currentPatrolIndex'i sıfıra ayarlayın
        currentPatrolIndex = 0;

        // NPC'yi ilk hedef noktaya yerleştirin
        transform.position = patrolPoints[currentPatrolIndex].position;
    }

    private void Update()
    {
        // NPC'yi hedef noktaya doğru hareket ettirin
        MoveToTarget();

        // NPC, hedef noktaya ulaştıysa bir sonraki hedefe geçmesini sağlayın
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            SetNextPatrolPoint();
        }
    }

    private void MoveToTarget()
    {
        // Hedef noktaya doğru yönelin
        Vector3 direction = patrolPoints[currentPatrolIndex].position - transform.position;
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    private void SetNextPatrolPoint()
    {
        // Sonraki hedef noktaya geçmek için currentPatrolIndex'i bir artırın
        currentPatrolIndex++;

        // Eğer tüm hedef noktaları gezildiyse, başa dön
        if (currentPatrolIndex >= patrolPoints.Length)
        {
            currentPatrolIndex = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // NPC, bir şeyle çarpıştığında duraklatın ve bir sonraki hedef noktaya geçin
        SetNextPatrolPoint();
    }
}


