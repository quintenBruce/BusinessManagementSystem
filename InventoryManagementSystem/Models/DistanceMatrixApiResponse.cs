namespace InventoryManagementSystem.Models
{
    public class DistanceMatrixApiResponse
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string Status { get; set; }

        public class Row
        {
            public Element[] Elements { get; set; }
        }

        public class Element
        {
            public Distance Distance { get; set; }
            public Duration Duration { get; set; }
            public string Status { get; set; }
        }

        public class Distance
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        public class Duration
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }


        public static double GetDistance(DistanceMatrixApiResponse apiResponse)
        {
            int distanceInMeters = apiResponse.rows[0].Elements[0].Distance.Value;
            double distanceInMiles = Math.Round((double)distanceInMeters / (double)1609, 2);
            return distanceInMiles;
        }

        public static string GetDurationAsString(DistanceMatrixApiResponse apiResponse)
        {
            string duration = apiResponse.rows[0].Elements[0].Duration.Text;
            duration = duration.Replace("hours", "Hours").Replace("mins", "Minutes");
            return duration;
        }

        public static int GetDurationInSeconds(DistanceMatrixApiResponse apiResponse)
        {
            int duration = apiResponse.rows[0].Elements[0].Duration.Value;
            return duration;
        }

        public static double GetTripCost(double distanceInMiles, double fuelPrice)
        {
            double fuel = distanceInMiles / (double)17;
            double cost = Math.Round(fuel * fuelPrice, 2);
            return cost;
        }

        public static int GetSuggestedDeliveryFee(double distance)
        {
            int suggestedDeliveryFee = distance <= 5 ? 20 : (int)(20 + (4 * (Math.Round(distance) - 5)));
            

            return suggestedDeliveryFee;
        }


    }

    



    

    

    

}
