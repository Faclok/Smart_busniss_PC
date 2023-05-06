using UnityEngine;
using TMPro;

using Assets.View.SceneMove;
using Assets.MultiSetting;
using System.Threading.Tasks;

namespace Assets.View.Load
{

    /// <summary>
    /// ������ ��� ������ � UI
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class ModuleUI : MonoBehaviour
    {

        /// <summary>
        /// ������ reset
        /// </summary>
        [SerializeField]
        private GameObject _button;

        /// <summary>
        /// �������� ���������� 
        /// </summary>
        [SerializeField]
        private AnimationClip _loadLoop;

        /// <summary>
        /// �������� ������
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _textException;

        /// <summary>
        /// ������ ���������� � ����������
        /// </summary>
        private Animation _animation;

        /// <summary>
        /// ����������� �������
        /// </summary>
        private void Awake()
        {
            _animation = GetComponent<Animation>();
        }

        /// <summary>
        /// ����� ������ ����� ����� ��������� �����������
        /// </summary>
        public void OnUpdate()
        {
            _animation.Play(_loadLoop.name);

            _textException.text = string.Empty;
            _button.SetActive(false);
        }

        /// <summary>
        /// ��� ����� � �������
        /// </summary>
        /// <param name="isAccount"></param>
        public void OnLogin(bool isAccount)
        {
            if(isAccount)
                SceneLoad.Move(ScenesApp.Body);
            else SceneLoad.Move(ScenesApp.Login);

            _animation.Stop();
        }

        /// <summary>
        /// ���������� ������ ��� �������� � �������
        /// </summary>
        /// <param name="failed">��� ������</param>
        public void OnFailed(TypeException failed)
        {
            _animation.Stop();

            _textException.text = failed switch
            {
                TypeException.DisconnectedServer => "�� ������� ������������ � �������.\n���������� ���.",
                TypeException.NotNetwork => "����������� ��������, �������� ����������.",
                TypeException.SystemFailed => "�������� ��������� ������, ������ ���������� ����� ��������� ��������������� � ����� �����������.\n���������� ���������� � ����� ���������� (������).",
               TypeException.LogicApplication => "������ � ������������� ��� �� ������ ������ ����, ��� �� ����������� � ���� ������� =)",
                _ =>"��������� �� ��������� ������, ������������� ����������."
            };

            _button.SetActive(true);
        }
    }
}
