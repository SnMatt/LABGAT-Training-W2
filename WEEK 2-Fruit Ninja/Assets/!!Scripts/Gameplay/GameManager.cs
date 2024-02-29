using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Text _scoreTxt;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Image _fadeImage;

    [Header("References")]
    [SerializeField] private Blade _blade;
    [SerializeField] private Spawner _spawner;

    private int _score;

    [Header("Ads")]
    private int _continueCount;
    [SerializeField] private GameObject _watchAdButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        NewGame();
    }
    public void NewGame()
    {
        _continueCount = 1;
        _gameOverPanel.SetActive(false);
        _score = 0;
        _scoreTxt.text = "Score: " + _score;

        ClearScene();

        _blade.enabled = true;
        _spawner.enabled = true;

        Time.timeScale = 1;
    }
    public void ContinueWithAd()
    {
        _continueCount = 0;
        _gameOverPanel.SetActive(false);

        ClearScene();

        _blade.enabled = true;
        _spawner.enabled = true;

        Time.timeScale = 1;
    }
    private void ClearScene()
    {
        Veggie[] vegs = FindObjectsOfType<Veggie>();
        foreach (var item in vegs)
        {
            Destroy(item.gameObject);
        }
        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (var item in bombs)
        {
            Destroy(item.gameObject);
        }

        _fadeImage.color = Color.clear;
    }
    public void AddScore(int amount)
    {
        _score += amount;
        _scoreTxt.text = "Score: " + _score;
    }
    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;
        while(elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            _fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
    public void EndGame()
    {
        _blade.enabled = false;
        _spawner.enabled = false;

        StartCoroutine(ExplodeSequence());

        if(_continueCount > 0) //kalo perlu, cek apakah load ad success
        {
            _watchAdButton.SetActive(true);
        }else
        {
            _watchAdButton.SetActive(false);
        }

        _gameOverPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneHandler.Instance.MainMenu();
    }

    public void WatchRewardedAd()
    {
        AdManager.Instance.RewardAds.ShowAd();
    }
}
