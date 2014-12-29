using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class GetPackagesCompletedWithinDateRangeExample
    {
        public static string apiToken = "YOUR TOKEN HERE";
        public static string baseUrl = "ENVIRONMENT URL HERE";

        public static void Main (string[] args)
        {
            EslClient client = new EslClient (apiToken, baseUrl);

            // get the packages completed today
            String startDateRange = DateTime.Now.ToString();
            String endDateRange = DateTime.Now.ToString();

            Page<DocumentPackage> resultPage = client.PackageService.GetPackagesUpdatedWithinDateRange(DocumentPackageStatus.COMPLETED, new PageRequest( 1 ), startDateRange, endDateRange);

            Console.WriteLine(resultPage.NumberOfElements);
        }
    }
}