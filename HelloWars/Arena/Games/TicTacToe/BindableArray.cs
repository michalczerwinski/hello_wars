using System;
using System.ComponentModel;
using System.Windows.Data;

namespace Arena.Games.TicTacToe
{
    /// <summary>
    /// This class is a bindable encapsulation of a 2D array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableArray<T> : INotifyPropertyChanged
    {
        public int XSize;
        public int YSize;
        public event PropertyChangedEventHandler PropertyChanged;
        readonly T[,] _data;

        public T this[int c1, int c2]
        {
            get { return _data[c1, c2]; }
            set
            {
                _data[c1, c2] = value;
                Notify(Binding.IndexerName);
            }
        }

        public T this[string index]
        {
            get
            {
                int c1, c2;
                SplitIndex(index, out c1, out c2);
                return _data[c1, c2];
            }
            set
            {
                int c1, c2;
                SplitIndex(index, out c1, out c2);
                _data[c1, c2] = value;
                Notify(Binding.IndexerName);
            }
        }

        public BindableArray(int size1, int size2)
        {
            _data = new T[size1, size2];
            XSize = size1;
            YSize = size2;
        }

        private void Notify(string property)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public string GetStringIndex(int c1, int c2)
        {
            return c1 + "-" + c2;
        }

        private void SplitIndex(string index, out int c1, out int c2)
        {
            var parts = index.Split('-');
            if (parts.Length != 2)
            {
                throw new ArgumentException("The provided index is not valid");
            }

            c1 = int.Parse(parts[0]);
            c2 = int.Parse(parts[1]);
        }

        public static implicit operator T[,](BindableArray<T> a)
        {
            return a._data;
        }
    }
}
