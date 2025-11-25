using UnityEngine;

namespace Root.Timers {
    public class Timer {
        private float completionTime;
        
        public Timer() {
        }

        public void AddTime(float time) {
            completionTime += time;
        }

        public bool IsCompleted() {
            return Time.time >= completionTime;
        }

        public void Reset(float timeToWait) {
            completionTime = timeToWait + Time.time;
        }
    }
}