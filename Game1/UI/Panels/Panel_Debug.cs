using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class Panel_Debug
    {
        public static void Init(Vector2 size, Game1 game)
        {
            var eventsPanel = new Panel(size, PanelSkin.Simple, Anchor.CenterLeft, new Vector2(-10, 0))
            {
                Visible = false
            };
            UserInterface.Active.AddEntity(eventsPanel);

            // events log (single-time events)
            eventsPanel.AddChild(new Label("Debug Log:", Anchor.AutoCenter));
            var eventsLog = new SelectList(new Vector2(-1, size.Y - 70))
            {
                ItemsScale = 0.7f,
                Locked = true
            };
            eventsPanel.AddChild(eventsLog);

            // whenever events log list size changes, make sure its not too long. if it is, trim it.
            eventsLog.OnListChange = entity =>
            {
                var list = (SelectList) entity;
                if (list.Count > 100)
                {
                    list.RemoveItem(0);
                }
            };
            ListenForEvents(eventsLog);

            // add info button
            var infoBtn = new Button("  Debug", size: new Vector2(150, 50), anchor: Anchor.BottomLeft)
            {
                OnClick = entity => { eventsPanel.Visible = !eventsPanel.Visible; },
                ToolTipText = "Show debug log."
            };
            infoBtn.AddChild(new Icon(IconType.Scroll, Anchor.CenterLeft), true);
            UserInterface.Active.AddEntity(infoBtn);
        }

        /// <summary>
        /// listen to all global events - one timers
        /// </summary>
        /// <param name="eventsLog"></param>
        public static void ListenForEvents(SelectList eventsLog)
        {
            UserInterface.Active.OnClick = entity =>
            {
                eventsLog.AddItem("Click: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnRightClick = entity =>
            {
                eventsLog.AddItem("RightClick: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnMouseDown = entity =>
            {
                eventsLog.AddItem("MouseDown: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnRightMouseDown = entity =>
            {
                eventsLog.AddItem("RightMouseDown: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnMouseEnter = entity =>
            {
                eventsLog.AddItem("MouseEnter: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnMouseLeave = entity =>
            {
                eventsLog.AddItem("MouseLeave: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnMouseReleased = entity =>
            {
                eventsLog.AddItem("MouseReleased: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnMouseWheelScroll = entity =>
            {
                eventsLog.AddItem("Scroll: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnStartDrag = entity =>
            {
                eventsLog.AddItem("StartDrag: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnStopDrag = entity =>
            {
                eventsLog.AddItem("StopDrag: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnFocusChange = entity =>
            {
                eventsLog.AddItem("FocusChange: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
            UserInterface.Active.OnValueChange = entity =>
            {
                if (entity.Parent == eventsLog)
                {
                    return;
                }
                eventsLog.AddItem("ValueChanged: " + entity.GetType().Name);
                eventsLog.scrollToEnd();
            };
        }

    }
}