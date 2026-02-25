using System.Collections.Generic;

namespace BikeRentalApp
{
    public class BikeUtility
    {
        public void AddBikeDetails(string model,string brand,int pricePerDay)
        {
            int key=Program.bikeDetails.Count+1;
            Bike b=new Bike();
            b.Model=model;
            b.Brand=brand;
            b.PricePerDay=pricePerDay;
            Program.bikeDetails.Add(key,b);
        }

        public SortedDictionary<string,List<Bike>> GroupBikesByBrand()
        {
            SortedDictionary<string,List<Bike>> result=
                new SortedDictionary<string,List<Bike>>();
            foreach(var bike in Program.bikeDetails.Values)
            {
                if(!result.ContainsKey(bike.Brand))
                    result.Add(bike.Brand,new List<Bike>());


                result[bike.Brand].Add(bike);
            }
             return result;
        }
    }
}
