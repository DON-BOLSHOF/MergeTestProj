using Merge.MergeItems;

namespace Definitions
{
    public class MergeItemDefs : AbstractDefs<MergeItem>
    {
        public override MergeItem Get(string id)
        {
            return _items.Find(item => item.Id.Equals(id));
        }
    }
}