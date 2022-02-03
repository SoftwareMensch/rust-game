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
                this.disableDemolishableTimerFor(block);
            }
        }

        private void OnStructureUpgrade(BuildingBlock block, BasePlayer player, BuildingGrade.Enum grade)
        {
            this.disableDemolishableTimerFor(block);
        }
        
        private object OnHammerHit(BasePlayer player, HitInfo hitInfo)
        {
            var block = (hitInfo?.HitEntity ?? null) as BuildingBlock;
            if (block)
            {
                this.disableDemolishableTimerFor(block);
            }
            
            return null;
        }    

        private void disableDemolishableTimerFor(BuildingBlock block)
        {
            block.StartBeingDemolishable();
            block.CancelInvoke(block.StopBeingDemolishable); // disable timer
        }
    }
}
