﻿#pragma checksum "C:\code\Command-Centres-Design\WindowManager\WindowManager\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1E0B22E3AD679C5746DCC7E0985C01806A29F8A27703FC4257537A8D076C73D7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowManager
{
    partial class MainWindow : 
        global::Microsoft.UI.Xaml.Window, 
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
            case 2: // MainWindow.xaml line 14
                {
                    this.MainMenuBar = global::WinRT.CastExtensions.As<global::WindowManager.UserControls.MenuBar>(target);
                }
                break;
            case 3: // MainWindow.xaml line 29
                {
                    this.Panel1 = global::WinRT.CastExtensions.As<global::WindowManager.UserControls.WebPanel>(target);
                }
                break;
            case 4: // MainWindow.xaml line 31
                {
                    this.Panel2 = global::WinRT.CastExtensions.As<global::WindowManager.UserControls.WebPanel>(target);
                }
                break;
            case 5: // MainWindow.xaml line 32
                {
                    this.Panel3 = global::WinRT.CastExtensions.As<global::WindowManager.UserControls.WebPanel>(target);
                }
                break;
            case 6: // MainWindow.xaml line 33
                {
                    this.Panel4 = global::WinRT.CastExtensions.As<global::WindowManager.UserControls.WebPanel>(target);
                }
                break;
            case 7: // MainWindow.xaml line 35
                {
                    this.Panel5 = global::WinRT.CastExtensions.As<global::WindowManager.UserControls.WebPanel>(target);
                }
                break;
            case 8: // MainWindow.xaml line 36
                {
                    this.Panel6 = global::WinRT.CastExtensions.As<global::WindowManager.UserControls.WebPanel>(target);
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

