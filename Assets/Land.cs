using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Color32 captureColor;
    public Color32 britishColor;

    void Awake()
    {
        // Set the color of the land to the capture color
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is a unit
        if (collision.gameObject.tag == "unit")
        {
            // Set the color of the land to the capture color
            sprite.color = captureColor;
            this.gameObject.tag = "ownedLand";
        }
        // Check if the collider is a unit
        if (collision.gameObject.tag == "enemyUnit")
        {
            // Set the color of the land to the capture color
            sprite.color = britishColor;
            this.gameObject.tag = "land";
        }
    }

}
