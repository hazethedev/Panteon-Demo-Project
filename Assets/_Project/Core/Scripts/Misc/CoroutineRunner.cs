using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoProject.Misc
{
    [DefaultExecutionOrder(-9)]
    public class CoroutineRunner : Singleton<CoroutineRunner>
    {
        private readonly Dictionary<float, WaitForSeconds> m_WaitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();
        private readonly Dictionary<int, IEnumerator> m_RoutineDictionary = new Dictionary<int, IEnumerator>();

        public void RunInNextFrame(Action action)
        {
            var routine = CreateWaitForFrameRoutine(action);
            StartCoroutine(routine);
        }

        public void Run(int id, float delay, Action action)
        {
            var routine = CreateWaitForSecondsRoutine(id, delay, action);
            m_RoutineDictionary.Add(id, routine);

            StartCoroutine(routine);
        }

        public void RunWithoutId(float delay, Action action)
        {
            var routine = CreateWaitForSecondsRoutineWithoutId(delay, action);

            StartCoroutine(routine);
        }

        public void Stop(int id)
        {
            if (m_RoutineDictionary.TryGetValue(id, out var routine))
            {
                m_RoutineDictionary.Remove(id);
                StopCoroutine(routine);
            }
        }

        private IEnumerator CreateWaitForSecondsRoutine(int id, float delay, Action action)
        {
            yield return GetWaitForSeconds(delay);
            m_RoutineDictionary.Remove(id);
            action.Invoke();
        }
        
        private static IEnumerator CreateWaitForFrameRoutine(Action action)
        {
            yield return null;
            action.Invoke();
        }

        private IEnumerator CreateWaitForSecondsRoutineWithoutId(float delay, Action action)
        {
            yield return GetWaitForSeconds(delay);
            action.Invoke();
        }

        private WaitForSeconds GetWaitForSeconds(float seconds)
        {
            if (m_WaitForSecondsDictionary.TryGetValue(seconds, out var waitForSeconds))
            {
                return waitForSeconds;
            }

            m_WaitForSecondsDictionary.Add(seconds, new WaitForSeconds(seconds));

            return m_WaitForSecondsDictionary[seconds];
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
