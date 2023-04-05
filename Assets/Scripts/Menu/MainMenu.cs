using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsMenuPanel;
    [SerializeField]
    private GameObject _smallDifficultyPanel;
    [SerializeField]
    private GameObject _soundMenuPanel;
    private bool _settingsButtonInteractionAllowed = true;

    private static MainMenu _instance;
    public static MainMenu Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("MainMenuManager is NULL");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Play()
    {
        if (PlayerPrefs.GetInt("Difficulty") == 0)
        {
            _smallDifficultyPanel.SetActive(true);
        }
        else
        {
            LoadScene("Main");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DeleteStats()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ShowSettingsPanel()
    {
        if (!_settingsMenuPanel.activeInHierarchy)
        {
            if (_settingsButtonInteractionAllowed)
            {
                _settingsMenuPanel.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(HideSettingsPanel());
        }
    }

    IEnumerator HideSettingsPanel()
    {
        _settingsButtonInteractionAllowed = false;
        _settingsMenuPanel.GetComponent<Animator>().SetTrigger("Hide");

        yield return new WaitForSeconds(1.5f);

        _settingsMenuPanel.SetActive(false);
        _settingsButtonInteractionAllowed = true;
    }
}
