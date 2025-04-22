using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI levelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetLevelText();
    }

    private void SetLevelText()
    {
        levelText.text = "Monedas Recogidas: " + DontDestroy.instance.GetLevel();
    }
}
