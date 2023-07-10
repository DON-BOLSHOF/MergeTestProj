using Merge;
using Merge.MergeItems;
using UnityEngine;

namespace Definitions
{
    [CreateAssetMenu(fileName = "MergeInstructionsDefs", menuName = "Defs/MergeInstructionsDefs")]
    public class MergeChainsDefs : AbstractDefs<MergeChain>
    {
        public override MergeChain Get(string id)
        {
            return _items.Find(mergeInstruction => id.Equals(mergeInstruction.InstructionId));
        }

        public MergeChain GetChainWithItem(MergeItem item)//В дальнейшем измени GUI, сделай Enum инструкций, привяжи каждый MergeItem к инструкции. 
                                                                      //Более высокая связность, но более хорошая безопасность. 
        {
            return _items.Find(instruction =>
                instruction.Stages.Find(stage => stage.MergeItem.GetType() == item.GetType()) != null);
        }
    }
}