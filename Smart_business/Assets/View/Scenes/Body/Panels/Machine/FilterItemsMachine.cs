using MachineData = Assets.ViewModel.Datas.Machine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.Machine
{

    /// <summary>
    /// Ёлемент фильтр машин
    /// </summary>
    public class FilterItemsMachine : MonoBehaviour, IComparer<MachineBehaviour>
    {
        /// <summary>
        /// “ип фильтра
        /// </summary>
        [SerializeField] private FilterMachine _filter;

        /// <summary>
        /// ћетод дл€ сортивки машин
        /// </summary>
        /// <param name="one">ѕервый</param>
        /// <param name="two">¬торой</param>
        /// <returns></returns>
        public int Compare(MachineBehaviour one, MachineBehaviour two)
        {
            var x = two.Data;
            var y = one.Data;

            switch (_filter)
            {
                case FilterMachine.Inactive:
                    return  y.CountHours.CompareTo(x.CountHours);

                case FilterMachine.Active:
                    return x.CountHours.CompareTo(y.CountHours);

                case FilterMachine.Asc:
                    return x.Name.CompareTo(y.Name);

                case FilterMachine.Desc:
                    return y.Name.CompareTo(x.Name);

                case FilterMachine.ActiveRealtime:
                    return x.IsActive ? y.IsActive ? 0 : 1 : y.IsActive ? -1 : 0;

                case FilterMachine.InactiveRealtime:
                    return y.IsActive ? x.IsActive ? 0 : 1 : x.IsActive ? -1 : 0;
            }

            return 0;
        }

    }

    /// <summary>
    /// »мена фильтров
    /// </summary>
    public enum FilterMachine
    {
       Inactive, Active, Asc, Desc, ActiveRealtime, InactiveRealtime
    }
}
