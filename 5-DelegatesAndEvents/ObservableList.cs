namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> list = new List<TItem>();
        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => this.list.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => this.list.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                var tmp = this.list[index];
                this.list[index] = value;
                this.ElementChanged?.Invoke(this, value, tmp, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => this.list.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.list).GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            this.list.Add(item);
            this.ElementInserted?.Invoke(this, item, this.list.Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            IList<TItem> tmp = new List<TItem>(this.list);
            this.list.Clear();
            for (var i = 0; i < tmp.Count; i++)
            {
                this.ElementRemoved?.Invoke(this, tmp[i], i);
            }
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => this.list.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => this.list.CopyTo(array, arrayIndex);

            /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
            {
                var index = this.list.IndexOf(item);
                if (index >= 0)
                {
                    var elem = this.list[index];
                    this.list.RemoveAt(index);
                    this.ElementRemoved?.Invoke(this, elem, index);
                    return true;
                }
                return false;
            }

            /// <inheritdoc cref="IList{T}.IndexOf" />
            public int IndexOf(TItem item) => this.list.IndexOf(item);

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            this.list.Insert(index, item);
            this.ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            var tmp = this.list[index];
            this.list.RemoveAt(index);
            this.ElementRemoved?.Invoke(this, tmp, index);
        }

        protected bool Equals(ObservableList<TItem> other)
        {
            return Equals(list, other.list);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ObservableList<TItem>) obj);
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            return (list != null ? list.GetHashCode() : 0);
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            string s = "[";
            foreach (var el in this.list)
            {
                s += el.ToString() + ", ";
            }

            s += "]";
            return s;
        }
    }
}
