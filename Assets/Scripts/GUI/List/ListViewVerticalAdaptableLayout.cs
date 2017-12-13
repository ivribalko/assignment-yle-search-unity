using System;
using System.Collections.Generic;

using GUI.List;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.List
{
    public interface IListViewLayout
    {
        Vector2 ViewportSize { set; }

        Vector2 CanvasSize { get; }

        void GetVisibleRange(float start, out int firstIndex, out int lastIndex);

        Vector2 GetItemPosition(int index);

        void CalculateParameters();
    }

    public class ListViewVerticalAdaptableLayout : IListViewLayout
    {
        [Serializable]
        public class Settings
        {
            #region - State
            public float m_ItemSpacingY;
            public float m_PaddingTop;
            public float m_PaddingBottom;
            #endregion
        }

        #region - Lifecycle
        public ListViewVerticalAdaptableLayout(IViewModel dataSource, MonoBehaviour itemTemplate, Settings settings)
        {
            m_DataSource = dataSource;
            m_ItemTemplate = itemTemplate;
            m_ItemTemplateCasted = (IListItem)itemTemplate;
            m_Settings = settings;
        }
        #endregion

        #region - State
        readonly IViewModel m_DataSource;
        readonly MonoBehaviour m_ItemTemplate;
        readonly IListItem m_ItemTemplateCasted;
        readonly Settings m_Settings;

        readonly List<float> m_ItemsPositions = new List<float>();

        Vector2 m_ViewportSize;
        #endregion

        #region - Public
        public Vector2 ViewportSize { set { m_ViewportSize = value; } }

        public Vector2 CanvasSize {
            get {
                var height = m_ItemsPositions[m_DataSource.Count];
                return new Vector2(0, height);
            }
        }

        public void GetVisibleRange(float start, out int firstIndex, out int lastIndex)
        {
            firstIndex = lastIndex = 0;

            var end = start + m_ViewportSize.y;

            var dataCount = m_DataSource.Count;
            if (dataCount == 0) {
                firstIndex = -1;
                lastIndex = -2;

                return;
            }

            for (int i = 0; i < dataCount; i++) {
                if (m_ItemsPositions[i] > start) {
                    break;
                }

                firstIndex = i;
            }

            for (int i = firstIndex; i < dataCount; i++) {
                lastIndex = i;

                if (m_ItemsPositions[i] > end) {
                    break;
                }
            }
        }

        public Vector2 GetItemPosition(int index)
        {
            return new Vector2(0, -m_ItemsPositions[index]);
        }

        public void CalculateParameters()
        {
            m_ItemsPositions.Clear();

            var dataCount = m_DataSource.Count;
            var totalYSpace = m_Settings.m_PaddingTop;

            m_ItemsPositions.Add(totalYSpace);

            if (dataCount == 0) {
                return;
            }

            var itemLayoutElement = m_ItemTemplate.GetComponent<ILayoutElement>();

            m_ItemTemplate.gameObject.SetActive(true);

            for (int i = 0; i < dataCount - 1; i++) {
                var data = m_DataSource.Get(i);

                totalYSpace += GetPreferredHeight(itemLayoutElement, data) + m_Settings.m_ItemSpacingY;

                m_ItemsPositions.Add(totalYSpace);
            }

            var lastData = m_DataSource.Get(dataCount - 1);
            totalYSpace += GetPreferredHeight(itemLayoutElement, lastData) + m_Settings.m_PaddingBottom;

            m_ItemsPositions.Add(totalYSpace);

            m_ItemTemplate.gameObject.SetActive(false);
        }
        #endregion

        #region - Private
        float GetPreferredHeight(ILayoutElement itemLayoutElement, object data)
        {
            m_ItemTemplateCasted.Set(data);
            itemLayoutElement.CalculateLayoutInputVertical();

            return itemLayoutElement.preferredHeight;
        }
        #endregion
    }
}