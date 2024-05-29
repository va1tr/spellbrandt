#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

namespace Spellbrandt
{
    public class AbilityPanel : Panel
    {
        [SerializeField]
        private Text textComponent;

        [SerializeField]
        private Slider[] sliders;

        private Attribute _cost;
        private Attribute _cooldown;

        public void Bind(AbilitySpec spec)
        {
            textComponent.text = spec.Asset.Name;

            _cost = spec.Cost;
            _cooldown = spec.Cooldown;

            SetNumberOfSliderViews();
        }

        public override void Show()
        {
            base.Show();

            if (_cost != null && _cooldown != null)
            {
                _cost.onAttributeChanged += Refresh;
                _cooldown.onAttributeChanged += Refresh;
            }
        }

        private void SetNumberOfSliderViews()
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                var view = sliders[i];

                if (_cost.MaxValue > i)
                {
                    view.gameObject.SetActive(true);
                    view.value = 1f;
                }
                else
                {
                    view.value = 0f;
                    view.gameObject.SetActive(false);
                }
            }
        }

        private void Refresh()
        {
            int count = Mathf.RoundToInt(_cost.MaxValue - 1);
            int value = Mathf.RoundToInt(_cost.Value);

            for (int i = count; i >= 0; i--)
            {
                if (value == i)
                {
                    float normalisedValue = _cooldown.Value / _cooldown.MaxValue;
                    sliders[i].value = normalisedValue;
                }
                else if (value > i)
                {
                    sliders[i].value = 1f;
                }
                else
                {
                    sliders[i].value = 0f;
                }
            }
        }

        public override void Hide()
        {
            base.Hide();

            if (_cost != null && _cooldown != null)
            {
                _cost.onAttributeChanged -= Refresh;
                _cooldown.onAttributeChanged -= Refresh;
            }
        }
    }
}
