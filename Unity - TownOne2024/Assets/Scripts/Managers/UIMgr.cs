using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIMgr : Singleton<UIMgr> {
    
    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private float _fadeOutDuration = 0.5f;
    [SerializeField] private int _sortGap = 10;
    
    [FormerlySerializedAs("_screenFader")]
    [Header("Menus")]
    [SerializeField] private MenuBase _screenFaderPrefab;
    [SerializeField] private MenuBase _splashMenuPrefab;
    [SerializeField] private MenuBase _mainMenuPrefab;
    [SerializeField] private MenuBase _settingsMenuPrefab;
    [SerializeField] private MenuBase _inGameUIPrefab;
    [SerializeField] private MenuBase _gameOverMenuPrefab;
    [SerializeField] private MenuBase _pauseMenuPrefab;
    [SerializeField] private MenuBase _controlMenuPrefab;
    [SerializeField] private MenuBase _tutorialMenuPrefab;

    private Dictionary<GameMenus, MenuBase> _menuInstances = new();
    private Stack<MenuBase> _activeMenus = new();
    private Dictionary<GameMenus, MenuBase> _disabledMenus = new();
    
    public void CloseAllMenus()
    {
        while (_activeMenus.Count > 0)
        {
            var menu = _activeMenus.Pop();
            menu.PerformFullFadeOut(_fadeOutDuration);
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
            menu.PerformFullFadeIn(_fadeInDuration, onMenuOpenComplete);
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
            screenFadeOverlay.PerformHalfFadeIn(_fadeInDuration, onComplete);
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
            sortOverride = currentTop.SortOrder + _sortGap;
        }
        else
        {
            sortOverride = 0;
        }

        uiObj.SortOrder = sortOverride;
        
        uiObj.PerformFullFadeIn(_fadeInDuration);
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
            menu.PerformFullFadeOut(_fadeOutDuration, onMenuFullyHidden);
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
                menu = _screenFaderPrefab;
                break;
            case GameMenus.Splash:
                menu = _splashMenuPrefab;
                break;
            case GameMenus.MainMenu:
                menu = _mainMenuPrefab;
                break;
            case GameMenus.SettingsMenu:
                menu = _settingsMenuPrefab;
                break;
            case GameMenus.InGameUI:
                menu = _inGameUIPrefab;
                break;
            case GameMenus.GameOverMenu:
                menu = _gameOverMenuPrefab;
                break;
            case GameMenus.PauseMenu:
                menu = _pauseMenuPrefab;
                break;
            case GameMenus.ControlMenu:
                menu = _controlMenuPrefab;
                break;
            case GameMenus.TutorialMenu:
                menu = _tutorialMenuPrefab;
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