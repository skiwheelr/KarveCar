﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Syncfusion.Windows.Controls;
using Syncfusion.Windows.Tools.Controls;
using Prism.Regions;
using System.Windows.Media;
using System.Reflection;

namespace KarveCommon.Generic
{
    public class CloseTabAction : TriggerAction<Button>
    {
        protected override void Invoke(object parameter)
        {
            var args = parameter as RoutedEventArgs;
            if (args == null)
                return;

            var tabItem = FindParent<TabItemExt>(args.OriginalSource as DependencyObject);
            if (tabItem == null)
                return;

            var tabControl = FindParent<TabControlExt>(tabItem);
            if (tabControl == null)
                return;

            IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
            if (region == null)
                return;

            RemoveItemFromRegion(tabItem.Content, region);
        }
        public void Execute(object tabItemValue)
        {
            var tabItem = FindParent<TabItem>(tabItemValue as DependencyObject);
            if (tabItem == null)
                return;

            var tabControl = FindParent<TabControl>(tabItem);
            if (tabControl == null)
                return;

            IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
            if (region == null)
                return;

            RemoveItemFromRegion(tabItem.Content, region);
        }

        void RemoveItemFromRegion(object item, IRegion region)
        {
            var navigationContext = new NavigationContext(region.NavigationService, null);
            if (CanRemove(item, navigationContext))
            {
                InvokeOnNavigatedFrom(item, navigationContext);
                region.Remove(item);
            }
        }
        void InvokeOnNavigatedFrom(object item, NavigationContext navigationContext)
        {
            var navigationAwareItem = item as INavigationAware;
            if (navigationAwareItem != null)
                navigationAwareItem.OnNavigatedFrom(navigationContext);

            var frameworkElement = item as FrameworkElement;
            if (frameworkElement != null)
            {
                INavigationAware navigationAwareDataContext = frameworkElement.DataContext as INavigationAware;
                if (navigationAwareDataContext != null)
                {
                    navigationAwareDataContext.OnNavigatedFrom(navigationContext);

                }
                if (frameworkElement != null)
                {
                    var viewModel = frameworkElement.DataContext;
                    Type viewModelType = viewModel.GetType();
                    MethodInfo methodInfo = viewModelType.GetMethod("DisposeEvents");
                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(viewModel, null);
                    }
                }
            }
        }
       
        bool CanRemove(object item, NavigationContext navigationContext)
        {
            bool canRemove = true;

            var confirmRequestItem = item as IConfirmNavigationRequest;
            if (confirmRequestItem != null)
            {
                confirmRequestItem.ConfirmNavigationRequest(navigationContext, result =>
                {
                    canRemove = result;
                });
            }

            var frameworkElement = item as FrameworkElement;
            if (frameworkElement != null && canRemove)
            {
                IConfirmNavigationRequest confirmRequestDataContext = frameworkElement.DataContext as IConfirmNavigationRequest;
                if (confirmRequestDataContext != null)
                {
                    confirmRequestDataContext.ConfirmNavigationRequest(navigationContext, result =>
                    {
                        canRemove = result;
                    });
                }
            }

            return canRemove;
        }

        static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            var parent = parentObject as T;
            if (parent != null)
                return parent;

            return FindParent<T>(parentObject);
        }
    }
}
