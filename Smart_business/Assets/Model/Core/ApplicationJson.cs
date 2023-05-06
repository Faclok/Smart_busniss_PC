using UnityEngine;

namespace Assets.Model
{

    /// <summary>
    /// Для работы с Json
    /// </summary>
    public static class ApplicationJson
    {

        /// <summary>
        /// Загрузка класса из json
        /// </summary>
        /// <typeparam name="T">Тип вытягивания элемента</typeparam>
        /// <param name="name">Имя по которому вытягивать данные</param>
        /// <returns></returns>
        public static T FromJson<T>(string name)
        where T : class, IJsonData , new()
        {
            name = name.Replace(' ', '_');

            if (PlayerPrefs.HasKey(name))
                return JsonUtility.FromJson<T>(PlayerPrefs.GetString(name));
            else
                return new();
        }

        /// <summary>
        /// Загрузка класса в json
        /// </summary>
        /// <typeparam name="T">Тип для сохраннения</typeparam>
        /// <param name="name">Имя для сохраннения, и дальнейшего использования вытягивания</param>
        /// <param name="save">Элемент сохранения</param>
        public static void ToJson<T>(this T save)
        where T : class, IJsonData , new()
        {
            var nameKey = save.NameKey.Replace(' ', '_');
            string JsonData = JsonUtility.ToJson(save, true);

            PlayerPrefs.SetString(nameKey, JsonData);
        }
    }
}
