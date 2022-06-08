using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RialDataBase_2._0.Infrasrtucture.BaseCommand;

namespace RialDataBase_2._0.Infrasrtucture
{
    internal class LambaCommand : DefaultCommand
    {

        private Action <object> _execute { get; set; }
        private Func <object,bool> _canExecute { get; set; }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) => _execute(parameter);

        public LambaCommand(Action <object> ex, Func <object, bool> can)
        {
            _execute = ex;
            _canExecute = can;
        }
    }
}
