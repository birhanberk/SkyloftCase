using Managers;
using UI;
using VContainer;

namespace GameState.States
{
    public class LevelSuccessState : IGameState
    {
        [Inject] private readonly UIManager _uiManager;
        [Inject] private readonly LevelManager _levelManager;
        [Inject] private readonly PersistentDataManager _persistentDataManager;

        public void Enter()
        {
            _uiManager.Show(UIPanelType.LevelSuccess);
            _levelManager.OnLevelSuccessful();
            _uiManager.Joystick.OnReset();
            UpdateTotalKilled();
        }
        
        private void UpdateTotalKilled()
        {
            var totalKilled = _persistentDataManager.Load(_persistentDataManager.TotalKilledKey);
            _persistentDataManager.Save(_persistentDataManager.TotalKilledKey, totalKilled + _levelManager.CurrentLevel.KillCount);
        }

        public void Tick() { }

        public void Exit()
        {
        }
    }
}