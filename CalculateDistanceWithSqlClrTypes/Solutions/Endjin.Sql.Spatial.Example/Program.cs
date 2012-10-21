namespace Endjin.Sql.Spatial.Example
{
    #region Using Directives

    using System;
    using System.Data.SqlTypes;

    using Microsoft.SqlServer.Types;

    #endregion 

    /// <summary>
    /// Sample app that uses SQL Spatial Types to calculate the distance between two locations.
    /// </summary>
    /// <remarks>
    /// If you want to find our a specific lon / lat use http://www.doogal.co.uk/LatLong.php
    /// </remarks>
    public class Program
    {
        public static void Main(string[] args)
        {
            const int WorldGeodeticSystemId = 4326;

            var firstlocationLonLat = new Tuple<string, string>("-0.081389", "51.502195");
            var secondlocationLonLat = new Tuple<string, string>("-0.185348", "51.410933");

            var firstLocationAsPoint = string.Format("POINT({0} {1})", firstlocationLonLat.Item1, firstlocationLonLat.Item2);
            var secondLocationAsPoint = string.Format("POINT({0} {1})", secondlocationLonLat.Item1, secondlocationLonLat.Item2);

            var firstLocation = SqlGeography.STGeomFromText(new SqlChars(firstLocationAsPoint), WorldGeodeticSystemId);
            var secondLocation = SqlGeography.STGeomFromText(new SqlChars(secondLocationAsPoint), WorldGeodeticSystemId);

            var distance = firstLocation.STDistance(secondLocation);

            Console.WriteLine("First Location is " + MetersToMiles((double)distance).ToString("0") + " miles from Second Location");
            Console.ReadKey();
        }

        public static double MetersToMiles(double? meters)
        {
            if (meters == null)
            {
                return 0F;
            }

            return meters.Value * 0.000621371192;
        }
    }
}
