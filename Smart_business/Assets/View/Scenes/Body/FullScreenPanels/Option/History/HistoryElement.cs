using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.OptionsWindow.History
{
    public class HistoryElement : MonoBehaviour
    {

        [Header("UI")]
        [SerializeField]
        private Text _title;

        [SerializeField]
        private Text _description;

        [SerializeField]
        private Image _icon;

        [HideInInspector]
        public new GameObject gameObject;

        private void Awake()
        {
            gameObject = base.gameObject;
        }

        public void UpdateData(string title, string description, Sprite icon)
        {
            _title.text = title;
            _description.text = description;
            _icon.sprite = icon;
        }
    }
}
