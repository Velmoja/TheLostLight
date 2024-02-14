using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private bool isGrounded;

    [SerializeField] private GameObject objForIsGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Walk();
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        anim.SetBool("IsFlying", IsFlying());
    }

    public void Walk()
    {
        float movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        anim.SetFloat("moveX", Mathf.Abs(movement));
        if (movement > 0) 
        {
            spriteRenderer.flipX = false;
        }
        if (movement < 0) 
        {
            spriteRenderer.flipX = true;
        }   
    }


    private void Jump() 
    {
        CheckGround();

        if (isGrounded) 
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    private bool IsFlying() 
    {
        if (rb.velocity.y < 0)
        {
            anim.SetBool("Jump", false);
            return true;
        }
        else 
        {
            return false;
        }
        
    }

    private void CheckGround() 
    {
        float rayLenth = 0.5f;
        Vector3 rayStartPoth = objForIsGrounded.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPoth, rayStartPoth + Vector3.down, rayLenth);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else 
        {
            isGrounded= false;
        }
    }
}
