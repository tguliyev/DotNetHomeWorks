using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

namespace UserApplication.Collections
{
    internal class ObservableList<T> : IEnumerable<T>, INotifyCollectionChanged where T : class
    {
        private DbSet<T>? InternalList;
        private string? SearchString;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ObservableList(IEnumerable<T> collection)
        {
            if(collection == null)
                throw new ArgumentNullException(nameof(collection));

            InternalList = collection as DbSet<T>;
        }

        private void RaiseCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        public void Contains(string Searchstring)
        {
            this.SearchString = Searchstring;
            RaiseCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Update() => RaiseCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T>? enumerable = InternalList;

            if(string.IsNullOrEmpty(SearchString))
                return enumerable.GetEnumerator();

            return (from u in InternalList?.AsParallel()
                   where u.ToString().Contains(SearchString)
                   select u).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}