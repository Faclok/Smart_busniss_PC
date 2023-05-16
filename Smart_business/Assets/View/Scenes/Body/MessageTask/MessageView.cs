using Assets.MultiSetting;
using Assets.View.Body.ItemsAnimationLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.FullScreen.MessageTask
{

    public class MessageView : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private ItemAnimation _itemAnimation;

        [SerializeField]
        private Text _textViewl;

        [SerializeField]
        private Button _cancel;

        [SerializeField]
        private Button _ok;

        private Func<Task> _funcTask;
        private Action _actionOk;

        private static MessageView _singleton;

        private void Awake()
        {
            _singleton = this;
        }

        public static void ShowTask(string qyestion, Func<Task> funcTask, Action actionOk)
            => _singleton.Show(qyestion, funcTask, actionOk);

        public void Show(string question, Func<Task> funcTask, Action actionOk)
        {
            _cancel.interactable = _ok.interactable = true;
            _textViewl.text = question;
            _funcTask = funcTask;
            _actionOk = actionOk;

            _content.SetActive(true);
        }

        public void Ok()
        {
            _cancel.interactable = _ok.interactable = false;

            _itemAnimation.gameObject.SetActive(true);
            _itemAnimation.Play();

            _funcTask().GetTaskCompleted(OnOk);
        }

        private void OnOk()
        {
            _actionOk();
            Hide();
        }

        public void Hide()
        {
            _itemAnimation.gameObject.SetActive(false);

            _content.SetActive(false);
        }

        private void OnDestroy()
        {
            _singleton = null;
        }
    }
}
