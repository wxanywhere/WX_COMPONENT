using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void SingleDesignConnectionActionEventHandler(object sender, SingleDesignConnectionActionEventArgs e);

    public class SingleDesignConnectionActionEventArgs
    {
        private DesignConnectionInfo _designConnection;

        public DesignConnectionInfo DesignConnection
        {
            get
            {
                return this._designConnection;
            }
        }

        public SingleDesignConnectionActionEventArgs(DesignConnectionInfo designConnection)
            : base()
        {
            this._designConnection = designConnection;
        }
    }
}
