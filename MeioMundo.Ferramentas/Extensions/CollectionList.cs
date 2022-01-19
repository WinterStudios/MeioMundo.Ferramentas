using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundo.Ferramentas.Extensions
{
    public class CollectionList<T> : INotifyPropertyChanged
    {
        #region 
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
#endregion
        private const int _defaultCapacity = 4;
        private T[] _items;
        [ContractPublicPropertyName("Count")]
        private int _size;
        static readonly T[] _emptyArray = new T[0];     

        
        public CollectionList()
        {
            _items = _emptyArray;
        }
        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)_size)
                    throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if ((uint)index >= (uint)_size)
                    throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }
        


        public void Add(T item)
        {
            if (_size == _items.Length)
                EnsureCapacity(_size + 1);
            _items[_size++] = item;
            //OnPropertyChanged();
        }
        public void AddRange(T[] items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
            OnPropertyChanged();
        }

        private void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > Array.MaxLength)
                    newCapacity = Array.MaxLength;
                //Capacity = newCapacity;
            }              
        }
    }
}
