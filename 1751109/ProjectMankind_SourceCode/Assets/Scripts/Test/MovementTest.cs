using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    bool run = false;
    public Vector2 pos,des,newpos;
    public float speed = 3f;
    Rigidbody2D rb2D;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButton(0)) {
            
            run = true;
            // Debug.Log("Work");
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (pos - rb2D.position).normalized;
            

            if ((pos - rb2D.position).magnitude > 0.5f)
                newpos = rb2D.position + (Vector2) transform.up;

            // des = rb2D.position + (pos - rb2D.position).normalized;
           // goTime = (pos - rb2D.position).magnitude;
           
        }
        
    } 
    
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
         if (run)
            MoveBody(rb2D.position,newpos,speed * Time.deltaTime);
    }

    public void MoveBody (Vector2 from, Vector2 to, float time) {

        rb2D.MovePosition (Vector2.Lerp (from, to, time));
    }
}
