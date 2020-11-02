﻿using System;
using System.Linq;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.Item;
using SWLOR.Game.Server.Legacy.Data.Entity;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.Event.SWLOR;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Messaging;
using SWLOR.Game.Server.Legacy.Service;
using SWLOR.Game.Server.Service;
using static SWLOR.Game.Server.Core.NWScript.NWScript;

namespace SWLOR.Game.Server.Legacy.Scripts.Placeable.StructureStorage
{
    public class OnDisturbed : IScript
    {
        public void SubscribeEvents()
        {
        }

        public void UnsubscribeEvents()
        {
        }

        public void Main()
        {
            NWPlayer oPC = (GetLastDisturbed());
            NWItem item = (GetInventoryDisturbItem());
            NWPlaceable container = (OBJECT_SELF);
            var disturbType = GetInventoryDisturbType();
            var structureID = new Guid(container.GetLocalString("PC_BASE_STRUCTURE_ID"));
            var structure = DataService.PCBaseStructure.GetByID(structureID);
            var baseStructure = DataService.BaseStructure.GetByID(structure.BaseStructureID);
            var itemLimit = baseStructure.Storage + structure.StructureBonus;

            var itemCount = container.InventoryItems.Count();
            var itemResref = item.Resref;

            if (disturbType == DisturbType.Added)
            {
                if (GetHasInventory(item) == true)
                {
                    SetLocalBool(item, "RETURNING_ITEM", true);
                    ItemService.ReturnItem(oPC, item);
                    oPC.SendMessage(ColorToken.Red("Containers cannot currently be stored inside banks."));
                    return;
                }
                
                if (itemCount > itemLimit)
                {
                    ItemService.ReturnItem(oPC, item);
                    oPC.SendMessage(ColorToken.Red("No more items can be placed inside."));
                }
                else if (item.BaseItemType == BaseItem.Gold)
                {
                    ItemService.ReturnItem(oPC, item);
                    oPC.SendMessage(ColorToken.Red("Credits cannot be placed inside."));
                }
                else
                {
                    var itemEntity = new PCBaseStructureItem
                    {
                        ItemName = item.Name,
                        ItemResref = itemResref,
                        ItemTag = item.Tag,
                        PCBaseStructureID = structureID,
                        ItemGlobalID = item.GlobalID.ToString(),
                        ItemObject = SerializationService.Serialize(item)
                    };
                    DataService.SubmitDataChange(itemEntity, DatabaseActionType.Insert);
                    MessageHub.Instance.Publish(new OnStoreStructureItem(oPC, itemEntity));
                }
            }
            else if (disturbType == DisturbType.Removed)
            {
                if (GetLocalBool(item, "RETURNING_ITEM"))
                {
                    item.DeleteLocalInt("RETURNING_ITEM");
                }
                else
                {
                    var dbItem = DataService.PCBaseStructureItem.GetByItemGlobalID(item.GlobalID.ToString());
                    DataService.SubmitDataChange(dbItem, DatabaseActionType.Delete);
                    MessageHub.Instance.Publish(new OnRemoveStructureItem(oPC, dbItem));
                }
            }

            oPC.SendMessage(ColorToken.White("Item Limit: " + itemCount + " / ") + ColorToken.Red(itemLimit.ToString()));
        }
    }
}
