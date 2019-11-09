using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Allows CanvasManager to change Visible state of desired Canvas, and any Drawer which implements ICanvasVisibility should not Update it's elements if Visible = false
/// </summary>
//interface ICanvasVisibility
//{
//    bool Visible { get; set; }
//}