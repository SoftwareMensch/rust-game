using UnityEngine;
using ConVar;

namespace Oxide.Plugins
{
    [Info("Demolishable", "Romano (RKL.tv) Kleinwaechter", "1.0.0")]
    [Description("Disables demolishable timer for building blocks")]
    public class Demolishable : CovalencePlugin
    {
        private void Init()
        {
            Puts("Demolishable loaded");
        }

        private void OnEntityBuilt(Planner plan, GameObject objectBlock)
        {
            var block = (objectBlock?.ToBaseEntity() ?? null) as BuildingBlock;
            if (block)
            {
                this.disableStopTimerOf(block);
            }
        }

        private void OnStructureUpgrade(BuildingBlock block, BasePlayer player, BuildingGrade.Enum grade)
        {
            this.disableStopTimerOf(block);
        }
        
        private object OnHammerHit(BasePlayer player, HitInfo hitInfo)
        {
            var block = (hitInfo?.HitEntity ?? null) as BuildingBlock;
            if (block)
            {
                this.disableStopTimerOf(block);
            }
            
            return null;
        }

        private void disableStopTimerOf(BuildingBlock block)
        {
            block.CancelInvoke(block.StopBeingDemolishable); // disable old timer
            block.SetFlag(BaseEntity.Flags.Reserved2, true); // enables demolishable
        }
    }
}
