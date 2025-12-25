using System;
using System.Collections.Generic;
using UI;
using UI.Panels;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<UIPanelEntry> panels;
        [SerializeField] private FadePanel fadePanel;

        private readonly Dictionary<UIPanelType, BasePanel> _panelMap = new();
        private BasePanel _currentPanel;

        private void Awake()
        {
            RegisterPanels();
            HideAll();
        }

        private void RegisterPanels()
        {
            _panelMap.Clear();

            foreach (var entry in panels)
            {
                _panelMap.Add(entry.type, entry.panel);
            }
        }

        public void Show(UIPanelType panelType)
        {
            if (_panelMap.TryGetValue(panelType, out var panel))
            {
                _currentPanel?.Hide();
                _currentPanel = panel;
                _currentPanel.Show();
            }
        }
        
        public void ShowLevelSuccess(int killCount)
        {
            if (_panelMap.TryGetValue(UIPanelType.LevelSuccess, out var panel))
            {
                var levelSuccessPanel = panel as LevelSuccessPanel;

                _currentPanel?.Hide();
                _currentPanel = levelSuccessPanel;

                levelSuccessPanel?.Show(killCount);
            }
        }

        public void FadeIn(Action onComplete = null)
        {
            fadePanel.FadeIn(onComplete);
        }

        public void FadeOut(Action onComplete = null)
        {
            fadePanel.FadeOut(onComplete);
        }

        private void HideAll()
        {
            foreach (var panel in _panelMap.Values)
            {
                panel.Hide();
            }

            _currentPanel = null;
        }
    }

    [Serializable]
    public class UIPanelEntry
    {
        public UIPanelType type;
        public BasePanel panel;
    }
}