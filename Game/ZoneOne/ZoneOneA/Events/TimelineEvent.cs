using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace game.Events
{

    public class TimelineEvent : MonoBehaviour
    {

        public PlayableDirector eventTimeline;


        

        bool played = false;

        public void Play()
        {
            Debug.Log("Playing Sequence");
            if (!played)
            {
                eventTimeline.Play();
                played = true;
            }
        }

        public void Stop()
        {
          
            
        }


        public void EventReset()
        {

        }
    }
}
