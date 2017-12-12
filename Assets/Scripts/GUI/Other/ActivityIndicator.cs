using UnityEngine;

namespace GUI.Other
{
    public class ActivityIndicator : MonoBehaviour
    {
        #region - Constant
        const float AnimationDuration = 0.3f;
        //fix infinite approach
        const float SmoothCorrective = 10f;
        #endregion

        #region - LifeCycle
        void OnDisable()
        {
            m_AnimationRunning = false;
        }

        void Update()
        {
            if (!m_AnimationRunning || ScreenChanged()) {
                AnimationStart();
            }

            var position = GetPosition();

            m_Runner.anchoredPosition = position;
        }
        #endregion

        #region - State
        [SerializeField] RectTransform m_Runner;

        bool m_AnimationRunning;
        bool m_AnimationReverse;
        float m_PathLength;
        float m_CurrentVelocity;
        int m_ScreenWidth;
        #endregion

        #region - Private
        bool ScreenChanged()
        {
            var result = m_ScreenWidth != Screen.width;

            m_ScreenWidth = Screen.width;

            return result;
        }

        void AnimationStart()
        {
            m_Runner.anchoredPosition = Vector2.zero;

            var allLength = GetComponent<RectTransform>().rect.width;
            var runnerLength = m_Runner.rect.width;

            m_PathLength = allLength - runnerLength;
            m_AnimationRunning = true;
        }

        bool IsReverse()
        {
            m_AnimationReverse = m_AnimationReverse
                ? m_Runner.anchoredPosition.x > 0
                : m_Runner.anchoredPosition.x > m_PathLength;

            return m_AnimationReverse;
        }

        Vector2 GetPosition()
        {
            var target = IsReverse()
                ? 0 - SmoothCorrective
                : m_PathLength + SmoothCorrective;

            var smoothed = Mathf.SmoothDamp(
                               m_Runner.anchoredPosition.x,
                               target,
                               ref m_CurrentVelocity,
                               AnimationDuration
                           );

            var result = new Vector2(smoothed, 0f);

            return result;
        }
        #endregion
    }
}
