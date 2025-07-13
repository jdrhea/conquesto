using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitScript : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 target;
    public bool isSelected = false;
    public static bool isDespawn = false;
    public GameObject unitPrefab;
    public GameObject factoryPrefab;
    public Vector3 newPosition;
    public Image healthBar;
    public float healthAmount = 100f;
    public bool isCollision;
    public static bool isTouchingLand;
    public GameObject clone;

    void Start()
    {
        target = transform.position;
        this.GetComponent<Renderer>().material.color = Color.white;

    }

    void Update()
    {
        Debug.Log(isDespawn);
        if (CompareTag("unit"))
        {
            if (Input.GetMouseButtonDown(1))
            {
                isSelected = false;
                Debug.Log("Unit deselected");
                this.GetComponent<Renderer>().material.color = Color.white;
            }
            if (Input.GetMouseButtonDown(0) && isSelected)
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (isCollision && CompareTag("unit"))
            {
                TakeDamage(20f); // Example damage value
                Debug.Log("Unit hit by enemy");
                if (healthAmount <= 0)
                {
                    Destroy(clone.gameObject);
                    Debug.Log("Unit destroyed");
                }
                isCollision = false; // Reset collision state
            }
        }
    }
    public void CloneUnit()
    {
        if (Balance.canBuy)
        {
            isDespawn = false;
            // Get the camera's transform
            Transform cameraTransform = Camera.main.transform;

            // Calculate the spawn position
            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 2f; // Adjust the distance as needed

            // Instantiate the object
            Instantiate(unitPrefab, spawnPosition, cameraTransform.rotation);

            //decrease balance by 10
            Balance.increaseAmount -= 5; // Decrease balance by 10 every second
        }
        else
        {

        }

    }
    public void CloneFactory()
    {
        if (Balance.canBuy2)
        {
            isDespawn = false;
            // Get the camera's transform
            Transform cameraTransform = Camera.main.transform;

            // Calculate the spawn position
            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 2f; // Adjust the distance as needed

            // Instantiate the object
            Instantiate(factoryPrefab, spawnPosition, cameraTransform.rotation);

            //increase balance by 5/second
            Balance.increaseAmount += 5; // Increase balance by 5 every second
        }
        else
        {

        }
    }
    public void DespwanUnitButton()
    {
        isDespawn = true;
    }
    private void OnMouseDown()
    {
        if (CompareTag("unit"))
        {
            isSelected = true;
            Debug.Log("Unit selected");
            this.GetComponent<Renderer>().material.color = Color.green;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyUnit"))
        {
            isCollision = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ownedLand") && CompareTag("pointer"))
        {
            isTouchingLand = true;
        }
        else
        {
            isTouchingLand = false;
        }
        if (isDespawn && CompareTag("pointer") && collision.CompareTag("unit"))
        {
            isSelected = false;
            Destroy(collision.gameObject);
            Debug.Log("Unit despawned");
            if (CompareTag("unit"))
            {
                Balance.increaseAmount += 5; // Increase balance by 10 when unit is despawned
            }
            else if (CompareTag("factory"))
            {
                Balance.increaseAmount -= 5; // decrease by 5 when factory is despawned
            }
        }
        else
        {
            isDespawn = false;
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;


    }

}
