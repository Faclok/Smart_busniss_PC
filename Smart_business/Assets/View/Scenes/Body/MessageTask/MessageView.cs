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

        private Func<Task> _action;
        private Action _close;

        public void Show(string text, Func<Task> action, Action close)
        {
            _cancel.interactable = _ok.interactable = true;
            _close = close;
            _textViewl.text = text;
            _action = action;
            _content.SetActive(true);
        }

        public void Ok()
        {
            _cancel.interactable = _ok.interactable = false;
            _itemAnimation.Play();
            var task = _action();
            task.GetTaskCompleted(OkEnd);
        }

        private void OkEnd()
        {
            Hide();
            _close();
        }

        public void Hide()
        {
            _itemAnimation.Stop();
            _content.SetActive(false);
        }
    }
}
