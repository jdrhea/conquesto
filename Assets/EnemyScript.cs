using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 target;
    public bool isEnemyPrefab;
    public bool isDetectionRangeIndicator;
    public Image healthBar;
    public float healthAmount = 100f;
    private bool isCollision;
    private Vector3 targetPosition;
    private bool isDetected = false;
    public GameObject enemyPrefab;

    void Start()
    {
        target = transform.position;
    }
    void Update()
    {
        if (isCollision)
        {
            TakeDamage(20f); // Example damage value
            Debug.Log("Enemy hit by unit");
            if (healthAmount <= 0)
            {
                Destroy(enemyPrefab);
                Debug.Log("Enemy destroyed");
            }
            isCollision = false; // Reset collision state
        }
        if (isDetected)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, target, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("unit") && isEnemyPrefab)
        {
            isCollision = true;
            // Set the target to the unit's position
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("unit") && isDetectionRangeIndicator)
        {
            isDetected = true;
            target = collision.transform.position;
            Debug.Log($"Detected unit! Setting target to: {target}");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("unit") && isDetectionRangeIndicator)
        {
            isDetected = false;
            Debug.Log("Unit left detection range. Stopping follow.");
        }
    }
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;


    }
}
