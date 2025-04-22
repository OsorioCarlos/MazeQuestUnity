using Unity.Mathematics;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MenuItem[] menuItems;
    [SerializeField] private InputMenuManagerSO inputMenuManager;
    [SerializeField] private AudioClip itemButtonSFX;

    private int selectedItemIndex = 0;
    private AudioManager audioManager;

    private void OnEnable()
    {
        inputMenuManager.OnNavigate += Navigate;
        inputMenuManager.OnSubmit += Submit;
    }

    private void Awake()
    {
        audioManager = GameObject.FindFirstObjectByType<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuItems[selectedItemIndex].SelectItem();
    }

    private void Navigate(Vector2 ctx)
    {
        int value = 0;
        if (ctx.y > 0)
        {
            value = -1;
        }
        else if (ctx.y < 0)
        {
            value = 1;
        }
        int newIndex = Mathf.Clamp(selectedItemIndex + value, 0, menuItems.Length - 1);

        if (selectedItemIndex != newIndex)
        {
            audioManager.PlaySFX(itemButtonSFX);
            menuItems[selectedItemIndex].DeselectItem();
            selectedItemIndex = newIndex;
            menuItems[selectedItemIndex].SelectItem();
        }
    }

    private void Submit()
    {
        menuItems[selectedItemIndex].ExecuteAction(inputMenuManager);
    }
}
