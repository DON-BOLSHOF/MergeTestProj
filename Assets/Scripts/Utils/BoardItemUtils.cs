using BoardElements;
using Merge.MergeItems;

namespace Utils
{
    public static class BoardItemUtils
    {
        public static bool IsMergeItemBelongsToBoardItem(BoardItem boardItem, MergeItem mergeItem)
        {
            return boardItem != null && boardItem.MergeItem.Equals(mergeItem);
        }
    }
}