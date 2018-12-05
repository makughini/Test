using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    public float speed = 0;
    public GameObject p1;
    public GameObject p2;
    bool alive;
    private float direction = 1;

    // Use this for initialization
    void Start()
    {
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (transform.position.x - p1.transform.position.x < 0.1f)
            {
                direction = 1;
            }
            else if (transform.position.x - p2.transform.position.x > -0.1f)
            {
                direction = -1;
            }
            transform.position += direction * Vector3.right * speed * Time.deltaTime;
               }
    }
    public void Die()
    {
           GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * 4, ForceMode2D.Impulse);
        alive = false;
    }
}
