#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

namespace Spellbrandt
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField]
        private Slider[] fillSliders;

        [SerializeField]
        private Slider[] maskSliders;

        public void Refresh(float value, bool absolute = true)
        {
            for (int i = 0; i < fillSliders.Length; i++)
            {
                maskSliders[i].value = value;

                if (!absolute)
                {
                    fillSliders[i].value = Mathf.Max(value - 0.125f, 0f);
                }
                else
                {
                    fillSliders[i].value = value;
                }
            }
        }
    }
}
