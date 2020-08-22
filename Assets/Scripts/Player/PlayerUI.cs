#pragma warning disable 0649

using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private float minDistance = 0.01f;

    public void UpdateHealth(float health)
    {
        StartCoroutine(UpdateHealthInternal(health / 100f));
    }

    private IEnumerator UpdateHealthInternal(float health)
    {
        while (Mathf.Abs(healthBar.fillAmount - health) > minDistance)
        {
            healthBar.fillAmount = (float)Mathf.Lerp((float)healthBar.fillAmount, (float)health, smoothing);
            
            yield return null;
        }

        healthBar.fillAmount = health;
    }
}
