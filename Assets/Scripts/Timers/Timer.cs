using System;

namespace WildGunman.Timers
{
    public class Timer
    {
        private bool _isActive;
        private float _remainingSec;
        private Action _callback;

        public void Start(float durationSec, Action callback)
        {
            _isActive = true;
            _remainingSec = durationSec;
            _callback = callback;
        }

        public void Update(float deltaTime)
        {
            if (!_isActive)
            {
                return;
            }

            _remainingSec -= deltaTime;
            if (_remainingSec <= 0)
            {
                Stop();
                _callback?.Invoke();
            }
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}