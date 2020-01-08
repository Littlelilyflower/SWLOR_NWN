

using SWLOR.Game.Server.Data.Contracts;
using System;
using System.Collections.Generic;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Perk;

namespace SWLOR.Game.Server.Data.Entity
{
    public class Player : IEntity
    {
        public Player()
        {
            ID = Guid.NewGuid();
            RespawnAreaResref = "";
            Cooldowns = new Dictionary<PerkCooldownGroup, DateTime>();
            Skills = new Dictionary<Skill, PCSkill>();
            SkillPools = new Dictionary<SkillCategory, int>();

            SavedHelmets = new Dictionary<int, string>();
            SavedOutfits = new Dictionary<int, string>();
            SavedWeapons = new Dictionary<int, string>();

            RegionalFame = new Dictionary<FameRegion, int>();
        }

        [Key]
        public Guid ID { get; set; }
        public string CharacterName { get; set; }
        public int HitPoints { get; set; }
        public string LocationAreaResref { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public double LocationZ { get; set; }
        public double LocationOrientation { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public int UnallocatedSP { get; set; }
        public int HPRegenerationAmount { get; set; }
        public int RegenerationTick { get; set; }
        public int RegenerationRate { get; set; }
        public int VersionNumber { get; set; }
        public int MaxFP { get; set; }
        public int CurrentFP { get; set; }
        public int CurrentFPTick { get; set; }
        public string RespawnAreaResref { get; set; }
        public double RespawnLocationX { get; set; }
        public double RespawnLocationY { get; set; }
        public double RespawnLocationZ { get; set; }
        public double RespawnLocationOrientation { get; set; }
        public DateTime DateSanctuaryEnds { get; set; }
        public bool IsSanctuaryOverrideEnabled { get; set; }
        public int STRBase { get; set; }
        public int DEXBase { get; set; }
        public int CONBase { get; set; }
        public int INTBase { get; set; }
        public int WISBase { get; set; }
        public int CHABase { get; set; }
        public int TotalSPAcquired { get; set; }
        public bool DisplayHelmet { get; set; }
        public Guid? PrimaryResidencePCBaseStructureID { get; set; }
        public DateTime? DatePerkRefundAvailable { get; set; }
        public AssociationType AssociationID { get; set; }
        public bool DisplayHolonet { get; set; }
        public bool DisplayDiscord { get; set; }
        public Guid? PrimaryResidencePCBaseID { get; set; }
        public bool IsUsingNovelEmoteStyle { get; set; }
        public bool IsDeleted { get; set; }
        public int XPBonus { get; set; }
        public int LeaseRate { get; set; }
        public Guid? LocationInstanceID { get; set; }
        public int GoldTill { get; set; }
        public int RoleplayPoints { get; set; }
        public int RoleplayXP { get; set; }
        public SpecializationType SpecializationID { get; set; }
        public int? ActiveConcentrationPerkID { get; set; }
        public int ActiveConcentrationTier { get; set; }

        public Dictionary<PerkCooldownGroup, DateTime> Cooldowns { get; set; }
        public Dictionary<Skill, PCSkill> Skills { get; set; }
        public Dictionary<SkillCategory, int> SkillPools { get; set; }
        public Dictionary<int, string> SavedOutfits { get; set; }
        public Dictionary<int, string> SavedWeapons { get; set; }
        public Dictionary<int, string> SavedHelmets { get; set; }
        public Dictionary<FameRegion, int> RegionalFame { get; set; }
    }

    public class PCSkill
    {
        public int XP { get; set; }
        public int Rank { get; set; }
        public bool IsLocked { get; set; }
    }
}
