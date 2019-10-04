using System.Collections.Generic;

namespace CAM.Core.SharedKernel
{
    public class BaseEntity
    {
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}