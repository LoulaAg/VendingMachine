using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4.Hardware;
using Frontend4;

namespace seng301_asgn4.src
{

    class CommsFacade
        //this deals with the Selection Button only (for now)
    {
        ProductRack toDispense;
        HardwareFacade hardwareFacade;
        public event EventHandler SelectionPress;

        public CommsFacade(HardwareFacade hardware)
        {
            hardwareFacade = hardware;
            foreach(var button in this.hardwareFacade.SelectionButtons)
            {
                button.Pressed += new EventHandler(ButtonPress);
            }
        }
        public void ButtonPress(Object sender, EventArgs e)
        {
            int idx = 0;
            //find which button was pressed
            foreach(var button in hardwareFacade.SelectionButtons)
            {
                if (sender.Equals(button))
                {
                    idx = Array.IndexOf(this.hardwareFacade.SelectionButtons, button);
                }
            }
            //find the corresponding product
            ProductKind product = this.hardwareFacade.ProductKinds[idx];
            this.SelectionPress(product, new EventArgs());
        }



    }
}
