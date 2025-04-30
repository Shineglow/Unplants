using System.Collections.Generic;

namespace Unplants.Scripts.General.Systems.DI
{
    public abstract class DIBindingBuilderMultipleBindingsAbstract
    {
        protected DIRecord[] records;

        public virtual IReadOnlyList<DIRecord> GetRecords()
        {
            var result = records;
            records = null;
            return result;
        }
    }
}
