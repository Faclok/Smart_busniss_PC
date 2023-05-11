using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.View.SceneMove
{

    /// <summary>
    /// Отвечает за загрузку сцен
    /// </summary>
    [RequireComponent(typeof(AnimationUI))]
    public class SceneLoad : MonoBehaviour
    {
        
        /// <summary>
        /// Моудуль работающий с UI
        /// </summary>
        private AnimationUI _animationUI;

        /// <summary>
        /// Паттер singleton, используется для вызова и пермещения
        /// используя установленный компонент. Так же инкапсулирован
        /// </summary>
        private static SceneLoad _singleton;

        /// <summary>
        /// Операция загрузки объекта
        /// </summary>
        private static AsyncOperation _loadSceneAsync;

        /// <summary>
        /// Пробуждение объекта
        /// </summary>
        private void Awake()
        {
            _singleton = this;

            _animationUI = GetComponent<AnimationUI>();
        }

        /// <summary>
        /// Первый кадр
        /// </summary>
        private void Start()
        {
            _animationUI.Hide();
        }

        /// <summary>
        /// Используется в Animation event
        /// </summary>
        private void AnimationLastFrame()
        {
            _loadSceneAsync.allowSceneActivation = true;
        }

        /// <summary>
        /// Метод перемещения на следующую сцену
        /// </summary>
        /// <param name="scene">Сцена</param>
        public static AsyncOperation Move(ScenesApp scene)
        {
           _loadSceneAsync = SceneManager.LoadSceneAsync(scene.ToString(),LoadSceneMode.Single);
            _loadSceneAsync.allowSceneActivation = false;

            _singleton._animationUI.Show();

            return _loadSceneAsync;
        }

        /// <summary>
        /// Удаленние объекта
        /// </summary>
        private void OnDestroy()
        {
            _singleton = null;
        }
    }

    /// <summary>
    /// Сцены в проекте
    /// </summary>
    public enum ScenesApp
    {
        /// <summary>
        /// Сцена загрузки
        /// </summary>
        SceneLoad,

        /// <summary>
        /// Сцена входа
        /// </summary>
        Login,
        
        /// <summary>
        /// Основаня сцена
        /// </summary>
        Body
    }
}
