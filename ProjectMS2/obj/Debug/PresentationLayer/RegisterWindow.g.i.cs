﻿#pragma checksum "..\..\..\PresentationLayer\RegisterWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "253CA0200F08DC4972D767B209258F4AD9F0741D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ProjectMS2.PresentationLayer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ProjectMS2.PresentationLayer {
    
    
    /// <summary>
    /// RegisterWindow
    /// </summary>
    public partial class RegisterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\PresentationLayer\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_register;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\PresentationLayer\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBox_usernameReg;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\PresentationLayer\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_userNameReg;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\PresentationLayer\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_gIDReg;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\PresentationLayer\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBox_gIDReg;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\PresentationLayer\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_register;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\PresentationLayer\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_exit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ProjectMS2;component/presentationlayer/registerwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PresentationLayer\RegisterWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lbl_register = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.txtBox_usernameReg = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\..\PresentationLayer\RegisterWindow.xaml"
            this.txtBox_usernameReg.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtbox_usernameReg);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lbl_userNameReg = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lbl_gIDReg = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.txtBox_gIDReg = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\PresentationLayer\RegisterWindow.xaml"
            this.txtBox_gIDReg.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtbox_gIDReg);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_register = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\PresentationLayer\RegisterWindow.xaml"
            this.btn_register.Click += new System.Windows.RoutedEventHandler(this.btn_register_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_exit = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\PresentationLayer\RegisterWindow.xaml"
            this.btn_exit.Click += new System.Windows.RoutedEventHandler(this.btn_exit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

