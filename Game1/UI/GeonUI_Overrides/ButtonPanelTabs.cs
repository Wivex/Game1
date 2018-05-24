using System.Collections.Generic;
using Game1.UI;
using Microsoft.Xna.Framework;

namespace GeonBit.UI.Entities
{
    /// <summary>
    /// A graphical panel or form you can create and add entities to.
    /// Used to group together entities with common logic.
    /// </summary>
    public class ButtonPanelTabs : PanelEmpty
    {
        /// <summary>Contains the button and panel of a single tab in the PanelTabs.</summary>
        public class Tab
        {
            public ButtonPanel ButtonPanel { get; }
            /// <summary>The tab panel.</summary>
            public PanelEmpty TabPanel { get; }

            /// <summary>
            /// Create the new tab type.
            /// </summary>
            /// <param name="tabPanel">Tab panel.</param>
            /// <param name="buttonPanel">Tab button.</param>
            public Tab(ButtonPanel buttonPanel, PanelEmpty tabPanel)
            {
                ButtonPanel = buttonPanel;
                TabPanel = tabPanel;
            }
        }

        /// <summary>List of tabs data currently in panel tabs.</summary>
        public List<Tab> Tabs { get; } = new List<Tab>();
        /// <summary>A special internal panel to hold all the tab buttons.</summary>
        public PanelEmpty SelectionPanel { get; }
        /// <summary>A special internal panel to hold all the panels.</summary>
        public PanelEmpty AreaPanel { get; }
        /// <summary>Currently active tab.</summary>
        public Tab ActiveTab { get; set; }

        /// <summary>
        /// Create the panel tabs.
        /// </summary>
        public ButtonPanelTabs(Vector2 size) : base (size)
        {
            SelectionPanel =
                new PanelEmpty(new Vector2((int)size.X / 3, size.Y),
                    Anchor.CenterLeft)
                {
                    PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll
                };
            AreaPanel =
                new PanelEmpty(new Vector2(size.X - SelectionPanel.Size.X, SelectionPanel.Size.Y),
                    Anchor.CenterRight);
            AddChild(SelectionPanel);
            AddChild(AreaPanel);
        }

        /// <summary>
        /// Add a new tab to the panel tabs.
        /// </summary>
        public Tab AddTab(ButtonPanel buttonPanel, PanelEmpty tabPanel)
        {
            var tab = new Tab(buttonPanel, tabPanel);
            Tabs.Add(tab);

            // initialize first tab as default active
            if (ActiveTab == null)
            {
                ActiveTab = Tabs[0];
                buttonPanel.Check();
            }

            // hide all but active panels by default
            if (tabPanel != ActiveTab.TabPanel)
                tabPanel.Visible = false;

            // attach event handler to the selectable panel
            buttonPanel.OnClick = entity =>
            {
                // get self as a panel
                var clickedPanel = (ButtonPanel)entity;
                // do nothing if clicked on active panel
                if (ActiveTab.ButtonPanel == clickedPanel) return;
                // hide previous active tabPanel and uncheck its button
                ActiveTab.ButtonPanel.Check();
                ActiveTab.TabPanel.Visible = false;
                // set new one
                ActiveTab = tab;
                ActiveTab.ButtonPanel.Check();
                ActiveTab.TabPanel.Visible = true;
            };

            // add buttonPanel and tabPanel to their corresponding containers
            SelectionPanel.AddChild(buttonPanel);
            AreaPanel.AddChild(tabPanel);

            return tab;
        }
    }
}
