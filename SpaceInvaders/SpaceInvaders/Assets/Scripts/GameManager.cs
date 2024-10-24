using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using TMPro;

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

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
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
    }

    private IEnumerator SmoothZoomInOnPlayer(Player player)
    {
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

        // v�nta p� cutscene ska finish sedan logik f�r att reseta eller f� en restart button
        yield return new WaitForSeconds(10f);
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
