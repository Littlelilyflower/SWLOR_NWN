﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SWLOR.Game.Server.Core.NWNX;
using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.Item;
using SWLOR.Game.Server.Legacy.Event.Module;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Messaging;
using SWLOR.Game.Server.Legacy.Mod.Contracts;
using SWLOR.Game.Server.Legacy.ValueObject;
using SWLOR.Game.Server.Service;
using SkillType = SWLOR.Game.Server.Legacy.Enumeration.SkillType;

namespace SWLOR.Game.Server.Legacy.Service
{
    public static class ModService
    {
        private static readonly Dictionary<int, IModHandler> _modHandlers;

        static ModService()
        {
            _modHandlers = new Dictionary<int, IModHandler>();
        }

        public static void SubscribeEvents()
        {
            MessageHub.Instance.Subscribe<OnModuleApplyDamage>(message => OnModuleApplyDamage());
            MessageHub.Instance.Subscribe<OnModuleLoad>(message => OnModuleLoad());
        }

        private static void OnModuleLoad()
        {
            RegisterModHandlers();
        }

        private static void RegisterModHandlers()
        {
            // Use reflection to get all of IModHandler implementations.
            var classes = Assembly.GetCallingAssembly().GetTypes()
                .Where(p => typeof(IModHandler).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToArray();
            foreach (var type in classes)
            {
                var instance = Activator.CreateInstance(type) as IModHandler;
                if (instance == null)
                {
                    throw new NullReferenceException("Unable to activate instance of type: " + type);
                }
                _modHandlers.Add(instance.ModTypeID, instance);
            }
        }

        public static bool IsModHandlerRegistered(int modTypeID)
        {
            return _modHandlers.ContainsKey(modTypeID);
        }

        public static IModHandler GetModHandler(int modTypeID)
        {
            if (!_modHandlers.ContainsKey(modTypeID))
            {
                throw new KeyNotFoundException("Mod type ID " + modTypeID + " is not registered. Did you add a script for it?");
            }

            return _modHandlers[modTypeID];
        }

        public static ItemPropertyType GetModType(NWItem item)
        {
            var ipType = ItemPropertyType.Invalid;
            foreach (var ip in item.ItemProperties)
            {
                var type = NWScript.GetItemPropertyType(ip);
                if (type == ItemPropertyType.RedMod ||
                    type == ItemPropertyType.BlueMod ||
                    type == ItemPropertyType.GreenMod ||
                    type == ItemPropertyType.YellowMod)
                {
                    ipType = (ItemPropertyType)type;
                    break;
                }
            }

            return ipType;
        }

        public static ModSlots GetModSlots(NWItem item)
        {
            var modSlots = new ModSlots();
            foreach (var ip in item.ItemProperties)
            {
                var type = NWScript.GetItemPropertyType(ip);
                switch (type)
                {
                    case ItemPropertyType.ModSlotRed:
                        modSlots.RedSlots++;
                        break;
                    case ItemPropertyType.ModSlotBlue:
                        modSlots.BlueSlots++;
                        break;
                    case ItemPropertyType.ModSlotGreen:
                        modSlots.GreenSlots++;
                        break;
                    case ItemPropertyType.ModSlotYellow:
                        modSlots.YellowSlots++;
                        break;
                    case ItemPropertyType.ModSlotPrismatic:
                        modSlots.PrismaticSlots++;
                        break;
                }
            }

            for (var red = 1; red <= modSlots.RedSlots; red++)
            {
                var modID = item.GetLocalInt("MOD_SLOT_RED_" + red);
                if (modID > 0)
                    modSlots.FilledRedSlots++;
            }
            for (var blue = 1; blue <= modSlots.BlueSlots; blue++)
            {
                var modID = item.GetLocalInt("MOD_SLOT_BLUE_" + blue);
                if (modID > 0)
                    modSlots.FilledBlueSlots++;
            }
            for (var green = 1; green <= modSlots.GreenSlots; green++)
            {
                var modID = item.GetLocalInt("MOD_SLOT_GREEN_" + green);
                if (modID > 0)
                    modSlots.FilledGreenSlots++;
            }
            for (var yellow = 1; yellow <= modSlots.YellowSlots; yellow++)
            {
                var modID = item.GetLocalInt("MOD_SLOT_YELLOW_" + yellow);
                if (modID > 0)
                    modSlots.FilledYellowSlots++;
            }
            for (var prismatic = 1; prismatic <= modSlots.PrismaticSlots; prismatic++)
            {
                var modID = item.GetLocalInt("MOD_SLOT_PRISMATIC_" + prismatic);
                if (modID > 0)
                    modSlots.FilledPrismaticSlots++;
            }

            return modSlots;
        }

        public static bool IsRune(NWItem item)
        {
            return GetModType(item) != ItemPropertyType.Invalid;
        }

        public static string PrismaticString()
        {
            return ColorToken.Red("p") + ColorToken.Orange("r") + ColorToken.Yellow("i") + ColorToken.Green("s") + ColorToken.Blue("m") +
                                   ColorToken.LightPurple("a") + ColorToken.Purple("t") + ColorToken.White("i") + ColorToken.Black("c");
        }

        public static string OnModuleExamine(string existingDescription, NWPlayer examiner, NWObject examinedObject)
        {
            if (examinedObject.ObjectType != ObjectType.Item) return existingDescription;
            NWItem examinedItem = (examinedObject.Object);
            var description = string.Empty;
            var slot = GetModSlots(examinedItem);
            
            for (var red = 1; red <= slot.FilledRedSlots; red++)
            {
                description += ColorToken.Red("Red Slot #" + red + ": ") + examinedItem.GetLocalString("MOD_SLOT_RED_DESC_" + red) + "\n";
            }
            for (var blue = 1; blue <= slot.FilledBlueSlots; blue++)
            {
                description += ColorToken.Red("Blue Slot #" + blue + ": ") + examinedItem.GetLocalString("MOD_SLOT_BLUE_DESC_" + blue) + "\n";
            }
            for (var green = 1; green <= slot.FilledGreenSlots; green++)
            {
                description += ColorToken.Red("Green Slot #" + green + ": ") + examinedItem.GetLocalString("MOD_SLOT_GREEN_DESC_" + green) + "\n";
            }
            for (var yellow = 1; yellow <= slot.FilledYellowSlots; yellow++)
            {
                description += ColorToken.Red("Yellow Slot #" + yellow + ": ") + examinedItem.GetLocalString("MOD_SLOT_YELLOW_DESC_" + yellow) + "\n";
            }
            for (var prismatic = 1; prismatic <= slot.FilledPrismaticSlots; prismatic++)
            {
                description += PrismaticString() + " Slot #" + prismatic + ": " + examinedItem.GetLocalString("MOD_SLOT_PRISMATIC_DESC_" + prismatic) + "\n";
            }
            
            return existingDescription + "\n" + description;
        }

        private static void OnModuleApplyDamage()
        {
            var data = Damage.GetDamageEventData();
            if (data.Base <= 0) return;

            NWObject damager = data.Damager;
            if (!damager.IsPlayer) return;
            NWCreature target = NWScript.OBJECT_SELF;

            // Check that this was a normal attack, and not (say) a damage over time effect.
            if (target.GetLocalInt(AbilityService.LAST_ATTACK + damager.GlobalID) != AbilityService.ATTACK_PHYSICAL) return;

            NWItem weapon = (NWScript.GetLastWeaponUsed(damager.Object));
            var damageBonus = weapon.DamageBonus;

            NWPlayer player = (damager.Object);
            var itemLevel = weapon.RecommendedLevel;
            var skill = ItemService.GetSkillTypeForItem(weapon);
            if (skill == SkillType.Unknown) return;

            var rank = SkillService.GetPCSkillRank(player, skill);
            var delta = itemLevel - rank;
            if (delta >= 1) damageBonus--;
            damageBonus = damageBonus - delta / 5;

            if (damageBonus <= 0) damageBonus = 0;
            
            data.Base += damageBonus;
            Damage.SetDamageEventData(data);
        }
    }
}
