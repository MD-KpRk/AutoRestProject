using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoRestProject
{
    class PageController
    {
        private static PageController? instance;
        Frame target;

        public static PageController getInstance(Frame target)
        {
            if (instance == null)
                instance = new PageController(target);
            return instance;
        }

        public static PageController? getInstance() => instance;

        PageController(Frame target)
        {
            this.target = target;
        }

        public void Goto(Page page)
        {
            target.Navigate(page);
        }
    }
}
