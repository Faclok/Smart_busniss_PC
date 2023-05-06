using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// ��������� ��� ���������� window � ������� �������
    /// </summary>
    public interface IPanelContent
    {

        /// <summary>
        /// ����� window ���������
        /// </summary>
        public void Open();

        /// <summary>
        /// ����� window ���������
        /// </summary>
        public void Close();

        /// <summary>
        /// ����� window ������� �� �����
        /// </summary>
        public void Destroy();
    }
}