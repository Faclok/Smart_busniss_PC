using TMPro;
using UnityEngine;
using ProductData = Assets.ViewModel.Datas.Product;

namespace Assets.View.Body.Product 
{
    public class ProductBehaviourShop : MonoBehaviour
    {

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI _titleField;

        [SerializeField]
        private TextMeshProUGUI _descriptionField;

        [SerializeField]
        private TextMeshProUGUI _countField;

        public ProductData ProductData { get; private set; }

        public int Count => _count;

        private int _count;

        public void UpdateData(ProductData data)
        {
            _titleField.text = data.Name;;
            _descriptionField.text = data.Price;
            ProductData = data;
        }

        public void Click(bool isPlus)
            => _countField.text = (_count += isPlus ? 1 : -1).ToString();

        public void Replace()
        {
            _count = 0;
            _countField.text = "0";
        }
    }
}
