using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BTDB.ODBLayer;
using Gmc.Cloud.Prodaas.Db.Enums;
using Gmc.Cloud.Prodaas.Db.Indexes;
using NLog;
using NUnit.Framework;
using Prodaas.Test.Common;
using LogFactory = Gmc.Cloud.Infrastructure.Core.Logs.LogFactory;

namespace Prodaas.Test.Db
{
    [TestFixture]
    public class BatchJobDateTest : ProdaasTestBaseV2
    {
        int size = 1_000;


        [Test]
        public void Test1()
        {
            Stopwatch stopwatch = new Stopwatch();

            string outpu = "";

/*scan by companyId and batchId first record */
            stopwatch.Start();
            IEnumerable<BatchJobDate> scanById = BatchJobDate.Table.ScanById(
                Constraint.Unsigned.Exact(266128073),
                Constraint.DateTime.Any,
                Constraint.Guid.Exact(new Guid("14a018b1-d909-406d-ad77-0feebb15c2b8"))
            );
            outpu = outpu + "scan by companyId and batchId first record: " + stopwatch.Elapsed + "\r\n";
            int i = scanById.Count();
            outpu = outpu + "scan by companyId and batchId first record Count: " + i + "" + stopwatch.Elapsed + "\r\n";
            var batchJobDates = scanById.ToList();
            outpu = outpu + "scan by companyId and batchId first record ToList: " + stopwatch.Elapsed + "\r\n";
            var count = batchJobDates.Count;
            outpu = outpu + "scan by companyId and batchId first record ToList.Count: " + count + "" +
                    stopwatch.Elapsed + "\r\n";


/*scan by companyId.Any and non existent? batchId */
            stopwatch.Restart();
            IEnumerable<BatchJobDate> scanByIdNonexist = BatchJobDate.Table.ScanById(
                Constraint.Unsigned.Any,
                Constraint.DateTime.Any,
                Constraint.Guid.Exact(new Guid("607e4b8e-af62-41c0-b02d-2e8832135064"))
            );
            outpu = outpu + "scan by companyId.Any and non existent batchId : " + stopwatch.Elapsed + "\r\n";
            int count1 = scanByIdNonexist.Count();
            outpu = outpu + "scan by companyId.Any and non existent batchId Count: " + count1 + "" + stopwatch.Elapsed +
                    "\r\n";
            var batchJobDatesNonexist = scanByIdNonexist.ToList();
            outpu = outpu + "scan by companyId.Any and non existent batchId ToList: " + stopwatch.Elapsed + "\r\n";
            var countNonexist = batchJobDatesNonexist.Count;
            outpu = outpu + "scan by companyId.Any and non existent ToList.batchId Count: " + countNonexist + "" +
                    stopwatch.Elapsed + "\r\n";


/*scan by scenarioId nullable guid single value */
            stopwatch.Restart();
            IEnumerable<BatchJobDate> scanByScenarioId = BatchJobDate.Table.ScanById(
                Constraint.Unsigned.Any,
                Constraint.DateTime.Any,
                ConstraintGuidAny.Any,
                Constraint.Enum<ProductionBatchState>.Any,
                Constraint.Exact<Guid?>(new Guid("607e4b8e-af62-41c0-b02d-2e8832135064"))
            );
            outpu = outpu + "scan by scenarioId nullable guid single value: " + stopwatch.Elapsed + "\r\n";
            int count2 = scanByScenarioId.Count();
            outpu = outpu + "scan by scenarioId nullable guid single value Count: " + count2 + "" + stopwatch.Elapsed +
                    "\r\n";
            var batchJobDatesSenId = scanByScenarioId.ToList();
            outpu = outpu + "scan by scenarioId nullable guid single value ToList: " + stopwatch.Elapsed + "\r\n";
            var countSenId = batchJobDatesSenId.Count;
            outpu = outpu + "scan by scenarioId nullable guid single value ToList.Count: " + countSenId + "" +
                    stopwatch.Elapsed + "\r\n";


/*scan by time range first 30 days */
            stopwatch.Restart();
            IEnumerable<BatchJobDate> scanByBatchIdTimeRange = BatchJobDate.Table.ScanById(
                Constraint.Unsigned.Any,
                Constraint.DateTime.Range(DateTime.Today.AddDays(-365), DateTime.Today.AddDays(-335)),
                ConstraintGuidAny.Any
            );

            outpu = outpu + "scan by time range first 30 days: " + stopwatch.Elapsed + "\r\n";
            int count3 = scanByBatchIdTimeRange.Count();
            outpu = outpu + "scan by time range first 30 days Count: " + count3 + "" + stopwatch.Elapsed + "\r\n";
            var batchJobDatesBatchId = scanByBatchIdTimeRange.ToList();
            outpu = outpu + "scan by time range first 30 days ToList: " + stopwatch.Elapsed + "\r\n";
            var countBatchId = batchJobDatesBatchId.Count;
            outpu = outpu + "scan by time range first 30 days ToList.Count: " + countBatchId + "" + stopwatch.Elapsed +
                    "\r\n";

            stopwatch.Stop();
        }
    }
}
