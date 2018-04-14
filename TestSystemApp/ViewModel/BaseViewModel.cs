using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemApp.ViewModel
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var e = PropertyChanged;
            if (e != null)
                e(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaiseEx(params string[] propertyNames)
        {
            if (PropertyChanged != null)
                for (int i = 0; i < propertyNames.Length; i++)
                    RaisePropertyChanged(propertyNames[i]);
        }

        [MethodImpl(MethodImplOptions.NoInlining)] // to preserve method call 
        protected static void Raise() { }

        #endregion

        public virtual void LoadData()
        {
            
        }
    }
}
