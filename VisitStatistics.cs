using System;
using System.Collections.Generic;
using System.Text;

namespace SiteVisitStatistics
{
    class VisitStatistics
    {
        string[] visitList;
        public string[] VisitList
        { 
            get
            {
                return visitList;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Visit statistics list not set");
                visitList = value;
            }
        }

        Dictionary<string, int> statisticsIPDataVisit = new Dictionary<string, int>();
        Dictionary<string, Dictionary<string, int>> statisticsIPDataPopularDay = new Dictionary<string, Dictionary<string, int>>();
        Dictionary<string, List<TimeSpan>> statisticsIPDataTime = new Dictionary<string, List<TimeSpan>>();

        public VisitStatistics(string[] visitList)
        {
            VisitList = visitList;
            foreach (var elem in visitList)
            {
                string[] input = elem.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (!statisticsIPDataVisit.TryAdd(input[0], 1))
                    statisticsIPDataVisit[input[0]]++;

                if (!statisticsIPDataPopularDay.TryAdd(input[0], new Dictionary<string, int>() { { input[2], 1 } }))
                    if (!statisticsIPDataPopularDay[input[0]].TryAdd(input[2], 1))
                        statisticsIPDataPopularDay[input[0]][input[2]]++;

                if (!statisticsIPDataTime.TryAdd(input[0], new List<TimeSpan>() { TimeSpan.Parse(input[1]) }))
                    statisticsIPDataTime[input[0]].Add(TimeSpan.Parse(input[1]));
            }

            foreach (var elem in statisticsIPDataTime)
                elem.Value.Sort();
        }

        public Dictionary<string, int> StatisticsIPCountVisit()
        {
            return statisticsIPDataVisit;
        }

        public Dictionary<string, string> StatisticsIPMostPopularDay()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            
            foreach (var elem in statisticsIPDataPopularDay)
            {
                int max = 0;
                string maxDayName = "";
                foreach (var elem2 in elem.Value)
                    if (max < elem2.Value)
                    {
                        max = elem2.Value;
                        maxDayName = elem2.Key;
                    }

                keyValuePairs.Add(elem.Key, maxDayName);
            }

            return keyValuePairs;
        }

        public Dictionary<string, (TimeSpan, TimeSpan)> StatisticsIPMostPopularTime()
        {
            Dictionary<string, (TimeSpan, TimeSpan)> keyValuePairs = new Dictionary<string, (TimeSpan, TimeSpan)>();

            TimeSpan maxTime = new TimeSpan(0, 0, 0), tempMaxTime;
            int maxCount, tempMaxCount;

            foreach (var elem in statisticsIPDataTime)
            {
                maxCount = 0;
                for (int i = 0; i < elem.Value.Count; i++)
                {
                    tempMaxTime = elem.Value[i] + TimeSpan.FromHours(1);
                    tempMaxCount = 1;

                    int j = i + 1;
                    while (j < elem.Value.Count && tempMaxTime >= elem.Value[j])
                    {
                        tempMaxCount++;
                        j++;
                    }

                    if (tempMaxCount > maxCount)
                    {
                        maxCount = tempMaxCount;
                        maxTime = tempMaxTime - TimeSpan.FromHours(1);
                    }
                }

                keyValuePairs.Add(elem.Key, (maxTime, maxTime + TimeSpan.FromHours(1)));
            }

            return keyValuePairs;
        }

        public (TimeSpan, TimeSpan) StatisticsSiteMostPopularTime()
        {
            TimeSpan maxTime = new TimeSpan(0, 0, 0), tempMaxTime;
            int maxCount = 0, tempMaxCount;

            List<TimeSpan> timeSpans = new List<TimeSpan>();
            foreach (var elem in statisticsIPDataTime)
                timeSpans.AddRange(elem.Value);

            for (int i = 0; i < timeSpans.Count; i++)
            {
                tempMaxTime = timeSpans[i] + TimeSpan.FromHours(1);
                tempMaxCount = 0;

                int j = i + 1;
                while (j < timeSpans.Count && tempMaxTime > timeSpans[j])
                {
                    tempMaxCount++;
                    j++;
                }

                if (tempMaxCount > maxCount)
                {
                    maxCount = tempMaxCount;
                    maxTime = tempMaxTime - TimeSpan.FromHours(1);
                }
            }

            return (maxTime, maxTime + TimeSpan.FromHours(1));
        }
    }
}
