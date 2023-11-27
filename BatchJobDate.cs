#nullable enable

using System;
using System.Collections.Generic;
using BTDB.ODBLayer;
using Gmc.Cloud.Infrastructure.Core.Context;
using Gmc.Cloud.Infrastructure.DataProtection;
using Gmc.Cloud.Prodaas.Db.Enums;

namespace Gmc.Cloud.Prodaas.Db.Indexes;

public class BatchJobDate : ICompanyRecord
{
    [PrimaryKey(1)]
    public ulong CompanyId { get; set; }
    [PrimaryKey(2)]
    public DateTime Started { get; set; }
    [PrimaryKey(3)]
    public Guid BatchJobId { get; set; }
    [InKeyValue(4)]
    public ProductionBatchState State { get; set; }
    [InKeyValue(5)]
    public Guid? SenJobId { get; set; }
    public TimeSpan Duration { get; set; }

    public static IBatchJobDateTable Table => Ambient.GetTable<IBatchJobDateTable>();

    public interface IBatchJobDateTable : ICompanyItemTableBase<BatchJobDate>
    {
        IEnumerable<BatchJobDate> ScanById(Constraint<ulong> companyId, Constraint<DateTime> started);
        IEnumerable<BatchJobDate> ScanById(Constraint<ulong> companyId, Constraint<DateTime> started, Constraint<Guid> batchJobId);
        IEnumerable<BatchJobDate> ScanById(Constraint<ulong> companyId, Constraint<DateTime> started, Constraint<Guid> batchJobId, Constraint<ProductionBatchState> state);
        IEnumerable<BatchJobDate> ScanById(Constraint<ulong> companyId, Constraint<DateTime> started, Constraint<Guid> batchJobId, Constraint<ProductionBatchState> state, Constraint<Guid?> senJobId);
    }
}
