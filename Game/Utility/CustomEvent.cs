using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.Events
{
    public abstract class CustomEvent : MonoBehaviour
    {
        public bool startAutomatically { get; set; }
        public bool isPlaying { get; set; }
        


        private void Awake()
        {
            
        }


        private void Update()
        {
            if (startAutomatically)
            {
                Play();
            }
        }

        public virtual void Play()
        {
            isPlaying = true;
        }
        public virtual void Stop()
        {
            isPlaying = false;
           
        }
        public abstract void EventReset();

    }
}
