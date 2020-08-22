using System.Linq;
using System.Security.Policy;
using UnityEngine;

[RequireComponent(typeof(PlayerUI))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float jumpForce = 400f;
    public float speed = 40f;

    private PlayerMotor motor;
    private PlayerStats stats;
    private PlayerAnimator animator;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    public float Speed { get => speed; }
    public float JumpForce { get => jumpForce; }

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        void ManageInputEvent(string eventName)
        {
            if (Input.GetButton(eventName) && !animator.animator.GetCurrentAnimatorStateInfo(0).IsName(eventName))
            {
                animator.SetTrigger(eventName);
            }
        }

        if(stats.Dead) 
        {
            enabled = false;
            motor.enabled = false;
            return; 
        }

        horizontalMove = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        // Manage inputs
        if (motor.IsGrounded && Input.GetButton("Jump"))
        {
            jump = true;
        }

        ManageInputEvent(PlayerAnimator.Attacking);
        ManageInputEvent(PlayerAnimator.Taunt);

        /*
        if (Input.GetButtonDown("Crouch")) { crouch = true; }
        else if (Input.GetButtonUp("Crouch")) { crouch = false; }*/
    }
    
    void FixedUpdate()
    {
        motor.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("stone"))
            this.transform.parent = col.transform;
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("stone"))
            this.transform.parent = null;
        
    }
}
