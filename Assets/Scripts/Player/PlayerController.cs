using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

// Ensures listed components are attached to the gameObject
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public bool TestMode = false;
    // Components
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // Movement Var
    [SerializeField] private float speed = 7.0f;
    [SerializeField] private float jumpforce = 300f;

    // Ground Checking
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isFalling;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private bool isAirAttack;
    [SerializeField] private bool isAttack;



    // Start is called before the first frame update
    void Start()
    {
        // Component references grabbed through script
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Foolproofing
        if (speed <= 0)
        {
            speed = 7.0f;
            if (TestMode) Debug.Log("Speed too low! Object speed set to default value of 7.0f: " + gameObject.name);
        }
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            if (TestMode) Debug.Log("GroundCheck radius was <= 0, defaulted to 0.2f: " + gameObject.name);
        }
        if (GroundCheck == null)
        { 
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            GroundCheck = obj.transform;
            if (TestMode) Debug.Log("GroundCheck Object is created: " + obj.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (isGrounded)
        {
            rb.gravityScale = 1;
            anim.ResetTrigger("JumpAtk");
        }

        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);

        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);

        if (clipInfo[0].clip.name == "Attack")
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(xInput *speed, rb.velocity.y);
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("Throw");
            }
        }

        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            anim.SetTrigger("JumpAtk");
        }

        if (rb.velocity.y < 0.1) isFalling = true;
        else isFalling = false;
        
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsFalling", isFalling);
        anim.SetFloat("Speed", Mathf.Abs(xInput));
        
        

        // Flip sprite if going left
        if (xInput != 0) sr.flipX = (xInput < 0);

    }
    public void IncreaseGravity()
    {
        rb.gravityScale = 4;
    }

    // Trigger functions are called most other times - but would still require at least one object to be physics enabled
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    // Collision functions are only called - when one of the two objects is a dynamic rigidbody
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}

/*
public class AirAttackStateMachineBehaviour : StateMachineBehaviour
{
    // This function is called when the state is exited.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if the current state is the AirAttack state.
        if (stateInfo.IsName("AirAttack"))
        {
            // Trigger your custom event or function when the AirAttack animation finishes.
            animator.SetTrigger("AirAttackFinished");
        }
    }
}
*/