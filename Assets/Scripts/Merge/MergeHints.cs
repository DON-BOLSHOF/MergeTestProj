using System.Collections.Generic;
using System.Linq;
using BoardElements;

namespace Merge
{
    public class MergeHints
    {
        private List<BoardSlot> _activatedHints = new List<BoardSlot>();
        
        public void DeactivateCurrentMergeHints()
        {
            foreach (var activatedHint in _activatedHints.Where(slot => slot.BoardItem != null))
            {
                activatedHint.DeactivateGlowingHint();
            }

            _activatedHints?.Clear();
        }

        public void ActivatePotentialMergeHints(List<BoardSlot> slots)
        {
            foreach (var slot in slots)
            {
                slot.ActivateGlowingHint();
                _activatedHints.Add(slot);
            }
        }

        public void OnMerged(BoardSlot boardSlot)
        {
            _activatedHints.Remove(boardSlot);
        }
    }
}