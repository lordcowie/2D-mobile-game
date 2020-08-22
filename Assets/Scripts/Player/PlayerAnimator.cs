using System;

using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 3f;

    public const string Attacking = "Attacking";
    public const string Dying = "Dying";
    public const string Hurt = "Hurt";
    public const string Idle = "Idle";
    public const string IdleBlink = "Idle Blink";
    public const string JumpLand = "Jump Land";
    public const string JumpStart = "Jump Start";
    public const string JumpLoop = "Jump Loop";
    public const string Taunt = "Taunt";
    public const string Walking = "Walking";

    [NonSerialized] public Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        InvokeRepeating("Blink", 0, blinkInterval);
    }

    private void Blink() => SetTrigger(IdleBlink);

    public void SetTrigger(string trig) => animator.SetTrigger(trig);
    
    public void SetBool(string name, bool value)
    {
        if(animator.GetBool(name) == value) { return; }
        animator.SetBool(name, value);
    }

    public void SetFloat(string name, float value)
    {
        if(animator.GetFloat(name) == value) { return; }
        animator.SetFloat(name, value);
    }
}
