﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Xamarin.Forms;

namespace EPTS.UI.Mobile
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            var property = (MemberExpression)expression.Body;
            OnPropertyChanged(property.Member.Name);
        }

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
