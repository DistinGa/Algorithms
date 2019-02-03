using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class BaseMenuItem
    {
        public string MenuItemText { get; private set; }
        Action MenuAction;

        public BaseMenuItem(string text, Action menuAction)
        {
            MenuItemText = text;
            MenuAction = menuAction;
        }

        /// <summary>
        /// Функционал данного пункта меню
        /// </summary>
        public void DoMenuAction()
        {
            MenuAction();
        }
    }
}
