namespace SRF.UI
{
    using Internal;
    using UnityEngine;

    [AddComponentMenu(ComponentMenuPaths.LongPressButton)]
    public class LongPressButton : UnityEngine.UI.Button
    {
        [SerializeField] private ButtonClickedEvent _onLongPress = new ButtonClickedEvent();
        public float StartTime = 0.6f;
        public float Internal = 0.1f; //按住连续触发事件的间隔
        public bool Repeat = false;

        private bool _handled;
        private bool _pressed;
        private float _pressedTime;
        private float _triggerTime;

        public ButtonClickedEvent onLongPress
        {
            get { return _onLongPress; }
            set { _onLongPress = value; }
        }

        public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            _pressed = false;
        }

        public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (eventData.button != UnityEngine.EventSystems.PointerEventData.InputButton.Left)
            {
                return;
            }
            _pressed = true;
            _handled = false;
            _pressedTime = Time.realtimeSinceStartup;
            _triggerTime = 0;
        }

        public override void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
        {
            //if (!_handled)
            //{
                base.OnPointerUp(eventData);
            //}

            _pressed = false;
            _triggerTime = 0;
        }

        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (!_handled)
            {
                base.OnPointerClick(eventData);
            }
        }

        private void Update()
        {
            if (!_pressed)
            {
                return;
            }

            if (Time.realtimeSinceStartup - _pressedTime >= StartTime)
            {
                _handled = true;

                if (Repeat)
                {
                    _triggerTime += Time.deltaTime;
                    if (_triggerTime >= Internal)
                    {
                        _triggerTime = 0;
                        onLongPress.Invoke();
                    }
                }
                else
                {
                    _pressed = false;
                    onLongPress.Invoke();
                }
            }
        }
    }
}
