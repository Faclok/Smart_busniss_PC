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
    public class ProfileControll : MonoBehaviour
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

        private bool isoOpen = false;

        public void Open()
        {
            if (isoOpen)
            {
                DoubleOpen();
                return;
            }

            PanelContent.OnPanel += Close;

            PanelContent.DisableCurrent();

            isoOpen = true;
            transform.SetAsLastSibling();
        }

        private void DoubleOpen()
        {
            isoOpen = false;
            PanelContent.EnableCurrent();
            transform.SetSiblingIndex(transform.parent.childCount - 2);
            Close();
        }

        public void Close()
        {
            isoOpen = false;

            PanelContent.OnPanel -= Close;
        }

        /// <summary>
        /// ����������� �������
        /// </summary>
        public void Awake()
        {
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
