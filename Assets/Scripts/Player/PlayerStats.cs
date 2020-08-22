using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float health = 100f; // %

    private bool dead = false;
    private PlayerUI ui;
    private PlayerAnimator animator;

    public bool Dead { get => dead; }

    private void Start()
    {
        ui = GetComponent<PlayerUI>();
        animator = GetComponent<PlayerAnimator>();
    }

    public void ApplyDamages(float damages)
    {
        if (Dead) { return; }

        health -= damages;

        if (health <= 0)
        {
            dead = true;

            Debug.Log("Dead");
            ui.UpdateHealth(0);

            animator.SetTrigger(PlayerAnimator.Dying);

            // Todo: handle death and gameover
        }
        else
        {
            Debug.Log($"Health is now : {health} (took {damages} damages)");
            ui.UpdateHealth(health);

            animator.SetTrigger(PlayerAnimator.Hurt);
        }
    }
}
