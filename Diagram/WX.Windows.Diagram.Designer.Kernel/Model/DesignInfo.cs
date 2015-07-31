using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public class DesignInfo : ModelBase
    {
        private String _Backgroud;
        public String Backgroud
        {
            get { return _Backgroud; }
            set
            {
                if (_Backgroud != value)
                {
                    _Backgroud = value;
                    this.OnPropertyChanged("Backgroud");
                }
            }
        }

        private String _Foreground;
        public String Foreground
        {
            get { return _Foreground; }
            set
            {
                if (_Foreground != value)
                {
                    _Foreground = value;
                    this.OnPropertyChanged("Foreground");
                }
            }
        }

        private Decimal _FontSize;
        //[ItemsSource(typeof(FontSizeItemsSource))]
        public Decimal FontSize
        {
            get { return _FontSize; }
            set
            {
                if (_FontSize != value)
                {
                    _FontSize = value;
                    this.OnPropertyChanged("FontSize");
                }
            }
        }

        private Decimal _Bold;
        public Decimal Bold
        {
            get { return _Bold; }
            set
            {
                if (_Bold != value)
                {
                    _Bold = value;
                    this.OnPropertyChanged("Bold");
                }
            }
        }

        private Decimal _Italic;
        public Decimal Italic
        {
            get { return _Italic; }
            set
            {
                if (_Italic != value)
                {
                    _Italic = value;
                    this.OnPropertyChanged("Italic");
                }
            }
        }

        private Decimal _HorizontalAlignment;
        public Decimal HorizontalAlignment
        {
            get { return _HorizontalAlignment; }
            set
            {
                if (_HorizontalAlignment != value)
                {
                    _HorizontalAlignment = value;
                    this.OnPropertyChanged("HorizontalAlignment");
                }
            }
        }

        private Decimal _VerticalAlignment;
        public Decimal VerticalAlignment
        {
            get { return _VerticalAlignment; }
            set
            {
                if (_VerticalAlignment != value)
                {
                    _VerticalAlignment = value;
                    this.OnPropertyChanged("VerticalAlignment");
                }
            }
        }
    }
}
