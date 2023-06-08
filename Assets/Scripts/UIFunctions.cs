using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{
    [SerializeField] private List<GameObject> healthImages;
    [SerializeField] private Button retryButton;
    private void Start()
    {
        retryButton.gameObject.SetActive(true);
        retryButton.onClick.AddListener(() => { GameManager.Instance.ResetHealth(); SceneManager.LoadScene(0); });
        retryButton.gameObject.SetActive(false);
    }
    public void UpdateHealth()
    {
        for (int i = 0; i < 3; i++)
        {
            healthImages[i].SetActive(false);
        }
        for (int i = 0; i < GameManager.Instance.CurHealth; i++)
        {
            healthImages[i].SetActive(true);
        }
    }

    public void ActiveResetButton()
    {
        retryButton.gameObject.SetActive(true);
    }
}
