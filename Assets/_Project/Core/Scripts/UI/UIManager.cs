using System.Text;
using DemoProject.Collectibles;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace DemoProject.UI
{
    public class UIManager : MonoBehaviour
    {
        public Image CoinPrefab;
        public RectTransform Container;
        public Image CoinUIImage;
        public TextMeshProUGUI CoinText;

        private Camera m_Camera;
        private ObjectPool<Image> m_ImageObjectPool;
        private StringBuilder m_CoinTextBuilder;

        private int CoinCollectCount;

        private void Awake()
        {
            CreateImageObjectPool();
            m_Camera = Camera.main;
            m_CoinTextBuilder = new StringBuilder(5).Insert(0, '0');
            UpdateCoinText();
        }

        private void OnDestroy()
        {
            m_ImageObjectPool.Clear();
        }

        public void PlayCollectibleAnimation(Collectible collected)
        {
            CoinCollectCount += collected.Amount;
            
            var screenPosition = m_Camera.WorldToScreenPoint(collected.transform.position);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect: Container,
                screenPoint: screenPosition,
                cam: null,
                localPoint: out var canvasPosition
            );

            CreateImageTweenAnimation(canvasPosition);
        }

        private void CreateImageTweenAnimation(Vector2 fromAnchored)
        {
            const float moveDownOffsetRaw = 100f;
            const float moveDownTweenDuration = .15f;
            const float moveTweenToTargetDuration = .35f;
            const float timeOffsetPerCoinAnimation = .1f;
            const float totalDurationPerTween = moveDownTweenDuration + moveTweenToTargetDuration;
            const int count = 5;

            var timePosition = 0f;
            var coinAnimSequence = DOTween.Sequence();

            var targetPosition = ((Vector2)CoinUIImage.rectTransform.position) + CoinUIImage.rectTransform.anchoredPosition;
            
            var direction = (targetPosition - fromAnchored).normalized;
            var moveDownOffset = direction * moveDownOffsetRaw;
            
            for (var i = 0; i < count; i++)
            {
                var image = m_ImageObjectPool.Get();
                image.rectTransform.anchoredPosition = fromAnchored;

                Tweener moveDownTween = image.rectTransform
                    .DOLocalMove(image.rectTransform.localPosition - new Vector3(moveDownOffset.x, moveDownOffset.y), moveDownTweenDuration)
                    .SetEase(Ease.InQuad);
                Tweener moveToTargetTween = image.rectTransform
                    .DOMove(targetPosition, moveTweenToTargetDuration)
                    .SetEase(Ease.OutQuad);
                
                coinAnimSequence
                    .Insert(timePosition, moveDownTween)
                    .Insert(timePosition + moveDownTweenDuration, moveToTargetTween)
                    .InsertCallback(timePosition, () => image.enabled = true)
                    .InsertCallback(timePosition + totalDurationPerTween, () => m_ImageObjectPool.Release(image));
                timePosition += timeOffsetPerCoinAnimation;
            }

            coinAnimSequence.InsertCallback(timePosition + totalDurationPerTween, UpdateCoinText);
        }

        private void CreateImageObjectPool()
        {
            m_ImageObjectPool = new ObjectPool<Image>(
                createFunc: () => Instantiate(CoinPrefab, Vector3.zero, Quaternion.identity, Container),
                actionOnGet: image => image.enabled = false,
                actionOnRelease: image => image.enabled = false,
                collectionCheck: false
            );
        }
        
        private void UpdateCoinText()
        {
            m_CoinTextBuilder.Clear().Append(CoinCollectCount);
            CoinText.SetText(m_CoinTextBuilder);
        }
    }
}