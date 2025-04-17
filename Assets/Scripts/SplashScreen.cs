using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Image logoImage;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private string nextScene = "MainMenu";
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Text loadingText;
    [SerializeField] private float progressSpeed = 0.3f;

    private void Start()
    {
        StartCoroutine(PlaySplashScreen());
    }

    private IEnumerator PlaySplashScreen()
    {
        logoImage.canvasRenderer.SetAlpha(0f);
        loadingBar.value = 0f;
        loadingBar.gameObject.SetActive(true);
        loadingText.text = "0%";

        logoImage.CrossFadeAlpha(1f, fadeDuration, false);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        asyncLoad.allowSceneActivation = false;

        float currentProgress = 0f;

        while (!asyncLoad.isDone)
        {
            float targetProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            currentProgress = Mathf.MoveTowards(currentProgress, targetProgress, progressSpeed * Time.deltaTime);
            loadingBar.value = currentProgress;

            int percentage = Mathf.RoundToInt(currentProgress * 100f);
            loadingText.text = percentage.ToString() + "%";

            if (currentProgress >= 1f && asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f);
                logoImage.CrossFadeAlpha(0f, fadeDuration, false);
                yield return new WaitForSeconds(fadeDuration);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
