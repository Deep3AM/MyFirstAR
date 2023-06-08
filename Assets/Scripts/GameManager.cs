using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int curHealth = 3;
    public int CurHealth { get { return curHealth; } }
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void OnAttacked()
    {
        var UIFunctions = GameObject.Find("UIFunction").GetComponent<UIFunctions>();
        Debug.Log("Attacked");
        curHealth--;
        UIFunctions.UpdateHealth();
        if (curHealth <= 0)
        {
            Debug.Log("Game End");
            UIFunctions.ActiveResetButton();
        }
    }

    public void ResetHealth()
    {
        curHealth = 0;
    }
}
