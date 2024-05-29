using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public abstract class Panel : MonoBehaviour
    {
        public virtual void Initialise()
        {

        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
