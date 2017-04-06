using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend4.Hardware;
using Frontend4;

namespace seng301_asgn4.src
{
    class PaymentFacade
    {
        int availableFunds;
        public Cents credit;
        HardwareFacade hardware;
        Dictionary<Cents, int> coinKindToCoinRackIndex;

        public PaymentFacade(HardwareFacade hardware)
        {
            this.hardware = hardware;
            availableFunds = 0;
            credit = new Cents(0);

            this.hardware.CoinSlot.CoinAccepted += new EventHandler<CoinEventArgs>(InsertCoin);

            this.coinKindToCoinRackIndex = new Dictionary<Cents, int>();
            for (int i = 0; i < this.hardware.CoinRacks.Length; i++)
            {
                this.coinKindToCoinRackIndex[this.hardware.GetCoinKindForCoinRack(i)] = i;
            }
        }

        public void InsertCoin(Object sender, CoinEventArgs e)
        {
            credit += e.Coin.Value;
        }

        public void DispenseChange(Cents cost)
        {   //change method by Tony Tang, from assignment 2
            availableFunds = credit.Value;
            int changeNeeded = availableFunds - cost.Value;
            //make a dictionary
            while (changeNeeded > 0)
            {
                var coinRacksWithMoney = this.coinKindToCoinRackIndex.Where(ck =>ck.Key.Value <= changeNeeded && this.hardware.CoinRacks[ck.Value].Count > 0).OrderByDescending(ck => ck.Key.Value);
                
                if (coinRacksWithMoney.Count() == 0)
                {
                    availableFunds= changeNeeded; // this is what's left as available funds
                }

                var biggestCoinRackCoinKind = coinRacksWithMoney.First().Key;
                var biggestCoinRackIndex = coinRacksWithMoney.First().Value;
                var biggestCoinRack = this.hardware.CoinRacks[biggestCoinRackIndex];

                changeNeeded = changeNeeded - biggestCoinRackCoinKind.Value;
                biggestCoinRack.ReleaseCoin();
            }
            credit= new Cents(availableFunds);
        }
    }
}
