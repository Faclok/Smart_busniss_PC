using System;
using System.Collections;
using UnityEngine;

namespace Assets.View.Body.FullScreen.OptionsWindow
{
    public abstract class BodyOptionBlock : MonoBehaviour
    {
        [HideInInspector]
        public new Transform transform;

        public static event Action<BodyOptionBlock> OnEnable;

        public static event Action<BodyOptionBlock> OnDisable;

        private protected static BodyOptionBlock _currentBody;

        public virtual void Awake()
        {
            transform = base.transform;
        }

        public virtual void Focus()
        {
            if (_currentBody == this)
                return;

            OnDisable?.Invoke(_currentBody);
            OnEnable?.Invoke(_currentBody = this);
            transform.SetAsLastSibling();
        }

        private void OnDestroy()
        {
            OnEnable = null;
            OnDisable = null;
        }
    }
}