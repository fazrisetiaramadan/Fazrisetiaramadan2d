using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionBar : MonoBehaviour
{
    public Image barImage;
    public Color32 startColor = Color.white;
    public Color32 endColor = Color.blue;
    public float maxWidth = 200f;

    private float progress = 0f;
    private RectTransform barRect;

    void Start()
    {
        barRect = barImage.GetComponent<RectTransform>();
        barImage.material = new Material(Shader.Find("UI/Default")); // Pastikan shader default
        UpdateMissionBar(0f);
    }

    public void UpdateMissionBar(float increase)
    {
        float targetProgress = Mathf.Clamp01(progress + increase);
        StartCoroutine(AnimateMissionBar(progress, targetProgress));
        progress = targetProgress;
    }

    IEnumerator AnimateMissionBar(float startProgress, float targetProgress)
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float smoothProgress = Mathf.Lerp(startProgress, targetProgress, t);

            barRect.sizeDelta = new Vector2(smoothProgress * maxWidth, barRect.sizeDelta.y);

            // Gunakan Color32 agar warna solid dan alpha tetap 255
            Color32 newColor = Color32.Lerp(startColor, endColor, smoothProgress);
            newColor.a = 255; // Alpha penuh
            barImage.color = newColor;

            yield return null;
        }

        // Set warna akhir tetap solid
        Color32 finalColor = Color32.Lerp(startColor, endColor, targetProgress);
        finalColor.a = 255;
        barImage.color = finalColor;
    }
}
