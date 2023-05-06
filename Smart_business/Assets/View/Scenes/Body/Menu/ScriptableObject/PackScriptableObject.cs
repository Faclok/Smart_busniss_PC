using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// Набор всех отделов для загрузки
    /// </summary>
    [CreateAssetMenu(fileName = "Pack panels", menuName = "ScriptableObject/Pack panels")]
    public class PackScriptableObject : ScriptableObject
    {
        /// <summary>
        /// Список отделов,
        /// не используется свойство, т.к. обладая самой ссылкой, обладаем объектом
        /// </summary>
        public ItemInPackScriptableObject[] itemsInPackScriptableObject;
    }
}
