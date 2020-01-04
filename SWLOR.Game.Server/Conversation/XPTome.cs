﻿using System.Collections.Generic;
using System.Linq;
using SWLOR.Game.Server.Data.Entity;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Extension;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.ValueObject.Dialog;

namespace SWLOR.Game.Server.Conversation
{
    public class XPTome: ConversationBase
    {
        private class Model
        {
            public NWItem Item { get; set; }
            public Skill SkillID { get; set; }
        }


        public override PlayerDialog SetUp(NWPlayer player)
        {
            PlayerDialog dialog = new PlayerDialog("CategoryPage");

            DialogPage categoryPage = new DialogPage(
                "This tome holds techniques lost to the ages. Select a skill to earn experience in that art."
            );

            DialogPage skillListPage = new DialogPage(
                "This tome holds techniques lost to the ages. Select a skill to earn experience in that art."
            );

            DialogPage confirmPage = new DialogPage(
                "<SET LATER>",
                "Select this skill."
            );

            dialog.AddPage("CategoryPage", categoryPage);
            dialog.AddPage("SkillListPage", skillListPage);
            dialog.AddPage("ConfirmPage", confirmPage);

            return dialog;
        }

        public override void Initialize()
        {
            var categories = SkillService.GetAllSkillCategoriesWithContributingToCapSkills();
            foreach (SkillCategory category in categories)
            {
                var attr = category.GetAttribute<SkillCategory, SkillCategoryAttribute>();
                AddResponseToPage("CategoryPage", attr.Name, true, category);
            }

            Model vm = GetDialogCustomData<Model>();
            vm.Item = (GetPC().GetLocalObject("XP_TOME_OBJECT"));
            SetDialogCustomData(vm);
        }

        public override void DoAction(NWPlayer player, string pageName, int responseID)
        {
            switch (pageName)
            {
                case "CategoryPage":
                    HandleCategoryPageResponse(responseID);
                    break;
                case "SkillListPage":
                    HandleSkillListResponse(responseID);
                    break;
                case "ConfirmPage":
                    HandleConfirmPageResponse();
                    break;
            }
        }

        public override void Back(NWPlayer player, string beforeMovePage, string afterMovePage)
        {
        }

        private void HandleCategoryPageResponse(int responseID)
        {
            var response = GetResponseByID("CategoryPage", responseID);
            var categoryID = (SkillCategory)response.CustomData;
            
            List<PCSkill> pcSkills = SkillService.GetPCSkillsForCategory(GetPC().GlobalID, categoryID);

            ClearPageResponses("SkillListPage");
            foreach (PCSkill pcSkill in pcSkills)
            {
                var skill = SkillService.GetSkill(pcSkill.SkillID);
                AddResponseToPage("SkillListPage", skill.Name, true, pcSkill.SkillID);
            }

            ChangePage("SkillListPage");
        }

        private void HandleSkillListResponse(int responseID)
        {
            DialogResponse response = GetResponseByID("SkillListPage", responseID);
            var skillID = (Skill)response.CustomData;
            var skill = SkillService.GetSkill(skillID);
            string header = "Are you sure you want to improve your " + skill.Name + " skill?";
            SetPageHeader("ConfirmPage", header);

            Model vm = GetDialogCustomData<Model>();
            vm.SkillID = skillID;
            SetDialogCustomData(vm);
            ChangePage("ConfirmPage");
        }

        private void HandleConfirmPageResponse()
        {
            Model vm = GetDialogCustomData<Model>();

            if (vm.Item != null && vm.Item.IsValid)
            {
                var skill = SkillService.GetSkill(vm.SkillID);
                if (!skill.ContributesToSkillCap)
                {
                    GetPC().FloatingText("You cannot raise that skill with this tome.");
                    return;
                }

                int xp = vm.Item.GetLocalInt("XP_TOME_AMOUNT");
                SkillService.GiveSkillXP(GetPC(), vm.SkillID, xp, false, false);
                vm.Item.Destroy();
            }

            EndConversation();
        }

        public override void EndDialog()
        {
        }
    }
}
