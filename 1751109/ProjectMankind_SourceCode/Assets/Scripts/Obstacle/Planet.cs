using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Planet : MonoBehaviour
{
    public Sprite[] planets;

    private void Awake() {
        int i = Random.Range(0,planets.Length);
        GetComponent<SpriteRenderer>().sprite = planets[i];
    }
}
