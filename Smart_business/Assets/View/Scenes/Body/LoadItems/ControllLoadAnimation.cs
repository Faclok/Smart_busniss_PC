using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.Body.ItemsAnimationLoad
{

    /// <summary>
    /// Контроль элементов загрузки
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ControllLoadAnimation : MonoBehaviour
    {
        
        /// <summary>
        /// Высота объектов загрузки
        /// </summary>
        [Header("Item height")]
        [SerializeField]
        private RectTransform _rectHeight;

        /// <summary>
        /// Тело в которое все устанавливается
        /// </summary>
        [Header("Content instantiate")]
        [SerializeField]
        private Transform _content;

        /// <summary>
        /// Префаб для установки элементов
        /// </summary>
        [Header("prefab item")]
        [SerializeField]
        private ItemAnimation _prefabItem;

        /// <summary>
        /// Кол-во создаваемых объектов
        /// </summary>
        [Header("count view items")]
        [SerializeField]
        private int _countItems;

        private Image _image;

        /// <summary>
        /// Установленные элементы анимции
        /// </summary>
        private ItemAnimation[] _itemsAnimation;

        private GameObject _contentGameIbject;

        /// <summary>
        /// Пробуждение
        /// </summary>
        private void Awake()
        {
            _image = GetComponent<Image>();
            _contentGameIbject = _content.gameObject;
            _itemsAnimation = new ItemAnimation[_countItems];

            for(int i = 0; i < _countItems; i++)
            {
                _itemsAnimation[i] = Instantiate(_prefabItem,_content, false);
                _itemsAnimation[i].Height = _rectHeight.sizeDelta.y;
            }
        }

        /// <summary>
        /// Вызывается в анимации 
        /// </summary>
        public void ShowItems()
        {
            _contentGameIbject.SetActive(_image.enabled = true);

            foreach (var item in _itemsAnimation)
                item.Play();
        }

        public void HideItems()
        {
            foreach (var item in _itemsAnimation)
                item.Stop();

            _contentGameIbject.SetActive(_image.enabled =false);
        }
    }
}
