#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

namespace Spellbrandt
{
    public class MonsterPanel : Panel
    {
        [SerializeField]
        private Text textComponent;

        [SerializeField]
        private Slider[] sliders;

        private Attribute _health;

        public void Bind(AbilitySystemComponent abilitySystemComponent)
        {
            textComponent.text = abilitySystemComponent.name;

            _health = abilitySystemComponent.GetAttributeByType(AttributeType.Health);

            SetSliderCount();
            Refresh();
        }

        public override void Show()
        {
            base.Show();

            if (_health != null)
            {
                _health.onAttributeChanged += Refresh;
            }
        }

        private void SetSliderCount()
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                if (_health.MaxValue > i)
                {
                    sliders[i].gameObject.SetActive(true);
                }
                else
                {
                    sliders[i].gameObject.SetActive(false);
                }
            }
        }

        private void Refresh()
        {
            for (int i = 0; i < _health.MaxValue; i++)
            {
                if (_health.Value > i)
                {
                    sliders[i].value = 1;
                }
                else
                {
                    sliders[i].value = 0;
                }
            }
        }

        public void Lock()
        {
            textComponent.text = string.Empty;

            for (int i = 0; i < sliders.Length; i++)
            {
                sliders[i].value = 0;
            }

            if (_health != null)
            {
                _health.onAttributeChanged -= Refresh;
            }
        }

        public override void Hide()
        {
            base.Hide();

            if (_health != null)
            {
                _health.onAttributeChanged -= Refresh;
            }
        }
    }
}
