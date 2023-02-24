using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : CanShootObject
{
    public Sprite[] sprites;
    float presentHealth;

    private void Start() {
        // create random rock
        int i = Random.Range(0,sprites.Length);
        health = i;
        presentHealth = health;
        GetComponent<SpriteRenderer>().sprite = sprites[i];
        gameObject.AddComponent<PolygonCollider2D>().usedByComposite = true;
    }

    private void Update() {
        
        if (presentHealth != health) {
            GetComponent<SpriteRenderer>().sprite = sprites[health];
            UpdateCollier();
        }
    }

    public void UpdateCollier() {

        //update shape of collider
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>().usedByComposite = true;
    }

}
