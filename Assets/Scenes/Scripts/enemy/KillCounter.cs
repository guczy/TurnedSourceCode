using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance;
    private int killCount = 0;
    public Text killCountText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateKillCountText();
    }

    public void AddKill()
    {
        killCount++;
        UpdateKillCountText();
    }

    void UpdateKillCountText()
    {
        killCountText.text = "Kills: " + killCount.ToString();
    }
}
