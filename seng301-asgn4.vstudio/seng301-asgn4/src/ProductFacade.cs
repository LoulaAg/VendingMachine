using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4.Hardware;
using Frontend4;

namespace seng301_asgn4.src
{
    class ProductFacade
    {
        HardwareFacade hardware; 

        public ProductFacade(HardwareFacade hardware)
        {
            this.hardware = hardware;

        }

        public void DispenseProduct(ProductKind product)
        {
            int idx;
            idx = Array.IndexOf(this.hardware.ProductKinds, product);
            //dispenseProduct() from ProductRack
            ProductRack rack = this.hardware.ProductRacks[idx];
            rack.DispenseProduct();
        }

        public void LoadProduct(int[] products)
        {
            hardware.LoadProducts(products);
        }

        public void LoadCoins(int[] coins)
        {
            hardware.LoadCoins(coins);
        }
    }
}
