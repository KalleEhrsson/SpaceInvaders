using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    private Missile[] missile;
    private Laser laser;


    public Camera mainCamera;
    public float zoomSpeed = 2f;     // uses for the death cutscene type thing
    public float targetZoomSize = 3f;
    private float originalZoomSize;
    public Vector3 originalCameraPosition;
    public Vector3 zoomCameraOffset = new Vector3(0f, 0f, -10f);
    private Image blackoutImage;

    public Button restartButton;
    public Button mainMenu;
    // private AudioSource gameAudioSource;
    public AudioSource cutsceneSound;
    public AudioSource gameAudio;

    float timer = -1;
    float deathtimer = -1;
    public float wave = 1f;

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
        //restartButton = GetComponent<Button>();
        //restartButton.gameObject.SetActive(false);

        NewGame();
    }


    private void Update()
    {

        laser = FindObjectOfType<Laser>();
        missile = FindObjectsOfType<Missile>();

        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

        // A bunch of timers to delay certain fucntions, like for example between waves and death
        timer -= 10f;
        deathtimer -= 10f;

        if (timer == 0f) NewRound();
        if (deathtimer == 0f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (score < 0) SetScore(0);

    }

    public void OnRestartButtonClick()
    {
        restartButton.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);

        NewGame();
    }
    private void NewGame()
    {
        Time.timeScale = 1f;

        SetScore(0);
        SetLives(3);

        gameAudio.Play();
        cutsceneSound.Stop();
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
        // Sets the score
        score = playerScore;
        scoreText.text = $"{score}" + " - " + $"{wave}";

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
        Time.timeScale = 0f;

        cutsceneSound.Play();
        gameAudio.Stop();


        player.gameObject.SetActive(false);
        invaders.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);

        foreach (Bunker activeBunker in bunkers)
        {
            activeBunker.gameObject.SetActive(false);
        }

        foreach(Missile activeMissile in missile)
        {
            activeMissile.gameObject.SetActive(false);
        }

        if (laser != null)
        {
            laser.gameObject.SetActive(false);
        }

        restartButton.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);

        // Starta cutscene-effekten
        // StartCoroutine(SmoothZoomInOnPlayer(player));
    }

    /*
    private IEnumerator SmoothZoomInOnPlayer(Player player)
    {
        yield return null;
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
        GameObject invaderGrid = GameObject.Find("InvaderGrid");
        if (invaderGrid != null)
        {
            Destroy(invaderGrid);
        }

        Laser laser = FindObjectOfType<Laser>();
        if (laser != null)
        {
            laser.enabled = false;
        }

        Missile missile = FindObjectOfType<Missile>();
        if (missile != null)
        {
            missile.enabled = false;
        }

        MysteryShip mysteryShip = FindObjectOfType<MysteryShip>();
        if (mysteryShip != null)
        {
            mysteryShip.enabled = false;
        }

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.enabled = false;
        }

        StartCoroutine(SmoothZoomInOnPlayer(player));
        deathtimer = 1500f;
    }

    private IEnumerator SmoothZoomOnPlayer(Player player)
    {
        // Zooms in on player (I don't know I didn't write this)
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
    }
    */

    public void OnInvaderKilled(Invader invader)
    {
        // v�nta p� cutscene ska finish sedan logik f�r att reseta

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
            SetScore(score);
            NextWave();
        }
    }

    public void NextWave()
    {
        wave++;
        timer = 350f;
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
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
