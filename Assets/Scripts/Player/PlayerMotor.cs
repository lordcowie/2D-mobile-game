#pragma warning disable 0649
#pragma warning disable 0414

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] [Range(0, 0.3f)] private float smoothing = 0.05f;
    [SerializeField] [Range(0, 1f)] private float crouchMultiplier = 0.3f;

    [Header("Interactions")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float ceilingCheckRadius = 0.2f;

    public const float PHYSIC_MULTIPLIER = 10f;

    private new Rigidbody2D rigidbody;
    private PlayerController controller;
    private PlayerAnimator animator;

    private bool isGrounded = false;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    public bool IsGrounded { get => isGrounded; }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        controller = GetComponent<PlayerController>();
        animator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] nearestColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayerMask);
        for (int i = 0; i < nearestColliders.Length; i++)
        {
            if(nearestColliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        animator.SetBool(PlayerAnimator.JumpLoop, !isGrounded);
    }

   

    
    public void Move(float amount, bool crouch, bool jump)
    {
      
        if(!crouch)
        {
            crouch = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayerMask);
        }

        if(IsGrounded && crouch)
        {
            amount *= crouchMultiplier;
        }

        Vector3 velocity = new Vector3(amount * controller.Speed * PHYSIC_MULTIPLIER, rigidbody.velocity.y);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, velocity, ref this.velocity, smoothing);

        animator.SetBool(PlayerAnimator.Walking, IsGrounded && amount != 0);

        if (IsGrounded && jump)
        {
            isGrounded = false;
            rigidbody.AddForce(Vector2.up * controller.JumpForce * PHYSIC_MULTIPLIER);
        }

        if((amount > 0 && !facingRight) || (amount < 0 && facingRight))
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }
    }
}
