using Assets.ViewModel;
using Assets.ViewModel.PullDatas;
using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ProductData = Assets.ViewModel.Datas.Product;

namespace Assets.View.Body.Analyst
{
    public class AnalystItemGraphic : MonoBehaviour
    {

        [Header("Links")]
        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private TextMeshProUGUI _description;

        [SerializeField]
        private Image _iconVector;

        [SerializeField]
        private MiniGraphicIcon _graphicLine;

        private ProductData _data;

        private AnalystControllChangerPrice _controllPrice;

        public ProductData Data => _data;

        public string Title => _title.text;

        public string Description => _description.text;

        public async void UpdateData(ProductData data, AnalystControllChangerPrice controllPrice)
        {
            _controllPrice = controllPrice;
            _data = data;
            _title.text = data.Name;
             
            var priceChange = await GetLastChanger(data);
            var isNullPrice = priceChange != null;

            _description.text = data.Price + (isNullPrice ? $" <color={(priceChange.PriceChangerProcent > 0M ? "green" : "red")}>{priceChange.PriceChangerProcent}</color>":string.Empty);
            _iconVector.sprite = isNullPrice ? AnalystControll.GetIcon($"{priceChange["state"]}"): null;
            _graphicLine.UpdateData(await FuncLoadGraphicAsync());
        }

        private async Task<float[]> FuncLoadGraphicAsync()
        {
            var data = await Task.Run(() => ModelDatabase.GetPullLinkObjectAsync<PriceChangePull>(PriceChangePull.TABLE, PriceChangePull.COLUMN_LINK, _data, PriceChangePull.COLUMN_DATE, DateTime.MinValue, DateTime.MaxValue));

            var values = data.Select(o => (float)o.PriceChanger).ToArray();
            var maxValue = values.Length > 0 ? values.Max<float>() : 0f;
            var result = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
                result[i] = values[i] / maxValue;

            return result;
        }

        public void Click()
        {
            _controllPrice.Focus(this);
        }

        public static async Task<PriceChangePull> GetLastChanger(ProductData product)
        {
            var data = await ModelDatabase.GetPullLinkObjectAsync<PriceChangePull>(PriceChangePull.TABLE, PriceChangePull.COLUMN_LINK, product, PriceChangePull.COLUMN_DATE, DateTime.MaxValue, DateTime.MaxValue);

            if (data.Length > 0)
                return data[^1];

            return null;
        }
    }
}
