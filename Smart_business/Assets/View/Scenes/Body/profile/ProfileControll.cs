using Assets.View.Body.Menu;
using UnityEngine;
using TMPro;
using Assets.ViewModel;
using Assets.View.SceneMove;

namespace Assets.View.Body.Profile
{

    /// <summary>
    /// Контроль данных пользователя
    /// </summary>
    public class ProfileControll : PanelContent
    {

        /// <summary>
        /// Поле имени
        /// </summary>
        [Header("Text Container")]
        [SerializeField] private TextMeshProUGUI _nameUser;

        /// <summary>
        /// Поле должности
        /// </summary>
        [SerializeField] private TextMeshProUGUI _apointment;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            UpdateInfo();
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        public void UpdateInfo()
        {
            var account = ManagementAssistant.Profile;

            _nameUser.text = account["name"];
            _apointment.text = account.Property["position"];
        }

        /// <summary>
        /// Кнопка выхода
        /// </summary>
        public async void Exit()
        {
            await ManagementAssistant.QuitAccount();
            SceneLoad.Move(ScenesApp.SceneLoad);
        }
    }
}
