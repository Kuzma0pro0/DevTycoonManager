using System;
using System.Collections;
using UnityEngine;

namespace DevIdle.Game.UI
{
    public class ScreenController : MonoBehaviour
    {
        public event Action Closing;
        public event Action Closed;
        public event Func<bool> CanClose;

        public bool CanDestroy = true;
        private bool isClosingCoroutineActive;

        public virtual void Init(params object[] param)
        { }

        public void Close()
        {
            ClosePrivate(false);
        }

        private void ClosePrivate(bool force)
        {
            if (!force && CanClose != null && !CanClose())
            {
                if (!isClosingCoroutineActive)
                {
                    isClosingCoroutineActive = true;
                    StartCoroutine(WaitToClose());
                }

                return;
            }

            Closing?.Invoke();
            Closing = null;
            BeforeClosed();

            if (CanDestroy)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Closed?.Invoke();
            Closed = null;

            OnDestroyScreen();
        }

        protected virtual void OnDestroyScreen()
        { }

        protected virtual void BeforeClosed()
        { }

        private IEnumerator WaitToClose()
        {
            while (!CanClose())
            {
                yield return null;
            }

            ClosePrivate(true);
        }
    }
}