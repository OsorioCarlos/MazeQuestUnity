using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ItemAction action;

    [Header("Estilo Item Normal")]
    [SerializeField] private float normalFontSize;
    [SerializeField] private Color normalColor;

    [Header("Estilo Item Seleccionado")]
    [SerializeField] private float seletedFontSize;
    [SerializeField] private Color selectedColor;

    private TextMeshProUGUI textMeshPro;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void SelectItem()
    {
        textMeshPro.fontSize = seletedFontSize;
        textMeshPro.color = selectedColor;
    }

    public void DeselectItem()
    {
        textMeshPro.fontSize = normalFontSize;
        textMeshPro.color = normalColor;
    }

    public void ExecuteAction(InputMenuManagerSO inputMenuManager)
    {
        if (action == ItemAction.Jugar)
        {
            DontDestroy.instance.ResetLevel();
            inputMenuManager.DisableControls();
            SceneManager.LoadScene("Maze");
        }
        else if (action == ItemAction.Salir)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}

enum ItemAction
{
    Jugar,
    Salir
}
