using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class UIJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Header("UI")]
        [SerializeField] private GameObject root;
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform handle;

        [Header("Settings")]
        [SerializeField] private float radius = 100f;
        [SerializeField] private float deadZone = 0.1f;

        public Vector2 Direction { get; private set; }

        private void Start()
        {
            root.SetActive(false);
        }

        public void OnReset()
        {
            OnPointerUp(null);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            root.SetActive(true);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                root.transform as RectTransform,
                eventData.position,
                null,
                out var localPos
            );

            background.anchoredPosition = localPos;
            handle.anchoredPosition = Vector2.zero;

            Direction = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background,
                eventData.position,
                null,
                out var pos
            );

            var clamped = Vector2.ClampMagnitude(pos, radius);
            handle.anchoredPosition = clamped;

            var normalized = clamped / radius;
            Direction = normalized.magnitude < deadZone ? Vector2.zero : normalized;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Direction = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            root.SetActive(false);
        }
    }
}