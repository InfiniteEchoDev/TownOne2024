using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIMgr : Singleton<UIMgr> {
    
    [FormerlySerializedAs("_fadeInDuration")] [SerializeField] private float FadeInDuration = 0.5f;
    [FormerlySerializedAs("_fadeOutDuration")] [SerializeField] private float FadeOutDuration = 0.5f;
    [FormerlySerializedAs("_sortGap")] [SerializeField] private int SortGap = 10;
    
    [FormerlySerializedAs("_screenFaderPrefab")]
    [FormerlySerializedAs("_screenFader")]
    [Header("Menus")]
    [SerializeField] private MenuBase ScreenFaderPrefab;
    [FormerlySerializedAs("_splashMenuPrefab")] [SerializeField] private MenuBase SplashMenuPrefab;
    [FormerlySerializedAs("_mainMenuPrefab")] [SerializeField] private MenuBase MainMenuPrefab;
    [FormerlySerializedAs("_settingsMenuPrefab")] [SerializeField] private MenuBase SettingsMenuPrefab;
    [FormerlySerializedAs("_inGameUIPrefab")] [SerializeField] private MenuBase InGameUIPrefab;
    [FormerlySerializedAs("_gameOverMenuPrefab")] [SerializeField] private MenuBase GameOverMenuPrefab;
    [FormerlySerializedAs("_pauseMenuPrefab")] [SerializeField] private MenuBase PauseMenuPrefab;
    [FormerlySerializedAs("_controlMenuPrefab")] [SerializeField] private MenuBase ControlMenuPrefab;
    [FormerlySerializedAs("_tutorialMenuPrefab")] [SerializeField] private MenuBase TutorialMenuPrefab;

    private Dictionary<GameMenus, MenuBase> _menuInstances = new();
    private Stack<MenuBase> _activeMenus = new();
    private Dictionary<GameMenus, MenuBase> _disabledMenus = new();
    
    public void CloseAllMenus()
    {
        while (_activeMenus.Count > 0)
        {
            var menu = _activeMenus.Pop();
            menu.PerformFullFadeOut(FadeOutDuration);
            _disabledMenus.Add(menu.MenuType(), menu);
        }
    }
    
    public MenuBase ShowMenu(GameMenus menuToOpen, Action onMenuOpenComplete = null, bool fadeIn = true)
    {
        if(menuToOpen == GameMenus.None)return null;
        var menu = PushMenu(menuToOpen);
        
        if (menu == null)
        {
            return null;
        }
        if (fadeIn)
        {
            menu.PerformFullFadeIn(FadeInDuration, onMenuOpenComplete);
        }
        else 
        {
            onMenuOpenComplete?.Invoke();
        }
        return menu;
    }

    public void ShowSplash(Action onComplete)
    {
        var menu = ShowMenu(GameMenus.Splash);
        if (menu is SplashMenu splashMenu)
        {
            splashMenu.OnShow(onComplete);
        }
    }

    public MenuBase ShowHalfFader(Action onComplete)
    {
        var menu = ShowMenu(GameMenus.Fader, fadeIn: false);
        if (menu is ScreenFadeOverlay screenFadeOverlay)
        {
            screenFadeOverlay.PerformHalfFadeIn(FadeInDuration, onComplete);
        }

        return menu;
    }
    
    private MenuBase PushMenu(GameMenus menu)
    {
        // Check if object already exists
        if (!_menuInstances.ContainsKey(menu))
        {
            // instantiate the game object
            var createdMenu = Instantiate(GetMenuPrefabFromType(menu), transform);
            createdMenu.OnInstantiate();
            _menuInstances.Add(menu, createdMenu);
        }
        var uiObj = _menuInstances[menu];
        
        if (_activeMenus.Contains(uiObj))
        {
            Debug.LogError($"Already opened menu {menu}");
            return uiObj;
        }

        if (_disabledMenus.ContainsKey(menu))
        {
            _disabledMenus.Remove(menu);
        }

        int sortOverride;
        
        if (_activeMenus.TryPeek(out var currentTop))
        {
            sortOverride = currentTop.SortOrder + SortGap;
        }
        else
        {
            sortOverride = 0;
        }

        uiObj.SortOrder = sortOverride;
        
        uiObj.PerformFullFadeIn(FadeInDuration);
        _activeMenus.Push(uiObj);
        
        return uiObj;
    }

    public void HideMenu(GameMenus menuToClose, Action onMenuFullyHidden = null, bool fadeOut = true)
    {
        var menu = PopMenu(menuToClose);
        if (menu == null)
            return;
        
        if (fadeOut)
        {
            menu.PerformFullFadeOut(FadeOutDuration, onMenuFullyHidden);
        }
        else
        {
            onMenuFullyHidden?.Invoke();
        }
    }

    private MenuBase PopMenu(GameMenus menu)
    {
        if (!_menuInstances.TryGetValue(menu, out var uiObj))
        {
            Debug.LogError($"Menu {menu} was never created");
            return null;
        }

        if (_activeMenus.TryPeek(out var peekedUI))
        {
            if (peekedUI != uiObj)
            {
                Debug.LogError($"The top of the stack {peekedUI.name} wasn't the object we wanted to hide {uiObj.name}");
                return null;
            }
        }

        if (_activeMenus.TryPop(out var poppedUI))
        {
            if (!_disabledMenus.TryAdd(menu, poppedUI))
            {
                Debug.LogError($"Failed to add {menu} to the disabled menus list. Was it already marked as disabled?");
            }
        }

        return poppedUI;
    }


    private MenuBase GetMenuPrefabFromType(GameMenus menuType)
    {
        MenuBase menu;
        switch (menuType)
        {
            case GameMenus.Fader:
                menu = ScreenFaderPrefab;
                break;
            case GameMenus.Splash:
                menu = SplashMenuPrefab;
                break;
            case GameMenus.MainMenu:
                menu = MainMenuPrefab;
                break;
            case GameMenus.SettingsMenu:
                menu = SettingsMenuPrefab;
                break;
            case GameMenus.InGameUI:
                menu = InGameUIPrefab;
                break;
            case GameMenus.GameOverMenu:
                menu = GameOverMenuPrefab;
                break;
            case GameMenus.PauseMenu:
                menu = PauseMenuPrefab;
                break;
            case GameMenus.ControlMenu:
                menu = ControlMenuPrefab;
                break;
            case GameMenus.TutorialMenu:
                menu = TutorialMenuPrefab;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(menuType), menuType, null);
        }

        if (menu == null)
        {
            Debug.LogError($"Failed to find prefab for {menuType}");
        }

        return menu;
    }
}