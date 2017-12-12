using System;
using System.Globalization;
using System.Xml;

using Auxiliary;
using GUI;
using UnityEngine;
using UnityEngine.UI;
using Yle;

namespace GUI.Details
{
    public interface IDetailsView : IView
    {
        event Action onBackButton;

        void Set(ProgramData data);
    }

    public class DetailsWindow : View, IDetailsView
    {
        #region - Constants
        const string TextFormat = "{0}: {1}";
        #endregion

        #region - LifeCycle
        void Start()
        {
            m_backButton.onClick.AddListener(() => onBackButton());
        }

        #if UNITY_ANDROID
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                onBackButton();
            }
        }
        #endif
        #endregion

        #region - State
        [SerializeField] Button m_backButton;
        [SerializeField] Text m_TitleText;
        [SerializeField] Text m_TypeText;
        [SerializeField] Text m_CountryText;
        [SerializeField] Text m_DataModifiedText;
        [SerializeField] Text m_CreatorText;
        [SerializeField] Text m_DescriptionText;
        [SerializeField] ScrollRect m_DescriptionScrollRect;

        readonly CultureInfo m_FinnishCulture = new CultureInfo("fi-FI");
        #endregion

        #region - Public
        public event Action onBackButton;

        public void Set(ProgramData data)
        {
            m_TitleText.text = data.title
                .Localised()
                .ProveNotEmpty();

            SetText(m_TypeText, "Type", data.type);

            SetText(m_CountryText, "Country", data.countryOfOrigin);

            var format = XmlDateTimeSerializationMode.Utc;
            var date = XmlConvert.ToDateTime(data.indexDataModified, format)
                .ToString("d", m_FinnishCulture);
            
            SetText(m_DataModifiedText, "Updated", date);

            SetText(m_CreatorText, "Creator", data.creator, creator => creator.name);

            SetText(m_DescriptionText, "Description", data.description.Localised());

            m_DescriptionScrollRect.verticalNormalizedPosition = 1f;
        }
        #endregion

        #region - Private
        void SetText<T>(Text target, string name, T[] array, Func<T, string> retrive = null)
        {
            string value = null;

            if (array != null && array.Length != 0) {
                value = retrive == null
                    ? array[0].ToString()
                    : retrive(array[0]);
            }

            SetText(target, name, value);
        }

        void SetText(Text target, string name, string value)
        {
            value = value.ProveNotEmpty();

            target.text = string.Format(TextFormat, name, value);
        }
        #endregion
    }
}