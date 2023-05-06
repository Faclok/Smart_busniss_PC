using Assets.ViewModel;
using Assets.ViewModel.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.View.Body.FullScreen.MessageTask;

namespace Assets.View.Body.Profile.Log
{

    public class LogBugs : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField]
        private MessageView _messageView;

        [SerializeField]
        private InputField _messageText;

        [HideInInspector]
        public new GameObject gameObject;

        private void Awake()
        {
            gameObject = base.gameObject;   
        }

        public void Request()
        {
            _messageView.Show("request server log?", ServerRequest, Close);
        }

        private Task ServerRequest()
        {
            var request = new LogBug();

            request["id"] = null;
            request["userId"] = ManagementAssistant.Profile["id"];
            request["message"] = _messageText.text;
            request["requestDate"] = $"{DateTime.Now:yyyy.MM.dd HH:mm}";

            return Task.Run(() => ModelDatabase.CreatObject(request));
        }

        public void Close()
        {
            _messageText.text = string.Empty;
            gameObject.SetActive(false);
        }
    }
}