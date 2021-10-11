using System;
using System.IO;

namespace SiteVisitStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VisitStatistics visitStatistics = new VisitStatistics(Input.FileStaticticsInput().Split("\n", StringSplitOptions.RemoveEmptyEntries));

                Console.WriteLine("NUMBER OF VISITS");
                var IPVisitCount = visitStatistics.StatisticsIPCountVisit();
                foreach (var elem in IPVisitCount)
                    Console.WriteLine($"IP adress: {elem.Key}    Count: {elem.Value}");

                Console.WriteLine("\nMOST POPULAR DAY");
                var IPMostPopularDay = visitStatistics.StatisticsIPMostPopularDay();
                foreach (var elem in IPMostPopularDay)
                    Console.WriteLine($"IP adress: {elem.Key}    Day: {elem.Value}");

                Console.WriteLine("\nMOST POPYLAR TIME");
                var IPMostPopularTime = visitStatistics.StatisticsIPMostPopularTime();
                foreach (var elem in IPMostPopularTime)
                    Console.WriteLine($"IP adress: {elem.Key}    Time:  from {elem.Value.Item1} to {elem.Value.Item2}");

                Console.WriteLine("\nMOST POPYLAR TIME ON SITE");
                var siteMostPopularTime = visitStatistics.StatisticsSiteMostPopularTime();
                Console.WriteLine($"From {siteMostPopularTime.Item1} to {siteMostPopularTime.Item2}");
            }
            catch(FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
