using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4;

namespace seng301_asgn4.src
{
    class BusinessRule
    {
        PaymentFacade payments;
        ProductFacade products;
        CommsFacade comms;
        ProductKind[] productList;
        //int credit;

        public BusinessRule(PaymentFacade payfac, ProductFacade profac, CommsFacade commfac)
        {
            payments = payfac;
            products = profac;
            comms = commfac;

            comms.SelectionPress += new EventHandler(Button);
        }

        public void Configure(ProductKind[] productKinds)
        {
            this.productList = productKinds;
        }

        public void Button(Object sender, EventArgs e)
        {
            int idx = 0;
            //var product = sender;
            foreach (var prod in this.productList)
            {
                if (sender.Equals(prod))
                {
                    idx = Array.IndexOf(this.productList, prod);
                }
            }
            ProductKind product = this.productList[idx];
            if (product.Cost <= this.payments.credit)
            {
                //when the Buton is selected
                //call Product Facade to dispense the product
                products.DispenseProduct(product);
                payments.DispenseChange(product.Cost);
            }
            
        }
    }
}
