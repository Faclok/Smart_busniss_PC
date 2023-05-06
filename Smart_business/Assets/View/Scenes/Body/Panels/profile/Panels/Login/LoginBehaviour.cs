using Assets.ViewModel.PullDatas;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.Profile.Login 
{
    public class LoginBehaviour : MonoBehaviour
    {

        [Header("Links")]
        [SerializeField]
        private Text _title;

        [SerializeField]
        private Text _description;

        [SerializeField]
        private Button _button;

        private LoginPull _login;

        public void UpdateData(LoginPull loginPull)
        {
            _login = loginPull;
            _title.text = loginPull["os"];
            _description.text = loginPull["readingTime"];
            _button.interactable = loginPull["codeLogin"] == LoginPull.ACTIVE;
        }

        public void Click()
        {
            ControllLogins.DeleteLogin(_login);
        }
    }
}
