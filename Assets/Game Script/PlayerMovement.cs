using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    [Header("Comment")]
    [SerializeField] private Rigidbody2D rb;    
    [SerializeField] private LayerMask groundLayer;    
    [SerializeField] private Animator anim;
    
    [Header("Movement Setting")]     
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;
    [SerializeField]private Vector3 basceScale;
    [SerializeField] private Transform groundCheck;            
    [SerializeField] private AudioSource jumpingAudiosource;

    private float dirX;
    private float checkRadius = 0.15f;

    [Header("Wall Sliding")]
    [SerializeField] private Transform frontCheck;
    private bool _isTouchFront;
    private bool _isWallSliding;

    [SerializeField] private float xWallJum;
    [SerializeField] private float yWallJum;
    private bool _isWallJumping;
    [SerializeField] private float wallJumpTime;
    

    private enum Movementstate {idle, running, jumping, hurt }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(frontCheck.position, checkRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
           
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {     
        dirX = Input.GetAxisRaw("Horizontal");
        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpingAudiosource.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        //Wall Sliding
        _isTouchFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundLayer);
        if(_isTouchFront && !IsGrounded() && dirX != 0 )
        {
            _isWallSliding= true;
        }
        else
        {
            _isWallSliding= false;
        }
        if(Input.GetButtonDown("Jump") && _isWallSliding)
        {
            _isWallJumping= true;
            Invoke(nameof(SetWallJumpingToFalse), wallJumpTime);
        }
        if( _isWallJumping)
        {
            rb.velocity = new Vector2(xWallJum * -dirX, yWallJum);
        }

        UpdateAnimation();
        Flip();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }
    private void Flip()
    {
        switch(dirX)
        {
            case > 0: transform.localScale = basceScale; break;
            case < 0: transform.localScale = new Vector3(-1* basceScale.x,basceScale.y,basceScale.z); break;
        }
    }
    private void SetWallJumpingToFalse()
    {
        _isWallJumping = false;
    }
    private void UpdateAnimation()
    {
        Movementstate state;

        if (dirX > 0f)
        {
            state= Movementstate.running;           
        }
        else if (dirX < 0f)
        {
            state = Movementstate.running;            
        }
        else
        {
            state= Movementstate.idle;
        }
        if(rb.velocity.y > 0.1f)
        {
            state= Movementstate.jumping;
        }
        else if(rb.velocity.y < -.1f) 
        { 
            state= Movementstate.hurt;
        }
        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
    }
    public bool CanAttack()
    {
        return dirX == 0 && IsGrounded() && !_isTouchFront;
    }
}
