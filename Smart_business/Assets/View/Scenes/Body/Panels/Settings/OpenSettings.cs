using Assets.View.Body.Menu;
using UnityEngine;

namespace Assets.View.Body.Setting
{

    public class OpenSettings : MonoBehaviour
    {
        private bool isoOpen = false;

        public void Open()
        {
            if (isoOpen)
            {
                DoubleOpen();
                return;
            }

            PanelContent.OnPanel += Close;

            PanelContent.DisableCurrent();

            isoOpen = true;
            transform.SetAsLastSibling();
        }

        private void DoubleOpen()
        {
            isoOpen = false;
            PanelContent.EnableCurrent();
            transform.SetSiblingIndex(transform.parent.childCount - 2);
            Close();
        }

        public void Close()
        {
            isoOpen = false;

            PanelContent.OnPanel -= Close;
        }
    }
}
