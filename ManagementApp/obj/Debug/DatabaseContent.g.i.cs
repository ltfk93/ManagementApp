﻿#pragma checksum "..\..\DatabaseContent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FA68D1FFB8463F4F3002DAD663B3F3C814869593869FF8F28234C3883AC81256"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ManagementApp;
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


namespace ManagementApp {
    
    
    /// <summary>
    /// DatabaseContent
    /// </summary>
    public partial class DatabaseContent : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\DatabaseContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView peopleDatabase;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\DatabaseContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel buttonPanel;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\DatabaseContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button manageBtn;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\DatabaseContent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ManagementApp;component/databasecontent.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DatabaseContent.xaml"
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
            this.peopleDatabase = ((System.Windows.Controls.ListView)(target));
            
            #line 15 "..\..\DatabaseContent.xaml"
            this.peopleDatabase.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.peopleDatabase_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\DatabaseContent.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.listViewColumn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 25 "..\..\DatabaseContent.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.listViewColumn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 30 "..\..\DatabaseContent.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.listViewColumn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 35 "..\..\DatabaseContent.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.listViewColumn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 40 "..\..\DatabaseContent.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.listViewColumn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 45 "..\..\DatabaseContent.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.listViewColumn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.buttonPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.manageBtn = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\DatabaseContent.xaml"
            this.manageBtn.Click += new System.Windows.RoutedEventHandler(this.manageBtn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.exitBtn = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\DatabaseContent.xaml"
            this.exitBtn.Click += new System.Windows.RoutedEventHandler(this.exitBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

