using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    public Camera mainCamera; 
    public float zoomSpeed = 2f;     // uses for the death cutscene type thing
    public float targetZoomSize = 3f; 
    private float originalZoomSize;  
    public Vector3 originalCameraPosition;
    public Vector3 zoomCameraOffset = new Vector3(0f, 0f, -10f);

    public Image blackoutImage;             
    public GameObject restartButton;         
    public AudioSource gameAudioSource;      
    public AudioClip cutsceneSound;
    private Button restartButtonComponent;


    public TextMeshProUGUI scoreText;
    public int lives { get; private set; } = 3;

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        originalZoomSize = mainCamera.orthographicSize;
        originalCameraPosition = mainCamera.transform.position;
        restartButtonComponent = restartButton.GetComponentInChildren<Button>();

        Color buttonColor = restartButtonComponent.GetComponent<Image>().color;
        buttonColor.a = 0f; // Gör knappen osynlig
        restartButtonComponent.GetComponent<Image>().color = buttonColor;
        restartButtonComponent.onClick.AddListener(OnRestartButtonClick);
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }

    private void OnRestartButtonClick()
    {
        // Ladda om den scenen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void NewGame()
    {

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }

        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        invaders.gameObject.SetActive(false);
    }

    public void SetScore(int playerScore)
    {
        score = playerScore;
        scoreText.text = $"{score}";

        if (score > 0 && score % 100 == 0)
        {
            invaders.IncreaseSpeed();
        }
    }


    private void SetLives(int lives)
    {

    }

    public void OnPlayerKilled(Player player)
    {
        // Tidigare kod för att rensa objekt och inaktivera script
        GameObject invaderGrid = GameObject.Find("InvaderGrid");
        if (invaderGrid != null) Destroy(invaderGrid);

        Laser laser = FindObjectOfType<Laser>();
        if (laser != null) laser.enabled = false;

        Missile missile = FindObjectOfType<Missile>();
        if (missile != null) missile.enabled = false;

        MysteryShip mysteryShip = FindObjectOfType<MysteryShip>();
        if (mysteryShip != null) mysteryShip.enabled = false;

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null) playerScript.enabled = false;

        // Hitta och stäng av HeartCode-skriptet
        HeartCode heartCodeScript = FindObjectOfType<HeartCode>();
        if (heartCodeScript != null)
        {
            heartCodeScript.enabled = false;
        }

        // Stoppa alla ljud inom GregoryHeart-objektet
        GameObject gregoryHeart = GameObject.Find("GregoryHeart");
        if (gregoryHeart != null)
        {
            AudioSource[] audioSources = gregoryHeart.GetComponentsInChildren<AudioSource>();
            foreach (var audioSource in audioSources)
            {
                audioSource.Stop();
            }
        }

        // Spela upp cutscene-ljudet
        if (gameAudioSource != null && cutsceneSound != null)
        {
            gameAudioSource.clip = cutsceneSound;
            gameAudioSource.loop = false;
            gameAudioSource.Play();
        }

        // Starta cutscene-effekten
        StartCoroutine(SmoothZoomInOnPlayer(player));
    }


    private IEnumerator SmoothZoomInOnPlayer(Player player)
    {
        // Stoppar allt ljud
        if (gameAudioSource != null)
        {
            gameAudioSource.Stop();
        }

        // Startar cutscene-ljudet
        if (cutsceneSound != null)
        {
            gameAudioSource.clip = cutsceneSound;
            gameAudioSource.Play();
        }

        // Zoom in mot spelaren
        float zoomDuration = 1f / zoomSpeed;
        float elapsedTime = 0f;

        float startSize = mainCamera.orthographicSize;
        Vector3 startPosition = mainCamera.transform.position;

        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, mainCamera.transform.position.z) + zoomCameraOffset;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / zoomDuration;

            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetZoomSize, t);
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        mainCamera.orthographicSize = targetZoomSize;
        mainCamera.transform.position = targetPosition;

        Debug.Log("start cutscene");

        StartCoroutine(FadeBlackoutAndShowRestart());
    }

    private IEnumerator FadeBlackoutAndShowRestart()
    {
        float fadeDuration = 2f;

        Color startColor = blackoutImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        float elapsed = 0f;

        // Fade in blackoutImage
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            blackoutImage.color = Color.Lerp(startColor, endColor, elapsed / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        // Fade in restartButton
        Color buttonStartColor = restartButton.GetComponent<Image>().color; // hämta start colour
        Color buttonEndColor = new Color(buttonStartColor.r, buttonStartColor.g, buttonStartColor.b, 1f); // Slutfärgen med alpha 1

        elapsed = 0f;

        // Fade in restartButton
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            Color newColor = Color.Lerp(buttonStartColor, buttonEndColor, elapsed / fadeDuration);
            restartButton.GetComponent<Image>().color = newColor; // fade in effekt
            yield return null;
        }

        restartButton.GetComponent<Image>().color = buttonEndColor;
        // v�nta p� cutscene ska finish sedan logik f�r att reseta eller f� en restart button
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }





public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);

        if (invader.invaderType == 1)
        {
            SetScore(score + 10);
        }
        if (invader.invaderType == 2)
        {
            SetScore(score + 20);
        }
        if (invader.invaderType == 3)
        {
            SetScore(score + 30);
        }
        if (invaders.GetInvaderCount() == 0)
        {
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        mysteryShip.gameObject.SetActive(false);
        SetScore(score + 225);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false); 
            OnPlayerKilled(player);
        }
    }

}
