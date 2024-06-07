using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Image loadingImage;
    [SerializeField] private Slider loadingSlider; // Reference to the Slider

    void Start()
    {
        loadingImage.gameObject.SetActive(false); // Ensure the loading image is hidden initially
        loadingSlider.gameObject.SetActive(false); // Ensure the loading slider is hidden initially
    }

    public void PlayGame()
    {
        // Hide the play and quit buttons
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // Show the loading image and slider
        loadingImage.gameObject.SetActive(true);
        loadingSlider.gameObject.SetActive(true);

        // Start the coroutine to wait and load the scene with a loading bar
        StartCoroutine(LoadSceneWithProgress());
    }

    IEnumerator LoadSceneWithProgress()
    {
        float duration = 5f; // Duration for the loading bar to fill
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = Mathf.Clamp01(elapsedTime / duration); // Update slider value
            yield return null;
        }

        // Load the scene after the loading bar is full
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
