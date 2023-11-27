using System;
using System.Timers;

using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;


namespace Utils
{
    public class Utils
    {
      
        public bool InRange(float value, int min, int max)
        {
            return value > min && value < max;
        }

        public void SpawnPopUpText(string text, Vector3 position, float duration)
        {
            var tmp = new GameObject().AddComponent<TextMeshPro>();
            tmp.text = text;
            
            DelayAction(4f, () => UnityEngine.Object.Destroy(tmp));
            
        }
        
        public GameObject SpawnPopUpText(string text, Vector3 position)
        {
            var obj = new GameObject();
            obj.AddComponent<TextMeshPro>().text = text;
            return obj;
        }


        // public static IEnumerator TimeOut(float duration,Action callback)
        // {
        //     yield return new WaitForSeconds(duration);
        //     callback();
        // }
        
        public static void DelayAction(float duration, Action callback)
        {
            var timer = new Timer();
            
            timer.Elapsed += delegate
            {
                callback.Invoke();
                timer.Stop();
            };
            timer.Interval = duration;
            timer.Start();
        }

        
        
        
        
        
        
        
        
    }
}