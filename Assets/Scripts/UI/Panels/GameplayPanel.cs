using TMPro;
using UnityEngine;

namespace UI.Panels
{
    public class GameplayPanel : BasePanel
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text killCountText;
    }
}