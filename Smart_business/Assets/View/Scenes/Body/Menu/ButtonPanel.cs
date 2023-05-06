using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// ������ ������������� � ������ ������ ����
    /// </summary>
    [RequireComponent(typeof(Button), typeof(Animation))]
    public class ButtonPanel : MonoBehaviour
    {

        /// <summary>
        /// ��������� � ������� ����� ������������ ������ ������
        /// </summary>
        [Header("UI")]
        [SerializeField] private Image _icon;

        /// <summary>
        /// �������� ����������� transform, �.�. base �� �����������
        /// </summary>
        [HideInInspector] public new Transform transform;

        /// <summary>
        /// �������� ������
        /// </summary>
        public Sprite Icon
        {
            get => _icon.sprite;
            set => _icon.sprite = value;
        }

        /// <summary>
        /// RequireComponent ����������� ��� ����������� ����� �������
        /// </summary>
        private Animation _animation;

        /// <summary>
        /// ����������� �������
        /// </summary>
        void Awake()
        {
            transform = base.transform;
            _animation = GetComponent<Animation>();
        }

        /// <summary>
        /// ����� ������ ���������� ��������
        /// </summary>
        public void Enable()
        {
            _animation.Play("ActiveButton");
        }

        /// <summary>
        /// ����� ������ ����������� �� ��������
        /// </summary>
        public void Disable()
        {
            _animation.Play("DisableButton");
        }

        /// <summary>
        /// ����� �������� �� ������
        /// </summary>
        public void Click()
        {
            LowerPanel.ClickButton(this);
        }
    }
}