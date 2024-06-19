using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private int scoreValue;

    [SerializeField] private GameObject buttonEndGame;

    [Header("Reload details")]
    [SerializeField] private BoxCollider2D reloadWindow;
    [SerializeField] private GunController theGun;
    [SerializeField] private int reloadSteps;
    [SerializeField] private ReloadButtonUI[] reloadButtons;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        reloadButtons = GetComponentsInChildren<ReloadButtonUI>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 0)
        {
            timerText.text = Time.time.ToString("#,#");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenReloadUI();
        }
    }

    public void OpenReloadUI()
    {
        foreach (ReloadButtonUI button in reloadButtons)
        {
            button.gameObject.SetActive(true);

            float randomX = Random.Range(reloadWindow.bounds.min.x, reloadWindow.bounds.max.x);
            float randomY = Random.Range(reloadWindow.bounds.min.y, reloadWindow.bounds.max.y);

            button.transform.position = new Vector2(randomX, randomY);
        }

        Time.timeScale = .4f;

        reloadSteps = reloadButtons.Length;

    }

    public void AttemptToReload()
    {
        if (reloadSteps > 0)
        {
            reloadSteps--;
        }
        
        if (reloadSteps <= 0)
        {
            theGun.ReloadGun();
        }
    }

    public void AddScore()
    {
        scoreValue++;
        scoreText.text = scoreValue.ToString("#,#");
    }

    public void UpdateInfoAmmo(int currentBullets, int maxBullets)
    {
        ammoText.text = currentBullets + "/" + maxBullets;
    }

    public void OpenEndScene()
    {
        Time.timeScale = 0;
        buttonEndGame.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
