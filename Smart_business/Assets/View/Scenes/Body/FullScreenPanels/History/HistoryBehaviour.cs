
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.HistoryWindow
{
    public class HistoryBehaviour : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private Text _title;

        [SerializeField]
        private Text _desciption;

        [SerializeField]
        private Image _icon;

        public void UpdateData(string title, string description, Sprite icon)
        {
            _title.text = title;
            _desciption.text = description;
            _icon.sprite = icon;
        }
    }
}
