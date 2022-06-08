using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RialDataBase_2._0.ViewModel.ViewModel
{
    internal class ViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberNameAttribute] string propertieName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertieName));
        }
    }
}
