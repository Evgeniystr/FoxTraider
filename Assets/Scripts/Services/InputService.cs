using System;
using UnityEngine;

namespace Aestar.Services
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector3> OnMoveInput;
        public bool IsMoveInputBlocked;

        [SerializeField]
        private bl_Joystick _joystick;

        private const string _horizontalAxisName = "Horizontal";
        private const string _verticalAxisName = "Vertical";
        private const float _inputThreshold = 0.1f;

        private bool _isInputActive;


        private void FixedUpdate()
        {
            Input();
        }

        public void SetMoveInputBlock(bool state)
        {
            IsMoveInputBlocked = state;
        }

        private void Input()
        {
            if (IsMoveInputBlocked)
                return;

#if UNITY_STANDALONE
            _joystick.SetViewAvaliability(false);
            var xInputValue = UnityEngine.Input.GetAxis(_horizontalAxisName);
            var yInputValue = UnityEngine.Input.GetAxis(_verticalAxisName);
#elif UNITY_ANDROID
            _joystick.SetViewAvaliability(true);
            var xInputValue = _joystick.Horizontal;
            var yInputValue = _joystick.Vertical;
#endif

            if (Math.Abs(xInputValue) > _inputThreshold || Math.Abs(yInputValue) > _inputThreshold)
            {
                _isInputActive = true;
                var movement = new Vector3(xInputValue, 0, yInputValue);
                OnMoveInput?.Invoke(movement);
            }
            else if (_isInputActive)
            {
                _isInputActive = false;
                OnMoveInput?.Invoke(Vector3.zero);
            }
        }
    }
}
