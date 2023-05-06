using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.SceneMove
{

    /// <summary>
    /// ��� ������ � ���������� UI
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class AnimationUI : MonoBehaviour
    {
        /// <summary>
        /// �������� ��������� 
        /// </summary>
        [Header("Clips")]
        [SerializeField]
        private AnimationClip _Show;

        /// <summary>
        /// �������� �������
        /// </summary>
        [SerializeField]
        private AnimationClip _Hide;

        /// <summary>
        /// ��������� Animation
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
        /// ����� ��������� �������� ������
        /// </summary>
        public void Show()
        {
            _animation.Play(_Show.name);
        }

        /// <summary>
        /// ����� ��������� �������� �������
        /// </summary>
        public void Hide()
        {
            _animation.Play(_Hide.name);
        }
    }
}
