﻿#pragma checksum "C:\code\Command-Centres-Design\WindowManager\WindowManager\UserControls\PanelFrame.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5B7BEEC6C5C592FEB67F01EC846C2C220854B2D3A68FFDF510F9B6F039889DBC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowManager.UserControls
{
    partial class PanelFrame : 
        global::Microsoft.UI.Xaml.Controls.UserControl, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // UserControls\PanelFrame.xaml line 11
                {
                    this.Frame1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Frame>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Frame)this.Frame1).DragStarting += this.Frame1_DragStarting;
                    ((global::Microsoft.UI.Xaml.Controls.Frame)this.Frame1).DragOver += this.Frame_DragOver;
                    ((global::Microsoft.UI.Xaml.Controls.Frame)this.Frame1).Drop += this.Frame_Drop;
                    ((global::Microsoft.UI.Xaml.Controls.Frame)this.Frame1).DropCompleted += this.Frame_DropCompleted;
                    ((global::Microsoft.UI.Xaml.Controls.Frame)this.Frame1).PointerEntered += this.Frame_PointerEntered;
                    ((global::Microsoft.UI.Xaml.Controls.Frame)this.Frame1).PointerExited += this.Frame_PointerExited;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

