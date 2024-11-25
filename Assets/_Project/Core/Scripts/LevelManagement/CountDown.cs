using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DemoProject.LevelManagement
{
    public class CountDown : MonoBehaviour
    {
        public TextMeshProUGUI CountDownText;
        public int CountDownTime;

        private LevelManager m_LevelManager;

        private void Awake()
        {
            CountDownText.gameObject.SetActive(false);
        }

        private void Start()
        {
            CountDownText.gameObject.SetActive(true);
            StartCoroutine(StartCounting());
        }

        private IEnumerator StartCounting()
        {
            while (CountDownTime > 0)
            {
                CountDownText.SetText(CountDownTime.ToString());
                yield return new WaitForSeconds(1f);

                CountDownTime--;
            }
            
            CountDownText.SetText("GO!");
            LevelManager.Instance.StartRacing();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}