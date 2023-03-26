using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsMenuPanel;
    [SerializeField]
    private GameObject _soundMenuPanel;
    private bool _settingsButtonInteractionAllowed = true;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
