using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionBar : MonoBehaviour
{
    public Image barImage;
    public Color32 startColor = Color.white;
    public Color32 endColor = Color.blue;
    public float maxWidth = 200f;
    public GameObject flag; // Referensi ke bendera

    private float progress = 0f;
    private RectTransform barRect;

    void Start()
    {
        barRect = barImage.GetComponent<RectTransform>();
        barImage.material = new Material(Shader.Find("UI/Default"));
        UpdateMissionBar(0f);

        flag.SetActive(false); // Pastikan bendera mati di awal
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

            Color32 newColor = Color32.Lerp(startColor, endColor, smoothProgress);
            newColor.a = 255;
            barImage.color = newColor;

            yield return null;
        }

        Color32 finalColor = Color32.Lerp(startColor, endColor, targetProgress);
        finalColor.a = 255;
        barImage.color = finalColor;

        if (targetProgress >= 1f)
        {
            flag.SetActive(true); // Aktifkan bendera saat progress penuh
        }
    }
}
