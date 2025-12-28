using GameState;
using GameState.States;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class LevelSuccessPanel : BasePanel
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text killCountText;
        
        [Header("Buttons")]
        [SerializeField] private Button nextButton;
        [SerializeField] private Button returnButton;

        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private LevelManager _levelManager;

        private void OnEnable()
        {
            nextButton.onClick.AddListener(NextButtonPerformed);
            returnButton.onClick.AddListener(ReturnButtonPerformed);
            UpdateKillCountText(_levelManager.CurrentLevel.KillCount);
        }

        private void OnDisable()
        {
            nextButton.onClick.RemoveListener(NextButtonPerformed);
            returnButton.onClick.RemoveListener(ReturnButtonPerformed);
        }

        private void NextButtonPerformed()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
        }

        private void ReturnButtonPerformed()
        {
            _gameStateMachine.ChangeState<StartState>();
        }

        private void UpdateKillCountText(int killCount)
        {
            killCountText.text = killCount.ToString();
        }
    }
}