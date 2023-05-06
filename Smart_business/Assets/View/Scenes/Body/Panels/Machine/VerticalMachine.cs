using Assets.MultiSetting;
using Assets.View.Body.FullScreen;
using Assets.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using animationItems = Assets.View.Body.ItemsAnimationLoad;
using MachineData = Assets.ViewModel.Datas.Machine;

namespace Assets.View.Body.Machine
{

    /// <summary>
    /// Используется для контроля машин в главном меню
    /// </summary>
    public class VerticalMachine : MonoBehaviour
    {
        /// <summary>
        /// Заранее сохранненый объект
        /// </summary>
        [HideInInspector] public new GameObject gameObject;

        /// <summary>
        /// Заранее сохранненый объект
        /// </summary>
        [HideInInspector] public new Transform transform;

        /// <summary>
        /// Префаб машины в меню
        /// </summary>
        [Header("Prefab")]
        [SerializeField] private MachineBehaviour _prefabMachine;

        /// <summary>
        /// Контент в который устанавливаются машины
        /// </summary>
        [Header("Content Machine")]
        [SerializeField]
        private Transform _contentMachine;

        /// <summary>
        /// Иконка для перезагрузки отдела
        /// </summary>
        [Header("Icon load")]
        [SerializeField]
        private Image _iconUpdate;

        /// <summary>
        /// Высота для отягивания иконки загрузки
        /// </summary>
        [SerializeField]
        private RectTransform _transformItemHeight;

        /// <summary>
        /// Аниматор загрузки
        /// </summary>
        [SerializeField]
        private animationItems.ControllLoadAnimation _animtionItems;

        /// <summary>
        /// Высота по которой отягивается иконка
        /// </summary>
        private float _itemHeight;
        
        /// <summary>
        /// Изначальная высота
        /// </summary>
        private float _startHeight;

        /// <summary>
        /// Установление машины
        /// </summary>
        private MachineBehaviour[] _machineBehaviours = new MachineBehaviour[0];

        /// <summary>
        /// Загруженые машины
        /// </summary>
        private MachineData[] _machineDatas;

        /// <summary>
        /// Свойтсво загруженных машин
        /// </summary>
        public MachineData[] MachineDatas => _machineDatas;

        /// <summary>
        /// Пробуждение
        /// </summary>
        private void Awake()
        {
            gameObject = base.gameObject;
            transform = base.transform;
        }

        /// <summary>
        /// Рендер первого кадра
        /// </summary>
        private void Start()
        {
            _animtionItems.ShowItems();
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<MachineData>(MachineData.TableContains); }).GetTaskCompleted(OnDatasLoad);


            _itemHeight = _transformItemHeight.rect.height;
            _startHeight = transform.localPosition.y;
            FilterControllMachine.FilterClick += OnSortFilter;
        }

        /// <summary>
        /// Сортировать машины по фильтру
        /// </summary>
        /// <param name="comparer">фильтр</param>
        private void OnSortFilter(IComparer<MachineBehaviour> comparer)
        {
            if (_machineBehaviours == null)
                return;

            Array.Sort(_machineBehaviours, comparer);

            for (int i = 1; i <= _machineBehaviours.Length; i++)
                _machineBehaviours[i - 1].transform.SetSiblingIndex(i);
        }

        /// <summary>
        /// Когда загрузка закончена
        /// </summary>
        public void OnDatasLoad(MachineData[] machines)
        {
            _machineDatas = machines;

            var machineBehaviours = InstantiateExtensions.GetOverwriteInstantiate(_prefabMachine, _contentMachine, _machineBehaviours, _machineDatas);

            for (int i = 0; i < machines.Length; i++)
                machineBehaviours[i].UpdateData(machines[i]);

            _machineBehaviours = machineBehaviours;

            _animtionItems.HideItems();
        }

        /// <summary>
        /// Срабатывает когда скролите главное меню
        /// </summary>
        /// <param name="content"></param>
        public void CheckScrollVertical(RectTransform content)
        {
            var moveFrame = _startHeight - content.localPosition.y;
            var alpha = moveFrame / _itemHeight;

            _iconUpdate.color = new Color(1f, 1f, 1f, _iconUpdate.fillAmount = alpha);

            if (_iconUpdate.fillAmount >= 1f)
            {
#if PLATFORM_ANDROID
                if (Input.touchCount >= 1)
                    return;
#else
                 if(Input.mousePresent)
                    return;
#endif
                _animtionItems.ShowItems();
                Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<MachineData>(MachineData.TableContains); }).GetTaskCompleted(OnDatasLoad);
            }
        }

        public void UpdateDatasOnChanger()
        {
            _animtionItems.ShowItems();
            Task.Run(async () => { return await ModelDatabase.GetUniqueObjectAsync<MachineData>(MachineData.TableContains); }).GetTaskCompleted(OnDatasLoad);
        }
        /// <summary>
        /// Выход из поисковика
        /// </summary>
        public void SearchClose()
        {
            for (int i = 0; i < _machineBehaviours?.Length; i++)
                _machineBehaviours[i].gameObject.SetActive(true);
        }

        /// <summary>
        /// Когда вводите сымволы в поиск
        /// </summary>
        /// <param name="input"></param>
        public void SearchChanger(InputField input)
        {
            if (string.IsNullOrWhiteSpace(input.text))
                return;

            for(int i = 0; i < _machineBehaviours?.Length; i++)
                _machineBehaviours[i].gameObject.SetActive(_machineBehaviours[i].Data.Name.Contains(input.text));
        }
    }
}