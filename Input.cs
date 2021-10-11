using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SiteVisitStatistics
{
    static class Input
    {
        public static string FileStaticticsInput()
        {
            StreamReader streamReader = new StreamReader("../../../input.txt");

            return streamReader.ReadToEnd();
        }
    }
}
