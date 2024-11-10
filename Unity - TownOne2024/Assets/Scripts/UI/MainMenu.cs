using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField] private Button _startButton;
    public override GameMenus MenuType()
    {
        return GameMenus.MainMenu;
    }

    private void OnEnable()
    {
        _startButton.Select();
    }
}
