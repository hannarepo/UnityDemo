using System;

namespace UnityDemo
{
    public class Timer
    {
        private float _startTime = 0.0f;
        private float _time = 0.0f;
        private Action _timeOutAction = null;

        /// <summary>
        /// Creates a new timer.
        /// </summary>
        /// <param name="startTime"> Duration of the timer. </param>
        /// <param name="timeoutAction"> Optionsl action to invoke when timer is finished. </param>
        public Timer(float time, Action timeoutAction = null)
        {
            _startTime = time;
            _time = time;
            _timeOutAction = timeoutAction;
        }

        public float TimerProgress()
        {
            return _startTime / _time;
        }

        /// <summary>
        /// Checks whether timer is finished.
        /// </summary>
        /// <returns> True if timer is finished, false otherwise. </returns>
        public bool IsTimerFinished()
        {
            if (_time <= 0) return true;
            return false;
        }

        /// <summary>
        /// Resets the timer to original start time.
        /// </summary>
        public void ResetTimer()
        {
            _time = _startTime;
        }

        /// <summary>
        /// Updates the timer with given delta time and invokes timeout action if it is set.
        /// </summary>
        /// <param name="deltaTime"> Delta time. </param>
        public void UpdateTimer(float deltaTime)
        {
            _time -= deltaTime;
            if (_time <= 0 && _timeOutAction != null)
            {
                _timeOutAction.Invoke();
            }
        }
    }
}
