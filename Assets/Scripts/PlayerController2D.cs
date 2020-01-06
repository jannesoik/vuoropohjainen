using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float runSpeed = 1.5f;
    public float jumpSpeed = 4.0f;

    bool isAttacking = false;

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject attackHitbox;

    //ground checks
    bool isGrounded;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackHitbox.SetActive(false);
    }


    private void Update()
    {        
        //attacking       
        if (Input.GetKeyDown("x") && !isAttacking)
        {
            isAttacking = true;

            if (!isGrounded)
            {
                animator.Play("Player_jumpAttack");
            }
            else
            {
                animator.Play("Player_attack");
            }
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(.13f);
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(.5f);
        isAttacking = false;
    }


    private void FixedUpdate()
    {

        //ground checks with linecasts
        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            if (!isAttacking)
                animator.Play("Player_jump");

        }


        //=================================================================
        //moving and jumping START 
        //=================================================================
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if (isGrounded && !isAttacking)
                animator.Play("Player_run");

            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if (isGrounded && !isAttacking)
                animator.Play("Player_run");

            transform.localScale = new Vector3(-1, 1, 1);
        }
        else //not moving
        {
            if (isGrounded && !isAttacking)
                animator.Play("Player_idle");

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        //jumping
        if (Input.GetKey("space") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.Play("Player_jump");
        }
        ///=================================================================
        //moving and jumping END 
        //=================================================================

    }    
}
