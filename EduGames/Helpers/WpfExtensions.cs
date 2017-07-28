using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EduGames.Helpers
{
    public static class WpfExtensions
    {
        public static void OnPropertyChanged<T>(this T obj, DependencyProperty prop, Action<T> callback) where T : DependencyObject
        {
            if (callback != null)
            {
                obj.OnPropertyChanged(prop, new EventHandler((o, e) =>
                {
                    callback((T)o);
                }));
            }
        }

        public static void OnPropertyChanged<T>(this T obj, DependencyProperty prop, EventHandler handler) where T : DependencyObject
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(prop, typeof(T));
            descriptor.AddValueChanged(obj, new EventHandler((o, e) =>
            {
                if (handler != null)
                {
                    if (o == null) { handler(o, e); }
                    else
                    {
                        lock (PreventRecursions)
                        {
                            if (IsRecursing(obj, prop)) { return; }
                            SetIsRecursing(obj, prop, true);
                        }

                        try
                        {
                            handler(o, e);
                        }
                        finally
                        {
                            SetIsRecursing(obj, prop, false);
                        }
                    }
                }
            }));
        }

        #region OnPropertyChanged Recursion Prevention

        private static readonly Dictionary<object, List<DependencyProperty>> PreventRecursions = new Dictionary<object, List<DependencyProperty>>();

        private static bool IsRecursing(object obj, DependencyProperty prop)
        {
            lock (PreventRecursions)
            {
                List<DependencyProperty> propList = null;
                if (PreventRecursions.ContainsKey(obj))
                {
                    propList = PreventRecursions[obj];
                }

                return propList == null ? false : propList.Contains(prop);
            }
        }

        private static void SetIsRecursing(object obj, DependencyProperty prop, bool value)
        {
            lock (PreventRecursions)
            {
                List<DependencyProperty> propList = null;
                if (PreventRecursions.ContainsKey(obj))
                {
                    propList = PreventRecursions[obj];
                }

                if (propList == null)
                {
                    if (!value) { return; }

                    propList = PreventRecursions[obj] = new List<DependencyProperty>();
                }

                if (value)
                {
                    if (!propList.Contains(prop))
                    {
                        propList.Add(prop);
                    }
                }
                else
                {
                    while (propList.Contains(prop))
                    {
                        propList.Remove(prop);
                    }

                    if (!propList.Any())
                    {
                        propList = PreventRecursions[obj] = null;
                    }
                }
            }
        }

        #endregion

        public static bool IsInDesignMode(this DependencyObject obj)
        {
            try
            {
                return DesignerProperties.GetIsInDesignMode(obj);
            }
            catch { /* do nothing */ }

            return false;
        }
    }
}