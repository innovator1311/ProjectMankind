using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanShootObject : MonoBehaviour
{
    public int health;

    public void ChangeHealth(int damage) {

        Debug.Log("Enemy" + health);

        health += damage;
        if (health <= 0)
            Destroy(gameObject);

    }

}
