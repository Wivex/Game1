using System.Collections.Generic;
using Game1.UI;

namespace GeonBit.UI.Entities
{
    /// <summary>
    /// A graphical panel or form you can create and add entities to.
    /// Used to group together entities with common logic.
    /// </summary>
    public class PanelTabs_Button : Entity
    {
        /// <summary>Contains the button and panel of a single tab in the PanelTabs.</summary>
        public class Tab
        {
            public Button Button { get; }
            /// <summary>The tab panel.</summary>
            public PanelEmpty TabPanel { get; }

            /// <summary>
            /// Create the new tab type.
            /// </summary>
            /// <param name="tabPanel">Tab panel.</param>
            /// <param name="button">Tab button.</param>
            public Tab(Button button, PanelEmpty tabPanel)
            {
                Button = button;
                TabPanel = tabPanel;
            }
        }

        /// <summary>List of tabs data currently in panel tabs.</summary>
        public List<Tab> Tabs { get; } = new List<Tab>();
        /// <summary>A special internal panel to hold all the tab buttons.</summary>
        public PanelEmpty TabSelectionPanel { get; }
        /// <summary>A special internal panel to hold all the panels.</summary>
        public PanelEmpty TabAreaPanel { get; }
        /// <summary>Currently active tab.</summary>
        public Tab ActiveTab { get; set; }

        /// <summary>
        /// Create the panel tabs.
        /// </summary>
        public PanelTabs_Button(PanelEmpty tabSelectionPanel, PanelEmpty tabAreaPanel)
        {
            TabSelectionPanel = tabSelectionPanel;
            TabAreaPanel = tabAreaPanel;
            AddChild(TabSelectionPanel);
            AddChild(TabAreaPanel);
        }

        /// <summary>
        /// Add a new tab to the panel tabs.
        /// </summary>
        public Tab AddTab(Button button, PanelEmpty tabPanel)
        {
            var tab = new Tab(button, tabPanel);
            Tabs.Add(tab);

            // initialize first tab as default active
            if (ActiveTab == null)
            {
                ActiveTab = Tabs[0];
                button.Checked = true;
            }

            // hide all but active panels by default
            if (tabPanel != ActiveTab.TabPanel)
                tabPanel.Visible = false;

            // attach event handler to the button
            button.OnClick = entity =>
            {
                // get self as a button
                var clickedButton = (Button)entity;
                // do nothing if clicked on active button
                if (ActiveTab.Button == clickedButton) return;
                // hide previous active tabPanel and uncheck its button
                ActiveTab.TabPanel.Visible = false;
                ActiveTab.Button.Checked = false;
                // set new one
                ActiveTab = tab;
                // show new active tabPanel and check its button
                ActiveTab.TabPanel.Visible = true;
                ActiveTab.Button.Checked = true;
            };

            // add button and panel to their corresponding containers
            TabSelectionPanel.AddChild(button);
            TabAreaPanel.AddChild(tabPanel);

            return tab;
        }
    }
}