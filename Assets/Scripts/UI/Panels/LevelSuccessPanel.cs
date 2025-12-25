using GameState;
using GameState.States;
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

        [Inject] private GameStateMachine _gameStateMachine;
        
        private void OnEnable()
        {
            nextButton.onClick.AddListener(NextButtonPerformed);
        }

        private void OnDisable()
        {
            nextButton.onClick.RemoveListener(NextButtonPerformed);
        }

        private void NextButtonPerformed()
        {
            _gameStateMachine.ChangeState<LevelTransitionState>();
        }

        public void Show(int killCount)
        {
            Show();
            UpdateKillCountText(killCount);
        }

        private void UpdateKillCountText(int killCount)
        {
            killCountText.text = killCount.ToString();
        }
    }
}