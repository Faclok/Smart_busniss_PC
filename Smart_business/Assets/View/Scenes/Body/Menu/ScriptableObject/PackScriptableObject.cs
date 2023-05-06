using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Body.Menu
{

    /// <summary>
    /// ����� ���� ������� ��� ��������
    /// </summary>
    [CreateAssetMenu(fileName = "Pack panels", menuName = "ScriptableObject/Pack panels")]
    public class PackScriptableObject : ScriptableObject
    {
        /// <summary>
        /// ������ �������,
        /// �� ������������ ��������, �.�. ������� ����� �������, �������� ��������
        /// </summary>
        public ItemInPackScriptableObject[] itemsInPackScriptableObject;
    }
}
