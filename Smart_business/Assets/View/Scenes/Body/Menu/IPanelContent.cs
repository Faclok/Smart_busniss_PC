using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// Интерфейс для реализации window в главном экранне
    /// </summary>
    public interface IPanelContent
    {

        /// <summary>
        /// Когда window открывают
        /// </summary>
        public void Open();

        /// <summary>
        /// Когда window закрывают
        /// </summary>
        public void Close();

        /// <summary>
        /// Когда window удаляют со сцены
        /// </summary>
        public void Destroy();
    }
}