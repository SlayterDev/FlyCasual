﻿using Ship;
using Board;
using Abilities;
using ActionsList;

namespace Ship
{
    namespace LancerClassPursuitCraft
    {
        public class SabineWren : LancerClassPursuitCraft
        {
            public SabineWren() : base()
            {
                PilotName = "Sabine Wren";
                PilotSkill = 5;
                Cost = 35;

                IsUnique = true;

                PilotAbilities.Add(new SabineWrenLancerPilotAbility());
            }
        }
    }
}

namespace Abilities
{
    public class SabineWrenLancerPilotAbility : GenericAbility
    {

        public override void ActivateAbility()
        {
            HostShip.AfterGenerateAvailableActionEffectsList += AddSabinebility;
        }

        public override void DeactivateAbility()
        {
            HostShip.AfterGenerateAvailableActionEffectsList -= AddSabinebility;
        }

        private void AddSabinebility(GenericShip ship)
        {
            ship.AddAvailableActionEffect(new SabineWrenDiceModification());
        }

        private class SabineWrenDiceModification : GenericAction
        {
            public SabineWrenDiceModification()
            {
                Name = EffectName = "Sabine Wren's ability";
            }

            public override void ActionEffect(System.Action callBack)
            {
                Combat.CurrentDiceRoll.AddDice(DieSide.Focus).ShowWithoutRoll();
                Combat.CurrentDiceRoll.OrganizeDicePositions();
                callBack();
            }

            public override bool IsActionEffectAvailable()
            {
                bool result = false;
                if (Combat.AttackStep == CombatStep.Defence)
                {
                    ShipShotDistanceInformation shotInfo = new ShipShotDistanceInformation(Combat.Defender, Combat.Attacker);
                    if (shotInfo.InMobileArc) result = true;
                }
                return result;
            }

            public override int GetActionEffectPriority()
            {
                return 110;
            }
        }

    }
}
