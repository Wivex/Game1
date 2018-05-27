using Game1.Concepts;
using Game1.Objects.Units;
using Game1.UI.GeonUI_Overrides;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public class TabExpeditions : ButtonPanelTabs
    {
        public PanelEmpty NoExpeditionsPanel { get; set; }

        public TabExpeditions(Vector2 size) : base(size)
        {
            Globals.TabExpeditions = this;

            // add special panel if no expeditions present
            NoExpeditionsPanel = new PanelBlack(SizeInternal);
            AddChild(NoExpeditionsPanel);
            NoExpeditionsPanel.AddChild(new Header("NO EXPEDITONS", Anchor.Center));
        }

        public void AddExpeditionTab(Expedition expedition)
        {
            // minor adjustments in size to make sizes even
            var expeditionPanelSize = new Vector2(SelectionPanel.SizeInternal.X - 3,
                (int) SelectionPanel.SizeInternal.Y / 3 - 1);
            var expeditionPanel = new PanelExpeditionOverview(expeditionPanelSize, expedition);
            // Vector2(6, 0) is an offset for bad overlapping of VerticalScrollbar
            var detailsPanel = new PanelExpeditionDetails(AreaPanel.SizeInternal - new Vector2(6, 0), expedition);
            AddTab(expeditionPanel, detailsPanel);
        }

        /// <summary>
        /// Depends on Globals.GameSpeed
        /// </summary>
        public override void UpdateChildrenVisibility()
        {
            // special case if no expeditions
            switch (Globals.ExpeditionsDict.Count)
            {
                case 0:
                    NoExpeditionsPanel.Visible = true;
                    break;
                default:
                    if (NoExpeditionsPanel.Visible) NoExpeditionsPanel.Visible = false;
                    break;
            }

            //foreach (var tab in Tabs)
            //{
            //    tab.ButtonPanel.UpdateChildrenVisibility();
            //}
        }

        /// <summary>
        /// Depends on Globals.GameSpeed
        /// </summary>
        public void UpdateData()
        {
            foreach (var expedition in Globals.ExpeditionsDict)
            {
                expedition.Value.Update();
            }
        }
    }
}