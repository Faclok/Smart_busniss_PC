using Assets.View.Body.Menu;
using UnityEngine;
using TMPro;
using Assets.ViewModel;
using Assets.View.SceneMove;

namespace Assets.View.Body.Profile
{

    /// <summary>
    /// �������� ������ ������������
    /// </summary>
    public class ProfileControll : PanelContent
    {

        /// <summary>
        /// ���� �����
        /// </summary>
        [Header("Text Container")]
        [SerializeField] private TextMeshProUGUI _nameUser;

        /// <summary>
        /// ���� ���������
        /// </summary>
        [SerializeField] private TextMeshProUGUI _apointment;

        /// <summary>
        /// ����������� �������
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            UpdateInfo();
        }

        /// <summary>
        /// ���������� ������
        /// </summary>
        public void UpdateInfo()
        {
            var account = ManagementAssistant.Profile;

            _nameUser.text = account["name"];
            _apointment.text = account.Property["position"];
        }

        /// <summary>
        /// ������ ������
        /// </summary>
        public async void Exit()
        {
            await ManagementAssistant.QuitAccount();
            SceneLoad.Move(ScenesApp.SceneLoad);
        }
    }
}
