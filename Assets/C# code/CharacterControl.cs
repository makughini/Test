using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float playerspeed;
    public float JumpForce;
    public float enemyHight = 1;
    public LayerMask raycastLayer;
    public Animator anim;

        private bool IsGround;
      // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))   // Move Right
        {
            transform.position += Vector3.right * playerspeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))    // Move Left
        {
            transform.position += Vector3.left * playerspeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            anim.SetBool("Jump", true);
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
           
        }
        
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)    // On the ground
        {
            IsGround = true;
            anim.SetBool("Jump", false);
        }
        if (collision.gameObject.layer == 10)   // Condition to kill enemy or player
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, GetComponent<BoxCollider2D>().size * .8f, 0, Vector2.down, Mathf.Infinity, raycastLayer);
            if (hit.collider != null && hit.collider.gameObject.layer == 10)
            {
                KillEnemy(collision.rigidbody.gameObject);
            }
            else
            {
                KillPlayer();
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //blah blah blah
        if (collision.gameObject.layer == 9)    // Not on the ground
        {
            IsGround = false;
        }
    }


    void KillPlayer()   // Player spawn after been killed
    {
        transform.position = new Vector3(-4, -0.5F, 0);
    }


    void KillEnemy(GameObject enemy)    // Enemy die
    {
        enemy.GetComponent<EnemyControl>().Die();
    }
}
