using System.Collections.Generic;
using CAM.Core.Entities;
using CAM.Core.Entities.DiscrepancyAggregate;

namespace CAM.Core.Interfaces
{
    public interface IDocumentGenerator
    {
        void GenerateWorkOrder(WorkOrder workOrder, List<Discrepancy> discreps);
        void GenerateDiscrepancySingle(Discrepancy discrep);
    }
}