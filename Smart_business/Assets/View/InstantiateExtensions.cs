using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.View
{
    public static class InstantiateExtensions
    {

        public static T Instantiate<T>(this T prefab, Transform content)
            where T : MonoBehaviour
            => UnityEngine.Object.Instantiate(prefab, content, false);

        public static TResult[] GetOverwriteInstantiate<TResult, TValue>(TResult prefab, Transform content, TResult[] oldArray, TValue[] newArray)
            where TResult : MonoBehaviour
       => GetOverwriteInstantiate(prefab,content,oldArray, newArray);

        public static TResult[] GetOverwriteInstantiate<TResult>(TResult prefab, Transform content, TResult[] oldArray, int counNewArray)
           where TResult : MonoBehaviour
        {
            var items = oldArray.ToList();
            var countAdd = counNewArray - items.Count;

            switch (countAdd)
            {
                case > 0:

                    for (int i = 0; i < countAdd; i++)
                        items.Add(prefab.Instantiate(content));

                    break;

                case < 0:

                    var newCount = items.Count + countAdd;

                    for (int i = items.Count - 1; items.Count > newCount; i--)
                    {
                        var currentItem = items[i];
                        items.Remove(currentItem);
                        UnityEngine.Object.Destroy(currentItem.gameObject);
                    }

                    break;
            }

            return items.ToArray();
        }
    }
}
