using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncerSystem : MonoBehaviour
{
    public static AnnouncerSystem Instance;

    [Header("UI Elements")]
    public Image announcerImage; // Reference to the Image component in the Canvas
    public Sprite[] sprites; // Array of sprites (1st, 2nd, and 3rd used for idle and talking animation)
    public AudioClip[] announcerQuotes; // Array of audio clips for announcer quotes
    public AudioSource audioSource; // Reference to the AudioSource component

    [Header("Settings")]
    public float fadeDuration = 1.0f; // Time for the image to fade in and out

    private void Awake()
    {
        // Singleton pattern for easy access from the missile script
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to trigger the announcer sequence
    public void TriggerAnnouncer()
    {
        StartCoroutine(AnnouncerSequence());
    }

    // Coroutine to handle the fade in, sprite change, and audio playback
    private IEnumerator AnnouncerSequence()
    {
        // Step 1: Fade in the image to full opacity
        yield return StartCoroutine(FadeImage(1f));

        // Step 2: Switch to the talking sprites (1 and 2) while audio is played
        AudioClip randomClip = announcerQuotes[Random.Range(0, announcerQuotes.Length)];

        // Start playing the audio
        audioSource.PlayOneShot(randomClip);

        // Step 3: Start talking animation (switch between sprite 1 and sprite 2 every 0.2 seconds)
        yield return StartCoroutine(PlayTalkingAnimation(randomClip.length));

        // Step 4: Wait for the audio clip to finish
        yield return new WaitForSeconds(randomClip.length);

        // Step 5: Fade out the image back to transparent
        yield return StartCoroutine(FadeImage(0f));

        // Switch back to the first sprite after fade out
        announcerImage.sprite = sprites[0];
    }

    // Coroutine to fade the image in and out
    private IEnumerator FadeImage(float targetAlpha)
    {
        float startAlpha = announcerImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            announcerImage.color = new Color(announcerImage.color.r, announcerImage.color.g, announcerImage.color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set
        announcerImage.color = new Color(announcerImage.color.r, announcerImage.color.g, announcerImage.color.b, targetAlpha);
    }

    // Coroutine to handle talking animation (switching between sprite 1 and sprite 2)
    private IEnumerator PlayTalkingAnimation(float duration)
    {
        float timeElapsed = 0f;
        int currentSpriteIndex = 1; // Start with sprite 1

        while (timeElapsed < duration)
        {
            // Alternate between sprite 1 and sprite 2 every 0.2 seconds
            announcerImage.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = currentSpriteIndex == 1 ? 2 : 1; // Toggle between 1 and 2

            yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds before switching
            timeElapsed += 0.2f;
        }

        // After animation ends, return to sprite 1 as default before fading out
        announcerImage.sprite = sprites[1];
    }
}
