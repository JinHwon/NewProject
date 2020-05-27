using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewProject
{
    public class MenuItemVM
    {
        public string Caption { get; set; }
        public string PageCode { get; set; }
        public string Module { get; set; }

        //public Action Action { get; set; }
        public ICommand Command { get; set; }
        public IList<MenuItemVM> ChildItems { get; set; } = new List<MenuItemVM>();

        public MenuItemVM(string caption, string linkedPage, string module, Action action, IList<MenuItemVM> children = null)
        {
            Caption = caption;
            PageCode = linkedPage;
            Module = module;

            //Action = action;
            Command = new DelegateCommand(action);
            ChildItems = children;
        }
    }
}
