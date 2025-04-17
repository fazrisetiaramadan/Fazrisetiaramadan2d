using System.Collections;
using UnityEngine;

public class BlinkingSpike : MonoBehaviour
{
    public float blinkSpeed = 1.5f; 

    private SpriteRenderer spriteRenderer;
    private bool isBlinking = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        while (isBlinking)
        {
            // Fade Out (Menghilang)
            for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
            {
                SetAlpha(alpha);
                yield return new WaitForSeconds(blinkSpeed / 10);
            }

            yield return new WaitForSeconds(0.5f); 

            // Fade In (Muncul lagi)
            for (float alpha = 0; alpha <= 1f; alpha += 0.1f)
            {
                SetAlpha(alpha);
                yield return new WaitForSeconds(blinkSpeed / 10);
            }

            yield return new WaitForSeconds(0.5f); 
        }
    }

    private void SetAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha; 
            spriteRenderer.color = color;
        }
    }
}
