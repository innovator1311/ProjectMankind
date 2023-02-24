using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    Vector2 clickPos;
    Vector2 dragPos;
    Vector3 targetPos;
    
    public float speed;
    public Transform startPoint;
    // Start is called before the first frame update
    void Start()
    {
        clickPos = Vector2.zero;
        dragPos = Vector2.zero;
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            clickPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) {
            dragPos = Input.mousePosition;
            Vector3 direction = Camera.main.ScreenToWorldPoint(dragPos) - Camera.main.ScreenToWorldPoint(clickPos);
            direction *= -speed;
            targetPos = transform.position + direction;
            //transform.Translate(direction, Space.World);
            
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);

        
    }
}
