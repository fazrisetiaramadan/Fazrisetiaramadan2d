using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public Image logoImage; // Masukkan logo di Inspector
    public float fadeDuration = 1.5f; // Waktu fade in/out
    public float displayTime = 2f; // Waktu logo tetap tampil sebelum fade out
    public string nextScene = "MainMenu"; // Nama scene tujuan

    void Start()
    {
        StartCoroutine(PlaySplashScreen());
    }

    IEnumerator PlaySplashScreen()
    {
        // Mulai dengan logo transparan
        logoImage.canvasRenderer.SetAlpha(0.0f);

        // Fade in
        logoImage.CrossFadeAlpha(1.0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration + displayTime);

        // Fade out
        logoImage.CrossFadeAlpha(0.0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        // Pindah ke Main Menu
        SceneManager.LoadScene(nextScene);
    }
}
