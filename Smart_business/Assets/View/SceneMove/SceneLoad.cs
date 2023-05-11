using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.View.SceneMove
{

    /// <summary>
    /// �������� �� �������� ����
    /// </summary>
    [RequireComponent(typeof(AnimationUI))]
    public class SceneLoad : MonoBehaviour
    {
        
        /// <summary>
        /// ������� ���������� � UI
        /// </summary>
        private AnimationUI _animationUI;

        /// <summary>
        /// ������ singleton, ������������ ��� ������ � ����������
        /// ��������� ������������� ���������. ��� �� ��������������
        /// </summary>
        private static SceneLoad _singleton;

        /// <summary>
        /// �������� �������� �������
        /// </summary>
        private static AsyncOperation _loadSceneAsync;

        /// <summary>
        /// ����������� �������
        /// </summary>
        private void Awake()
        {
            _singleton = this;

            _animationUI = GetComponent<AnimationUI>();
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        private void Start()
        {
            _animationUI.Hide();
        }

        /// <summary>
        /// ������������ � Animation event
        /// </summary>
        private void AnimationLastFrame()
        {
            _loadSceneAsync.allowSceneActivation = true;
        }

        /// <summary>
        /// ����� ����������� �� ��������� �����
        /// </summary>
        /// <param name="scene">�����</param>
        public static AsyncOperation Move(ScenesApp scene)
        {
           _loadSceneAsync = SceneManager.LoadSceneAsync(scene.ToString(),LoadSceneMode.Single);
            _loadSceneAsync.allowSceneActivation = false;

            _singleton._animationUI.Show();

            return _loadSceneAsync;
        }

        /// <summary>
        /// ��������� �������
        /// </summary>
        private void OnDestroy()
        {
            _singleton = null;
        }
    }

    /// <summary>
    /// ����� � �������
    /// </summary>
    public enum ScenesApp
    {
        /// <summary>
        /// ����� ��������
        /// </summary>
        SceneLoad,

        /// <summary>
        /// ����� �����
        /// </summary>
        Login,
        
        /// <summary>
        /// �������� �����
        /// </summary>
        Body
    }
}
