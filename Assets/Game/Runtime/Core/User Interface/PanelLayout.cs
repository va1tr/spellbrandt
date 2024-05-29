using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public abstract class PanelLayout : MonoBehaviour
    {
        protected readonly List<Panel> panels = new List<Panel>();

        protected Panel currentPanel;

        public virtual void Initialise()
        {

        }

        public void Change<T>() where T : Panel
        {
            for (int i = 0; i < panels.Count; i++)
            {
                var panel = panels[i];

                if (panel is T)
                {
                    if (currentPanel != null)
                    {
                        currentPanel.Hide();
                    }

                    currentPanel = panel;
                    currentPanel.Show();

                    break;
                }
            }
        }

        public bool TryGetPanel<T>(out Panel panel) where T : Panel
        {
            panel = null;

            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i] is T)
                {
                    panel = panels[i];

                    return true;
                }
            }

            return false;
        }

        public virtual void Hide()
        {
            if (currentPanel != null)
            {
                currentPanel.Hide();
            }
        }

        public virtual void Show()
        {
            if (currentPanel != null)
            {
                currentPanel.Show();
            }
        }
    }
}
