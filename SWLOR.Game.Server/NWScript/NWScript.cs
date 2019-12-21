﻿using SWLOR.Game.Server.NWNX;
using SWLOR.Game.Server.NWScript.Enumerations;

namespace NWN
{
    public partial class _
    {
        /// <summary>
        /// Assign aActionToAssign to oActionSubject.
        /// * No return value, but if an error occurs, the log file will contain
        ///   "AssignCommand failed."
        ///   (If the object doesn't exist, nothing happens.)
        /// </summary>
        public static void AssignCommand(NWGameObject oActionSubject, ActionDelegate aActionToAssign)
        {
            Internal.ClosureAssignCommand(oActionSubject, aActionToAssign);
        }

        /// <summary>
        /// Delay aActionToDelay by fSeconds.
        /// * No return value, but if an error occurs, the log file will contain
        ///   "DelayCommand failed.".
        /// It is suggested that functions which create effects should not be used
        /// as parameters to delayed actions.  Instead, the effect should be created in the
        /// script and then passed into the action.  For example:
        /// effect eDamage = EffectDamage(nDamage, DAMAGE_TYPE_MAGICAL);
        /// DelayCommand(fDelay, ApplyEffectToObject(DurationType.Instant, eDamage, oTarget);
        /// </summary>
        public static void DelayCommand(float fSeconds, ActionDelegate aActionToDelay)
        {
            Internal.ClosureDelayCommand(NWGameObject.OBJECT_SELF, fSeconds, aActionToDelay);
        }

        /// <summary>
        /// Do aActionToDo.
        /// </summary>
        public static void ActionDoCommand(ActionDelegate aActionToDo)
        {
            Internal.ClosureActionDoCommand(NWGameObject.OBJECT_SELF, aActionToDo);
        }

        /// <summary>
        ///  Make oTarget run sScript and then return execution to the calling script.
        ///  If sScript does not specify a compiled script, nothing happens.
        /// </summary>
        public static void ExecuteScript(string sScript, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushStringUTF8(sScript);
            Internal.NativeFunctions.CallBuiltIn(8);
        }

        /// <summary>
        ///  Clear all the actions of the caller.
        ///  * No return value, but if an error occurs, the log file will contain
        ///    "ClearAllActions failed.".
        ///  - nClearCombatState: if true, this will immediately clear the combat state
        ///    on a creature, which will stop the combat music and allow them to rest,
        ///    engage in dialog, or other actions that they would normally have to wait for.
        /// </summary>
        public static void ClearAllActions(bool nClearCombatState = false)
        {
            Internal.NativeFunctions.StackPushInteger(nClearCombatState ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(9);
        }

        /// <summary>
        ///  Cause the caller to face fDirection.
        ///  - fDirection is expressed as anticlockwise degrees from Due East.
        ///    DIRECTION_EAST, DIRECTION_NORTH, DIRECTION_WEST and DIRECTION_SOUTH are
        ///    predefined. (0.0f=East, 90.0f=North, 180.0f=West, 270.0f=South)
        /// </summary>
        public static void SetFacing(float fDirection)
        {
            Internal.NativeFunctions.StackPushFloat(fDirection);
            Internal.NativeFunctions.CallBuiltIn(10);
        }

        /// <summary>
        ///  Set the calendar to the specified date.
        ///  - nYear should be from 0 to 32000 inclusive
        ///  - nMonth should be from 1 to 12 inclusive
        ///  - nDay should be from 1 to 28 inclusive
        ///  1) Time can only be advanced forwards; attempting to set the time backwards
        ///     will result in no change to the calendar.
        ///  2) If values larger than the month or day are specified, they will be wrapped
        ///     around and the overflow will be used to advance the next field.
        ///     e.g. Specifying a year of 1350, month of 33 and day of 10 will result in
        ///     the calender being set to a year of 1352, a month of 9 and a day of 10.
        /// </summary>
        public static void SetCalendar(int nYear, int nMonth, int nDay)
        {
            Internal.NativeFunctions.StackPushInteger(nDay);
            Internal.NativeFunctions.StackPushInteger(nMonth);
            Internal.NativeFunctions.StackPushInteger(nYear);
            Internal.NativeFunctions.CallBuiltIn(11);
        }

        /// <summary>
        ///  Set the time to the time specified.
        ///  - nHour should be from 0 to 23 inclusive
        ///  - nMinute should be from 0 to 59 inclusive
        ///  - nSecond should be from 0 to 59 inclusive
        ///  - nMillisecond should be from 0 to 999 inclusive
        ///  1) Time can only be advanced forwards; attempting to set the time backwards
        ///     will result in the day advancing and then the time being set to that
        ///     specified, e.g. if the current hour is 15 and then the hour is set to 3,
        ///     the day will be advanced by 1 and the hour will be set to 3.
        ///  2) If values larger than the max hour, minute, second or millisecond are
        ///     specified, they will be wrapped around and the overflow will be used to
        ///     advance the next field, e.g. specifying 62 hours, 250 minutes, 10 seconds
        ///     and 10 milliseconds will result in the calendar day being advanced by 2
        ///     and the time being set to 18 hours, 10 minutes, 10 milliseconds.
        /// </summary>
        public static void SetTime(int nHour, int nMinute, int nSecond, int nMillisecond)
        {
            Internal.NativeFunctions.StackPushInteger(nMillisecond);
            Internal.NativeFunctions.StackPushInteger(nSecond);
            Internal.NativeFunctions.StackPushInteger(nMinute);
            Internal.NativeFunctions.StackPushInteger(nHour);
            Internal.NativeFunctions.CallBuiltIn(12);
        }

        /// <summary>
        ///  Get the current calendar year.
        /// </summary>
        public static int GetCalendarYear()
        {
            Internal.NativeFunctions.CallBuiltIn(13);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the current calendar month.
        /// </summary>
        public static int GetCalendarMonth()
        {
            Internal.NativeFunctions.CallBuiltIn(14);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the current calendar day.
        /// </summary>
        public static int GetCalendarDay()
        {
            Internal.NativeFunctions.CallBuiltIn(15);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the current hour.
        /// </summary>
        public static int GetTimeHour()
        {
            Internal.NativeFunctions.CallBuiltIn(16);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the current minute
        /// </summary>
        public static int GetTimeMinute()
        {
            Internal.NativeFunctions.CallBuiltIn(17);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the current second
        /// </summary>
        public static int GetTimeSecond()
        {
            Internal.NativeFunctions.CallBuiltIn(18);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the current millisecond
        /// </summary>
        public static int GetTimeMillisecond()
        {
            Internal.NativeFunctions.CallBuiltIn(19);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  The action subject will generate a random location near its current location
        ///  and pathfind to it.  ActionRandomwalk never ends, which means it is neccessary
        ///  to call ClearAllActions in order to allow a creature to perform any other action
        ///  once ActionRandomWalk has been called.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionRandomWalk failed."
        /// </summary>
        public static void ActionRandomWalk()
        {
            Internal.NativeFunctions.CallBuiltIn(20);
        }

        /// <summary>
        ///  The action subject will move to lDestination.
        ///  - lDestination: The object will move to this location.  If the location is
        ///    invalid or a path cannot be found to it, the command does nothing.
        ///  - bRun: If this is true, the action subject will run rather than walk
        ///  * No return value, but if an error occurs the log file will contain
        ///    "MoveToPoint failed."
        /// </summary>
        public static void ActionMoveToLocation(NWN.Location lDestination, bool bRun = false)
        {
            Internal.NativeFunctions.StackPushInteger(bRun ? 1 : 0);
            Internal.NativeFunctions.StackPushLocation(lDestination.Handle);
            Internal.NativeFunctions.CallBuiltIn(21);
        }

        /// <summary>
        ///  Cause the action subject to move to a certain distance from oMoveTo.
        ///  If there is no path to oMoveTo, this command will do nothing.
        ///  - oMoveTo: This is the object we wish the action subject to move to
        ///  - bRun: If this is true, the action subject will run rather than walk
        ///  - fRange: This is the desired distance between the action subject and oMoveTo
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionMoveToObject failed."
        /// </summary>
        public static void ActionMoveToObject(NWGameObject oMoveTo, bool bRun = false, float fRange = 1.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fRange);
            Internal.NativeFunctions.StackPushInteger(bRun ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oMoveTo != null ? oMoveTo.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(22);
        }

        /// <summary>
        ///  Cause the action subject to move to a certain distance away from oFleeFrom.
        ///  - oFleeFrom: This is the object we wish the action subject to move away from.
        ///    If oFleeFrom is not in the same area as the action subject, nothing will
        ///    happen.
        ///  - bRun: If this is true, the action subject will run rather than walk
        ///  - fMoveAwayRange: This is the distance we wish the action subject to put
        ///    between themselves and oFleeFrom
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionMoveAwayFromObject failed."
        /// </summary>
        public static void ActionMoveAwayFromObject(NWGameObject oFleeFrom, bool bRun = false, float fMoveAwayRange = 40.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fMoveAwayRange);
            Internal.NativeFunctions.StackPushInteger(bRun ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oFleeFrom != null ? oFleeFrom.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(23);
        }

        /// <summary>
        ///  Get the area that oTarget is currently in
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetArea(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(24);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  The value returned by this function depends on the object type of the caller:
        ///  1) If the caller is a door it returns the object that last
        ///     triggered it.
        ///  2) If the caller is a trigger, area of effect, module, area or encounter it
        ///     returns the object that last entered it.
        ///  * Return value on error: OBJECT_INVALID
        ///   When used for doors, this should only be called from the OnAreaTransitionClick
        ///   event.  Otherwise, it should only be called in OnEnter scripts.
        /// </summary>
        public static NWGameObject GetEnteringObject()
        {
            Internal.NativeFunctions.CallBuiltIn(25);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the object that last left the caller.  This function works on triggers,
        ///  areas of effect, modules, areas and encounters.
        ///  * Return value on error: OBJECT_INVALID
        ///  Should only be called in OnExit scripts.
        /// </summary>
        public static NWGameObject GetExitingObject()
        {
            Internal.NativeFunctions.CallBuiltIn(26);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the position of oTarget
        ///  * Return value on error: vector (0.0f, 0.0f, 0.0f)
        /// </summary>
        public static NWN.Vector GetPosition(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(27);
            return Internal.NativeFunctions.StackPopVector();
        }

        /// <summary>
        ///  Get the direction in which oTarget is facing, expressed as a float between
        ///  0.0f and 360.0f
        ///  * Return value on error: -1.0f
        /// </summary>
        public static float GetFacing(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(28);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Get the possessor of oItem
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetItemPossessor(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(29);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the object possessed by oCreature with the tag sItemTag
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetItemPossessedBy(NWGameObject oCreature, string sItemTag)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sItemTag);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(30);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Create an item with the template sItemTemplate in oTarget's inventory.
        ///  - nStackSize: This is the stack size of the item to be created
        ///  - sNewTag: If this string is not empty, it will replace the default tag from the template
        ///  * Return value: The object that has been created.  On error, this returns
        ///    OBJECT_INVALID.
        ///  If the item created was merged into an existing stack of similar items,
        ///  the function will return the merged stack object. If the merged stack
        ///  overflowed, the function will return the overflowed stack that was created.
        /// </summary>
        public static NWGameObject CreateItemOnObject(string sItemTemplate, NWGameObject oTarget = null, int nStackSize = 1, string sNewTag = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sNewTag);
            Internal.NativeFunctions.StackPushInteger(nStackSize);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushStringUTF8(sItemTemplate);
            Internal.NativeFunctions.CallBuiltIn(31);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Equip oItem into nInventorySlot.
        ///  - nInventorySlot: INVENTORY_SLOT_*
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionEquipItem failed."
        /// 
        ///  Note: 
        ///        If the creature already has an item equipped in the slot specified, it will be 
        ///        unequipped automatically by the call to ActionEquipItem.
        ///      
        ///        In order for ActionEquipItem to succeed the creature must be able to equip the
        ///        item oItem normally. This means that:
        ///        1) The item is in the creature's inventory.
        ///        2) The item must already be identified (if magical). 
        ///        3) The creature has the level required to equip the item (if magical and ILR is on).
        ///        4) The creature possesses the required feats to equip the item (such as weapon proficiencies).
        /// </summary>
        public static void ActionEquipItem(NWGameObject oItem, InventorySlot nInventorySlot)
        {
            Internal.NativeFunctions.StackPushInteger((int)nInventorySlot);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(32);
        }

        /// <summary>
        ///  Unequip oItem from whatever slot it is currently in.
        /// </summary>
        public static void ActionUnequipItem(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(33);
        }

        /// <summary>
        ///  Pick up oItem from the ground.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionPickUpItem failed."
        /// </summary>
        public static void ActionPickUpItem(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(34);
        }

        /// <summary>
        ///  Put down oItem on the ground.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionPutDownItem failed."
        /// </summary>
        public static void ActionPutDownItem(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(35);
        }

        /// <summary>
        ///  Get the last attacker of oAttackee.  This should only be used ONLY in the
        ///  OnAttacked events for creatures, placeables and doors.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetLastAttacker(NWGameObject oAttackee = null)
        {
            Internal.NativeFunctions.StackPushObject(oAttackee != null ? oAttackee.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(36);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Attack oAttackee.
        ///  - bPassive: If this is true, attack is in passive mode.
        /// </summary>
        public static void ActionAttack(NWGameObject oAttackee, bool bPassive = false)
        {
            Internal.NativeFunctions.StackPushInteger(bPassive ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oAttackee != null ? oAttackee.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(37);
        }

        /// <summary>
        ///  Get the creature nearest to oTarget, subject to all the criteria specified.
        ///  - nFirstCriteriaType: CREATURE_TYPE_*
        ///  - nFirstCriteriaValue:
        ///    -> CLASS_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_CLASS
        ///    -> SPELL_* if nFirstCriteriaType was CREATURE_TYPE_DOES_NOT_HAVE_SPELL_EFFECT
        ///       or CREATURE_TYPE_HAS_SPELL_EFFECT
        ///    -> true or false if nFirstCriteriaType was CREATURE_TYPE_IS_ALIVE
        ///    -> PERCEPTION_* if nFirstCriteriaType was CREATURE_TYPE_PERCEPTION
        ///    -> PLAYER_CHAR_IS_PC or PLAYER_CHAR_NOT_PC if nFirstCriteriaType was
        ///       CREATURE_TYPE_PLAYER_CHAR
        ///    -> RACIAL_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_RACIAL_TYPE
        ///    -> REPUTATION_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_REPUTATION
        ///    For example, to get the nearest PC, use:
        ///    (CREATURE_TYPE_PLAYER_CHAR, PLAYER_CHAR_IS_PC)
        ///  - oTarget: We're trying to find the creature of the specified type that is
        ///    nearest to oTarget
        ///  - nNth: We don't have to find the first nearest: we can find the Nth nearest...
        ///  - nSecondCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///    further specify the type of creature that we are looking for.
        ///  - nSecondCriteriaValue: This is used in the same way as nFirstCriteriaValue
        ///    to further specify the type of creature that we are looking for.
        ///  - nThirdCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///    further specify the type of creature that we are looking for.
        ///  - nThirdCriteriaValue: This is used in the same way as nFirstCriteriaValue to
        ///    further specify the type of creature that we are looking for.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetNearestCreature(int nFirstCriteriaType, int nFirstCriteriaValue, NWGameObject oTarget = null, int nNth = 1, int nSecondCriteriaType = -1, int nSecondCriteriaValue = -1, int nThirdCriteriaType = -1, int nThirdCriteriaValue = -1)
        {
            Internal.NativeFunctions.StackPushInteger(nThirdCriteriaValue);
            Internal.NativeFunctions.StackPushInteger(nThirdCriteriaType);
            Internal.NativeFunctions.StackPushInteger(nSecondCriteriaValue);
            Internal.NativeFunctions.StackPushInteger(nSecondCriteriaType);
            Internal.NativeFunctions.StackPushInteger(nNth);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nFirstCriteriaValue);
            Internal.NativeFunctions.StackPushInteger(nFirstCriteriaType);
            Internal.NativeFunctions.CallBuiltIn(38);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Add a speak action to the action subject.
        ///  - sStringToSpeak: String to be spoken
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void ActionSpeakString(string sStringToSpeak, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTalkVolume);
            Internal.NativeFunctions.StackPushString(sStringToSpeak);
            Internal.NativeFunctions.CallBuiltIn(39);
        }

        /// <summary>
        ///  Get the distance from the caller to oObject in metres.
        ///  * Return value on error: -1.0f
        /// </summary>
        public static float GetDistanceToObject(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(41);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  * Returns true if oObject is a valid object.
        /// </summary>
        public static bool GetIsObjectValid(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(42);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Cause the action subject to open oDoor
        /// </summary>
        public static void ActionOpenDoor(NWGameObject oDoor)
        {
            Internal.NativeFunctions.StackPushObject(oDoor != null ? oDoor.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(43);
        }

        /// <summary>
        ///  Cause the action subject to close oDoor
        /// </summary>
        public static void ActionCloseDoor(NWGameObject oDoor)
        {
            Internal.NativeFunctions.StackPushObject(oDoor != null ? oDoor.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(44);
        }

        /// <summary>
        ///  Change the direction in which the camera is facing
        ///  - fDirection is expressed as anticlockwise degrees from Due East.
        ///    (0.0f=East, 90.0f=North, 180.0f=West, 270.0f=South)
        ///  A value of -1.0f for any parameter will be ignored and instead it will
        ///  use the current camera value.
        ///  This can be used to change the way the camera is facing after the player
        ///  emerges from an area transition.
        ///  - nTransitionType: CAMERA_TRANSITION_TYPE_*  SNAP will immediately move the
        ///    camera to the new position, while the other types will result in the camera moving gradually into position
        ///  Pitch and distance are limited to valid values for the current camera mode:
        ///  Top Down: Distance = 5-20, Pitch = 1-50
        ///  Driving camera: Distance = 6 (can't be changed), Pitch = 1-62
        ///  Chase: Distance = 5-20, Pitch = 1-50
        ///  *** NOTE *** In NWN:Hordes of the Underdark the camera limits have been relaxed to the following:
        ///  Distance 1-25
        ///  Pitch 1-89
        /// </summary>
        public static void SetCameraFacing(float fDirection, float fDistance = -1.0f, float fPitch = -1.0f, CameraTransitionType nTransitionType = CameraTransitionType.Snap)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTransitionType);
            Internal.NativeFunctions.StackPushFloat(fPitch);
            Internal.NativeFunctions.StackPushFloat(fDistance);
            Internal.NativeFunctions.StackPushFloat(fDirection);
            Internal.NativeFunctions.CallBuiltIn(45);
        }

        /// <summary>
        ///  Play sSoundName
        ///  - sSoundName: TBD - SS
        ///  This will play a mono sound from the location of the object running the command.
        /// </summary>
        public static void PlaySound(string sSoundName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sSoundName);
            Internal.NativeFunctions.CallBuiltIn(46);
        }

        /// <summary>
        ///  Get the object at which the caller last cast a spell
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetSpellTargetObject()
        {
            Internal.NativeFunctions.CallBuiltIn(47);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  This action casts a spell at oTarget.
        ///  - nSpell: SPELL_*
        ///  - oTarget: Target for the spell
        ///  - nMetamagic: METAMAGIC_*
        ///  - bCheat: If this is true, then the executor of the action doesn't have to be
        ///    able to cast the spell.
        ///  - nDomainLevel: TBD - SS
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        ///  - bInstantSpell: If this is true, the spell is cast immediately. This allows
        ///    the end-user to simulate a high-level magic-user having lots of advance
        ///    warning of impending trouble
        /// </summary>
        public static void ActionCastSpellAtObject(Spell nSpell, NWGameObject oTarget, MetaMagic nMetaMagic = MetaMagic.Any, bool bCheat = false, int nDomainLevel = 0, ProjectilePathType nProjectilePathType = ProjectilePathType.Default, bool bInstantSpell = false)
        {
            Internal.NativeFunctions.StackPushInteger(bInstantSpell ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nProjectilePathType);
            Internal.NativeFunctions.StackPushInteger(nDomainLevel);
            Internal.NativeFunctions.StackPushInteger(bCheat ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nMetaMagic);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(48);
        }

        /// <summary>
        ///  Get the current hitpoints of oObject
        ///  * Return value on error: 0
        /// </summary>
        public static int GetCurrentHitPoints(NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(49);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the maximum hitpoints of oObject
        ///  * Return value on error: 0
        /// </summary>
        public static int GetMaxHitPoints(NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(50);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oObject's local integer variable sVarName
        ///  * Return value on error: 0
        /// </summary>
        public static int GetLocalInt(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushString(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(51);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oObject's local float variable sVarName
        ///  * Return value on error: 0.0f
        /// </summary>
        public static float GetLocalFloat(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushString(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(52);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Get oObject's local string variable sVarName
        ///  * Return value on error: ""
        /// </summary>
        public static string GetLocalString(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(53);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Get oObject's local object variable sVarName
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetLocalObject(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(54);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Set oObject's local integer variable sVarName to nValue
        /// </summary>
        public static void SetLocalInt(NWGameObject oObject, string sVarName, int nValue)
        {
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(55);
        }

        /// <summary>
        ///  Set oObject's local float variable sVarName to nValue
        /// </summary>
        public static void SetLocalFloat(NWGameObject oObject, string sVarName, float fValue)
        {
            Internal.NativeFunctions.StackPushFloat(fValue);
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(56);
        }

        /// <summary>
        ///  Set oObject's local string variable sVarName to nValue
        /// </summary>
        public static void SetLocalString(NWGameObject oObject, string sVarName, string sValue)
        {
            Internal.NativeFunctions.StackPushString(sValue);
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(57);
        }

        /// <summary>
        ///  Set oObject's local object variable sVarName to nValue
        /// </summary>
        public static void SetLocalObject(NWGameObject oObject, string sVarName, NWGameObject oValue)
        {
            Internal.NativeFunctions.StackPushObject(oValue != null ? oValue.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(58);
        }

        /// <summary>
        ///  Create a Heal effect. This should be applied as an instantaneous effect.
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nDamageToHeal < 0.
        /// </summary>
        public static Effect EffectHeal(int nDamageToHeal)
        {
            Internal.NativeFunctions.StackPushInteger(nDamageToHeal);
            Internal.NativeFunctions.CallBuiltIn(78);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Damage effect
        ///  - nDamageAmount: amount of damage to be dealt. This should be applied as an
        ///    instantaneous effect.
        ///  - nDamageType: DAMAGE_TYPE_*
        ///  - nDamagePower: DAMAGE_POWER_*
        /// </summary>
        public static Effect EffectDamage(int nDamageAmount, DamageType nDamageType = DamageType.Magical, DamagePower nDamagePower = DamagePower.Normal)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamagePower);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger(nDamageAmount);
            Internal.NativeFunctions.CallBuiltIn(79);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create an Ability Increase effect
        ///  - bAbilityToIncrease: ABILITY_*
        /// </summary>
        public static Effect EffectAbilityIncrease(Ability nAbilityToIncrease, int nModifyBy)
        {
            Internal.NativeFunctions.StackPushInteger(nModifyBy);
            Internal.NativeFunctions.StackPushInteger((int)nAbilityToIncrease);
            Internal.NativeFunctions.CallBuiltIn(80);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Damage Resistance effect that removes the first nAmount points of
        ///  damage of type nDamageType, up to nLimit (or infinite if nLimit is 0)
        ///  - nDamageType: DAMAGE_TYPE_*
        ///  - nAmount
        ///  - nLimit
        /// </summary>
        public static Effect EffectDamageResistance(DamageType nDamageType, int nAmount, int nLimit = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nLimit);
            Internal.NativeFunctions.StackPushInteger(nAmount);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(81);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Resurrection effect. This should be applied as an instantaneous effect.
        /// </summary>
        public static Effect EffectResurrection()
        {
            Internal.NativeFunctions.CallBuiltIn(82);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Summon Creature effect.  The creature is created and placed into the
        ///  caller's party/faction.
        ///  - sCreatureResref: Identifies the creature to be summoned
        ///  - nVisualEffectId: VFX_*
        ///  - fDelaySeconds: There can be delay between the visual effect being played, and the
        ///    creature being added to the area
        ///  - nUseAppearAnimation: should this creature play it's "appear" animation when it is
        ///    summoned. If zero, it will just fade in somewhere near the target.  If the value is 1
        ///    it will use the appear animation, and if it's 2 it will use appear2 (which doesn't exist for most creatures)
        /// </summary>
        public static Effect EffectSummonCreature(string sCreatureResref, Vfx nVisualEffectId = Vfx.None, float fDelaySeconds = 0.0f, int nUseAppearAnimation = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nUseAppearAnimation);
            Internal.NativeFunctions.StackPushFloat(fDelaySeconds);
            Internal.NativeFunctions.StackPushInteger((int)nVisualEffectId);
            Internal.NativeFunctions.StackPushStringUTF8(sCreatureResref);
            Internal.NativeFunctions.CallBuiltIn(83);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the level at which this creature cast it's last spell (or spell-like ability)
        ///  * Return value on error, or if oCreature has not yet cast a spell: 0;
        /// </summary>
        public static int GetCasterLevel(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(84);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the first in-game effect on oCreature.
        /// </summary>
        public static Effect GetFirstEffect(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(85);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the next in-game effect on oCreature.
        /// </summary>
        public static Effect GetNextEffect(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(86);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Remove eEffect from oCreature.
        ///  * No return value
        /// </summary>
        public static void RemoveEffect(NWGameObject oCreature, NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(87);
        }

        /// <summary>
        ///  * Returns true if eEffect is a valid effect. The effect must have been applied to
        ///  * an object or else it will return false
        /// </summary>
        public static bool GetIsEffectValid(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(88);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the duration type (DURATION_TYPE_*) of eEffect.
        ///  * Return value if eEffect is not valid: -1
        /// </summary>
        public static DurationType GetEffectDurationType(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(89);
            return (DurationType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the subtype (SUBTYPE_*) of eEffect.
        ///  * Return value on error: 0
        /// </summary>
        public static SubType GetEffectSubType(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(90);
            return (SubType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the object that created eEffect.
        ///  * Returns OBJECT_INVALID if eEffect is not a valid effect.
        /// </summary>
        public static NWGameObject GetEffectCreator(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(91);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the first object in oArea.
        ///  If no valid area is specified, it will use the caller's area.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetFirstObjectInArea(NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(93);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the next object in oArea.
        ///  If no valid area is specified, it will use the caller's area.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetNextObjectInArea(NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(94);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the magnitude of vVector; this can be used to determine the
        ///  distance between two points.
        ///  * Return value on error: 0.0f
        /// </summary>
        public static float VectorMagnitude(NWN.Vector? vVector)
        {
            Internal.NativeFunctions.StackPushVector(vVector.HasValue ? vVector.Value : new NWN.Vector());
            Internal.NativeFunctions.CallBuiltIn(104);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Get the metamagic type (METAMAGIC_*) of the last spell cast by the caller
        ///  * Return value if the caster is not a valid object: -1
        /// </summary>
        public static MetaMagic GetMetaMagicFeat()
        {
            Internal.NativeFunctions.CallBuiltIn(105);
            return (MetaMagic)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the object type (OBJECT_TYPE_*) of oTarget
        ///  * Return value if oTarget is not a valid object: -1
        /// </summary>
        public static ObjectType GetObjectType(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(106);
            return (ObjectType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the racial type (RACIAL_TYPE_*) of oCreature
        ///  * Return value if oCreature is not a valid creature: RACIAL_TYPE_INVALID
        /// </summary>
        public static RacialType GetRacialType(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(107);
            return (RacialType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Do a Fortitude Save check for the given DC
        ///  - oCreature
        ///  - nDC: Difficulty check
        ///  - nSaveType: SAVING_THROW_TYPE_*
        ///  - oSaveVersus
        ///  Returns: 0 if the saving throw roll failed
        ///  Returns: 1 if the saving throw roll succeeded
        ///  Returns: 2 if the target was immune to the save type specified
        ///  Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass
        ///  GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static int FortitudeSave(NWGameObject oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.None, NWGameObject oSaveVersus = null)
        {
            Internal.NativeFunctions.StackPushObject(oSaveVersus != null ? oSaveVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSaveType);
            Internal.NativeFunctions.StackPushInteger(nDC);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(108);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Does a Reflex Save check for the given DC
        ///  - oCreature
        ///  - nDC: Difficulty check
        ///  - nSaveType: SAVING_THROW_TYPE_*
        ///  - oSaveVersus
        ///  Returns: 0 if the saving throw roll failed
        ///  Returns: 1 if the saving throw roll succeeded
        ///  Returns: 2 if the target was immune to the save type specified
        ///  Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass
        ///  GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static int ReflexSave(NWGameObject oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.None, NWGameObject oSaveVersus = null)
        {
            Internal.NativeFunctions.StackPushObject(oSaveVersus != null ? oSaveVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSaveType);
            Internal.NativeFunctions.StackPushInteger(nDC);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(109);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Does a Will Save check for the given DC
        ///  - oCreature
        ///  - nDC: Difficulty check
        ///  - nSaveType: SAVING_THROW_TYPE_*
        ///  - oSaveVersus
        ///  Returns: 0 if the saving throw roll failed
        ///  Returns: 1 if the saving throw roll succeeded
        ///  Returns: 2 if the target was immune to the save type specified
        ///  Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass
        ///  GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static int WillSave(NWGameObject oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.None, NWGameObject oSaveVersus = null)
        {
            Internal.NativeFunctions.StackPushObject(oSaveVersus != null ? oSaveVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSaveType);
            Internal.NativeFunctions.StackPushInteger(nDC);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(110);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the DC to save against for a spell (10 + spell level + relevant ability
        ///  bonus).  This can be called by a creature or by an Area of Effect object.
        /// </summary>
        public static int GetSpellSaveDC()
        {
            Internal.NativeFunctions.CallBuiltIn(111);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the subtype of eEffect to Magical and return eEffect.
        ///  (Effects default to magical if the subtype is not set)
        ///  Magical effects are removed by resting, and by dispel magic
        /// </summary>
        public static Effect MagicalEffect(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(112);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Set the subtype of eEffect to Supernatural and return eEffect.
        ///  (Effects default to magical if the subtype is not set)
        ///  Permanent supernatural effects are not removed by resting
        /// </summary>
        public static Effect SupernaturalEffect(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(113);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Set the subtype of eEffect to Extraordinary and return eEffect.
        ///  (Effects default to magical if the subtype is not set)
        ///  Extraordinary effects are removed by resting, but not by dispel magic
        /// </summary>
        public static Effect ExtraordinaryEffect(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(114);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create an AC Increase effect
        ///  - nValue: size of AC increase
        ///  - nModifyType: AC_*_BONUS
        ///  - nDamageType: DAMAGE_TYPE_*
        ///    * Default value for nDamageType should only ever be used in this function prototype.
        /// </summary>
        public static Effect EffectACIncrease(int nValue, AC nModifyType = AC.DodgeBonus, DamageType nDamageType = DamageType.ACVsDamageTypeAll)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger((int)nModifyType);
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.CallBuiltIn(115);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  If oObject is a creature, this will return that creature's armour class
        ///  If oObject is an item, door or placeable, this will return zero.
        ///  - nForFutureUse: this parameter is not currently used
        ///  * Return value if oObject is not a creature, item, door or placeable: -1
        /// </summary>
        public static int GetAC(NWGameObject oObject, int nForFutureUse = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nForFutureUse);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(116);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Create a Saving Throw Increase effect
        ///  - nSave: SAVING_THROW_* (not SAVING_THROW_TYPE_*)
        ///           SAVING_THROW_ALL
        ///           SAVING_THROW_FORT
        ///           SAVING_THROW_REFLEX
        ///           SAVING_THROW_WILL 
        ///  - nValue: size of the Saving Throw increase
        ///  - nSaveType: SAVING_THROW_TYPE_* (e.g. SAVING_THROW_TYPE_ACID )
        /// </summary>
        public static Effect EffectSavingThrowIncrease(SavingThrow nSave, int nValue, SavingThrowType nSaveType = SavingThrowType.All)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSaveType);
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.StackPushInteger((int)nSave);
            Internal.NativeFunctions.CallBuiltIn(117);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create an Attack Increase effect
        ///  - nBonus: size of attack bonus
        ///  - nModifierType: ATTACK_BONUS_*
        /// </summary>
        public static Effect EffectAttackIncrease(int nBonus, AttackBonus nModifierType = AttackBonus.Misc)
        {
            Internal.NativeFunctions.StackPushInteger((int)nModifierType);
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.CallBuiltIn(118);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Damage Reduction effect
        ///  - nAmount: amount of damage reduction
        ///  - nDamagePower: DAMAGE_POWER_*
        ///  - nLimit: How much damage the effect can absorb before disappearing.
        ///    Set to zero for infinite
        /// </summary>
        public static Effect EffectDamageReduction(int nAmount, DamagePower nDamagePower, int nLimit = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nLimit);
            Internal.NativeFunctions.StackPushInteger((int)nDamagePower);
            Internal.NativeFunctions.StackPushInteger(nAmount);
            Internal.NativeFunctions.CallBuiltIn(119);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Damage Increase effect
        ///  - nBonus: DAMAGE_BONUS_*
        ///  - nDamageType: DAMAGE_TYPE_*
        ///  NOTE! You *must* use the DAMAGE_BONUS_* constants! Using other values may
        ///        result in odd behaviour.
        /// </summary>
        public static Effect EffectDamageIncrease(int nBonus, DamageType nDamageType = DamageType.Magical)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.CallBuiltIn(120);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Convert nRounds into a number of seconds
        ///  A round is always 6.0 seconds
        /// </summary>
        public static float RoundsToSeconds(int nRounds)
        {
            Internal.NativeFunctions.StackPushInteger(nRounds);
            Internal.NativeFunctions.CallBuiltIn(121);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Convert nHours into a number of seconds
        ///  The result will depend on how many minutes there are per hour (game-time)
        /// </summary>
        public static float HoursToSeconds(int nHours)
        {
            Internal.NativeFunctions.StackPushInteger(nHours);
            Internal.NativeFunctions.CallBuiltIn(122);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Convert nTurns into a number of seconds
        ///  A turn is always 60.0 seconds
        /// </summary>
        public static float TurnsToSeconds(int nTurns)
        {
            Internal.NativeFunctions.StackPushInteger(nTurns);
            Internal.NativeFunctions.CallBuiltIn(123);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) to represent oCreature's
        ///  Law/Chaos alignment
        ///  (100=law, 0=chaos)
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static int GetLawChaosValue(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(124);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) to represent oCreature's
        ///  Good/Evil alignment
        ///  (100=good, 0=evil)
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static int GetGoodEvilValue(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(125);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Return an ALIGNMENT_* constant to represent oCreature's law/chaos alignment
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static Alignment GetAlignmentLawChaos(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(126);
            return (Alignment)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Return an ALIGNMENT_* constant to represent oCreature's good/evil alignment
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static Alignment GetAlignmentGoodEvil(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(127);
            return (Alignment)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the first object in nShape
        ///  - nShape: SHAPE_*
        ///  - fSize:
        ///    -> If nShape == SHAPE_SPHERE, this is the radius of the sphere
        ///    -> If nShape == SHAPE_SPELLCYLINDER, this is the length of the cylinder
        ///       Spell Cylinder's always have a radius of 1.5m.
        ///    -> If nShape == SHAPE_CONE, this is the widest radius of the cone
        ///    -> If nShape == SHAPE_SPELLCONE, this is the length of the cone in the
        ///       direction of lTarget. Spell cones are always 60 degrees with the origin
        ///       at OBJECT_SELF.
        ///    -> If nShape == SHAPE_CUBE, this is half the length of one of the sides of
        ///       the cube
        ///  - lTarget: This is the centre of the effect, usually GetSpellTargetLocation(),
        ///    or the end of a cylinder or cone.
        ///  - bLineOfSight: This controls whether to do a line-of-sight check on the
        ///    object returned. Line of sight check is done from origin to target object
        ///    at a height 1m above the ground
        ///    (This can be used to ensure that spell effects do not go through walls.)
        ///  - nObjectFilter: This allows you to filter out undesired object types, using
        ///    bitwise "or".
        ///    For example, to return only creatures and doors, the value for this
        ///    parameter would be ObjectType.Creature | OBJECT_TYPE_DOOR
        ///  - vOrigin: This is only used for cylinders and cones, and specifies the
        ///    origin of the effect(normally the spell-caster's position).
        ///  Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetFirstObjectInShape(Shape nShape, float fSize, NWN.Location lTarget, bool bLineOfSight = false, ObjectType nObjectFilter = ObjectType.Creature, NWN.Vector? vOrigin = null)
        {
            Internal.NativeFunctions.StackPushVector(vOrigin.HasValue ? vOrigin.Value : new NWN.Vector());
            Internal.NativeFunctions.StackPushInteger((int)nObjectFilter);
            Internal.NativeFunctions.StackPushInteger(bLineOfSight ? 1 : 0);
            Internal.NativeFunctions.StackPushLocation(lTarget.Handle);
            Internal.NativeFunctions.StackPushFloat(fSize);
            Internal.NativeFunctions.StackPushInteger((int)nShape);
            Internal.NativeFunctions.CallBuiltIn(128);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the next object in nShape
        ///  - nShape: SHAPE_*
        ///  - fSize:
        ///    -> If nShape == SHAPE_SPHERE, this is the radius of the sphere
        ///    -> If nShape == SHAPE_SPELLCYLINDER, this is the length of the cylinder.
        ///       Spell Cylinder's always have a radius of 1.5m.
        ///    -> If nShape == SHAPE_CONE, this is the widest radius of the cone
        ///    -> If nShape == SHAPE_SPELLCONE, this is the length of the cone in the
        ///       direction of lTarget. Spell cones are always 60 degrees with the origin
        ///       at OBJECT_SELF.
        ///    -> If nShape == SHAPE_CUBE, this is half the length of one of the sides of
        ///       the cube
        ///  - lTarget: This is the centre of the effect, usually GetSpellTargetLocation(),
        ///    or the end of a cylinder or cone.
        ///  - bLineOfSight: This controls whether to do a line-of-sight check on the
        ///    object returned. (This can be used to ensure that spell effects do not go
        ///    through walls.) Line of sight check is done from origin to target object
        ///    at a height 1m above the ground
        ///  - nObjectFilter: This allows you to filter out undesired object types, using
        ///    bitwise "or". For example, to return only creatures and doors, the value for
        ///    this parameter would be ObjectType.Creature | OBJECT_TYPE_DOOR
        ///  - vOrigin: This is only used for cylinders and cones, and specifies the origin
        ///    of the effect (normally the spell-caster's position).
        ///  Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetNextObjectInShape(Shape nShape, float fSize, NWN.Location lTarget, bool bLineOfSight = false, ObjectType nObjectFilter = ObjectType.Creature, NWN.Vector? vOrigin = null)
        {
            Internal.NativeFunctions.StackPushVector(vOrigin.HasValue ? vOrigin.Value : new NWN.Vector());
            Internal.NativeFunctions.StackPushInteger((int)nObjectFilter);
            Internal.NativeFunctions.StackPushInteger(bLineOfSight ? 1 : 0);
            Internal.NativeFunctions.StackPushLocation(lTarget.Handle);
            Internal.NativeFunctions.StackPushFloat(fSize);
            Internal.NativeFunctions.StackPushInteger((int)nShape);
            Internal.NativeFunctions.CallBuiltIn(129);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Create an Entangle effect
        ///  When applied, this effect will restrict the creature's movement and apply a
        ///  (-2) to all attacks and a -4 to AC.
        /// </summary>
        public static Effect EffectEntangle()
        {
            Internal.NativeFunctions.CallBuiltIn(130);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Causes object oObject to run the event evToRun. The script on the object that is
        ///  associated with the event specified will run.
        ///  Events can be created using the following event functions:
        ///     EventActivateItem() - This creates an OnActivateItem module event. The script for handling
        ///                           this event can be set in Module Properties on the Event Tab.
        ///     EventConversation() - This creates on OnConversation creature event. The script for handling
        ///                           this event can be set by viewing the Creature Properties on a 
        ///                           creature and then clicking on the Scripts Tab.
        ///     EventSpellCastAt()  - This creates an OnSpellCastAt event. The script for handling this
        ///                           event can be set in the Scripts Tab of the Properties menu 
        ///                           for the object.
        ///     EventUserDefined()  - This creates on OnUserDefined event. The script for handling this event
        ///                           can be set in the Scripts Tab of the Properties menu for the object/area/module.
        /// </summary>
        public static void SignalEvent(NWGameObject oObject, NWN.Event evToRun)
        {
            Internal.NativeFunctions.StackPushEvent(evToRun.Handle);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(131);
        }

        /// <summary>
        ///  Create an event of the type nUserDefinedEventNumber
        ///  Note: This only creates the event. The event wont actually trigger until SignalEvent()
        ///  is called using this created UserDefined event as an argument.
        ///  For example:
        ///      SignalEvent(oObject, EventUserDefined(9999));
        ///  Once the event has been signaled. The script associated with the OnUserDefined event will
        ///  run on the object oObject.
        /// 
        ///  To specify the OnUserDefined script that should run, view the object's Properties
        ///  and click on the Scripts Tab. Then specify a script for the OnUserDefined event.
        ///  From inside the OnUserDefined script call:
        ///     GetUserDefinedEventNumber() to retrieve the value of nUserDefinedEventNumber
        ///     that was used when the event was signaled.
        /// </summary>
        public static NWN.Event EventUserDefined(int nUserDefinedEventNumber)
        {
            Internal.NativeFunctions.StackPushInteger(nUserDefinedEventNumber);
            Internal.NativeFunctions.CallBuiltIn(132);
            return new NWN.Event(Internal.NativeFunctions.StackPopEvent());
        }

        /// <summary>
        ///  Create a Death effect
        ///  - nSpectacularDeath: if this is true, the creature to which this effect is
        ///    applied will die in an extraordinary fashion
        ///  - nDisplayFeedback
        /// </summary>
        public static Effect EffectDeath(bool nSpectacularDeath = false, bool nDisplayFeedback = true)
        {
            Internal.NativeFunctions.StackPushInteger(nDisplayFeedback ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(nSpectacularDeath ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(133);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Knockdown effect
        ///  This effect knocks creatures off their feet, they will sit until the effect
        ///  is removed. This should be applied as a temporary effect with a 3 second
        ///  duration minimum (1 second to fall, 1 second sitting, 1 second to get up).
        /// </summary>
        public static Effect EffectKnockdown()
        {
            Internal.NativeFunctions.CallBuiltIn(134);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Give oItem to oGiveTo
        ///  If oItem is not a valid item, or oGiveTo is not a valid object, nothing will
        ///  happen.
        /// </summary>
        public static void ActionGiveItem(NWGameObject oItem, NWGameObject oGiveTo)
        {
            Internal.NativeFunctions.StackPushObject(oGiveTo != null ? oGiveTo.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(135);
        }

        /// <summary>
        ///  Take oItem from oTakeFrom
        ///  If oItem is not a valid item, or oTakeFrom is not a valid object, nothing
        ///  will happen.
        /// </summary>
        public static void ActionTakeItem(NWGameObject oItem, NWGameObject oTakeFrom)
        {
            Internal.NativeFunctions.StackPushObject(oTakeFrom != null ? oTakeFrom.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(136);
        }

        /// <summary>
        ///  Normalize vVector
        /// </summary>
        public static NWN.Vector VectorNormalize(NWN.Vector? vVector)
        {
            Internal.NativeFunctions.StackPushVector(vVector.HasValue ? vVector.Value : new NWN.Vector());
            Internal.NativeFunctions.CallBuiltIn(137);
            return Internal.NativeFunctions.StackPopVector();
        }

        /// <summary>
        ///  Create a Curse effect.
        ///  - nStrMod: strength modifier
        ///  - nDexMod: dexterity modifier
        ///  - nConMod: constitution modifier
        ///  - nIntMod: intelligence modifier
        ///  - nWisMod: wisdom modifier
        ///  - nChaMod: charisma modifier
        /// </summary>
        public static Effect EffectCurse(int nStrMod = 1, int nDexMod = 1, int nConMod = 1, int nIntMod = 1, int nWisMod = 1, int nChaMod = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nChaMod);
            Internal.NativeFunctions.StackPushInteger(nWisMod);
            Internal.NativeFunctions.StackPushInteger(nIntMod);
            Internal.NativeFunctions.StackPushInteger(nConMod);
            Internal.NativeFunctions.StackPushInteger(nDexMod);
            Internal.NativeFunctions.StackPushInteger(nStrMod);
            Internal.NativeFunctions.CallBuiltIn(138);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the ability score of type nAbility for a creature (otherwise 0)
        ///  - oCreature: the creature whose ability score we wish to find out
        ///  - nAbilityType: ABILITY_*
        ///  - nBaseAbilityScore: if set to true will return the base ability score without
        ///                       bonuses (e.g. ability bonuses granted from equipped items).
        ///  Return value on error: 0
        /// </summary>
        public static int GetAbilityScore(NWGameObject oCreature, Ability nAbilityType, bool nBaseAbilityScore = false)
        {
            Internal.NativeFunctions.StackPushInteger(nBaseAbilityScore ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nAbilityType);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(139);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  * Returns true if oCreature is a dead NPC, dead PC or a dying PC.
        /// </summary>
        public static bool GetIsDead(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(140);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Create a vector with the specified values for x, y and z
        /// </summary>
        public static NWN.Vector Vector(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(z);
            Internal.NativeFunctions.StackPushFloat(y);
            Internal.NativeFunctions.StackPushFloat(x);
            Internal.NativeFunctions.CallBuiltIn(142);
            return Internal.NativeFunctions.StackPopVector();
        }

        /// <summary>
        ///  Cause the caller to face vTarget
        /// </summary>
        public static void SetFacingPoint(NWN.Vector? vTarget)
        {
            Internal.NativeFunctions.StackPushVector(vTarget.HasValue ? vTarget.Value : new NWN.Vector());
            Internal.NativeFunctions.CallBuiltIn(143);
        }

        /// <summary>
        ///  Convert fAngle to a vector
        /// </summary>
        public static NWN.Vector AngleToVector(float fAngle)
        {
            Internal.NativeFunctions.StackPushFloat(fAngle);
            Internal.NativeFunctions.CallBuiltIn(144);
            return Internal.NativeFunctions.StackPopVector();
        }

        /// <summary>
        ///  Convert vVector to an angle
        /// </summary>
        public static float VectorToAngle(NWN.Vector? vVector)
        {
            Internal.NativeFunctions.StackPushVector(vVector.HasValue ? vVector.Value : new NWN.Vector());
            Internal.NativeFunctions.CallBuiltIn(145);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  The caller will perform a Melee Touch Attack on oTarget
        ///  This is not an action, and it assumes the caller is already within range of
        ///  oTarget
        ///  * Returns 0 on a miss, 1 on a hit and 2 on a critical hit
        /// </summary>
        public static int TouchAttackMelee(NWGameObject oTarget, bool bDisplayFeedback = true)
        {
            Internal.NativeFunctions.StackPushInteger(bDisplayFeedback ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(146);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  The caller will perform a Ranged Touch Attack on oTarget
        ///  * Returns 0 on a miss, 1 on a hit and 2 on a critical hit
        /// </summary>
        public static int TouchAttackRanged(NWGameObject oTarget, bool bDisplayFeedback = true)
        {
            Internal.NativeFunctions.StackPushInteger(bDisplayFeedback ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(147);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Create a Paralyze effect
        /// </summary>
        public static Effect EffectParalyze()
        {
            Internal.NativeFunctions.CallBuiltIn(148);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Spell Immunity effect.
        ///  There is a known bug with this function. There *must* be a parameter specified
        ///  when this is called (even if the desired parameter is SPELL_ALL_SPELLS),
        ///  otherwise an effect of type EFFECT_TYPE_INVALIDEFFECT will be returned.
        ///  - nImmunityToSpell: SPELL_*
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nImmunityToSpell is
        ///    invalid.
        /// </summary>
        public static Effect EffectSpellImmunity(Spell nImmunityToSpell = Spell.AllSpells)
        {
            Internal.NativeFunctions.StackPushInteger((int)nImmunityToSpell);
            Internal.NativeFunctions.CallBuiltIn(149);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Deaf effect
        /// </summary>
        public static Effect EffectDeaf()
        {
            Internal.NativeFunctions.CallBuiltIn(150);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the distance in metres between oObjectA and oObjectB.
        ///  * Return value if either object is invalid: 0.0f
        /// </summary>
        public static float GetDistanceBetween(NWGameObject oObjectA, NWGameObject oObjectB)
        {
            Internal.NativeFunctions.StackPushObject(oObjectB != null ? oObjectB.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oObjectA != null ? oObjectA.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(151);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Set oObject's local location variable sVarname to lValue
        /// </summary>
        public static void SetLocalLocation(NWGameObject oObject, string sVarName, NWN.Location lValue)
        {
            Internal.NativeFunctions.StackPushLocation(lValue.Handle);
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(152);
        }

        /// <summary>
        ///  Get oObject's local location variable sVarname
        /// </summary>
        public static NWN.Location GetLocalLocation(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(153);
            return new NWN.Location(Internal.NativeFunctions.StackPopLocation());
        }

        /// <summary>
        ///  Create a Sleep effect
        /// </summary>
        public static Effect EffectSleep()
        {
            Internal.NativeFunctions.CallBuiltIn(154);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the object which is in oCreature's specified inventory slot
        ///  - nInventorySlot: INVENTORY_SLOT_*
        ///  - oCreature
        ///  * Returns OBJECT_INVALID if oCreature is not a valid creature or there is no
        ///    item in nInventorySlot.
        /// </summary>
        public static NWGameObject GetItemInSlot(InventorySlot nInventorySlot, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nInventorySlot);
            Internal.NativeFunctions.CallBuiltIn(155);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Create a Charm effect
        /// </summary>
        public static Effect EffectCharmed()
        {
            Internal.NativeFunctions.CallBuiltIn(156);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Confuse effect
        /// </summary>
        public static Effect EffectConfused()
        {
            Internal.NativeFunctions.CallBuiltIn(157);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Frighten effect
        /// </summary>
        public static Effect EffectFrightened()
        {
            Internal.NativeFunctions.CallBuiltIn(158);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Dominate effect
        /// </summary>
        public static Effect EffectDominated()
        {
            Internal.NativeFunctions.CallBuiltIn(159);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Daze effect
        /// </summary>
        public static Effect EffectDazed()
        {
            Internal.NativeFunctions.CallBuiltIn(160);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Stun effect
        /// </summary>
        public static Effect EffectStunned()
        {
            Internal.NativeFunctions.CallBuiltIn(161);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Set whether oTarget's action stack can be modified
        /// </summary>
        public static void SetCommandable(bool bCommandable, NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(bCommandable ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(162);
        }

        /// <summary>
        ///  Determine whether oTarget's action stack can be modified.
        /// </summary>
        public static bool GetCommandable(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(163);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Create a Regenerate effect.
        ///  - nAmount: amount of damage to be regenerated per time interval
        ///  - fIntervalSeconds: length of interval in seconds
        /// </summary>
        public static Effect EffectRegenerate(int nAmount, float fIntervalSeconds)
        {
            Internal.NativeFunctions.StackPushFloat(fIntervalSeconds);
            Internal.NativeFunctions.StackPushInteger(nAmount);
            Internal.NativeFunctions.CallBuiltIn(164);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Movement Speed Increase effect.
        ///  - nPercentChange - range 0 through 99
        ///  eg.
        ///     0 = no change in speed
        ///    50 = 50% faster
        ///    99 = almost twice as fast
        /// </summary>
        public static Effect EffectMovementSpeedIncrease(int nPercentChange)
        {
            Internal.NativeFunctions.StackPushInteger(nPercentChange);
            Internal.NativeFunctions.CallBuiltIn(165);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the number of hitdice for oCreature.
        ///  * Return value if oCreature is not a valid creature: 0
        /// </summary>
        public static int GetHitDice(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(166);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  The action subject will follow oFollow until a ClearAllActions() is called.
        ///  - oFollow: this is the object to be followed
        ///  - fFollowDistance: follow distance in metres
        ///  * No return value
        /// </summary>
        public static void ActionForceFollowObject(NWGameObject oFollow, float fFollowDistance = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fFollowDistance);
            Internal.NativeFunctions.StackPushObject(oFollow != null ? oFollow.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(167);
        }

        /// <summary>
        ///  Get the Tag of oObject
        ///  * Return value if oObject is not a valid object: ""
        /// </summary>
        public static string GetTag(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(168);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Do a Spell Resistance check between oCaster and oTarget, returning true if
        ///  the spell was resisted.
        ///  * Return value if oCaster or oTarget is an invalid object: false
        ///  * Return value if spell cast is not a player spell: - 1
        ///  * Return value if spell resisted: 1
        ///  * Return value if spell resisted via magic immunity: 2
        ///  * Return value if spell resisted via spell absorption: 3
        /// </summary>
        public static int ResistSpell(NWGameObject oCaster, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oCaster != null ? oCaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(169);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the effect type (EFFECT_TYPE_*) of eEffect.
        ///  * Return value if eEffect is invalid: EFFECT_INVALIDEFFECT
        /// </summary>
        public static EffectType GetEffectType(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(170);
            return (EffectType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Create an Area Of Effect effect in the area of the creature it is applied to.
        ///  If the scripts are not specified, default ones will be used.
        /// </summary>
        public static Effect EffectAreaOfEffect(int nAreaEffectId, string sOnEnterScript = "", string sHeartbeatScript = "", string sOnExitScript = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sOnExitScript);
            Internal.NativeFunctions.StackPushStringUTF8(sHeartbeatScript);
            Internal.NativeFunctions.StackPushStringUTF8(sOnEnterScript);
            Internal.NativeFunctions.StackPushInteger(nAreaEffectId);
            Internal.NativeFunctions.CallBuiltIn(171);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  * Returns true if the Faction Ids of the two objects are the same
        /// </summary>
        public static int GetFactionEqual(NWGameObject oFirstObject, NWGameObject oSecondObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oSecondObject != null ? oSecondObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oFirstObject != null ? oFirstObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(172);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Make oObjectToChangeFaction join the faction of oMemberOfFactionToJoin.
        ///  NB. ** This will only work for two NPCs **
        /// </summary>
        public static void ChangeFaction(NWGameObject oObjectToChangeFaction, NWGameObject oMemberOfFactionToJoin)
        {
            Internal.NativeFunctions.StackPushObject(oMemberOfFactionToJoin != null ? oMemberOfFactionToJoin.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oObjectToChangeFaction != null ? oObjectToChangeFaction.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(173);
        }

        /// <summary>
        ///  * Returns true if oObject is listening for something
        /// </summary>
        public static bool GetIsListening(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(174);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Set whether oObject is listening.
        /// </summary>
        public static void SetListening(NWGameObject oObject, bool bValue)
        {
            Internal.NativeFunctions.StackPushInteger(bValue ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(175);
        }

        /// <summary>
        ///  Set the string for oObject to listen for.
        ///  Note: this does not set oObject to be listening.
        /// </summary>
        public static void SetListenPattern(NWGameObject oObject, string sPattern, int nNumber = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nNumber);
            Internal.NativeFunctions.StackPushString(sPattern);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(176);
        }

        /// <summary>
        ///  Get the appropriate matched string (this should only be used in
        ///  OnConversation scripts).
        ///  * Returns the appropriate matched string, otherwise returns ""
        /// </summary>
        public static string GetMatchedSubstring(int nString)
        {
            Internal.NativeFunctions.StackPushInteger(nString);
            Internal.NativeFunctions.CallBuiltIn(178);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Get the number of string parameters available.
        ///  * Returns -1 if no string matched (this could be because of a dialogue event)
        /// </summary>
        public static int GetMatchedSubstringsCount()
        {
            Internal.NativeFunctions.CallBuiltIn(179);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  * Create a Visual Effect that can be applied to an object.
        ///  - nVisualEffectId
        ///  - nMissEffect: if this is true, a random vector near or past the target will
        ///    be generated, on which to play the effect
        /// </summary>
        public static Effect EffectVisualEffect(Vfx nVisualEffectId, bool nMissEffect = false)
        {
            Internal.NativeFunctions.StackPushInteger(nMissEffect ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nVisualEffectId);
            Internal.NativeFunctions.CallBuiltIn(180);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the weakest member of oFactionMember's faction.
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static NWGameObject GetFactionWeakestMember(NWGameObject oFactionMember = null, bool bMustBeVisible = true)
        {
            Internal.NativeFunctions.StackPushInteger(bMustBeVisible ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(181);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the strongest member of oFactionMember's faction.
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static NWGameObject GetFactionStrongestMember(NWGameObject oFactionMember = null, bool bMustBeVisible = true)
        {
            Internal.NativeFunctions.StackPushInteger(bMustBeVisible ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(182);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the member of oFactionMember's faction that has taken the most hit points
        ///  of damage.
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static NWGameObject GetFactionMostDamagedMember(NWGameObject oFactionMember = null, bool bMustBeVisible = true)
        {
            Internal.NativeFunctions.StackPushInteger(bMustBeVisible ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(183);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the member of oFactionMember's faction that has taken the fewest hit
        ///  points of damage.
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static NWGameObject GetFactionLeastDamagedMember(NWGameObject oFactionMember = null, bool bMustBeVisible = true)
        {
            Internal.NativeFunctions.StackPushInteger(bMustBeVisible ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(184);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the amount of gold held by oFactionMember's faction.
        ///  * Returns -1 if oFactionMember's faction is invalid.
        /// </summary>
        public static int GetFactionGold(NWGameObject oFactionMember)
        {
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(185);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents how
        ///  oSourceFactionMember's faction feels about oTarget.
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageReputation(NWGameObject oSourceFactionMember, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oSourceFactionMember != null ? oSourceFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(186);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents the average
        ///  good/evil alignment of oFactionMember's faction.
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageGoodEvilAlignment(NWGameObject oFactionMember)
        {
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(187);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents the average
        ///  law/chaos alignment of oFactionMember's faction.
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageLawChaosAlignment(NWGameObject oFactionMember)
        {
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(188);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the average level of the members of the faction.
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageLevel(NWGameObject oFactionMember)
        {
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(189);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the average XP of the members of the faction.
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageXP(NWGameObject oFactionMember)
        {
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(190);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the most frequent class in the faction - this can be compared with the
        ///  constants CLASS_TYPE_*.
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionMostFrequentClass(NWGameObject oFactionMember)
        {
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(191);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the object faction member with the lowest armour class.
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static NWGameObject GetFactionWorstAC(NWGameObject oFactionMember = null, bool bMustBeVisible = true)
        {
            Internal.NativeFunctions.StackPushInteger(bMustBeVisible ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(192);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the object faction member with the highest armour class.
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static NWGameObject GetFactionBestAC(NWGameObject oFactionMember = null, bool bMustBeVisible = true)
        {
            Internal.NativeFunctions.StackPushInteger(bMustBeVisible ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oFactionMember != null ? oFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(193);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Sit in oChair.
        ///  Note: Not all creatures will be able to sit and not all
        ///        objects can be sat on.
        ///        The object oChair must also be marked as usable in the toolset.
        /// 
        ///  For Example: To get a player to sit in oChair when they click on it,
        ///  place the following script in the OnUsed event for the object oChair.
        ///  void main()
        ///  {
        ///     object oChair = OBJECT_SELF;
        ///     AssignCommand(GetLastUsedBy(),ActionSit(oChair));
        ///  }
        /// </summary>
        public static void ActionSit(NWGameObject oChair)
        {
            Internal.NativeFunctions.StackPushObject(oChair != null ? oChair.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(194);
        }

        /// <summary>
        ///  In an onConversation script this gets the number of the string pattern
        ///  matched (the one that triggered the script).
        ///  * Returns -1 if no string matched
        /// </summary>
        public static int GetListenPatternNumber()
        {
            Internal.NativeFunctions.CallBuiltIn(195);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Jump to an object ID, or as near to it as possible.
        /// </summary>
        public static void ActionJumpToObject(NWGameObject oToJumpTo, bool bWalkStraightLineToPoint = true)
        {
            Internal.NativeFunctions.StackPushInteger(bWalkStraightLineToPoint ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oToJumpTo != null ? oToJumpTo.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(196);
        }

        /// <summary>
        ///  Get the first waypoint with the specified tag.
        ///  * Returns OBJECT_INVALID if the waypoint cannot be found.
        /// </summary>
        public static NWGameObject GetWaypointByTag(string sWaypointTag)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sWaypointTag);
            Internal.NativeFunctions.CallBuiltIn(197);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the destination object for the given object.
        /// 
        ///  All objects can hold a transition target, but only Doors and Triggers
        ///  will be made clickable by the game engine (This may change in the
        ///  future). You can set and query transition targets on other objects for
        ///  your own scripted purposes.
        /// 
        ///  * Returns OBJECT_INVALID if oTransition does not hold a target.
        /// </summary>
        public static NWGameObject GetTransitionTarget(NWGameObject oTransition)
        {
            Internal.NativeFunctions.StackPushObject(oTransition != null ? oTransition.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(198);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Link the two supplied effects, returning eChildEffect as a child of
        ///  eParentEffect.
        ///  Note: When applying linked effects if the target is immune to all valid
        ///  effects all other effects will be removed as well. This means that if you
        ///  apply a visual effect and a silence effect (in a link) and the target is
        ///  immune to the silence effect that the visual effect will get removed as well.
        ///  Visual Effects are not considered "valid" effects for the purposes of
        ///  determining if an effect will be removed or not and as such should never be
        ///  packaged *only* with other visual effects in a link.
        /// </summary>
        public static Effect EffectLinkEffects(NWN.Effect eChildEffect, NWN.Effect eParentEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eParentEffect.Handle);
            Internal.NativeFunctions.StackPushEffect(eChildEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(199);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the nNth object with the specified tag.
        ///  - sTag
        ///  - nNth: the nth object with this tag may be requested
        ///  * Returns OBJECT_INVALID if the object cannot be found.
        ///  Note: The module cannot be retrieved by GetObjectByTag(), use GetModule() instead.
        /// </summary>
        public static NWGameObject GetObjectByTag(string sTag, int nNth = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nNth);
            Internal.NativeFunctions.StackPushStringUTF8(sTag);
            Internal.NativeFunctions.CallBuiltIn(200);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Adjust the alignment of oSubject.
        ///  - oSubject
        ///  - nAlignment:
        ///    -> ALIGNMENT_LAWFUL/ALIGNMENT_CHAOTIC/ALIGNMENT_GOOD/ALIGNMENT_EVIL: oSubject's
        ///       alignment will be shifted in the direction specified
        ///    -> ALIGNMENT_ALL: nShift will be added to oSubject's law/chaos and
        ///       good/evil alignment values
        ///    -> ALIGNMENT_NEUTRAL: nShift is applied to oSubject's law/chaos and
        ///       good/evil alignment values in the direction which is towards neutrality.
        ///      e.g. If oSubject has a law/chaos value of 10 (i.e. chaotic) and a
        ///           good/evil value of 80 (i.e. good) then if nShift is 15, the
        ///           law/chaos value will become (10+15)=25 and the good/evil value will
        ///           become (80-25)=55
        ///      Furthermore, the shift will at most take the alignment value to 50 and
        ///      not beyond.
        ///      e.g. If oSubject has a law/chaos value of 40 and a good/evil value of 70,
        ///           then if nShift is 15, the law/chaos value will become 50 and the
        ///           good/evil value will become 55
        ///  - nShift: this is the desired shift in alignment
        ///  - bAllPartyMembers: when true the alignment shift of oSubject also has a 
        ///                      diminished affect all members of oSubject's party (if oSubject is a Player).
        ///                      When false the shift only affects oSubject.
        ///  * No return value
        /// </summary>
        public static void AdjustAlignment(NWGameObject oSubject, Alignment nAlignment, int nShift, bool bAllPartyMembers = true)
        {
            Internal.NativeFunctions.StackPushInteger(bAllPartyMembers ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(nShift);
            Internal.NativeFunctions.StackPushInteger((int)nAlignment);
            Internal.NativeFunctions.StackPushObject(oSubject != null ? oSubject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(201);
        }

        /// <summary>
        ///  Do nothing for fSeconds seconds.
        /// </summary>
        public static void ActionWait(float fSeconds)
        {
            Internal.NativeFunctions.StackPushFloat(fSeconds);
            Internal.NativeFunctions.CallBuiltIn(202);
        }

        /// <summary>
        ///  Set the transition bitmap of a player; this should only be called in area
        ///  transition scripts. This action should be run by the person "clicking" the
        ///  area transition via AssignCommand.
        ///  - nPredefinedAreaTransition:
        ///    -> To use a predefined area transition bitmap, use one of AREA_TRANSITION_*
        ///    -> To use a custom, user-defined area transition bitmap, use
        ///       AREA_TRANSITION_USER_DEFINED and specify the filename in the second
        ///       parameter
        ///  - sCustomAreaTransitionBMP: this is the filename of a custom, user-defined
        ///    area transition bitmap
        /// </summary>
        public static void SetAreaTransitionBMP(AreaTransition nPredefinedAreaTransition, string sCustomAreaTransitionBMP = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sCustomAreaTransitionBMP);
            Internal.NativeFunctions.StackPushInteger((int)nPredefinedAreaTransition);
            Internal.NativeFunctions.CallBuiltIn(203);
        }

        /// <summary>
        ///  Starts a conversation with oObjectToConverseWith - this will cause their
        ///  OnDialog event to fire.
        ///  - oObjectToConverseWith
        ///  - sDialogResRef: If this is blank, the creature's own dialogue file will be used
        ///  - bPrivateConversation
        ///  Turn off bPlayHello if you don't want the initial greeting to play
        /// </summary>
        public static void ActionStartConversation(NWGameObject oObjectToConverseWith, string sDialogResRef = "", bool bPrivateConversation = false, bool bPlayHello = true)
        {
            Internal.NativeFunctions.StackPushInteger(bPlayHello ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bPrivateConversation ? 1 : 0);
            Internal.NativeFunctions.StackPushStringUTF8(sDialogResRef);
            Internal.NativeFunctions.StackPushObject(oObjectToConverseWith != null ? oObjectToConverseWith.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(204);
        }

        /// <summary>
        ///  Pause the current conversation.
        /// </summary>
        public static void ActionPauseConversation()
        {
            Internal.NativeFunctions.CallBuiltIn(205);
        }

        /// <summary>
        ///  Resume a conversation after it has been paused.
        /// </summary>
        public static void ActionResumeConversation()
        {
            Internal.NativeFunctions.CallBuiltIn(206);
        }

        /// <summary>
        ///  Create a Beam effect.
        ///  - nBeamVisualEffect: VFX_BEAM_*
        ///  - oEffector: the beam is emitted from this creature
        ///  - nBodyPart: BODY_NODE_*
        ///  - bMissEffect: If this is true, the beam will fire to a random vector near or
        ///    past the target
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nBeamVisualEffect is
        ///    not valid.
        /// </summary>
        public static Effect EffectBeam(Vfx nBeamVisualEffect, NWGameObject oEffector, BodyNode nBodyPart, bool bMissEffect = false)
        {
            Internal.NativeFunctions.StackPushInteger(bMissEffect ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nBodyPart);
            Internal.NativeFunctions.StackPushObject(oEffector != null ? oEffector.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nBeamVisualEffect);
            Internal.NativeFunctions.CallBuiltIn(207);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents how oSource
        ///  feels about oTarget.
        ///  -> 0-10 means oSource is hostile to oTarget
        ///  -> 11-89 means oSource is neutral to oTarget
        ///  -> 90-100 means oSource is friendly to oTarget
        ///  * Returns -1 if oSource or oTarget does not identify a valid object
        /// </summary>
        public static int GetReputation(NWGameObject oSource, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(208);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Adjust how oSourceFactionMember's faction feels about oTarget by the
        ///  specified amount.
        ///  Note: This adjusts Faction Reputation, how the entire faction that
        ///  oSourceFactionMember is in, feels about oTarget.
        ///  * No return value
        ///  Note: You can't adjust a player character's (PC) faction towards
        ///        NPCs, so attempting to make an NPC hostile by passing in a PC object
        ///        as oSourceFactionMember in the following call will fail:
        ///        AdjustReputation(oNPC,oPC,-100);
        ///        Instead you should pass in the PC object as the first
        ///        parameter as in the following call which should succeed: 
        ///        AdjustReputation(oPC,oNPC,-100);
        ///  Note: Will fail if oSourceFactionMember is a plot object.
        /// </summary>
        public static void AdjustReputation(NWGameObject oTarget, NWGameObject oSourceFactionMember, int nAdjustment)
        {
            Internal.NativeFunctions.StackPushInteger(nAdjustment);
            Internal.NativeFunctions.StackPushObject(oSourceFactionMember != null ? oSourceFactionMember.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(209);
        }

        /// <summary>
        ///  Get the creature that is currently sitting on the specified object.
        ///  - oChair
        ///  * Returns OBJECT_INVALID if oChair is not a valid placeable.
        /// </summary>
        public static NWGameObject GetSittingCreature(NWGameObject oChair)
        {
            Internal.NativeFunctions.StackPushObject(oChair != null ? oChair.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(210);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the creature that is going to attack oTarget.
        ///  Note: This value is cleared out at the end of every combat round and should
        ///  not be used in any case except when getting a "going to be attacked" shout
        ///  from the master creature (and this creature is a henchman)
        ///  * Returns OBJECT_INVALID if oTarget is not a valid creature.
        /// </summary>
        public static NWGameObject GetGoingToBeAttackedBy(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(211);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Create a Spell Resistance Increase effect.
        ///  - nValue: size of spell resistance increase
        /// </summary>
        public static Effect EffectSpellResistanceIncrease(int nValue)
        {
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.CallBuiltIn(212);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the location of oObject.
        /// </summary>
        public static NWN.Location GetLocation(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(213);
            return new NWN.Location(Internal.NativeFunctions.StackPopLocation());
        }

        /// <summary>
        ///  The subject will jump to lLocation instantly (even between areas).
        ///  If lLocation is invalid, nothing will happen.
        /// </summary>
        public static void ActionJumpToLocation(NWN.Location lLocation)
        {
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.CallBuiltIn(214);
        }

        /// <summary>
        ///  Create a location.
        /// </summary>
        public static NWN.Location Location(NWGameObject oArea, NWN.Vector? vPosition, float fOrientation)
        {
            Internal.NativeFunctions.StackPushFloat(fOrientation);
            Internal.NativeFunctions.StackPushVector(vPosition.HasValue ? vPosition.Value : new NWN.Vector());
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(215);
            return new NWN.Location(Internal.NativeFunctions.StackPopLocation());
        }

        /// <summary>
        ///  Apply eEffect at lLocation.
        /// </summary>
        public static void ApplyEffectAtLocation(DurationType nDurationType, NWN.Effect eEffect, NWN.Location lLocation, float fDuration = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fDuration);
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.StackPushInteger((int)nDurationType);
            Internal.NativeFunctions.CallBuiltIn(216);
        }

        /// <summary>
        ///  Convert fFeet into a number of meters.
        /// </summary>
        public static float FeetToMeters(float fFeet)
        {
            Internal.NativeFunctions.StackPushFloat(fFeet);
            Internal.NativeFunctions.CallBuiltIn(218);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Convert fYards into a number of meters.
        /// </summary>
        public static float YardsToMeters(float fYards)
        {
            Internal.NativeFunctions.StackPushFloat(fYards);
            Internal.NativeFunctions.CallBuiltIn(219);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Apply eEffect to oTarget.
        /// </summary>
        public static void ApplyEffectToObject(DurationType nDurationType, NWN.Effect eEffect, NWGameObject oTarget, float fDuration = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fDuration);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.StackPushInteger((int)nDurationType);
            Internal.NativeFunctions.CallBuiltIn(220);
        }

        /// <summary>
        ///  The caller will immediately speak sStringToSpeak (this is different from
        ///  ActionSpeakString)
        ///  - sStringToSpeak
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void SpeakString(string sStringToSpeak, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTalkVolume);
            Internal.NativeFunctions.StackPushString(sStringToSpeak);
            Internal.NativeFunctions.CallBuiltIn(221);
        }

        /// <summary>
        ///  Get the location of the caller's last spell target.
        /// </summary>
        public static NWN.Location GetSpellTargetLocation()
        {
            Internal.NativeFunctions.CallBuiltIn(222);
            return new NWN.Location(Internal.NativeFunctions.StackPopLocation());
        }

        /// <summary>
        ///  Get the position vector from lLocation.
        /// </summary>
        public static NWN.Vector GetPositionFromLocation(NWN.Location lLocation)
        {
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.CallBuiltIn(223);
            return Internal.NativeFunctions.StackPopVector();
        }

        /// <summary>
        ///  Get the area's object ID from lLocation.
        /// </summary>
        public static NWGameObject GetAreaFromLocation(NWN.Location lLocation)
        {
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.CallBuiltIn(224);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the orientation value from lLocation.
        /// </summary>
        public static float GetFacingFromLocation(NWN.Location lLocation)
        {
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.CallBuiltIn(225);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Get the creature nearest to lLocation, subject to all the criteria specified.
        ///  - nFirstCriteriaType: CREATURE_TYPE_*
        ///  - nFirstCriteriaValue:
        ///    -> CLASS_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_CLASS
        ///    -> SPELL_* if nFirstCriteriaType was CREATURE_TYPE_DOES_NOT_HAVE_SPELL_EFFECT
        ///       or CREATURE_TYPE_HAS_SPELL_EFFECT
        ///    -> true or false if nFirstCriteriaType was CREATURE_TYPE_IS_ALIVE
        ///    -> PERCEPTION_* if nFirstCriteriaType was CREATURE_TYPE_PERCEPTION
        ///    -> PLAYER_CHAR_IS_PC or PLAYER_CHAR_NOT_PC if nFirstCriteriaType was
        ///       CREATURE_TYPE_PLAYER_CHAR
        ///    -> RACIAL_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_RACIAL_TYPE
        ///    -> REPUTATION_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_REPUTATION
        ///    For example, to get the nearest PC, use
        ///    (CREATURE_TYPE_PLAYER_CHAR, PLAYER_CHAR_IS_PC)
        ///  - lLocation: We're trying to find the creature of the specified type that is
        ///    nearest to lLocation
        ///  - nNth: We don't have to find the first nearest: we can find the Nth nearest....
        ///  - nSecondCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///    further specify the type of creature that we are looking for.
        ///  - nSecondCriteriaValue: This is used in the same way as nFirstCriteriaValue
        ///    to further specify the type of creature that we are looking for.
        ///  - nThirdCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///    further specify the type of creature that we are looking for.
        ///  - nThirdCriteriaValue: This is used in the same way as nFirstCriteriaValue to
        ///    further specify the type of creature that we are looking for.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetNearestCreatureToLocation(int nFirstCriteriaType, int nFirstCriteriaValue, NWN.Location lLocation, int nNth = 1, int nSecondCriteriaType = -1, int nSecondCriteriaValue = -1, int nThirdCriteriaType = -1, int nThirdCriteriaValue = -1)
        {
            Internal.NativeFunctions.StackPushInteger(nThirdCriteriaValue);
            Internal.NativeFunctions.StackPushInteger(nThirdCriteriaType);
            Internal.NativeFunctions.StackPushInteger(nSecondCriteriaValue);
            Internal.NativeFunctions.StackPushInteger(nSecondCriteriaType);
            Internal.NativeFunctions.StackPushInteger(nNth);
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.StackPushInteger(nFirstCriteriaValue);
            Internal.NativeFunctions.StackPushInteger(nFirstCriteriaType);
            Internal.NativeFunctions.CallBuiltIn(226);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the Nth object nearest to oTarget that is of the specified type.
        ///  - nObjectType: OBJECT_TYPE_*
        ///  - oTarget
        ///  - nNth
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetNearestObject(ObjectType nObjectType = ObjectType.All, NWGameObject oTarget = null, int nNth = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nNth);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nObjectType);
            Internal.NativeFunctions.CallBuiltIn(227);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the nNth object nearest to lLocation that is of the specified type.
        ///  - nObjectType: OBJECT_TYPE_*
        ///  - lLocation
        ///  - nNth
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetNearestObjectToLocation(ObjectType nObjectType, NWN.Location lLocation, int nNth = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nNth);
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.StackPushInteger((int)nObjectType);
            Internal.NativeFunctions.CallBuiltIn(228);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the nth Object nearest to oTarget that has sTag as its tag.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetNearestObjectByTag(string sTag, NWGameObject oTarget = null, int nNth = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nNth);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushStringUTF8(sTag);
            Internal.NativeFunctions.CallBuiltIn(229);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Cast spell nSpell at lTargetLocation.
        ///  - nSpell: SPELL_*
        ///  - lTargetLocation
        ///  - nMetaMagic: METAMAGIC_*
        ///  - bCheat: If this is true, then the executor of the action doesn't have to be
        ///    able to cast the spell.
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        ///  - bInstantSpell: If this is true, the spell is cast immediately; this allows
        ///    the end-user to simulate
        ///    a high-level magic user having lots of advance warning of impending trouble.
        /// </summary>
        public static void ActionCastSpellAtLocation(Spell nSpell, NWN.Location lTargetLocation, MetaMagic nMetaMagic = MetaMagic.Any, bool bCheat = false, ProjectilePathType nProjectilePathType = ProjectilePathType.Default, bool bInstantSpell = false)
        {
            Internal.NativeFunctions.StackPushInteger(bInstantSpell ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nProjectilePathType);
            Internal.NativeFunctions.StackPushInteger(bCheat ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nMetaMagic);
            Internal.NativeFunctions.StackPushLocation(lTargetLocation.Handle);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(234);
        }

        /// <summary>
        ///  * Returns true if oSource considers oTarget as an enemy.
        /// </summary>
        public static bool GetIsEnemy(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(235);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  * Returns true if oSource considers oTarget as a friend.
        /// </summary>
        public static bool GetIsFriend(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(236);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  * Returns true if oSource considers oTarget as neutral.
        /// </summary>
        public static bool GetIsNeutral(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(237);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the PC that is involved in the conversation.
        ///  * Returns OBJECT_INVALID on error.
        /// </summary>
        public static NWGameObject GetPCSpeaker()
        {
            Internal.NativeFunctions.CallBuiltIn(238);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get a string from the talk table using nStrRef.
        /// </summary>
        public static string GetStringByStrRef(int nStrRef, Gender nGender = Gender.Male)
        {
            Internal.NativeFunctions.StackPushInteger((int)nGender);
            Internal.NativeFunctions.StackPushInteger(nStrRef);
            Internal.NativeFunctions.CallBuiltIn(239);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Causes the creature to speak a translated string.
        ///  - nStrRef: Reference of the string in the talk table
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void ActionSpeakStringByStrRef(int nStrRef, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTalkVolume);
            Internal.NativeFunctions.StackPushInteger(nStrRef);
            Internal.NativeFunctions.CallBuiltIn(240);
        }

        /// <summary>
        ///  Destroy oObject (irrevocably).
        ///  This will not work on modules and areas.
        /// </summary>
        public static void DestroyObject(NWGameObject oDestroy, float fDelay = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fDelay);
            Internal.NativeFunctions.StackPushObject(oDestroy != null ? oDestroy.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(241);
        }

        /// <summary>
        ///  Get the module.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetModule()
        {
            Internal.NativeFunctions.CallBuiltIn(242);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Create an event which triggers the "SpellCastAt" script
        ///  Note: This only creates the event. The event wont actually trigger until SignalEvent()
        ///  is called using this created SpellCastAt event as an argument.
        ///  For example:
        ///      SignalEvent(oCreature, EventSpellCastAt(oCaster, SPELL_MAGIC_MISSILE, true));
        ///  This function doesn't cast the spell specified, it only creates an event so that
        ///  when the event is signaled on an object, the object will use its OnSpellCastAt script
        ///  to react to the spell being cast.
        /// 
        ///  To specify the OnSpellCastAt script that should run, view the Object's Properties 
        ///  and click on the Scripts Tab. Then specify a script for the OnSpellCastAt event.
        ///  From inside the OnSpellCastAt script call:
        ///      GetLastSpellCaster() to get the object that cast the spell (oCaster).
        ///      GetLastSpell() to get the type of spell cast (nSpell)
        ///      GetLastSpellHarmful() to determine if the spell cast at the object was harmful.
        /// </summary>
        public static NWN.Event EventSpellCastAt(NWGameObject oCaster, Spell nSpell, bool bHarmful = true)
        {
            Internal.NativeFunctions.StackPushInteger(bHarmful ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.StackPushObject(oCaster != null ? oCaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(244);
            return new NWN.Event(Internal.NativeFunctions.StackPopEvent());
        }

        /// <summary>
        ///  This is for use in a "Spell Cast" script, it gets who cast the spell.
        ///  The spell could have been cast by a creature, placeable or door.
        ///  * Returns OBJECT_INVALID if the caller is not a creature, placeable or door.
        /// </summary>
        public static NWGameObject GetLastSpellCaster()
        {
            Internal.NativeFunctions.CallBuiltIn(245);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  This is for use in a "Spell Cast" script, it gets the ID of the spell that
        ///  was cast.
        /// </summary>
        public static int GetLastSpell()
        {
            Internal.NativeFunctions.CallBuiltIn(246);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  This is for use in a user-defined script, it gets the event number.
        /// </summary>
        public static int GetUserDefinedEventNumber()
        {
            Internal.NativeFunctions.CallBuiltIn(247);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  This is for use in a Spell script, it gets the ID of the spell that is being
        ///  cast (SPELL_*).
        /// </summary>
        public static int GetSpellId()
        {
            Internal.NativeFunctions.CallBuiltIn(248);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Generate a random name.
        ///  nNameType: The type of random name to be generated (NAME_*)
        /// </summary>
        public static string RandomName(Name nNameType = Name.FirstGenericMale)
        {
            Internal.NativeFunctions.StackPushInteger((int)nNameType);
            Internal.NativeFunctions.CallBuiltIn(249);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Create a Poison effect.
        ///  - nPoisonType: POISON_*
        /// </summary>
        public static Effect EffectPoison(Poison nPoisonType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nPoisonType);
            Internal.NativeFunctions.CallBuiltIn(250);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Disease effect.
        ///  - nDiseaseType: DISEASE_*
        /// </summary>
        public static Effect EffectDisease(Disease nDiseaseType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDiseaseType);
            Internal.NativeFunctions.CallBuiltIn(251);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Silence effect.
        /// </summary>
        public static Effect EffectSilence()
        {
            Internal.NativeFunctions.CallBuiltIn(252);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Set the name of oObject.
        /// 
        ///  - oObject: the object for which you are changing the name (area, creature, placeable, item, or door).
        ///  - sNewName: the new name that the object will use.
        ///  Note: SetName() does not work on player objects.
        ///        Setting an object's name to "" will make the object
        ///        revert to using the name it had originally before any
        ///        SetName() calls were made on the object.
        /// </summary>
        public static string GetName(NWGameObject oObject, bool bOriginalName = false)
        {
            Internal.NativeFunctions.StackPushInteger(bOriginalName ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(253);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Use this in a conversation script to get the person with whom you are conversing.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static NWGameObject GetLastSpeaker()
        {
            Internal.NativeFunctions.CallBuiltIn(254);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnDialog script to start up the dialog tree.
        ///  - sResRef: if this is not specified, the default dialog file will be used
        ///  - oObjectToDialog: if this is not specified the person that triggered the
        ///    event will be used
        /// </summary>
        public static int BeginConversation(string sResRef = "", NWGameObject oObjectToDialog = null)
        {
            Internal.NativeFunctions.StackPushObject(oObjectToDialog != null ? oObjectToDialog.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushStringUTF8(sResRef);
            Internal.NativeFunctions.CallBuiltIn(255);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Use this in an OnPerception script to get the object that was perceived.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static NWGameObject GetLastPerceived()
        {
            Internal.NativeFunctions.CallBuiltIn(256);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was
        ///  perceived was heard.
        /// </summary>
        public static int GetLastPerceptionHeard()
        {
            Internal.NativeFunctions.CallBuiltIn(257);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was
        ///  perceived has become inaudible.
        /// </summary>
        public static int GetLastPerceptionInaudible()
        {
            Internal.NativeFunctions.CallBuiltIn(258);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was
        ///  perceived was seen.
        /// </summary>
        public static int GetLastPerceptionSeen()
        {
            Internal.NativeFunctions.CallBuiltIn(259);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Use this in an OnClosed script to get the object that closed the door or placeable.
        ///  * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static NWGameObject GetLastClosedBy()
        {
            Internal.NativeFunctions.CallBuiltIn(260);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was
        ///  perceived has vanished.
        /// </summary>
        public static int GetLastPerceptionVanished()
        {
            Internal.NativeFunctions.CallBuiltIn(261);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the first object within oPersistentObject.
        ///  - oPersistentObject
        ///  - nResidentObjectType: OBJECT_TYPE_*
        ///  - nPersistentZone: PERSISTENT_ZONE_ACTIVE. [This could also take the value
        ///    PERSISTENT_ZONE_FOLLOW, but this is no longer used.]
        ///  * Returns OBJECT_INVALID if no object is found.
        /// </summary>
        public static NWGameObject GetFirstInPersistentObject(NWGameObject oPersistentObject = null, ObjectType nResidentObjectType = ObjectType.Creature, PersistentZone nPersistentZone = PersistentZone.Active)
        {
            Internal.NativeFunctions.StackPushInteger((int)nPersistentZone);
            Internal.NativeFunctions.StackPushInteger((int)nResidentObjectType);
            Internal.NativeFunctions.StackPushObject(oPersistentObject != null ? oPersistentObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(262);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the next object within oPersistentObject.
        ///  - oPersistentObject
        ///  - nResidentObjectType: OBJECT_TYPE_*
        ///  - nPersistentZone: PERSISTENT_ZONE_ACTIVE. [This could also take the value
        ///    PERSISTENT_ZONE_FOLLOW, but this is no longer used.]
        ///  * Returns OBJECT_INVALID if no object is found.
        /// </summary>
        public static NWGameObject GetNextInPersistentObject(NWGameObject oPersistentObject = null, ObjectType nResidentObjectType = ObjectType.Creature, PersistentZone nPersistentZone = PersistentZone.Active)
        {
            Internal.NativeFunctions.StackPushInteger((int)nPersistentZone);
            Internal.NativeFunctions.StackPushInteger((int)nResidentObjectType);
            Internal.NativeFunctions.StackPushObject(oPersistentObject != null ? oPersistentObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(263);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  This returns the creator of oAreaOfEffectObject.
        ///  * Returns OBJECT_INVALID if oAreaOfEffectObject is not a valid Area of Effect object.
        /// </summary>
        public static NWGameObject GetAreaOfEffectCreator(NWGameObject oAreaOfEffectObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oAreaOfEffectObject != null ? oAreaOfEffectObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(264);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Delete oObject's local integer variable sVarName
        /// </summary>
        public static void DeleteLocalInt(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(265);
        }

        /// <summary>
        ///  Delete oObject's local float variable sVarName
        /// </summary>
        public static void DeleteLocalFloat(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(266);
        }

        /// <summary>
        ///  Delete oObject's local string variable sVarName
        /// </summary>
        public static void DeleteLocalString(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(267);
        }

        /// <summary>
        ///  Delete oObject's local object variable sVarName
        /// </summary>
        public static void DeleteLocalObject(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(268);
        }

        /// <summary>
        ///  Delete oObject's local location variable sVarName
        /// </summary>
        public static void DeleteLocalLocation(NWGameObject oObject, string sVarName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sVarName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(269);
        }

        /// <summary>
        ///  Create a Haste effect.
        /// </summary>
        public static Effect EffectHaste()
        {
            Internal.NativeFunctions.CallBuiltIn(270);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Slow effect.
        /// </summary>
        public static Effect EffectSlow()
        {
            Internal.NativeFunctions.CallBuiltIn(271);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Convert oObject into a hexadecimal string.
        /// </summary>
        public static string ObjectToString(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(272);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Create an Immunity effect.
        ///  - nImmunityType: IMMUNITY_TYPE_*
        /// </summary>
        public static Effect EffectImmunity(ImmunityType nImmunityType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nImmunityType);
            Internal.NativeFunctions.CallBuiltIn(273);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  - oCreature
        ///  - nImmunityType: IMMUNITY_TYPE_*
        ///  - oVersus: if this is specified, then we also check for the race and
        ///    alignment of oVersus
        ///  * Returns true if oCreature has immunity of type nImmunity versus oVersus.
        /// </summary>
        public static bool GetIsImmune(NWGameObject oCreature, ImmunityType nImmunityType, NWGameObject oVersus = null)
        {
            Internal.NativeFunctions.StackPushObject(oVersus != null ? oVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nImmunityType);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(274);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Creates a Damage Immunity Increase effect.
        ///  - nDamageType: DAMAGE_TYPE_*
        ///  - nPercentImmunity
        /// </summary>
        public static Effect EffectDamageImmunityIncrease(DamageType nDamageType, int nPercentImmunity)
        {
            Internal.NativeFunctions.StackPushInteger(nPercentImmunity);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(275);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Determine whether oEncounter is active.
        /// </summary>
        public static bool GetEncounterActive(NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(276);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Set oEncounter's active state to nNewValue.
        ///  - nNewValue: true/false
        ///  - oEncounter
        /// </summary>
        public static void SetEncounterActive(bool nNewValue, NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nNewValue ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(277);
        }

        /// <summary>
        ///  Get the maximum number of times that oEncounter will spawn.
        /// </summary>
        public static int GetEncounterSpawnsMax(NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(278);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the maximum number of times that oEncounter can spawn
        /// </summary>
        public static void SetEncounterSpawnsMax(int nNewValue, NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nNewValue);
            Internal.NativeFunctions.CallBuiltIn(279);
        }

        /// <summary>
        ///  Get the number of times that oEncounter has spawned so far
        /// </summary>
        public static int GetEncounterSpawnsCurrent(NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(280);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the number of times that oEncounter has spawned so far
        /// </summary>
        public static void SetEncounterSpawnsCurrent(int nNewValue, NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nNewValue);
            Internal.NativeFunctions.CallBuiltIn(281);
        }

        /// <summary>
        ///  Use this in an OnItemAcquired script to get the item that was acquired.
        ///  * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static NWGameObject GetModuleItemAcquired()
        {
            Internal.NativeFunctions.CallBuiltIn(282);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnItemAcquired script to get the creatre that previously
        ///  possessed the item.
        ///  * Returns OBJECT_INVALID if the item was picked up from the ground.
        /// </summary>
        public static NWGameObject GetModuleItemAcquiredFrom()
        {
            Internal.NativeFunctions.CallBuiltIn(283);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Set the value for a custom token.
        /// </summary>
        public static void SetCustomToken(int nCustomTokenNumber, string sTokenValue)
        {
            Internal.NativeFunctions.StackPushString(sTokenValue);
            Internal.NativeFunctions.StackPushInteger(nCustomTokenNumber);
            Internal.NativeFunctions.CallBuiltIn(284);
        }

        /// <summary>
        ///  Determine whether oCreature has nSkill, and nSkill is useable.
        ///  - nSkill: SKILL_*
        ///  - oCreature
        /// </summary>
        public static bool GetHasSkill(Skill nSkill, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(286);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Use nFeat on oTarget.
        ///  - nFeat: FEAT_*
        ///  - oTarget
        /// </summary>
        public static void ActionUseFeat(Feat nFeat, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nFeat);
            Internal.NativeFunctions.CallBuiltIn(287);
        }

        /// <summary>
        ///  Runs the action "UseSkill" on the current creature
        ///  Use nSkill on oTarget.
        ///  - nSkill: SKILL_*
        ///  - oTarget
        ///  - nSubSkill: SUBSKILL_*
        ///  - oItemUsed: Item to use in conjunction with the skill
        /// </summary>
        public static void ActionUseSkill(Skill nSkill, NWGameObject oTarget, SubSkill nSubSkill = SubSkill.None, NWGameObject oItemUsed = null)
        {
            Internal.NativeFunctions.StackPushObject(oItemUsed != null ? oItemUsed.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSubSkill);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(288);
        }

        /// <summary>
        ///  Determine whether oSource sees oTarget.
        ///  NOTE: This *only* works on creatures, as visibility lists are not
        ///        maintained for non-creature objects.
        /// </summary>
        public static bool GetObjectSeen(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(289);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Determine whether oSource hears oTarget.
        ///  NOTE: This *only* works on creatures, as visibility lists are not
        ///        maintained for non-creature objects.
        /// </summary>
        public static bool GetObjectHeard(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(290);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Use this in an OnPlayerDeath module script to get the last player that died.
        /// </summary>
        public static NWGameObject GetLastPlayerDied()
        {
            Internal.NativeFunctions.CallBuiltIn(291);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnItemLost script to get the item that was lost/dropped.
        ///  * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static NWGameObject GetModuleItemLost()
        {
            Internal.NativeFunctions.CallBuiltIn(292);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnItemLost script to get the creature that lost the item.
        ///  * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static NWGameObject GetModuleItemLostBy()
        {
            Internal.NativeFunctions.CallBuiltIn(293);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Creates a conversation event.
        ///  Note: This only creates the event. The event wont actually trigger until SignalEvent()
        ///  is called using this created conversation event as an argument.
        ///  For example:
        ///      SignalEvent(oCreature, EventConversation());
        ///  Once the event has been signaled. The script associated with the OnConversation event will
        ///  run on the creature oCreature.
        /// 
        ///  To specify the OnConversation script that should run, view the Creature Properties on
        ///  the creature and click on the Scripts Tab. Then specify a script for the OnConversation event.
        /// </summary>
        public static NWN.Event EventConversation()
        {
            Internal.NativeFunctions.CallBuiltIn(295);
            return new NWN.Event(Internal.NativeFunctions.StackPopEvent());
        }

        /// <summary>
        ///  Set the difficulty level of oEncounter.
        ///  - nEncounterDifficulty: ENCOUNTER_DIFFICULTY_*
        ///  - oEncounter
        /// </summary>
        public static void SetEncounterDifficulty(EncounterDifficulty nEncounterDifficulty, NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nEncounterDifficulty);
            Internal.NativeFunctions.CallBuiltIn(296);
        }

        /// <summary>
        ///  Get the difficulty level of oEncounter.
        /// </summary>
        public static EncounterDifficulty GetEncounterDifficulty(NWGameObject oEncounter = null)
        {
            Internal.NativeFunctions.StackPushObject(oEncounter != null ? oEncounter.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(297);
            return (EncounterDifficulty)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the distance between lLocationA and lLocationB.
        /// </summary>
        public static float GetDistanceBetweenLocations(NWN.Location lLocationA, NWN.Location lLocationB)
        {
            Internal.NativeFunctions.StackPushLocation(lLocationB.Handle);
            Internal.NativeFunctions.StackPushLocation(lLocationA.Handle);
            Internal.NativeFunctions.CallBuiltIn(298);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Use this in spell scripts to get nDamage adjusted by oTarget's reflex and
        ///  evasion saves.
        ///  - nDamage
        ///  - oTarget
        ///  - nDC: Difficulty check
        ///  - nSaveType: SAVING_THROW_TYPE_*
        ///  - oSaveVersus
        /// </summary>
        public static int GetReflexAdjustedDamage(int nDamage, NWGameObject oTarget, int nDC, SavingThrowType nSaveType = SavingThrowType.None, NWGameObject oSaveVersus = null)
        {
            Internal.NativeFunctions.StackPushObject(oSaveVersus != null ? oSaveVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSaveType);
            Internal.NativeFunctions.StackPushInteger(nDC);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nDamage);
            Internal.NativeFunctions.CallBuiltIn(299);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Play nAnimation immediately.
        ///  - nAnimation: ANIMATION_*
        ///  - fSpeed
        ///  - fSeconds
        /// </summary>
        public static void PlayAnimation(Animation nAnimation, float fSpeed = 1.0f, float fSeconds = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fSeconds);
            Internal.NativeFunctions.StackPushFloat(fSpeed);
            Internal.NativeFunctions.StackPushInteger((int)nAnimation);
            Internal.NativeFunctions.CallBuiltIn(300);
        }

        /// <summary>
        ///  Create a Spell Talent.
        ///  - nSpell: SPELL_*
        /// </summary>
        public static NWN.Talent TalentSpell(Spell nSpell)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(301);
            return new NWN.Talent(Internal.NativeFunctions.StackPopTalent());
        }

        /// <summary>
        ///  Create a Feat Talent.
        ///  - nFeat: FEAT_*
        /// </summary>
        public static NWN.Talent TalentFeat(Feat nFeat)
        {
            Internal.NativeFunctions.StackPushInteger((int)nFeat);
            Internal.NativeFunctions.CallBuiltIn(302);
            return new NWN.Talent(Internal.NativeFunctions.StackPopTalent());
        }

        /// <summary>
        ///  Create a Skill Talent.
        ///  - nSkill: SKILL_*
        /// </summary>
        public static NWN.Talent TalentSkill(Skill nSkill)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(303);
            return new NWN.Talent(Internal.NativeFunctions.StackPopTalent());
        }

        /// <summary>
        ///  Determines whether oObject has any effects applied by nSpell
        ///  - nSpell: SPELL_*
        ///  - oObject
        ///  * The spell id on effects is only valid if the effect is created
        ///    when the spell script runs. If it is created in a delayed command
        ///    then the spell id on the effect will be invalid.
        /// </summary>
        public static bool GetHasSpellEffect(Spell nSpell, NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(304);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the spell (SPELL_*) that applied eSpellEffect.
        ///  * Returns -1 if eSpellEffect was applied outside a spell script.
        /// </summary>
        public static Spell GetEffectSpellId(NWN.Effect eSpellEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eSpellEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(305);
            return (Spell)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Determine whether oCreature has tTalent.
        /// </summary>
        public static bool GetCreatureHasTalent(NWN.Talent tTalent, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushTalent(tTalent.Handle);
            Internal.NativeFunctions.CallBuiltIn(306);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get a random talent of oCreature, within nCategory.
        ///  - nCategory: TALENT_CATEGORY_*
        ///  - oCreature
        /// </summary>
        public static NWN.Talent GetCreatureTalentRandom(TalentCategory nCategory, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nCategory);
            Internal.NativeFunctions.CallBuiltIn(307);
            return new NWN.Talent(Internal.NativeFunctions.StackPopTalent());
        }

        /// <summary>
        ///  Get the best talent (i.e. closest to nCRMax without going over) of oCreature,
        ///  within nCategory.
        ///  - nCategory: TALENT_CATEGORY_*
        ///  - nCRMax: Challenge Rating of the talent
        ///  - oCreature
        /// </summary>
        public static NWN.Talent GetCreatureTalentBest(TalentCategory nCategory, int nCRMax, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nCRMax);
            Internal.NativeFunctions.StackPushInteger((int)nCategory);
            Internal.NativeFunctions.CallBuiltIn(308);
            return new NWN.Talent(Internal.NativeFunctions.StackPopTalent());
        }

        /// <summary>
        ///  Use tChosenTalent on oTarget.
        /// </summary>
        public static void ActionUseTalentOnObject(NWN.Talent tChosenTalent, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushTalent(tChosenTalent.Handle);
            Internal.NativeFunctions.CallBuiltIn(309);
        }

        /// <summary>
        ///  Use tChosenTalent at lTargetLocation.
        /// </summary>
        public static void ActionUseTalentAtLocation(NWN.Talent tChosenTalent, NWN.Location lTargetLocation)
        {
            Internal.NativeFunctions.StackPushLocation(lTargetLocation.Handle);
            Internal.NativeFunctions.StackPushTalent(tChosenTalent.Handle);
            Internal.NativeFunctions.CallBuiltIn(310);
        }

        /// <summary>
        ///  Get the gold piece value of oItem.
        ///  * Returns 0 if oItem is not a valid item.
        /// </summary>
        public static int GetGoldPieceValue(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(311);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  * Returns true if oCreature is of a playable racial type.
        /// </summary>
        public static bool GetIsPlayableRacialType(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(312);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Jump to lDestination.  The action is added to the TOP of the action queue.
        /// </summary>
        public static void JumpToLocation(NWN.Location lDestination)
        {
            Internal.NativeFunctions.StackPushLocation(lDestination.Handle);
            Internal.NativeFunctions.CallBuiltIn(313);
        }

        /// <summary>
        ///  Create a Temporary Hitpoints effect.
        ///  - nHitPoints: a positive integer
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nHitPoints < 0.
        /// </summary>
        public static Effect EffectTemporaryHitpoints(int nHitPoints)
        {
            Internal.NativeFunctions.StackPushInteger(nHitPoints);
            Internal.NativeFunctions.CallBuiltIn(314);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the number of ranks that oTarget has in nSkill.
        ///  - nSkill: SKILL_*
        ///  - oTarget
        ///  - nBaseSkillRank: if set to true returns the number of base skill ranks the target
        ///                    has (i.e. not including any bonuses from ability scores, feats, etc).
        ///  * Returns -1 if oTarget doesn't have nSkill.
        ///  * Returns 0 if nSkill is untrained.
        /// </summary>
        public static int GetSkillRank(Skill nSkill, NWGameObject oTarget = null, bool nBaseSkillRank = false)
        {
            Internal.NativeFunctions.StackPushInteger(nBaseSkillRank ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(315);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the attack target of oCreature.
        ///  This only works when oCreature is in combat.
        /// </summary>
        public static NWGameObject GetAttackTarget(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(316);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the attack type (SPECIAL_ATTACK_*) of oCreature's last attack.
        ///  This only works when oCreature is in combat.
        /// </summary>
        public static SpecialAttack GetLastAttackType(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(317);
            return (SpecialAttack)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the attack mode (COMBAT_MODE_*) of oCreature's last attack.
        ///  This only works when oCreature is in combat.
        /// </summary>
        public static CombatMode GetLastAttackMode(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(318);
            return (CombatMode)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the master of oAssociate.
        /// </summary>
        public static NWGameObject GetMaster(NWGameObject oAssociate = null)
        {
            Internal.NativeFunctions.StackPushObject(oAssociate != null ? oAssociate.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(319);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  * Returns true if oCreature is in combat.
        /// </summary>
        public static bool GetIsInCombat(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(320);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the last command (ASSOCIATE_COMMAND_*) issued to oAssociate.
        /// </summary>
        public static AssociateCommand GetLastAssociateCommand(NWGameObject oAssociate = null)
        {
            Internal.NativeFunctions.StackPushObject(oAssociate != null ? oAssociate.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(321);
            return (AssociateCommand)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Give nGP gold to oCreature.
        /// </summary>
        public static void GiveGoldToCreature(NWGameObject oCreature, int nGP)
        {
            Internal.NativeFunctions.StackPushInteger(nGP);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(322);
        }

        /// <summary>
        ///  Set the destroyable status of the caller.
        ///  - bDestroyable: If this is false, the caller does not fade out on death, but
        ///    sticks around as a corpse.
        ///  - bRaiseable: If this is true, the caller can be raised via resurrection.
        ///  - bSelectableWhenDead: If this is true, the caller is selectable after death.
        /// </summary>
        public static void SetIsDestroyable(bool bDestroyable, bool bRaiseable = true, bool bSelectableWhenDead = false)
        {
            Internal.NativeFunctions.StackPushInteger(bSelectableWhenDead ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bRaiseable ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bDestroyable ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(323);
        }

        /// <summary>
        ///  Set the locked state of oTarget, which can be a door or a placeable object.
        /// </summary>
        public static void SetLocked(NWGameObject oTarget, bool bLocked)
        {
            Internal.NativeFunctions.StackPushInteger(bLocked ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(324);
        }

        /// <summary>
        ///  Get the locked state of oTarget, which can be a door or a placeable object.
        /// </summary>
        public static bool GetLocked(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(325);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Use this in a trigger's OnClick event script to get the object that last
        ///  clicked on it.
        ///  This is identical to GetEnteringObject.
        ///  GetClickingObject() should not be called from a placeable's OnClick event,
        ///  instead use GetPlaceableLastClickedBy();
        /// </summary>
        public static NWGameObject GetClickingObject()
        {
            Internal.NativeFunctions.CallBuiltIn(326);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Initialise oTarget to listen for the standard Associates commands.
        /// </summary>
        public static void SetAssociateListenPatterns(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(327);
        }

        /// <summary>
        ///  Get the last weapon that oCreature used in an attack.
        ///  * Returns OBJECT_INVALID if oCreature did not attack, or has no weapon equipped.
        /// </summary>
        public static NWGameObject GetLastWeaponUsed(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(328);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use oPlaceable.
        /// </summary>
        public static void ActionInteractObject(NWGameObject oPlaceable)
        {
            Internal.NativeFunctions.StackPushObject(oPlaceable != null ? oPlaceable.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(329);
        }

        /// <summary>
        ///  Get the last object that used the placeable object that is calling this function.
        ///  * Returns OBJECT_INVALID if it is called by something other than a placeable or
        ///    a door.
        /// </summary>
        public static NWGameObject GetLastUsedBy()
        {
            Internal.NativeFunctions.CallBuiltIn(330);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Returns the ability modifier for the specified ability
        ///  Get oCreature's ability modifier for nAbility.
        ///  - nAbility: ABILITY_*
        ///  - oCreature
        /// </summary>
        public static int GetAbilityModifier(Ability nAbility, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nAbility);
            Internal.NativeFunctions.CallBuiltIn(331);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Determined whether oItem has been identified.
        /// </summary>
        public static bool GetIdentified(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(332);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Set whether oItem has been identified.
        /// </summary>
        public static void SetIdentified(NWGameObject oItem, bool bIdentified)
        {
            Internal.NativeFunctions.StackPushInteger(bIdentified ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(333);
        }

        /// <summary>
        ///  Summon an Animal Companion
        /// </summary>
        public static void SummonAnimalCompanion(NWGameObject oMaster = null)
        {
            Internal.NativeFunctions.StackPushObject(oMaster != null ? oMaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(334);
        }

        /// <summary>
        ///  Summon a Familiar
        /// </summary>
        public static void SummonFamiliar(NWGameObject oMaster = null)
        {
            Internal.NativeFunctions.StackPushObject(oMaster != null ? oMaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(335);
        }

        /// <summary>
        ///  Get the last blocking door encountered by the caller of this function.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static NWGameObject GetBlockingDoor()
        {
            Internal.NativeFunctions.CallBuiltIn(336);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  - oTargetDoor
        ///  - nDoorAction: DOOR_ACTION_*
        ///  * Returns true if nDoorAction can be performed on oTargetDoor.
        /// </summary>
        public static bool GetIsDoorActionPossible(NWGameObject oTargetDoor, DoorAction nDoorAction)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDoorAction);
            Internal.NativeFunctions.StackPushObject(oTargetDoor != null ? oTargetDoor.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(337);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Perform nDoorAction on oTargetDoor.
        /// </summary>
        public static void DoDoorAction(NWGameObject oTargetDoor, DoorAction nDoorAction)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDoorAction);
            Internal.NativeFunctions.StackPushObject(oTargetDoor != null ? oTargetDoor.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(338);
        }

        /// <summary>
        ///  Get the first item in oTarget's inventory (start to cycle through oTarget's
        ///  inventory).
        ///  * Returns OBJECT_INVALID if the caller is not a creature, item, placeable or store,
        ///    or if no item is found.
        /// </summary>
        public static NWGameObject GetFirstItemInInventory(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(339);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the next item in oTarget's inventory (continue to cycle through oTarget's
        ///  inventory).
        ///  * Returns OBJECT_INVALID if the caller is not a creature, item, placeable or store,
        ///    or if no item is found.
        /// </summary>
        public static NWGameObject GetNextItemInInventory(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(340);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  A creature can have up to three classes.  This function determines the
        ///  creature's class (CLASS_TYPE_*) based on nClassPosition.
        ///  - nClassPosition: 1, 2 or 3
        ///  - oCreature
        ///  * Returns CLASS_TYPE_INVALID if the oCreature does not have a class in
        ///    nClassPosition (i.e. a single-class creature will only have a value in
        ///    nClassLocation=1) or if oCreature is not a valid creature.
        /// </summary>
        public static ClassType GetClassByPosition(ClassPosition nClassPosition, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nClassPosition);
            Internal.NativeFunctions.CallBuiltIn(341);
            return (ClassType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  A creature can have up to three classes.  This function determines the
        ///  creature's class level based on nClass Position.
        ///  - nClassPosition: 1, 2 or 3
        ///  - oCreature
        ///  * Returns 0 if oCreature does not have a class in nClassPosition
        ///    (i.e. a single-class creature will only have a value in nClassLocation=1)
        ///    or if oCreature is not a valid creature.
        /// </summary>
        public static int GetLevelByPosition(ClassPosition nClassPosition, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nClassPosition);
            Internal.NativeFunctions.CallBuiltIn(342);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Determine the levels that oCreature holds in nClassType.
        ///  - nClassType: CLASS_TYPE_*
        ///  - oCreature
        /// </summary>
        public static int GetLevelByClass(ClassType nClassType, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nClassType);
            Internal.NativeFunctions.CallBuiltIn(343);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the amount of damage of type nDamageType that has been dealt to the caller.
        ///  - nDamageType: DAMAGE_TYPE_*
        /// </summary>
        public static int GetDamageDealtByType(DamageType nDamageType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(344);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the total amount of damage that has been dealt to the caller.
        /// </summary>
        public static int GetTotalDamageDealt()
        {
            Internal.NativeFunctions.CallBuiltIn(345);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the last object that damaged oObject
        ///  * Returns OBJECT_INVALID if the passed in object is not a valid object.
        /// </summary>
        public static NWGameObject GetLastDamager(NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(346);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the last object that disarmed the trap on the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid placeable, trigger or
        ///    door.
        /// </summary>
        public static NWGameObject GetLastDisarmed()
        {
            Internal.NativeFunctions.CallBuiltIn(347);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the last object that disturbed the inventory of the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature or placeable.
        /// </summary>
        public static NWGameObject GetLastDisturbed()
        {
            Internal.NativeFunctions.CallBuiltIn(348);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the last object that locked the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static NWGameObject GetLastLocked()
        {
            Internal.NativeFunctions.CallBuiltIn(349);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the last object that unlocked the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static NWGameObject GetLastUnlocked()
        {
            Internal.NativeFunctions.CallBuiltIn(350);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Create a Skill Increase effect.
        ///  - nSkill: SKILL_*
        ///  - nValue
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nSkill is invalid.
        /// </summary>
        public static Effect EffectSkillIncrease(Skill nSkill, int nValue)
        {
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(351);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the type of disturbance (INVENTORY_DISTURB_*) that caused the caller's
        ///  OnInventoryDisturbed script to fire.  This will only work for creatures and
        ///  placeables.
        /// </summary>
        public static InventoryDisturbType GetInventoryDisturbType()
        {
            Internal.NativeFunctions.CallBuiltIn(352);
            return (InventoryDisturbType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  get the item that caused the caller's OnInventoryDisturbed script to fire.
        ///  * Returns OBJECT_INVALID if the caller is not a valid object.
        /// </summary>
        public static NWGameObject GetInventoryDisturbItem()
        {
            Internal.NativeFunctions.CallBuiltIn(353);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the henchman belonging to oMaster.
        ///  * Return OBJECT_INVALID if oMaster does not have a henchman.
        ///  -nNth: Which henchman to return.
        /// </summary>
        public static NWGameObject GetHenchman(NWGameObject oMaster = null, int nNth = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nNth);
            Internal.NativeFunctions.StackPushObject(oMaster != null ? oMaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(354);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Set eEffect to be versus a specific alignment.
        ///  - eEffect
        ///  - nLawChaos: ALIGNMENT_LAWFUL/ALIGNMENT_CHAOTIC/ALIGNMENT_ALL
        ///  - nGoodEvil: ALIGNMENT_GOOD/ALIGNMENT_EVIL/ALIGNMENT_ALL
        /// </summary>
        public static Effect VersusAlignmentEffect(NWN.Effect eEffect, Alignment nLawChaos = Alignment.All, Alignment nGoodEvil = Alignment.All)
        {
            Internal.NativeFunctions.StackPushInteger((int)nGoodEvil);
            Internal.NativeFunctions.StackPushInteger((int)nLawChaos);
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(355);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Set eEffect to be versus nRacialType.
        ///  - eEffect
        ///  - nRacialType: RACIAL_TYPE_*
        /// </summary>
        public static Effect VersusRacialTypeEffect(NWN.Effect eEffect, RacialType nRacialType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nRacialType);
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(356);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Set eEffect to be versus traps.
        /// </summary>
        public static Effect VersusTrapEffect(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(357);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the gender of oCreature.
        /// </summary>
        public static Gender GetGender(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(358);
            return (Gender)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  * Returns true if tTalent is valid.
        /// </summary>
        public static bool GetIsTalentValid(NWN.Talent tTalent)
        {
            Internal.NativeFunctions.StackPushTalent(tTalent.Handle);
            Internal.NativeFunctions.CallBuiltIn(359);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Causes the action subject to move away from lMoveAwayFrom.
        /// </summary>
        public static void ActionMoveAwayFromLocation(NWN.Location lMoveAwayFrom, bool bRun = false, float fMoveAwayRange = 40.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fMoveAwayRange);
            Internal.NativeFunctions.StackPushInteger(bRun ? 1 : 0);
            Internal.NativeFunctions.StackPushLocation(lMoveAwayFrom.Handle);
            Internal.NativeFunctions.CallBuiltIn(360);
        }

        /// <summary>
        ///  Get the target that the caller attempted to attack - this should be used in
        ///  conjunction with GetAttackTarget(). This value is set every time an attack is
        ///  made, and is reset at the end of combat.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static NWGameObject GetAttemptedAttackTarget()
        {
            Internal.NativeFunctions.CallBuiltIn(361);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the type (TALENT_TYPE_*) of tTalent.
        /// </summary>
        public static TalentType GetTypeFromTalent(NWN.Talent tTalent)
        {
            Internal.NativeFunctions.StackPushTalent(tTalent.Handle);
            Internal.NativeFunctions.CallBuiltIn(362);
            return (TalentType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the ID of tTalent.  This could be a SPELL_*, FEAT_* or SKILL_*.
        /// </summary>
        public static int GetIdFromTalent(NWN.Talent tTalent)
        {
            Internal.NativeFunctions.StackPushTalent(tTalent.Handle);
            Internal.NativeFunctions.CallBuiltIn(363);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the associate of type nAssociateType belonging to oMaster.
        ///  - nAssociateType: ASSOCIATE_TYPE_*
        ///  - nMaster
        ///  - nTh: Which associate of the specified type to return
        ///  * Returns OBJECT_INVALID if no such associate exists.
        /// </summary>
        public static NWGameObject GetAssociate(AssociateType nAssociateType, NWGameObject oMaster = null, int nTh = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nTh);
            Internal.NativeFunctions.StackPushObject(oMaster != null ? oMaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nAssociateType);
            Internal.NativeFunctions.CallBuiltIn(364);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Add oHenchman as a henchman to oMaster
        ///  If oHenchman is either a DM or a player character, this will have no effect.
        /// </summary>
        public static void AddHenchman(NWGameObject oMaster, NWGameObject oHenchman = null)
        {
            Internal.NativeFunctions.StackPushObject(oHenchman != null ? oHenchman.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oMaster != null ? oMaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(365);
        }

        /// <summary>
        ///  Remove oHenchman from the service of oMaster, returning them to their original faction.
        /// </summary>
        public static void RemoveHenchman(NWGameObject oMaster, NWGameObject oHenchman = null)
        {
            Internal.NativeFunctions.StackPushObject(oHenchman != null ? oHenchman.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oMaster != null ? oMaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(366);
        }

        /// <summary>
        ///  Add a journal quest entry to oCreature.
        ///  - szPlotID: the plot identifier used in the toolset's Journal Editor
        ///  - nState: the state of the plot as seen in the toolset's Journal Editor
        ///  - oCreature
        ///  - bAllPartyMembers: If this is true, the entry will show up in the journal of
        ///    everyone in the party
        ///  - bAllPlayers: If this is true, the entry will show up in the journal of
        ///    everyone in the world
        ///  - bAllowOverrideHigher: If this is true, you can set the state to a lower
        ///    number than the one it is currently on
        /// </summary>
        public static void AddJournalQuestEntry(string szPlotID, int nState, NWGameObject oCreature, bool bAllPartyMembers = true, bool bAllPlayers = false, bool bAllowOverrideHigher = false)
        {
            Internal.NativeFunctions.StackPushInteger(bAllowOverrideHigher ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bAllPlayers ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bAllPartyMembers ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nState);
            Internal.NativeFunctions.StackPushStringUTF8(szPlotID);
            Internal.NativeFunctions.CallBuiltIn(367);
        }

        /// <summary>
        ///  Remove a journal quest entry from oCreature.
        ///  - szPlotID: the plot identifier used in the toolset's Journal Editor
        ///  - oCreature
        ///  - bAllPartyMembers: If this is true, the entry will be removed from the
        ///    journal of everyone in the party
        ///  - bAllPlayers: If this is true, the entry will be removed from the journal of
        ///    everyone in the world
        /// </summary>
        public static void RemoveJournalQuestEntry(string szPlotID, NWGameObject oCreature, bool bAllPartyMembers = true, bool bAllPlayers = false)
        {
            Internal.NativeFunctions.StackPushInteger(bAllPlayers ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bAllPartyMembers ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushStringUTF8(szPlotID);
            Internal.NativeFunctions.CallBuiltIn(368);
        }

        /// <summary>
        ///  Get the public part of the CD Key that oPlayer used when logging in.
        ///  - nSinglePlayerCDKey: If set to true, the player's public CD Key will 
        ///    be returned when the player is playing in single player mode 
        ///    (otherwise returns an empty string in single player mode).
        /// </summary>
        public static string GetPCPublicCDKey(NWGameObject oPlayer, bool nSinglePlayerCDKey = false)
        {
            Internal.NativeFunctions.StackPushInteger(nSinglePlayerCDKey ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(369);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Get the IP address from which oPlayer has connected.
        /// </summary>
        public static string GetPCIPAddress(NWGameObject oPlayer)
        {
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(370);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Get the name of oPlayer.
        /// </summary>
        public static string GetPCPlayerName(NWGameObject oPlayer)
        {
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(371);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Sets oPlayer and oTarget to like each other.
        /// </summary>
        public static void SetPCLike(NWGameObject oPlayer, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(372);
        }

        /// <summary>
        ///  Sets oPlayer and oTarget to dislike each other.
        /// </summary>
        public static void SetPCDislike(NWGameObject oPlayer, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(373);
        }

        /// <summary>
        ///  Send a server message (szMessage) to the oPlayer.
        /// </summary>
        public static void SendMessageToPC(NWGameObject oPlayer, string szMessage)
        {
            Internal.NativeFunctions.StackPushString(szMessage);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(374);
        }

        /// <summary>
        ///  Get the target at which the caller attempted to cast a spell.
        ///  This value is set every time a spell is cast and is reset at the end of
        ///  combat.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static NWGameObject GetAttemptedSpellTarget()
        {
            Internal.NativeFunctions.CallBuiltIn(375);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the last creature that opened the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid door, placeable or store.
        /// </summary>
        public static NWGameObject GetLastOpenedBy()
        {
            Internal.NativeFunctions.CallBuiltIn(376);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Determines the number of times that oCreature has nSpell memorised.
        ///  - nSpell: SPELL_*
        ///  - oCreature
        /// </summary>
        public static bool GetHasSpell(Spell nSpell, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(377);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Open oStore for oPC.
        ///  - nBonusMarkUp is added to the stores default mark up percentage on items sold (-100 to 100)
        ///  - nBonusMarkDown is added to the stores default mark down percentage on items bought (-100 to 100)
        /// </summary>
        public static void OpenStore(NWGameObject oStore, NWGameObject oPC, int nBonusMarkUp = 0, int nBonusMarkDown = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nBonusMarkDown);
            Internal.NativeFunctions.StackPushInteger(nBonusMarkUp);
            Internal.NativeFunctions.StackPushObject(oPC != null ? oPC.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oStore != null ? oStore.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(378);
        }

        /// <summary>
        ///  Create a Turned effect.
        ///  Turned effects are supernatural by default.
        /// </summary>
        public static Effect EffectTurned()
        {
            Internal.NativeFunctions.CallBuiltIn(379);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the first member of oMemberOfFaction's faction (start to cycle through
        ///  oMemberOfFaction's faction).
        ///  * Returns OBJECT_INVALID if oMemberOfFaction's faction is invalid.
        /// </summary>
        public static NWGameObject GetFirstFactionMember(NWGameObject oMemberOfFaction, bool bPCOnly = true)
        {
            Internal.NativeFunctions.StackPushInteger(bPCOnly ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oMemberOfFaction != null ? oMemberOfFaction.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(380);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the next member of oMemberOfFaction's faction (continue to cycle through
        ///  oMemberOfFaction's faction).
        ///  * Returns OBJECT_INVALID if oMemberOfFaction's faction is invalid.
        /// </summary>
        public static NWGameObject GetNextFactionMember(NWGameObject oMemberOfFaction, bool bPCOnly = true)
        {
            Internal.NativeFunctions.StackPushInteger(bPCOnly ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oMemberOfFaction != null ? oMemberOfFaction.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(381);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Force the action subject to move to lDestination.
        /// </summary>
        public static void ActionForceMoveToLocation(NWN.Location lDestination, bool bRun = false, float fTimeout = 30.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fTimeout);
            Internal.NativeFunctions.StackPushInteger(bRun ? 1 : 0);
            Internal.NativeFunctions.StackPushLocation(lDestination.Handle);
            Internal.NativeFunctions.CallBuiltIn(382);
        }

        /// <summary>
        ///  Force the action subject to move to oMoveTo.
        /// </summary>
        public static void ActionForceMoveToObject(NWGameObject oMoveTo, bool bRun = false, float fRange = 1.0f, float fTimeout = 30.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fTimeout);
            Internal.NativeFunctions.StackPushFloat(fRange);
            Internal.NativeFunctions.StackPushInteger(bRun ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oMoveTo != null ? oMoveTo.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(383);
        }

        /// <summary>
        ///  Get the experience assigned in the journal editor for szPlotID.
        /// </summary>
        public static int GetJournalQuestExperience(string szPlotID)
        {
            Internal.NativeFunctions.StackPushStringUTF8(szPlotID);
            Internal.NativeFunctions.CallBuiltIn(384);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Jump to oToJumpTo (the action is added to the top of the action queue).
        /// </summary>
        public static void JumpToObject(NWGameObject oToJumpTo, bool nWalkStraightLineToPoint = true)
        {
            Internal.NativeFunctions.StackPushInteger(nWalkStraightLineToPoint ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oToJumpTo != null ? oToJumpTo.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(385);
        }

        /// <summary>
        ///  Set whether oMapPin is enabled.
        ///  - oMapPin
        ///  - nEnabled: 0=Off, 1=On
        /// </summary>
        public static void SetMapPinEnabled(NWGameObject oMapPin, bool nEnabled)
        {
            Internal.NativeFunctions.StackPushInteger(nEnabled ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oMapPin != null ? oMapPin.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(386);
        }

        /// <summary>
        ///  Create a Hit Point Change When Dying effect.
        ///  - fHitPointChangePerRound: this can be positive or negative, but not zero.
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if fHitPointChangePerRound is 0.
        /// </summary>
        public static Effect EffectHitPointChangeWhenDying(float fHitPointChangePerRound)
        {
            Internal.NativeFunctions.StackPushFloat(fHitPointChangePerRound);
            Internal.NativeFunctions.CallBuiltIn(387);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Spawn a GUI panel for the client that controls oPC.
        ///  - oPC
        ///  - nGUIPanel: GUI_PANEL_*
        ///  * Nothing happens if oPC is not a player character or if an invalid value is
        ///    used for nGUIPanel.
        /// </summary>
        public static void PopUpGUIPanel(NWGameObject oPC, GuiPanel nGUIPanel)
        {
            Internal.NativeFunctions.StackPushInteger((int)nGUIPanel);
            Internal.NativeFunctions.StackPushObject(oPC != null ? oPC.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(388);
        }

        /// <summary>
        ///  Clear all personal feelings that oSource has about oTarget.
        /// </summary>
        public static void ClearPersonalReputation(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(389);
        }

        /// <summary>
        ///  oSource will temporarily be friends towards oTarget.
        ///  bDecays determines whether the personal reputation value decays over time
        ///  fDurationInSeconds is the length of time that the temporary friendship lasts
        ///  Make oSource into a temporary friend of oTarget using personal reputation.
        ///  - oTarget
        ///  - oSource
        ///  - bDecays: If this is true, the friendship decays over fDurationInSeconds;
        ///    otherwise it is indefinite.
        ///  - fDurationInSeconds: This is only used if bDecays is true, it is how long
        ///    the friendship lasts.
        ///  Note: If bDecays is true, the personal reputation amount decreases in size
        ///  over fDurationInSeconds. Friendship will only be in effect as long as
        ///  (faction reputation + total personal reputation) >= REPUTATION_TYPE_FRIEND.
        /// </summary>
        public static void SetIsTemporaryFriend(NWGameObject oTarget, NWGameObject oSource = null, bool bDecays = false, float fDurationInSeconds = 180.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fDurationInSeconds);
            Internal.NativeFunctions.StackPushInteger(bDecays ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(390);
        }

        /// <summary>
        ///  Make oSource into a temporary enemy of oTarget using personal reputation.
        ///  - oTarget
        ///  - oSource
        ///  - bDecays: If this is true, the enmity decays over fDurationInSeconds;
        ///    otherwise it is indefinite.
        ///  - fDurationInSeconds: This is only used if bDecays is true, it is how long
        ///    the enmity lasts.
        ///  Note: If bDecays is true, the personal reputation amount decreases in size
        ///  over fDurationInSeconds. Enmity will only be in effect as long as
        ///  (faction reputation + total personal reputation) <= REPUTATION_TYPE_ENEMY.
        /// </summary>
        public static void SetIsTemporaryEnemy(NWGameObject oTarget, NWGameObject oSource = null, bool bDecays = false, float fDurationInSeconds = 180.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fDurationInSeconds);
            Internal.NativeFunctions.StackPushInteger(bDecays ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(391);
        }

        /// <summary>
        ///  Make oSource temporarily neutral to oTarget using personal reputation.
        ///  - oTarget
        ///  - oSource
        ///  - bDecays: If this is true, the neutrality decays over fDurationInSeconds;
        ///    otherwise it is indefinite.
        ///  - fDurationInSeconds: This is only used if bDecays is true, it is how long
        ///    the neutrality lasts.
        ///  Note: If bDecays is true, the personal reputation amount decreases in size
        ///  over fDurationInSeconds. Neutrality will only be in effect as long as
        ///  (faction reputation + total personal reputation) > REPUTATION_TYPE_ENEMY and
        ///  (faction reputation + total personal reputation) < REPUTATION_TYPE_FRIEND.
        /// </summary>
        public static void SetIsTemporaryNeutral(NWGameObject oTarget, NWGameObject oSource = null, bool bDecays = false, float fDurationInSeconds = 180.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fDurationInSeconds);
            Internal.NativeFunctions.StackPushInteger(bDecays ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(392);
        }

        /// <summary>
        ///  Gives nXpAmount to oCreature.
        /// </summary>
        public static void GiveXPToCreature(NWGameObject oCreature, int nXpAmount)
        {
            Internal.NativeFunctions.StackPushInteger(nXpAmount);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(393);
        }

        /// <summary>
        ///  Sets oCreature's experience to nXpAmount.
        /// </summary>
        public static void SetXP(NWGameObject oCreature, int nXpAmount)
        {
            Internal.NativeFunctions.StackPushInteger(nXpAmount);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(394);
        }

        /// <summary>
        ///  Get oCreature's experience.
        /// </summary>
        public static int GetXP(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(395);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Convert nInteger to hex, returning the hex value as a string.
        ///  * Return value has the format "0x????????" where each ? will be a hex digit
        ///    (8 digits in total).
        /// </summary>
        public static string IntToHexString(int nInteger)
        {
            Internal.NativeFunctions.StackPushInteger(nInteger);
            Internal.NativeFunctions.CallBuiltIn(396);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Get the base item type (BASE_ITEM_*) of oItem.
        ///  * Returns BASE_ITEM_INVALID if oItem is an invalid item.
        /// </summary>
        public static BaseItemType GetBaseItemType(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(397);
            return (BaseItemType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Determines whether oItem has nProperty.
        ///  - oItem
        ///  - nProperty: ITEM_PROPERTY_*
        ///  * Returns false if oItem is not a valid item, or if oItem does not have
        ///    nProperty.
        /// </summary>
        public static bool GetItemHasItemProperty(NWGameObject oItem, ItemPropertyType nProperty)
        {
            Internal.NativeFunctions.StackPushInteger((int)nProperty);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(398);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  The creature will equip the melee weapon in its possession that can do the
        ///  most damage. If no valid melee weapon is found, it will equip the most
        ///  damaging range weapon. This function should only ever be called in the
        ///  EndOfCombatRound scripts, because otherwise it would have to stop the combat
        ///  round to run simulation.
        ///  - oVersus: You can try to get the most damaging weapon against oVersus
        ///  - bOffHand
        /// </summary>
        public static void ActionEquipMostDamagingMelee(NWGameObject oVersus = null, bool bOffHand = false)
        {
            Internal.NativeFunctions.StackPushInteger(bOffHand ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oVersus != null ? oVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(399);
        }

        /// <summary>
        ///  The creature will equip the range weapon in its possession that can do the
        ///  most damage.
        ///  If no valid range weapon can be found, it will equip the most damaging melee
        ///  weapon.
        ///  - oVersus: You can try to get the most damaging weapon against oVersus
        /// </summary>
        public static void ActionEquipMostDamagingRanged(NWGameObject oVersus = null)
        {
            Internal.NativeFunctions.StackPushObject(oVersus != null ? oVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(400);
        }

        /// <summary>
        ///  Get the Armour Class of oItem.
        ///  * Return 0 if the oItem is not a valid item, or if oItem has no armour value.
        /// </summary>
        public static int GetItemACValue(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(401);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  The creature will rest if not in combat and no enemies are nearby.
        ///  - bCreatureToEnemyLineOfSightCheck: true to allow the creature to rest if enemies
        ///                                      are nearby, but the creature can't see the enemy.
        ///                                      false the creature will not rest if enemies are
        ///                                      nearby regardless of whether or not the creature
        ///                                      can see them, such as if an enemy is close by,
        ///                                      but is in a different room behind a closed door.
        /// </summary>
        public static void ActionRest(bool bCreatureToEnemyLineOfSightCheck = false)
        {
            Internal.NativeFunctions.StackPushInteger(bCreatureToEnemyLineOfSightCheck ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(402);
        }

        /// <summary>
        ///  Expose/Hide the entire map of oArea for oPlayer.
        ///  - oArea: The area that the map will be exposed/hidden for.
        ///  - oPlayer: The player the map will be exposed/hidden for.
        ///  - bExplored: true/false. Whether the map should be completely explored or hidden.
        /// </summary>
        public static void ExploreAreaForPlayer(NWGameObject oArea, NWGameObject oPlayer, bool bExplored = true)
        {
            Internal.NativeFunctions.StackPushInteger(bExplored ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(403);
        }

        /// <summary>
        ///  The creature will equip the armour in its possession that has the highest
        ///  armour class.
        /// </summary>
        public static void ActionEquipMostEffectiveArmor()
        {
            Internal.NativeFunctions.CallBuiltIn(404);
        }

        /// <summary>
        ///  * Returns true if it is currently day.
        /// </summary>
        public static bool GetIsDay()
        {
            Internal.NativeFunctions.CallBuiltIn(405);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  * Returns true if it is currently night.
        /// </summary>
        public static bool GetIsNight()
        {
            Internal.NativeFunctions.CallBuiltIn(406);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  * Returns true if it is currently dawn.
        /// </summary>
        public static bool GetIsDawn()
        {
            Internal.NativeFunctions.CallBuiltIn(407);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  * Returns true if it is currently dusk.
        /// </summary>
        public static bool GetIsDusk()
        {
            Internal.NativeFunctions.CallBuiltIn(408);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  * Returns true if oCreature was spawned from an encounter.
        /// </summary>
        public static bool GetIsEncounterCreature(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(409);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Use this in an OnPlayerDying module script to get the last player who is dying.
        /// </summary>
        public static NWGameObject GetLastPlayerDying()
        {
            Internal.NativeFunctions.CallBuiltIn(410);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the starting location of the module.
        /// </summary>
        public static NWN.Location GetStartingLocation()
        {
            Internal.NativeFunctions.CallBuiltIn(411);
            return new NWN.Location(Internal.NativeFunctions.StackPopLocation());
        }

        /// <summary>
        ///  Make oCreatureToChange join one of the standard factions.
        ///  ** This will only work on an NPC **
        ///  - nStandardFaction: STANDARD_FACTION_*
        /// </summary>
        public static void ChangeToStandardFaction(NWGameObject oCreatureToChange, StandardFaction nStandardFaction)
        {
            Internal.NativeFunctions.StackPushInteger((int)nStandardFaction);
            Internal.NativeFunctions.StackPushObject(oCreatureToChange != null ? oCreatureToChange.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(412);
        }

        /// <summary>
        ///  Play oSound.
        /// </summary>
        public static void SoundObjectPlay(NWGameObject oSound)
        {
            Internal.NativeFunctions.StackPushObject(oSound != null ? oSound.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(413);
        }

        /// <summary>
        ///  Stop playing oSound.
        /// </summary>
        public static void SoundObjectStop(NWGameObject oSound)
        {
            Internal.NativeFunctions.StackPushObject(oSound != null ? oSound.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(414);
        }

        /// <summary>
        ///  Set the volume of oSound.
        ///  - oSound
        ///  - nVolume: 0-127
        /// </summary>
        public static void SoundObjectSetVolume(NWGameObject oSound, int nVolume)
        {
            Internal.NativeFunctions.StackPushInteger(nVolume);
            Internal.NativeFunctions.StackPushObject(oSound != null ? oSound.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(415);
        }

        /// <summary>
        ///  Set the position of oSound.
        /// </summary>
        public static void SoundObjectSetPosition(NWGameObject oSound, NWN.Vector? vPosition)
        {
            Internal.NativeFunctions.StackPushVector(vPosition.HasValue ? vPosition.Value : new NWN.Vector());
            Internal.NativeFunctions.StackPushObject(oSound != null ? oSound.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(416);
        }

        /// <summary>
        ///  Immediately speak a conversation one-liner.
        ///  - sDialogResRef
        ///  - oTokenTarget: This must be specified if there are creature-specific tokens
        ///    in the string.
        /// </summary>
        public static void SpeakOneLinerConversation(string sDialogResRef = "", NWGameObject oTokenTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTokenTarget != null ? oTokenTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushStringUTF8(sDialogResRef);
            Internal.NativeFunctions.CallBuiltIn(417);
        }

        /// <summary>
        ///  Get the amount of gold possessed by oTarget.
        /// </summary>
        public static int GetGold(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(418);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Use this in an OnRespawnButtonPressed module script to get the object id of
        ///  the player who last pressed the respawn button.
        /// </summary>
        public static NWGameObject GetLastRespawnButtonPresser()
        {
            Internal.NativeFunctions.CallBuiltIn(419);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Play a voice chat.
        ///  - nVoiceChatID: VOICE_CHAT_*
        ///  - oTarget
        /// </summary>
        public static void PlayVoiceChat(VoiceChat nVoiceChatID, NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nVoiceChatID);
            Internal.NativeFunctions.CallBuiltIn(421);
        }

        /// <summary>
        ///  * Returns true if the weapon equipped is capable of damaging oVersus.
        /// </summary>
        public static int GetIsWeaponEffective(NWGameObject oVersus = null, bool bOffHand = false)
        {
            Internal.NativeFunctions.StackPushInteger(bOffHand ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oVersus != null ? oVersus.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(422);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Use this in a SpellCast script to determine whether the spell was considered
        ///  harmful.
        ///  * Returns true if the last spell cast was harmful.
        /// </summary>
        public static bool GetLastSpellHarmful()
        {
            Internal.NativeFunctions.CallBuiltIn(423);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Activate oItem.
        /// </summary>
        public static NWN.Event EventActivateItem(NWGameObject oItem, NWN.Location lTarget, NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushLocation(lTarget.Handle);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(424);
            return new NWN.Event(Internal.NativeFunctions.StackPopEvent());
        }

        /// <summary>
        ///  Play the background music for oArea.
        /// </summary>
        public static void MusicBackgroundPlay(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(425);
        }

        /// <summary>
        ///  Stop the background music for oArea.
        /// </summary>
        public static void MusicBackgroundStop(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(426);
        }

        /// <summary>
        ///  Set the delay for the background music for oArea.
        ///  - oArea
        ///  - nDelay: delay in milliseconds
        /// </summary>
        public static void MusicBackgroundSetDelay(NWGameObject oArea, int nDelay)
        {
            Internal.NativeFunctions.StackPushInteger(nDelay);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(427);
        }

        /// <summary>
        ///  Change the background day track for oArea to nTrack.
        ///  - oArea
        ///  - nTrack
        /// </summary>
        public static void MusicBackgroundChangeDay(NWGameObject oArea, int nTrack)
        {
            Internal.NativeFunctions.StackPushInteger(nTrack);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(428);
        }

        /// <summary>
        ///  Change the background night track for oArea to nTrack.
        ///  - oArea
        ///  - nTrack
        /// </summary>
        public static void MusicBackgroundChangeNight(NWGameObject oArea, int nTrack)
        {
            Internal.NativeFunctions.StackPushInteger(nTrack);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(429);
        }

        /// <summary>
        ///  Play the battle music for oArea.
        /// </summary>
        public static void MusicBattlePlay(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(430);
        }

        /// <summary>
        ///  Stop the battle music for oArea.
        /// </summary>
        public static void MusicBattleStop(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(431);
        }

        /// <summary>
        ///  Change the battle track for oArea.
        ///  - oArea
        ///  - nTrack
        /// </summary>
        public static void MusicBattleChange(NWGameObject oArea, int nTrack)
        {
            Internal.NativeFunctions.StackPushInteger(nTrack);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(432);
        }

        /// <summary>
        ///  Play the ambient sound for oArea.
        /// </summary>
        public static void AmbientSoundPlay(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(433);
        }

        /// <summary>
        ///  Stop the ambient sound for oArea.
        /// </summary>
        public static void AmbientSoundStop(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(434);
        }

        /// <summary>
        ///  Change the ambient day track for oArea to nTrack.
        ///  - oArea
        ///  - nTrack
        /// </summary>
        public static void AmbientSoundChangeDay(NWGameObject oArea, int nTrack)
        {
            Internal.NativeFunctions.StackPushInteger(nTrack);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(435);
        }

        /// <summary>
        ///  Change the ambient night track for oArea to nTrack.
        ///  - oArea
        ///  - nTrack
        /// </summary>
        public static void AmbientSoundChangeNight(NWGameObject oArea, int nTrack)
        {
            Internal.NativeFunctions.StackPushInteger(nTrack);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(436);
        }

        /// <summary>
        ///  Get the object that killed the caller.
        /// </summary>
        public static NWGameObject GetLastKiller()
        {
            Internal.NativeFunctions.CallBuiltIn(437);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in a spell script to get the item used to cast the spell.
        /// </summary>
        public static NWGameObject GetSpellCastItem()
        {
            Internal.NativeFunctions.CallBuiltIn(438);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the item that was activated.
        /// </summary>
        public static NWGameObject GetItemActivated()
        {
            Internal.NativeFunctions.CallBuiltIn(439);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the creature that
        ///  activated the item.
        /// </summary>
        public static NWGameObject GetItemActivator()
        {
            Internal.NativeFunctions.CallBuiltIn(440);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the location of the item's
        ///  target.
        /// </summary>
        public static NWN.Location GetItemActivatedTargetLocation()
        {
            Internal.NativeFunctions.CallBuiltIn(441);
            return new NWN.Location(Internal.NativeFunctions.StackPopLocation());
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the item's target.
        /// </summary>
        public static NWGameObject GetItemActivatedTarget()
        {
            Internal.NativeFunctions.CallBuiltIn(442);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  * Returns true if oObject (which is a placeable or a door) is currently open.
        /// </summary>
        public static bool GetIsOpen(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(443);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Take nAmount of gold from oCreatureToTakeFrom.
        ///  - nAmount
        ///  - oCreatureToTakeFrom: If this is not a valid creature, nothing will happen.
        ///  - bDestroy: If this is true, the caller will not get the gold.  Instead, the
        ///    gold will be destroyed and will vanish from the game.
        /// </summary>
        public static void TakeGoldFromCreature(int nAmount, NWGameObject oCreatureToTakeFrom, bool bDestroy = false)
        {
            Internal.NativeFunctions.StackPushInteger(bDestroy ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oCreatureToTakeFrom != null ? oCreatureToTakeFrom.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nAmount);
            Internal.NativeFunctions.CallBuiltIn(444);
        }

        /// <summary>
        ///  Determine whether oObject is in conversation.
        /// </summary>
        public static bool IsInConversation(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(445);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Create an Ability Decrease effect.
        ///  - nAbility: ABILITY_*
        ///  - nModifyBy: This is the amount by which to decrement the ability
        /// </summary>
        public static Effect EffectAbilityDecrease(Ability nAbility, int nModifyBy)
        {
            Internal.NativeFunctions.StackPushInteger(nModifyBy);
            Internal.NativeFunctions.StackPushInteger((int)nAbility);
            Internal.NativeFunctions.CallBuiltIn(446);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create an Attack Decrease effect.
        ///  - nPenalty
        ///  - nModifierType: ATTACK_BONUS_*
        /// </summary>
        public static Effect EffectAttackDecrease(int nPenalty, AttackBonus nModifierType = AttackBonus.Misc)
        {
            Internal.NativeFunctions.StackPushInteger((int)nModifierType);
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.CallBuiltIn(447);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Damage Decrease effect.
        ///  - nPenalty
        ///  - nDamageType: DAMAGE_TYPE_*
        /// </summary>
        public static Effect EffectDamageDecrease(int nPenalty, DamageType nDamageType = DamageType.Magical)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.CallBuiltIn(448);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Damage Immunity Decrease effect.
        ///  - nDamageType: DAMAGE_TYPE_*
        ///  - nPercentImmunity
        /// </summary>
        public static Effect EffectDamageImmunityDecrease(DamageType nDamageType, int nPercentImmunity)
        {
            Internal.NativeFunctions.StackPushInteger(nPercentImmunity);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(449);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create an AC Decrease effect.
        ///  - nValue
        ///  - nModifyType: AC_*
        ///  - nDamageType: DAMAGE_TYPE_*
        ///    * Default value for nDamageType should only ever be used in this function prototype.
        /// </summary>
        public static Effect EffectACDecrease(int nValue, AC nModifyType = AC.DodgeBonus, DamageType nDamageType = DamageType.ACVsDamageTypeAll)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger((int)nModifyType);
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.CallBuiltIn(450);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Movement Speed Decrease effect.
        ///  - nPercentChange - range 0 through 99
        ///  eg.
        ///     0 = no change in speed
        ///    50 = 50% slower
        ///    99 = almost immobile
        /// </summary>
        public static Effect EffectMovementSpeedDecrease(int nPercentChange)
        {
            Internal.NativeFunctions.StackPushInteger(nPercentChange);
            Internal.NativeFunctions.CallBuiltIn(451);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Saving Throw Decrease effect.
        ///  - nSave: SAVING_THROW_* (not SAVING_THROW_TYPE_*)
        ///           SAVING_THROW_ALL
        ///           SAVING_THROW_FORT
        ///           SAVING_THROW_REFLEX
        ///           SAVING_THROW_WILL 
        ///  - nValue: size of the Saving Throw decrease
        ///  - nSaveType: SAVING_THROW_TYPE_* (e.g. SAVING_THROW_TYPE_ACID )
        /// </summary>
        public static Effect EffectSavingThrowDecrease(int nSave, int nValue, SavingThrowType nSaveType = SavingThrowType.All)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSaveType);
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.StackPushInteger(nSave);
            Internal.NativeFunctions.CallBuiltIn(452);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Skill Decrease effect.
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nSkill is invalid.
        /// </summary>
        public static Effect EffectSkillDecrease(Skill nSkill, int nValue)
        {
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(453);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Spell Resistance Decrease effect.
        /// </summary>
        public static Effect EffectSpellResistanceDecrease(int nValue)
        {
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.CallBuiltIn(454);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Determine whether oTarget is a plot object.
        /// </summary>
        public static bool GetPlotFlag(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(455);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Set oTarget's plot object status.
        /// </summary>
        public static void SetPlotFlag(NWGameObject oTarget, bool nPlotFlag)
        {
            Internal.NativeFunctions.StackPushInteger(nPlotFlag ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(456);
        }

        /// <summary>
        ///  Create an Invisibility effect.
        ///  - nInvisibilityType: INVISIBILITY_TYPE_*
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nInvisibilityType
        ///    is invalid.
        /// </summary>
        public static Effect EffectInvisibility(InvisibilityType nInvisibilityType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nInvisibilityType);
            Internal.NativeFunctions.CallBuiltIn(457);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Concealment effect.
        ///  - nPercentage: 1-100 inclusive
        ///  - nMissChanceType: MISS_CHANCE_TYPE_*
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nPercentage < 1 or
        ///    nPercentage > 100.
        /// </summary>
        public static Effect EffectConcealment(int nPercentage, MissChanceType nMissType = MissChanceType.Normal)
        {
            Internal.NativeFunctions.StackPushInteger((int)nMissType);
            Internal.NativeFunctions.StackPushInteger(nPercentage);
            Internal.NativeFunctions.CallBuiltIn(458);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Darkness effect.
        /// </summary>
        public static Effect EffectDarkness()
        {
            Internal.NativeFunctions.CallBuiltIn(459);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Dispel Magic All effect.
        ///  If no parameter is specified, USE_CREATURE_LEVEL will be used. This will
        ///  cause the dispel effect to use the level of the creature that created the
        ///  effect.
        /// </summary>
        public static Effect EffectDispelMagicAll(int nCasterLevel = NWNConstants.UseCreatureLevel)
        {
            Internal.NativeFunctions.StackPushInteger(nCasterLevel);
            Internal.NativeFunctions.CallBuiltIn(460);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create an Ultravision effect.
        /// </summary>
        public static Effect EffectUltravision()
        {
            Internal.NativeFunctions.CallBuiltIn(461);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Negative Level effect.
        ///  - nNumLevels: the number of negative levels to apply.
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nNumLevels > 100.
        /// </summary>
        public static Effect EffectNegativeLevel(int nNumLevels, bool bHPBonus = false)
        {
            Internal.NativeFunctions.StackPushInteger(bHPBonus ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(nNumLevels);
            Internal.NativeFunctions.CallBuiltIn(462);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Polymorph effect.
        /// </summary>
        public static Effect EffectPolymorph(int nPolymorphSelection, bool nLocked = false)
        {
            Internal.NativeFunctions.StackPushInteger(nLocked ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(nPolymorphSelection);
            Internal.NativeFunctions.CallBuiltIn(463);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Sanctuary effect.
        ///  - nDifficultyClass: must be a non-zero, positive number
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nDifficultyClass <= 0.
        /// </summary>
        public static Effect EffectSanctuary(int nDifficultyClass)
        {
            Internal.NativeFunctions.StackPushInteger(nDifficultyClass);
            Internal.NativeFunctions.CallBuiltIn(464);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a True Seeing effect.
        /// </summary>
        public static Effect EffectTrueSeeing()
        {
            Internal.NativeFunctions.CallBuiltIn(465);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a See Invisible effect.
        /// </summary>
        public static Effect EffectSeeInvisible()
        {
            Internal.NativeFunctions.CallBuiltIn(466);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Time Stop effect.
        /// </summary>
        public static Effect EffectTimeStop()
        {
            Internal.NativeFunctions.CallBuiltIn(467);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Blindness effect.
        /// </summary>
        public static Effect EffectBlindness()
        {
            Internal.NativeFunctions.CallBuiltIn(468);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Determine whether oSource has a friendly reaction towards oTarget, depending
        ///  on the reputation, PVP setting and (if both oSource and oTarget are PCs),
        ///  oSource's Like/Dislike setting for oTarget.
        ///  Note: If you just want to know how two objects feel about each other in terms
        ///  of faction and personal reputation, use GetIsFriend() instead.
        ///  * Returns true if oSource has a friendly reaction towards oTarget
        /// </summary>
        public static bool GetIsReactionTypeFriendly(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(469);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Determine whether oSource has a neutral reaction towards oTarget, depending
        ///  on the reputation, PVP setting and (if both oSource and oTarget are PCs),
        ///  oSource's Like/Dislike setting for oTarget.
        ///  Note: If you just want to know how two objects feel about each other in terms
        ///  of faction and personal reputation, use GetIsNeutral() instead.
        ///  * Returns true if oSource has a neutral reaction towards oTarget
        /// </summary>
        public static bool GetIsReactionTypeNeutral(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(470);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Determine whether oSource has a Hostile reaction towards oTarget, depending
        ///  on the reputation, PVP setting and (if both oSource and oTarget are PCs),
        ///  oSource's Like/Dislike setting for oTarget.
        ///  Note: If you just want to know how two objects feel about each other in terms
        ///  of faction and personal reputation, use GetIsEnemy() instead.
        ///  * Returns true if oSource has a hostile reaction towards oTarget
        /// </summary>
        public static bool GetIsReactionTypeHostile(NWGameObject oTarget, NWGameObject oSource = null)
        {
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(471);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Create a Spell Level Absorption effect.
        ///  - nMaxSpellLevelAbsorbed: maximum spell level that will be absorbed by the
        ///    effect
        ///  - nTotalSpellLevelsAbsorbed: maximum number of spell levels that will be
        ///    absorbed by the effect
        ///  - nSpellSchool: SPELL_SCHOOL_*
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if:
        ///    nMaxSpellLevelAbsorbed is not between -1 and 9 inclusive, or nSpellSchool
        ///    is invalid.
        /// </summary>
        public static Effect EffectSpellLevelAbsorption(int nMaxSpellLevelAbsorbed, int nTotalSpellLevelsAbsorbed = 0, SpellSchool nSpellSchool = SpellSchool.General)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSpellSchool);
            Internal.NativeFunctions.StackPushInteger(nTotalSpellLevelsAbsorbed);
            Internal.NativeFunctions.StackPushInteger(nMaxSpellLevelAbsorbed);
            Internal.NativeFunctions.CallBuiltIn(472);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Dispel Magic Best effect.
        ///  If no parameter is specified, USE_CREATURE_LEVEL will be used. This will
        ///  cause the dispel effect to use the level of the creature that created the
        ///  effect.
        /// </summary>
        public static Effect EffectDispelMagicBest(int nCasterLevel = NWNConstants.UseCreatureLevel)
        {
            Internal.NativeFunctions.StackPushInteger(nCasterLevel);
            Internal.NativeFunctions.CallBuiltIn(473);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Try to send oTarget to a new server defined by sIPaddress.
        ///  - oTarget
        ///  - sIPaddress: this can be numerical "192.168.0.84" or alphanumeric
        ///    "www.bioware.com". It can also contain a port "192.168.0.84:5121" or
        ///    "www.bioware.com:5121"; if the port is not specified, it will default to
        ///    5121.
        ///  - sPassword: login password for the destination server
        ///  - sWaypointTag: if this is set, after portalling the character will be moved
        ///    to this waypoint if it exists
        ///  - bSeamless: if this is set, the client wil not be prompted with the
        ///    information window telling them about the server, and they will not be
        ///    allowed to save a copy of their character if they are using a local vault
        ///    character.
        /// </summary>
        public static void ActivatePortal(NWGameObject oTarget, string sIPaddress = "", string sPassword = "", string sWaypointTag = "", bool bSeemless = false)
        {
            Internal.NativeFunctions.StackPushInteger(bSeemless ? 1 : 0);
            Internal.NativeFunctions.StackPushStringUTF8(sWaypointTag);
            Internal.NativeFunctions.StackPushStringUTF8(sPassword);
            Internal.NativeFunctions.StackPushStringUTF8(sIPaddress);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(474);
        }

        /// <summary>
        ///  Get the number of stacked items that oItem comprises.
        /// </summary>
        public static int GetNumStackedItems(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(475);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Use this on an NPC to cause all creatures within a 10-metre radius to stop
        ///  what they are doing and sets the NPC's enemies within this range to be
        ///  neutral towards the NPC for roughly 3 minutes. If this command is run on a PC
        ///  or an object that is not a creature, nothing will happen.
        /// </summary>
        public static void SurrenderToEnemies()
        {
            Internal.NativeFunctions.CallBuiltIn(476);
        }

        /// <summary>
        ///  Create a Miss Chance effect.
        ///  - nPercentage: 1-100 inclusive
        ///  - nMissChanceType: MISS_CHANCE_TYPE_*
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nPercentage < 1 or
        ///    nPercentage > 100.
        /// </summary>
        public static Effect EffectMissChance(int nPercentage, MissChanceType nMissChanceType = MissChanceType.Normal)
        {
            Internal.NativeFunctions.StackPushInteger((int)nMissChanceType);
            Internal.NativeFunctions.StackPushInteger(nPercentage);
            Internal.NativeFunctions.CallBuiltIn(477);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the number of Hitdice worth of Turn Resistance that oUndead may have.
        ///  This will only work on undead creatures.
        /// </summary>
        public static int GetTurnResistanceHD(NWGameObject oUndead = null)
        {
            Internal.NativeFunctions.StackPushObject(oUndead != null ? oUndead.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(478);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the size (CREATURE_SIZE_*) of oCreature.
        /// </summary>
        public static CreatureSize GetCreatureSize(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(479);
            return (CreatureSize)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Create a Disappear/Appear effect.
        ///  The object will "fly away" for the duration of the effect and will reappear
        ///  at lLocation.
        ///  - nAnimation determines which appear and disappear animations to use. Most creatures
        ///  only have animation 1, although a few have 2 (like beholders)
        /// </summary>
        public static Effect EffectDisappearAppear(NWN.Location lLocation, int nAnimation = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nAnimation);
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.CallBuiltIn(480);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Disappear effect to make the object "fly away" and then destroy
        ///  itself.
        ///  - nAnimation determines which appear and disappear animations to use. Most creatures
        ///  only have animation 1, although a few have 2 (like beholders)
        /// </summary>
        public static Effect EffectDisappear(int nAnimation = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nAnimation);
            Internal.NativeFunctions.CallBuiltIn(481);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create an Appear effect to make the object "fly in".
        ///  - nAnimation determines which appear and disappear animations to use. Most creatures
        ///  only have animation 1, although a few have 2 (like beholders)
        /// </summary>
        public static Effect EffectAppear(int nAnimation = 1)
        {
            Internal.NativeFunctions.StackPushInteger(nAnimation);
            Internal.NativeFunctions.CallBuiltIn(482);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  The action subject will unlock oTarget, which can be a door or a placeable
        ///  object.
        /// </summary>
        public static void ActionUnlockObject(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(483);
        }

        /// <summary>
        ///  The action subject will lock oTarget, which can be a door or a placeable
        ///  object.
        /// </summary>
        public static void ActionLockObject(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(484);
        }

        /// <summary>
        ///  Create a Modify Attacks effect to add attacks.
        ///  - nAttacks: maximum is 5, even with the effect stacked
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nAttacks > 5.
        /// </summary>
        public static Effect EffectModifyAttacks(int nAttacks)
        {
            Internal.NativeFunctions.StackPushInteger(nAttacks);
            Internal.NativeFunctions.CallBuiltIn(485);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the last trap detected by oTarget.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static NWGameObject GetLastTrapDetected(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(486);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Create a Damage Shield effect which does (nDamageAmount + nRandomAmount)
        ///  damage to any melee attacker on a successful attack of damage type nDamageType.
        ///  - nDamageAmount: an integer value
        ///  - nRandomAmount: DAMAGE_BONUS_*
        ///  - nDamageType: DAMAGE_TYPE_*
        ///  NOTE! You *must* use the DAMAGE_BONUS_* constants! Using other values may
        ///        result in odd behaviour.
        /// </summary>
        public static Effect EffectDamageShield(int nDamageAmount, DamageBonus nRandomAmount, DamageType nDamageType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger((int)nRandomAmount);
            Internal.NativeFunctions.StackPushInteger(nDamageAmount);
            Internal.NativeFunctions.CallBuiltIn(487);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Get the trap nearest to oTarget.
        ///  Note : "trap objects" are actually any trigger, placeable or door that is
        ///  trapped in oTarget's area.
        ///  - oTarget
        ///  - nTrapDetected: if this is true, the trap returned has to have been detected
        ///    by oTarget.
        /// </summary>
        public static NWGameObject GetNearestTrapToObject(NWGameObject oTarget = null, bool nTrapDetected = true)
        {
            Internal.NativeFunctions.StackPushInteger(nTrapDetected ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(488);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the name of oCreature's deity.
        ///  * Returns "" if oCreature is invalid (or if the deity name is blank for
        ///    oCreature).
        /// </summary>
        public static string GetDeity(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(489);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Get the name of oCreature's sub race.
        ///  * Returns "" if oCreature is invalid (or if sub race is blank for oCreature).
        /// </summary>
        public static string GetSubRace(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(490);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Get oTarget's base fortitude saving throw value (this will only work for
        ///  creatures, doors, and placeables).
        ///  * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetFortitudeSavingThrow(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(491);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oTarget's base will saving throw value (this will only work for creatures,
        ///  doors, and placeables).
        ///  * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetWillSavingThrow(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(492);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oTarget's base reflex saving throw value (this will only work for
        ///  creatures, doors, and placeables).
        ///  * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetReflexSavingThrow(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(493);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oCreature's challenge rating.
        ///  * Returns 0.0 if oCreature is invalid.
        /// </summary>
        public static float GetChallengeRating(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(494);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Get oCreature's age.
        ///  * Returns 0 if oCreature is invalid.
        /// </summary>
        public static int GetAge(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(495);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oCreature's movement rate.
        ///  * Returns 0 if oCreature is invalid.
        /// </summary>
        public static CreatureMovementRate GetMovementRate(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(496);
            return (CreatureMovementRate)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oCreature's familiar creature type (FAMILIAR_CREATURE_TYPE_*).
        ///  * Returns FAMILIAR_CREATURE_TYPE_NONE if oCreature is invalid or does not
        ///    currently have a familiar.
        /// </summary>
        public static FamiliarCreatureType GetFamiliarCreatureType(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(497);
            return (FamiliarCreatureType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oCreature's animal companion creature type
        ///  (ANIMAL_COMPANION_CREATURE_TYPE_*).
        ///  * Returns ANIMAL_COMPANION_CREATURE_TYPE_NONE if oCreature is invalid or does
        ///    not currently have an animal companion.
        /// </summary>
        public static AnimalCompanionCreatureType GetAnimalCompanionCreatureType(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(498);
            return (AnimalCompanionCreatureType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get oCreature's familiar's name.
        ///  * Returns "" if oCreature is invalid, does not currently
        ///  have a familiar or if the familiar's name is blank.
        /// </summary>
        public static string GetFamiliarName(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(499);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Get oCreature's animal companion's name.
        ///  * Returns "" if oCreature is invalid, does not currently
        ///  have an animal companion or if the animal companion's name is blank.
        /// </summary>
        public static string GetAnimalCompanionName(NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(500);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  The action subject will fake casting a spell at oTarget; the conjure and cast
        ///  animations and visuals will occur, nothing else.
        ///  - nSpell
        ///  - oTarget
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        /// </summary>
        public static void ActionCastFakeSpellAtObject(Spell nSpell, NWGameObject oTarget, ProjectilePathType nProjectilePathType = ProjectilePathType.Default)
        {
            Internal.NativeFunctions.StackPushInteger((int)nProjectilePathType);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(501);
        }

        /// <summary>
        ///  The action subject will fake casting a spell at lLocation; the conjure and
        ///  cast animations and visuals will occur, nothing else.
        ///  - nSpell
        ///  - lTarget
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        /// </summary>
        public static void ActionCastFakeSpellAtLocation(Spell nSpell, NWN.Location lTarget, ProjectilePathType nProjectilePathType = ProjectilePathType.Default)
        {
            Internal.NativeFunctions.StackPushInteger((int)nProjectilePathType);
            Internal.NativeFunctions.StackPushLocation(lTarget.Handle);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(502);
        }

        /// <summary>
        ///  Removes oAssociate from the service of oMaster, returning them to their
        ///  original faction.
        /// </summary>
        public static void RemoveSummonedAssociate(NWGameObject oMaster, NWGameObject oAssociate = null)
        {
            Internal.NativeFunctions.StackPushObject(oAssociate != null ? oAssociate.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oMaster != null ? oMaster.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(503);
        }

        /// <summary>
        ///  Set the camera mode for oPlayer.
        ///  - oPlayer
        ///  - nCameraMode: CAMERA_MODE_*
        ///  * If oPlayer is not player-controlled or nCameraMode is invalid, nothing
        ///    happens.
        /// </summary>
        public static void SetCameraMode(NWGameObject oPlayer, CameraMode nCameraMode)
        {
            Internal.NativeFunctions.StackPushInteger((int)nCameraMode);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(504);
        }

        /// <summary>
        ///  * Returns true if oCreature is resting.
        /// </summary>
        public static bool GetIsResting(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(505);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the last PC that has rested in the module.
        /// </summary>
        public static NWGameObject GetLastPCRested()
        {
            Internal.NativeFunctions.CallBuiltIn(506);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Set the weather for oTarget.
        ///  - oTarget: if this is GetModule(), all outdoor areas will be modified by the
        ///    weather constant. If it is an area, oTarget will play the weather only if
        ///    it is an outdoor area.
        ///  - nWeather: WEATHER_*
        ///    -> WEATHER_USER_AREA_SETTINGS will set the area back to random weather.
        ///    -> WEATHER_CLEAR, WEATHER_RAIN, WEATHER_SNOW will make the weather go to
        ///       the appropriate precipitation *without stopping*.
        /// </summary>
        public static void SetWeather(NWGameObject oTarget, Weather nWeather)
        {
            Internal.NativeFunctions.StackPushInteger((int)nWeather);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(507);
        }

        /// <summary>
        ///  Determine the type (REST_EVENTTYPE_REST_*) of the last rest event (as
        ///  returned from the OnPCRested module event).
        /// </summary>
        public static RestEventType GetLastRestEventType()
        {
            Internal.NativeFunctions.CallBuiltIn(508);
            return (RestEventType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Shut down the currently loaded module and start a new one (moving all
        ///  currently-connected players to the starting point.
        /// </summary>
        public static void StartNewModule(string sModuleName)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sModuleName);
            Internal.NativeFunctions.CallBuiltIn(509);
        }

        /// <summary>
        ///  Create a Swarm effect.
        ///  - nLooping: If this is true, for the duration of the effect when one creature
        ///    created by this effect dies, the next one in the list will be created.  If
        ///    the last creature in the list dies, we loop back to the beginning and
        ///    sCreatureTemplate1 will be created, and so on...
        ///  - sCreatureTemplate1
        ///  - sCreatureTemplate2
        ///  - sCreatureTemplate3
        ///  - sCreatureTemplate4
        /// </summary>
        public static Effect EffectSwarm(bool nLooping, string sCreatureTemplate1, string sCreatureTemplate2 = "", string sCreatureTemplate3 = "", string sCreatureTemplate4 = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sCreatureTemplate4);
            Internal.NativeFunctions.StackPushStringUTF8(sCreatureTemplate3);
            Internal.NativeFunctions.StackPushStringUTF8(sCreatureTemplate2);
            Internal.NativeFunctions.StackPushStringUTF8(sCreatureTemplate1);
            Internal.NativeFunctions.StackPushInteger(nLooping ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(510);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  * Returns true if oItem is a ranged weapon.
        /// </summary>
        public static bool GetWeaponRanged(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(511);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Only if we are in a single player game, AutoSave the game.
        /// </summary>
        public static void DoSinglePlayerAutoSave()
        {
            Internal.NativeFunctions.CallBuiltIn(512);
        }

        /// <summary>
        ///  Get the game difficulty (GAME_DIFFICULTY_*).
        /// </summary>
        public static GameDifficulty GetGameDifficulty()
        {
            Internal.NativeFunctions.CallBuiltIn(513);
            return (GameDifficulty)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the main light color on the tile at lTileLocation.
        ///  - lTileLocation: the vector part of this is the tile grid (x,y) coordinate of
        ///    the tile.
        ///  - nMainLight1Color: TILE_MAIN_LIGHT_COLOR_*
        ///  - nMainLight2Color: TILE_MAIN_LIGHT_COLOR_*
        /// </summary>
        public static void SetTileMainLightColor(NWN.Location lTileLocation, TileMainLightColor nMainLight1Color, TileMainLightColor nMainLight2Color)
        {
            Internal.NativeFunctions.StackPushInteger((int)nMainLight2Color);
            Internal.NativeFunctions.StackPushInteger((int)nMainLight1Color);
            Internal.NativeFunctions.StackPushLocation(lTileLocation.Handle);
            Internal.NativeFunctions.CallBuiltIn(514);
        }

        /// <summary>
        ///  Set the source light color on the tile at lTileLocation.
        ///  - lTileLocation: the vector part of this is the tile grid (x,y) coordinate of
        ///    the tile.
        ///  - nSourceLight1Color: TILE_SOURCE_LIGHT_COLOR_*
        ///  - nSourceLight2Color: TILE_SOURCE_LIGHT_COLOR_*
        /// </summary>
        public static void SetTileSourceLightColor(NWN.Location lTileLocation, TileSourceLightColor nSourceLight1Color, TileSourceLightColor nSourceLight2Color)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSourceLight2Color);
            Internal.NativeFunctions.StackPushInteger((int)nSourceLight1Color);
            Internal.NativeFunctions.StackPushLocation(lTileLocation.Handle);
            Internal.NativeFunctions.CallBuiltIn(515);
        }

        /// <summary>
        ///  All clients in oArea will recompute the static lighting.
        ///  This can be used to update the lighting after changing any tile lights or if
        ///  placeables with lights have been added/deleted.
        /// </summary>
        public static void RecomputeStaticLighting(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(516);
        }

        /// <summary>
        ///  Get the color (TILE_MAIN_LIGHT_COLOR_*) for the main light 1 of the tile at
        ///  lTile.
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the tile.
        /// </summary>
        public static TileMainLightColor GetTileMainLight1Color(NWN.Location lTile)
        {
            Internal.NativeFunctions.StackPushLocation(lTile.Handle);
            Internal.NativeFunctions.CallBuiltIn(517);
            return (TileMainLightColor)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the color (TILE_MAIN_LIGHT_COLOR_*) for the main light 2 of the tile at
        ///  lTile.
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the
        ///    tile.
        /// </summary>
        public static TileMainLightColor GetTileMainLight2Color(NWN.Location lTile)
        {
            Internal.NativeFunctions.StackPushLocation(lTile.Handle);
            Internal.NativeFunctions.CallBuiltIn(518);
            return (TileMainLightColor)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the color (TILE_SOURCE_LIGHT_COLOR_*) for the source light 1 of the tile
        ///  at lTile.
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the
        ///    tile.
        /// </summary>
        public static TileSourceLightColor GetTileSourceLight1Color(NWN.Location lTile)
        {
            Internal.NativeFunctions.StackPushLocation(lTile.Handle);
            Internal.NativeFunctions.CallBuiltIn(519);
            return (TileSourceLightColor)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the color (TILE_SOURCE_LIGHT_COLOR_*) for the source light 2 of the tile
        ///  at lTile.
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the
        ///    tile.
        /// </summary>
        public static TileSourceLightColor GetTileSourceLight2Color(NWN.Location lTile)
        {
            Internal.NativeFunctions.StackPushLocation(lTile.Handle);
            Internal.NativeFunctions.CallBuiltIn(520);
            return (TileSourceLightColor)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Make the corresponding panel button on the player's client start or stop
        ///  flashing.
        ///  - oPlayer
        ///  - nButton: PANEL_BUTTON_*
        ///  - nEnableFlash: if this is true nButton will start flashing.  It if is false,
        ///    nButton will stop flashing.
        /// </summary>
        public static void SetPanelButtonFlash(NWGameObject oPlayer, PanelButton nButton, bool nEnableFlash)
        {
            Internal.NativeFunctions.StackPushInteger(nEnableFlash ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nButton);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(521);
        }

        /// <summary>
        ///  Get the current action (ACTION_*) that oObject is executing.
        /// </summary>
        public static ActionType GetCurrentAction(NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(522);
            return (ActionType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set how nStandardFaction feels about oCreature.
        ///  - nStandardFaction: STANDARD_FACTION_*
        ///  - nNewReputation: 0-100 (inclusive)
        ///  - oCreature
        /// </summary>
        public static void SetStandardFactionReputation(StandardFaction nStandardFaction, int nNewReputation, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nNewReputation);
            Internal.NativeFunctions.StackPushInteger((int)nStandardFaction);
            Internal.NativeFunctions.CallBuiltIn(523);
        }

        /// <summary>
        ///  Find out how nStandardFaction feels about oCreature.
        ///  - nStandardFaction: STANDARD_FACTION_*
        ///  - oCreature
        ///  Returns -1 on an error.
        ///  Returns 0-100 based on the standing of oCreature within the faction nStandardFaction.
        ///  0-10   :  Hostile.
        ///  11-89  :  Neutral.
        ///  90-100 :  Friendly.
        /// </summary>
        public static int GetStandardFactionReputation(StandardFaction nStandardFaction, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nStandardFaction);
            Internal.NativeFunctions.CallBuiltIn(524);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Display floaty text above the specified creature.
        ///  The text will also appear in the chat buffer of each player that receives the
        ///  floaty text.
        ///  - nStrRefToDisplay: String ref (therefore text is translated)
        ///  - oCreatureToFloatAbove
        ///  - bBroadcastToFaction: If this is true then only creatures in the same faction
        ///    as oCreatureToFloatAbove
        ///    will see the floaty text, and only if they are within range (30 metres).
        /// </summary>
        public static void FloatingTextStrRefOnCreature(int nStrRefToDisplay, NWGameObject oCreatureToFloatAbove, bool bBroadcastToFaction = true)
        {
            Internal.NativeFunctions.StackPushInteger(bBroadcastToFaction ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oCreatureToFloatAbove != null ? oCreatureToFloatAbove.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nStrRefToDisplay);
            Internal.NativeFunctions.CallBuiltIn(525);
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger
        ///  * Returns true if oTrapObject is disarmable.
        /// </summary>
        public static bool GetTrapDisarmable(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(527);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger
        ///  * Returns true if oTrapObject is detectable.
        /// </summary>
        public static bool GetTrapDetectable(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(528);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger
        ///  - oCreature
        ///  * Returns true if oCreature has detected oTrapObject
        /// </summary>
        public static bool GetTrapDetectedBy(NWGameObject oTrapObject, NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(529);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger
        ///  * Returns true if oTrapObject has been flagged as visible to all creatures.
        /// </summary>
        public static bool GetTrapFlagged(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(530);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the trap base type (TRAP_BASE_TYPE_*) of oTrapObject.
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static TrapBaseType GetTrapBaseType(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(531);
            return (TrapBaseType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger
        ///  * Returns true if oTrapObject is one-shot (i.e. it does not reset itself
        ///    after firing.
        /// </summary>
        public static bool GetTrapOneShot(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(532);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the creator of oTrapObject, the creature that set the trap.
        ///  - oTrapObject: a placeable, door or trigger
        ///  * Returns OBJECT_INVALID if oTrapObject was created in the toolset.
        /// </summary>
        public static NWGameObject GetTrapCreator(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(533);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the tag of the key that will disarm oTrapObject.
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static string GetTrapKeyTag(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(534);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Get the DC for disarming oTrapObject.
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static int GetTrapDisarmDC(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(535);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the DC for detecting oTrapObject.
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static int GetTrapDetectDC(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(536);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  * Returns true if a specific key is required to open the lock on oObject.
        /// </summary>
        public static bool GetLockKeyRequired(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(537);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the tag of the key that will open the lock on oObject.
        /// </summary>
        public static string GetLockKeyTag(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(538);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  * Returns true if the lock on oObject is lockable.
        /// </summary>
        public static bool GetLockLockable(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(539);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the DC for unlocking oObject.
        /// </summary>
        public static int GetLockUnlockDC(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(540);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the DC for locking oObject.
        /// </summary>
        public static int GetLockLockDC(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(541);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the last PC that levelled up.
        /// </summary>
        public static NWGameObject GetPCLevellingUp()
        {
            Internal.NativeFunctions.CallBuiltIn(542);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  - nFeat: FEAT_*
        ///  - oObject
        ///  * Returns true if oObject has effects on it originating from nFeat.
        /// </summary>
        public static bool GetHasFeatEffect(Feat nFeat, NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nFeat);
            Internal.NativeFunctions.CallBuiltIn(543);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Set the status of the illumination for oPlaceable.
        ///  - oPlaceable
        ///  - bIlluminate: if this is true, oPlaceable's illumination will be turned on.
        ///    If this is false, oPlaceable's illumination will be turned off.
        ///  Note: You must call RecomputeStaticLighting() after calling this function in
        ///  order for the changes to occur visually for the players.
        ///  SetPlaceableIllumination() buffers the illumination changes, which are then
        ///  sent out to the players once RecomputeStaticLighting() is called.  As such,
        ///  it is best to call SetPlaceableIllumination() for all the placeables you wish
        ///  to set the illumination on, and then call RecomputeStaticLighting() once after
        ///  all the placeable illumination has been set.
        ///  * If oPlaceable is not a placeable object, or oPlaceable is a placeable that
        ///    doesn't have a light, nothing will happen.
        /// </summary>
        public static void SetPlaceableIllumination(NWGameObject oPlaceable = null, bool bIlluminate = true)
        {
            Internal.NativeFunctions.StackPushInteger(bIlluminate ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPlaceable != null ? oPlaceable.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(544);
        }

        /// <summary>
        ///  * Returns true if the illumination for oPlaceable is on
        /// </summary>
        public static bool GetPlaceableIllumination(NWGameObject oPlaceable = null)
        {
            Internal.NativeFunctions.StackPushObject(oPlaceable != null ? oPlaceable.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(545);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  - oPlaceable
        ///  - nPlaceableAction: PLACEABLE_ACTION_*
        ///  * Returns true if nPlacebleAction is valid for oPlaceable.
        /// </summary>
        public static bool GetIsPlaceableObjectActionPossible(NWGameObject oPlaceable, PlaceableAction nPlaceableAction)
        {
            Internal.NativeFunctions.StackPushInteger((int)nPlaceableAction);
            Internal.NativeFunctions.StackPushObject(oPlaceable != null ? oPlaceable.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(546);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  The caller performs nPlaceableAction on oPlaceable.
        ///  - oPlaceable
        ///  - nPlaceableAction: PLACEABLE_ACTION_*
        /// </summary>
        public static void DoPlaceableObjectAction(NWGameObject oPlaceable, PlaceableAction nPlaceableAction)
        {
            Internal.NativeFunctions.StackPushInteger((int)nPlaceableAction);
            Internal.NativeFunctions.StackPushObject(oPlaceable != null ? oPlaceable.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(547);
        }

        /// <summary>
        ///  Get the first PC in the player list.
        ///  This resets the position in the player list for GetNextPC().
        /// </summary>
        public static NWGameObject GetFirstPC()
        {
            Internal.NativeFunctions.CallBuiltIn(548);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the next PC in the player list.
        ///  This picks up where the last GetFirstPC() or GetNextPC() left off.
        /// </summary>
        public static NWGameObject GetNextPC()
        {
            Internal.NativeFunctions.CallBuiltIn(549);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Set whether or not the creature oDetector has detected the trapped object oTrap.
        ///  - oTrap: A trapped trigger, placeable or door object.
        ///  - oDetector: This is the creature that the detected status of the trap is being adjusted for.
        ///  - bDetected: A Boolean that sets whether the trapped object has been detected or not.
        /// </summary>
        public static int SetTrapDetectedBy(NWGameObject oTrap, NWGameObject oDetector, bool bDetected = true)
        {
            Internal.NativeFunctions.StackPushInteger(bDetected ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oDetector != null ? oDetector.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTrap != null ? oTrap.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(550);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Note: Only placeables, doors and triggers can be trapped.
        ///  * Returns true if oObject is trapped.
        /// </summary>
        public static bool GetIsTrapped(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(551);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Create a Turn Resistance Decrease effect.
        ///  - nHitDice: a positive number representing the number of hit dice for the
        /// /  decrease
        /// </summary>
        public static Effect EffectTurnResistanceDecrease(int nHitDice)
        {
            Internal.NativeFunctions.StackPushInteger(nHitDice);
            Internal.NativeFunctions.CallBuiltIn(552);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Create a Turn Resistance Increase effect.
        ///  - nHitDice: a positive number representing the number of hit dice for the
        ///    increase
        /// </summary>
        public static Effect EffectTurnResistanceIncrease(int nHitDice)
        {
            Internal.NativeFunctions.StackPushInteger(nHitDice);
            Internal.NativeFunctions.CallBuiltIn(553);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Spawn in the Death GUI.
        ///  The default (as defined by BioWare) can be spawned in by PopUpGUIPanel, but
        ///  if you want to turn off the "Respawn" or "Wait for Help" buttons, this is the
        ///  function to use.
        ///  - oPC
        ///  - bRespawnButtonEnabled: if this is true, the "Respawn" button will be enabled
        ///    on the Death GUI.
        ///  - bWaitForHelpButtonEnabled: if this is true, the "Wait For Help" button will
        ///    be enabled on the Death GUI (Note: This button will not appear in single player games).
        ///  - nHelpStringReference
        ///  - sHelpString
        /// </summary>
        public static void PopUpDeathGUIPanel(NWGameObject oPC, bool bRespawnButtonEnabled = true, bool bWaitForHelpButtonEnabled = true, int nHelpStringReference = 0, string sHelpString = "")
        {
            Internal.NativeFunctions.StackPushString(sHelpString);
            Internal.NativeFunctions.StackPushInteger(nHelpStringReference);
            Internal.NativeFunctions.StackPushInteger(bWaitForHelpButtonEnabled ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bRespawnButtonEnabled ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPC != null ? oPC.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(554);
        }

        /// <summary>
        ///  Disable oTrap.
        ///  - oTrap: a placeable, door or trigger.
        /// </summary>
        public static void SetTrapDisabled(NWGameObject oTrap)
        {
            Internal.NativeFunctions.StackPushObject(oTrap != null ? oTrap.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(555);
        }

        /// <summary>
        ///  Get the last object that was sent as a GetLastAttacker(), GetLastDamager(),
        ///  GetLastSpellCaster() (for a hostile spell), or GetLastDisturbed() (when a
        ///  creature is pickpocketed).
        ///  Note: Return values may only ever be:
        ///  1) A Creature
        ///  2) Plot Characters will never have this value set
        ///  3) Area of Effect Objects will return the AOE creator if they are registered
        ///     as this value, otherwise they will return INVALID_OBJECT_ID
        ///  4) Traps will not return the creature that set the trap.
        ///  5) This value will never be overwritten by another non-creature object.
        ///  6) This value will never be a dead/destroyed creature
        /// </summary>
        public static NWGameObject GetLastHostileActor(NWGameObject oVictim = null)
        {
            Internal.NativeFunctions.StackPushObject(oVictim != null ? oVictim.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(556);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Force all the characters of the players who are currently in the game to
        ///  be exported to their respective directories i.e. LocalVault/ServerVault/ etc.
        /// </summary>
        public static void ExportAllCharacters()
        {
            Internal.NativeFunctions.CallBuiltIn(557);
        }

        /// <summary>
        ///  Get the Day Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetDayTrack(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(558);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Get the Night Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetNightTrack(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(559);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Write sLogEntry as a timestamped entry into the log file
        /// </summary>
        public static void WriteTimestampedLogEntry(string sLogEntry)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sLogEntry);
            Internal.NativeFunctions.CallBuiltIn(560);
        }

        /// <summary>
        ///  Get the module's name in the language of the server that's running it.
        ///  * If there is no entry for the language of the server, it will return an
        ///    empty string
        /// </summary>
        public static string GetModuleName()
        {
            Internal.NativeFunctions.CallBuiltIn(561);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Get the player leader of the faction of which oMemberOfFaction is a member.
        ///  * Returns OBJECT_INVALID if oMemberOfFaction is not a valid creature,
        ///    or oMemberOfFaction is a member of a NPC faction.
        /// </summary>
        public static NWGameObject GetFactionLeader(NWGameObject oMemberOfFaction)
        {
            Internal.NativeFunctions.StackPushObject(oMemberOfFaction != null ? oMemberOfFaction.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(562);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Sends szMessage to all the Dungeon Masters currently on the server.
        /// </summary>
        public static void SendMessageToAllDMs(string szMessage)
        {
            Internal.NativeFunctions.StackPushString(szMessage);
            Internal.NativeFunctions.CallBuiltIn(563);
        }

        /// <summary>
        ///  End the currently running game, play sEndMovie then return all players to the
        ///  game's main menu.
        /// </summary>
        public static void EndGame(string sEndMovie)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sEndMovie);
            Internal.NativeFunctions.CallBuiltIn(564);
        }

        /// <summary>
        ///  Remove oPlayer from the server.
        ///  You can optionally specify a reason to override the text shown to the player.
        /// </summary>
        public static void BootPC(NWGameObject oPlayer, string sReason = "")
        {
            Internal.NativeFunctions.StackPushString(sReason);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(565);
        }

        /// <summary>
        ///  Counterspell oCounterSpellTarget.
        /// </summary>
        public static void ActionCounterSpell(NWGameObject oCounterSpellTarget)
        {
            Internal.NativeFunctions.StackPushObject(oCounterSpellTarget != null ? oCounterSpellTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(566);
        }

        /// <summary>
        ///  Set the ambient day volume for oArea to nVolume.
        ///  - oArea
        ///  - nVolume: 0 - 100
        /// </summary>
        public static void AmbientSoundSetDayVolume(NWGameObject oArea, int nVolume)
        {
            Internal.NativeFunctions.StackPushInteger(nVolume);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(567);
        }

        /// <summary>
        ///  Set the ambient night volume for oArea to nVolume.
        ///  - oArea
        ///  - nVolume: 0 - 100
        /// </summary>
        public static void AmbientSoundSetNightVolume(NWGameObject oArea, int nVolume)
        {
            Internal.NativeFunctions.StackPushInteger(nVolume);
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(568);
        }

        /// <summary>
        ///  Get the Battle Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetBattleTrack(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(569);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Determine whether oObject has an inventory.
        ///  * Returns true for creatures and stores, and checks to see if an item or placeable object is a container.
        ///  * Returns false for all other object types.
        /// </summary>
        public static bool GetHasInventory(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(570);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the duration (in seconds) of the sound attached to nStrRef
        ///  * Returns 0.0f if no duration is stored or if no sound is attached
        /// </summary>
        public static float GetStrRefSoundDuration(int nStrRef)
        {
            Internal.NativeFunctions.StackPushInteger(nStrRef);
            Internal.NativeFunctions.CallBuiltIn(571);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Add oPC to oPartyLeader's party.  This will only work on two PCs.
        ///  - oPC: player to add to a party
        ///  - oPartyLeader: player already in the party
        /// </summary>
        public static void AddToParty(NWGameObject oPC, NWGameObject oPartyLeader)
        {
            Internal.NativeFunctions.StackPushObject(oPartyLeader != null ? oPartyLeader.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oPC != null ? oPC.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(572);
        }

        /// <summary>
        ///  Remove oPC from their current party. This will only work on a PC.
        ///  - oPC: removes this player from whatever party they're currently in.
        /// </summary>
        public static void RemoveFromParty(NWGameObject oPC)
        {
            Internal.NativeFunctions.StackPushObject(oPC != null ? oPC.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(573);
        }

        /// <summary>
        ///  Returns the stealth mode of the specified creature.
        ///  - oCreature
        ///  * Returns a constant STEALTH_MODE_*
        /// </summary>
        public static StealthMode GetStealthMode(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(574);
            return (StealthMode)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the detection mode of the specified creature.
        ///  - oCreature
        ///  * Returns a constant DETECT_MODE_*
        /// </summary>
        public static DetectMode GetDetectMode(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(575);
            return (DetectMode)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the defensive casting mode of the specified creature.
        ///  - oCreature
        ///  * Returns a constant DEFENSIVE_CASTING_MODE_*
        /// </summary>
        public static DefensiveCastingMode GetDefensiveCastingMode(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(576);
            return (DefensiveCastingMode)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  returns the appearance type of the specified creature.
        ///  * returns a constant APPEARANCE_TYPE_* for valid creatures
        ///  * returns APPEARANCE_TYPE_INVALID for non creatures/invalid creatures
        /// </summary>
        public static AppearanceType GetAppearanceType(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(577);
            return (AppearanceType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  in an onItemAcquired script, returns the size of the stack of the item
        ///  that was just acquired.
        ///  * returns the stack size of the item acquired
        /// </summary>
        public static int GetModuleItemAcquiredStackSize()
        {
            Internal.NativeFunctions.CallBuiltIn(579);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Decrement the remaining uses per day for this creature by one.
        ///  - oCreature: creature to modify
        ///  - nFeat: constant FEAT_*
        /// </summary>
        public static void DecrementRemainingFeatUses(NWGameObject oCreature, Feat nFeat)
        {
            Internal.NativeFunctions.StackPushInteger((int)nFeat);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(580);
        }

        /// <summary>
        ///  Decrement the remaining uses per day for this creature by one.
        ///  - oCreature: creature to modify
        ///  - nSpell: constant SPELL_*
        /// </summary>
        public static void DecrementRemainingSpellUses(NWGameObject oCreature, Spell nSpell)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(581);
        }

        /// <summary>
        ///  returns the template used to create this object (if appropriate)
        ///  * returns an empty string when no template found
        /// </summary>
        public static string GetResRef(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(582);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  returns an effect that will petrify the target
        ///  * currently applies EffectParalyze and the stoneskin visual effect.
        /// </summary>
        public static Effect EffectPetrify()
        {
            Internal.NativeFunctions.CallBuiltIn(583);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  returns an effect that is guaranteed to paralyze a creature.
        ///  this effect is identical to EffectParalyze except that it cannot be resisted.
        /// </summary>
        public static Effect EffectCutsceneParalyze()
        {
            Internal.NativeFunctions.CallBuiltIn(585);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  returns true if the item CAN be dropped
        ///  Droppable items will appear on a creature's remains when the creature is killed.
        /// </summary>
        public static bool GetDroppableFlag(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(586);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  returns true if the placeable object is usable
        /// </summary>
        public static bool GetUseableFlag(NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(587);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  returns true if the item is stolen
        /// </summary>
        public static bool GetStolenFlag(NWGameObject oStolen)
        {
            Internal.NativeFunctions.StackPushObject(oStolen != null ? oStolen.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(588);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Duplicates the object specified by oSource.
        ///  ONLY creatures and items can be specified.
        ///  If an owner is specified and the object is an item, it will be put into their inventory
        ///  If the object is a creature, they will be created at the location.
        ///  If a new tag is specified, it will be assigned to the new object.
        /// </summary>
        public static NWGameObject CopyObject(NWGameObject oSource, NWN.Location locLocation, NWGameObject oOwner = null, string sNewTag = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sNewTag);
            Internal.NativeFunctions.StackPushObject(oOwner != null ? oOwner.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushLocation(locLocation.Handle);
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(600);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Returns an effect that is guaranteed to dominate a creature
        ///  Like EffectDominated but cannot be resisted
        /// </summary>
        public static Effect EffectCutsceneDominated()
        {
            Internal.NativeFunctions.CallBuiltIn(604);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Returns stack size of an item
        ///  - oItem: item to query
        /// </summary>
        public static int GetItemStackSize(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(605);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets stack size of an item.
        ///  - oItem: item to change
        ///  - nSize: new size of stack.  Will be restricted to be between 1 and the
        ///    maximum stack size for the item type.  If a value less than 1 is passed it
        ///    will set the stack to 1.  If a value greater than the max is passed
        ///    then it will set the stack to the maximum size
        /// </summary>
        public static void SetItemStackSize(NWGameObject oItem, int nSize)
        {
            Internal.NativeFunctions.StackPushInteger(nSize);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(606);
        }

        /// <summary>
        ///  Returns charges left on an item
        ///  - oItem: item to query
        /// </summary>
        public static int GetItemCharges(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(607);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets charges left on an item.
        ///  - oItem: item to change
        ///  - nCharges: number of charges.  If value below 0 is passed, # charges will
        ///    be set to 0.  If value greater than maximum is passed, # charges will
        ///    be set to maximum.  If the # charges drops to 0 the item
        ///    will be destroyed.
        /// </summary>
        public static void SetItemCharges(NWGameObject oItem, int nCharges)
        {
            Internal.NativeFunctions.StackPushInteger(nCharges);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(608);
        }

        /// <summary>
        ///  ***********************  START OF ITEM PROPERTY FUNCTIONS  **********************
        ///  adds an item property to the specified item
        ///  Only temporary and permanent duration types are allowed.
        /// </summary>
        public static void AddItemProperty(DurationType nDurationType, NWN.ItemProperty ipProperty, NWGameObject oItem, float fDuration = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fDuration);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushItemProperty(ipProperty.Handle);
            Internal.NativeFunctions.StackPushInteger((int)nDurationType);
            Internal.NativeFunctions.CallBuiltIn(609);
        }

        /// <summary>
        ///  removes an item property from the specified item
        /// </summary>
        public static void RemoveItemProperty(NWGameObject oItem, NWN.ItemProperty ipProperty)
        {
            Internal.NativeFunctions.StackPushItemProperty(ipProperty.Handle);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(610);
        }

        /// <summary>
        ///  if the item property is valid this will return true
        /// </summary>
        public static bool GetIsItemPropertyValid(NWN.ItemProperty ipProperty)
        {
            Internal.NativeFunctions.StackPushItemProperty(ipProperty.Handle);
            Internal.NativeFunctions.CallBuiltIn(611);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Gets the first item property on an item
        /// </summary>
        public static NWN.ItemProperty GetFirstItemProperty(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(612);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Will keep retrieving the next and the next item property on an Item,
        ///  will return an invalid item property when the list is empty.
        /// </summary>
        public static NWN.ItemProperty GetNextItemProperty(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(613);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  will return the item property type (ie. holy avenger)
        /// </summary>
        public static ItemPropertyType GetItemPropertyType(NWN.ItemProperty ip)
        {
            Internal.NativeFunctions.StackPushItemProperty(ip.Handle);
            Internal.NativeFunctions.CallBuiltIn(614);
            return (ItemPropertyType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  will return the duration type of the item property
        /// </summary>
        public static DurationType GetItemPropertyDurationType(NWN.ItemProperty ip)
        {
            Internal.NativeFunctions.StackPushItemProperty(ip.Handle);
            Internal.NativeFunctions.CallBuiltIn(615);
            return (DurationType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns Item property ability bonus.  You need to specify an
        ///  ability constant(IP_CONST_ABILITY_*) and the bonus.  The bonus should
        ///  be a positive integer between 1 and 12.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyAbilityBonus(IPConst nAbility, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nAbility);
            Internal.NativeFunctions.CallBuiltIn(616);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property AC bonus.  You need to specify the bonus.
        ///  The bonus should be a positive integer between 1 and 20. The modifier
        ///  type depends on the item it is being applied to.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyACBonus(int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.CallBuiltIn(617);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. alignment group.  An example of
        ///  an alignment group is Chaotic, or Good.  You need to specify the
        ///  alignment group constant(IP_CONST_ALIGNMENTGROUP_*) and the AC bonus.
        ///  The AC bonus should be an integer between 1 and 20.  The modifier
        ///  type depends on the item it is being applied to.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyACBonusVsAlign(IPConst nAlignGroup, int nACBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nACBonus);
            Internal.NativeFunctions.StackPushInteger((int)nAlignGroup);
            Internal.NativeFunctions.CallBuiltIn(618);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. Damage type (ie. piercing).  You
        ///  need to specify the damage type constant(IP_CONST_DAMAGETYPE_*) and the
        ///  AC bonus.  The AC bonus should be an integer between 1 and 20.  The
        ///  modifier type depends on the item it is being applied to.
        ///  NOTE: Only the first 3 damage types may be used here, the 3 basic
        ///        physical types.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyACBonusVsDmgType(IPConst nDamageType, int nACBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nACBonus);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(619);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. Racial group.  You need to specify
        ///  the racial group constant(IP_CONST_RACIALTYPE_*) and the AC bonus.  The AC
        ///  bonus should be an integer between 1 and 20.  The modifier type depends
        ///  on the item it is being applied to.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyACBonusVsRace(IPConst nRace, int nACBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nACBonus);
            Internal.NativeFunctions.StackPushInteger((int)nRace);
            Internal.NativeFunctions.CallBuiltIn(620);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. Specific alignment.  You need to
        ///  specify the specific alignment constant(IP_CONST_ALIGNMENT_*) and the AC
        ///  bonus.  The AC bonus should be an integer between 1 and 20.  The
        ///  modifier type depends on the item it is being applied to.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyACBonusVsSAlign(IPConst nAlign, int nACBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nACBonus);
            Internal.NativeFunctions.StackPushInteger((int)nAlign);
            Internal.NativeFunctions.CallBuiltIn(621);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus.  You need to specify the
        ///  enhancement bonus.  The Enhancement bonus should be an integer between
        ///  1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyEnhancementBonus(int nEnhancementBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nEnhancementBonus);
            Internal.NativeFunctions.CallBuiltIn(622);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus vs. an Alignment group.  You
        ///  need to specify the alignment group constant(IP_CONST_ALIGNMENTGROUP_*)
        ///  and the enhancement bonus.  The Enhancement bonus should be an integer
        ///  between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyEnhancementBonusVsAlign(IPConst nAlignGroup, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nAlignGroup);
            Internal.NativeFunctions.CallBuiltIn(623);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus vs. Racial group.  You need
        ///  to specify the racial group constant(IP_CONST_RACIALTYPE_*) and the
        ///  enhancement bonus.  The enhancement bonus should be an integer between
        ///  1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyEnhancementBonusVsRace(IPConst nRace, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nRace);
            Internal.NativeFunctions.CallBuiltIn(624);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus vs. a specific alignment.  You
        ///  need to specify the alignment constant(IP_CONST_ALIGNMENT_*) and the
        ///  enhancement bonus.  The enhancement bonus should be an integer between
        ///  1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyEnhancementBonusVsSAlign(IPConst nAlign, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nAlign);
            Internal.NativeFunctions.CallBuiltIn(625);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Enhancment penalty.  You need to specify the
        ///  enhancement penalty.  The enhancement penalty should be a POSITIVE
        ///  integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyEnhancementPenalty(int nPenalty)
        {
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.CallBuiltIn(626);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property weight reduction.  You need to specify the weight
        ///  reduction constant(IP_CONST_REDUCEDWEIGHT_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyWeightReduction(int nReduction)
        {
            Internal.NativeFunctions.StackPushInteger(nReduction);
            Internal.NativeFunctions.CallBuiltIn(627);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Bonus Feat.  You need to specify the the feat
        ///  constant(IP_CONST_FEAT_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyBonusFeat(IPConst nFeat)
        {
            Internal.NativeFunctions.StackPushInteger((int)nFeat);
            Internal.NativeFunctions.CallBuiltIn(628);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Bonus level spell (Bonus spell of level).  You must
        ///  specify the class constant(IP_CONST_CLASS_*) of the bonus spell(MUST BE a
        ///  spell casting class) and the level of the bonus spell.  The level of the
        ///  bonus spell should be an integer between 0 and 9.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyBonusLevelSpell(IPConst nClass, int nSpellLevel)
        {
            Internal.NativeFunctions.StackPushInteger(nSpellLevel);
            Internal.NativeFunctions.StackPushInteger((int)nClass);
            Internal.NativeFunctions.CallBuiltIn(629);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Cast spell.  You must specify the spell constant
        ///  (IP_CONST_CASTSPELL_*) and the number of uses constant(IP_CONST_CASTSPELL_NUMUSES_*).
        ///  NOTE: The number after the name of the spell in the constant is the level
        ///        at which the spell will be cast.  Sometimes there are multiple copies
        ///        of the same spell but they each are cast at a different level.  The higher
        ///        the level, the more cost will be added to the item.
        ///  NOTE: The list of spells that can be applied to an item will depend on the
        ///        item type.  For instance there are spells that can be applied to a wand
        ///        that cannot be applied to a potion.  Below is a list of the types and the
        ///        spells that are allowed to be placed on them.  If you try to put a cast
        ///        spell effect on an item that is not allowed to have that effect it will
        ///        not work.
        ///  NOTE: Even if spells have multiple versions of different levels they are only
        ///        listed below once.
        /// 
        ///  WANDS:
        ///           Acid_Splash
        ///           Activate_Item
        ///           Aid
        ///           Amplify
        ///           Animate_Dead
        ///           AuraOfGlory
        ///           BalagarnsIronHorn
        ///           Bane
        ///           Banishment
        ///           Barkskin
        ///           Bestow_Curse
        ///           Bigbys_Clenched_Fist
        ///           Bigbys_Crushing_Hand
        ///           Bigbys_Forceful_Hand
        ///           Bigbys_Grasping_Hand
        ///           Bigbys_Interposing_Hand
        ///           Bless
        ///           Bless_Weapon
        ///           Blindness/Deafness
        ///           Blood_Frenzy
        ///           Bombardment
        ///           Bulls_Strength
        ///           Burning_Hands
        ///           Call_Lightning
        ///           Camoflage
        ///           Cats_Grace
        ///           Charm_Monster
        ///           Charm_Person
        ///           Charm_Person_or_Animal
        ///           Clairaudience/Clairvoyance
        ///           Clarity
        ///           Color_Spray
        ///           Confusion
        ///           Continual_Flame
        ///           Cure_Critical_Wounds
        ///           Cure_Light_Wounds
        ///           Cure_Minor_Wounds
        ///           Cure_Moderate_Wounds
        ///           Cure_Serious_Wounds
        ///           Darkness
        ///           Darkvision
        ///           Daze
        ///           Death_Ward
        ///           Dirge
        ///           Dismissal
        ///           Dispel_Magic
        ///           Displacement
        ///           Divine_Favor
        ///           Divine_Might
        ///           Divine_Power
        ///           Divine_Shield
        ///           Dominate_Animal
        ///           Dominate_Person
        ///           Doom
        ///           Dragon_Breath_Acid
        ///           Dragon_Breath_Cold
        ///           Dragon_Breath_Fear
        ///           Dragon_Breath_Fire
        ///           Dragon_Breath_Gas
        ///           Dragon_Breath_Lightning
        ///           Dragon_Breath_Paralyze
        ///           Dragon_Breath_Sleep
        ///           Dragon_Breath_Slow
        ///           Dragon_Breath_Weaken
        ///           Drown
        ///           Eagle_Spledor
        ///           Earthquake
        ///           Electric_Jolt
        ///           Elemental_Shield
        ///           Endurance
        ///           Endure_Elements
        ///           Enervation
        ///           Entangle
        ///           Entropic_Shield
        ///           Etherealness
        ///           Expeditious_Retreat
        ///           Fear
        ///           Find_Traps
        ///           Fireball
        ///           Firebrand
        ///           Flame_Arrow
        ///           Flame_Lash
        ///           Flame_Strike
        ///           Flare
        ///           Foxs_Cunning
        ///           Freedom_of_Movement
        ///           Ghostly_Visage
        ///           Ghoul_Touch
        ///           Grease
        ///           Greater_Magic_Fang
        ///           Greater_Magic_Weapon
        ///           Grenade_Acid
        ///           Grenade_Caltrops
        ///           Grenade_Chicken
        ///           Grenade_Choking
        ///           Grenade_Fire
        ///           Grenade_Holy
        ///           Grenade_Tangle
        ///           Grenade_Thunderstone
        ///           Gust_of_wind
        ///           Hammer_of_the_Gods
        ///           Haste
        ///           Hold_Animal
        ///           Hold_Monster
        ///           Hold_Person
        ///           Ice_Storm
        ///           Identify
        ///           Improved_Invisibility
        ///           Inferno
        ///           Inflict_Critical_Wounds
        ///           Inflict_Light_Wounds
        ///           Inflict_Minor_Wounds
        ///           Inflict_Moderate_Wounds
        ///           Inflict_Serious_Wounds
        ///           Invisibility
        ///           Invisibility_Purge
        ///           Invisibility_Sphere
        ///           Isaacs_Greater_Missile_Storm
        ///           Isaacs_Lesser_Missile_Storm
        ///           Knock
        ///           Lesser_Dispel
        ///           Lesser_Restoration
        ///           Lesser_Spell_Breach
        ///           Light
        ///           Lightning_Bolt
        ///           Mage_Armor
        ///           Magic_Circle_against_Alignment
        ///           Magic_Fang
        ///           Magic_Missile
        ///           Manipulate_Portal_Stone
        ///           Mass_Camoflage
        ///           Melfs_Acid_Arrow
        ///           Meteor_Swarm
        ///           Mind_Blank
        ///           Mind_Fog
        ///           Negative_Energy_Burst
        ///           Negative_Energy_Protection
        ///           Negative_Energy_Ray
        ///           Neutralize_Poison
        ///           One_With_The_Land
        ///           Owls_Insight
        ///           Owls_Wisdom
        ///           Phantasmal_Killer
        ///           Planar_Ally
        ///           Poison
        ///           Polymorph_Self
        ///           Prayer
        ///           Protection_from_Alignment
        ///           Protection_From_Elements
        ///           Quillfire
        ///           Ray_of_Enfeeblement
        ///           Ray_of_Frost
        ///           Remove_Blindness/Deafness
        ///           Remove_Curse
        ///           Remove_Disease
        ///           Remove_Fear
        ///           Remove_Paralysis
        ///           Resist_Elements
        ///           Resistance
        ///           Restoration
        ///           Sanctuary
        ///           Scare
        ///           Searing_Light
        ///           See_Invisibility
        ///           Shadow_Conjuration
        ///           Shield
        ///           Shield_of_Faith
        ///           Silence
        ///           Sleep
        ///           Slow
        ///           Sound_Burst
        ///           Spike_Growth
        ///           Stinking_Cloud
        ///           Stoneskin
        ///           Summon_Creature_I
        ///           Summon_Creature_I
        ///           Summon_Creature_II
        ///           Summon_Creature_III
        ///           Summon_Creature_IV
        ///           Sunburst
        ///           Tashas_Hideous_Laughter
        ///           True_Strike
        ///           Undeaths_Eternal_Foe
        ///           Unique_Power
        ///           Unique_Power_Self_Only
        ///           Vampiric_Touch
        ///           Virtue
        ///           Wall_of_Fire
        ///           Web
        ///           Wounding_Whispers
        /// 
        ///  POTIONS:
        ///           Activate_Item
        ///           Aid
        ///           Amplify
        ///           AuraOfGlory
        ///           Bane
        ///           Barkskin
        ///           Barkskin
        ///           Barkskin
        ///           Bless
        ///           Bless_Weapon
        ///           Bless_Weapon
        ///           Blood_Frenzy
        ///           Bulls_Strength
        ///           Bulls_Strength
        ///           Bulls_Strength
        ///           Camoflage
        ///           Cats_Grace
        ///           Cats_Grace
        ///           Cats_Grace
        ///           Clairaudience/Clairvoyance
        ///           Clairaudience/Clairvoyance
        ///           Clairaudience/Clairvoyance
        ///           Clarity
        ///           Continual_Flame
        ///           Cure_Critical_Wounds
        ///           Cure_Critical_Wounds
        ///           Cure_Critical_Wounds
        ///           Cure_Light_Wounds
        ///           Cure_Light_Wounds
        ///           Cure_Minor_Wounds
        ///           Cure_Moderate_Wounds
        ///           Cure_Moderate_Wounds
        ///           Cure_Moderate_Wounds
        ///           Cure_Serious_Wounds
        ///           Cure_Serious_Wounds
        ///           Cure_Serious_Wounds
        ///           Darkness
        ///           Darkvision
        ///           Darkvision
        ///           Death_Ward
        ///           Dispel_Magic
        ///           Dispel_Magic
        ///           Displacement
        ///           Divine_Favor
        ///           Divine_Might
        ///           Divine_Power
        ///           Divine_Shield
        ///           Dragon_Breath_Acid
        ///           Dragon_Breath_Cold
        ///           Dragon_Breath_Fear
        ///           Dragon_Breath_Fire
        ///           Dragon_Breath_Gas
        ///           Dragon_Breath_Lightning
        ///           Dragon_Breath_Paralyze
        ///           Dragon_Breath_Sleep
        ///           Dragon_Breath_Slow
        ///           Dragon_Breath_Weaken
        ///           Eagle_Spledor
        ///           Eagle_Spledor
        ///           Eagle_Spledor
        ///           Elemental_Shield
        ///           Elemental_Shield
        ///           Endurance
        ///           Endurance
        ///           Endurance
        ///           Endure_Elements
        ///           Entropic_Shield
        ///           Ethereal_Visage
        ///           Ethereal_Visage
        ///           Etherealness
        ///           Expeditious_Retreat
        ///           Find_Traps
        ///           Foxs_Cunning
        ///           Foxs_Cunning
        ///           Foxs_Cunning
        ///           Freedom_of_Movement
        ///           Ghostly_Visage
        ///           Ghostly_Visage
        ///           Ghostly_Visage
        ///           Globe_of_Invulnerability
        ///           Greater_Bulls_Strength
        ///           Greater_Cats_Grace
        ///           Greater_Dispelling
        ///           Greater_Dispelling
        ///           Greater_Eagles_Splendor
        ///           Greater_Endurance
        ///           Greater_Foxs_Cunning
        ///           Greater_Magic_Weapon
        ///           Greater_Owls_Wisdom
        ///           Greater_Restoration
        ///           Greater_Spell_Mantle
        ///           Greater_Stoneskin
        ///           Grenade_Acid
        ///           Grenade_Caltrops
        ///           Grenade_Chicken
        ///           Grenade_Choking
        ///           Grenade_Fire
        ///           Grenade_Holy
        ///           Grenade_Tangle
        ///           Grenade_Thunderstone
        ///           Haste
        ///           Haste
        ///           Heal
        ///           Hold_Animal
        ///           Hold_Monster
        ///           Hold_Person
        ///           Identify
        ///           Invisibility
        ///           Lesser_Dispel
        ///           Lesser_Dispel
        ///           Lesser_Mind_Blank
        ///           Lesser_Restoration
        ///           Lesser_Spell_Mantle
        ///           Light
        ///           Light
        ///           Mage_Armor
        ///           Manipulate_Portal_Stone
        ///           Mass_Camoflage
        ///           Mind_Blank
        ///           Minor_Globe_of_Invulnerability
        ///           Minor_Globe_of_Invulnerability
        ///           Mordenkainens_Disjunction
        ///           Negative_Energy_Protection
        ///           Negative_Energy_Protection
        ///           Negative_Energy_Protection
        ///           Neutralize_Poison
        ///           One_With_The_Land
        ///           Owls_Insight
        ///           Owls_Wisdom
        ///           Owls_Wisdom
        ///           Owls_Wisdom
        ///           Polymorph_Self
        ///           Prayer
        ///           Premonition
        ///           Protection_From_Elements
        ///           Protection_From_Elements
        ///           Protection_from_Spells
        ///           Protection_from_Spells
        ///           Raise_Dead
        ///           Remove_Blindness/Deafness
        ///           Remove_Curse
        ///           Remove_Disease
        ///           Remove_Fear
        ///           Remove_Paralysis
        ///           Resist_Elements
        ///           Resist_Elements
        ///           Resistance
        ///           Resistance
        ///           Restoration
        ///           Resurrection
        ///           Rogues_Cunning
        ///           See_Invisibility
        ///           Shadow_Shield
        ///           Shapechange
        ///           Shield
        ///           Shield_of_Faith
        ///           Special_Alcohol_Beer
        ///           Special_Alcohol_Spirits
        ///           Special_Alcohol_Wine
        ///           Special_Herb_Belladonna
        ///           Special_Herb_Garlic
        ///           Spell_Mantle
        ///           Spell_Resistance
        ///           Spell_Resistance
        ///           Stoneskin
        ///           Tensers_Transformation
        ///           True_Seeing
        ///           True_Strike
        ///           Unique_Power
        ///           Unique_Power_Self_Only
        ///           Virtue
        /// 
        ///  GENERAL USE (ie. everything else):
        ///           Just about every spell is useable by all the general use items so instead we
        ///           will only list the ones that are not allowed:
        ///           Special_Alcohol_Beer
        ///           Special_Alcohol_Spirits
        ///           Special_Alcohol_Wine
        /// 
        /// </summary>
        public static NWN.ItemProperty ItemPropertyCastSpell(IPConst nSpell, int nNumUses)
        {
            Internal.NativeFunctions.StackPushInteger(nNumUses);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(630);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage bonus.  You must specify the damage type constant
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageBonus(IPConst nDamageType, int nDamage)
        {
            Internal.NativeFunctions.StackPushInteger(nDamage);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(631);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage bonus vs. Alignment groups.  You must specify the
        ///  alignment group constant(IP_CONST_ALIGNMENTGROUP_*) and the damage type constant
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageBonusVsAlign(IPConst nAlignGroup, IPConst nDamageType, int nDamage)
        {
            Internal.NativeFunctions.StackPushInteger(nDamage);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger((int)nAlignGroup);
            Internal.NativeFunctions.CallBuiltIn(632);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage bonus vs. specific race.  You must specify the
        ///  racial group constant(IP_CONST_RACIALTYPE_*) and the damage type constant
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageBonusVsRace(IPConst nRace, IPConst nDamageType, int nDamage)
        {
            Internal.NativeFunctions.StackPushInteger(nDamage);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger((int)nRace);
            Internal.NativeFunctions.CallBuiltIn(633);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage bonus vs. specific alignment.  You must specify the
        ///  specific alignment constant(IP_CONST_ALIGNMENT_*) and the damage type constant
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageBonusVsSAlign(IPConst nAlign, IPConst nDamageType, int nDamage)
        {
            Internal.NativeFunctions.StackPushInteger(nDamage);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.StackPushInteger((int)nAlign);
            Internal.NativeFunctions.CallBuiltIn(634);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage immunity.  You must specify the damage type constant
        ///  (IP_CONST_DAMAGETYPE_*) that you want to be immune to and the immune bonus percentage
        ///  constant(IP_CONST_DAMAGEIMMUNITY_*).
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageImmunity(IPConst nDamageType, IPConst nImmuneBonus)
        {
            Internal.NativeFunctions.StackPushInteger((int)nImmuneBonus);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(635);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage penalty.  You must specify the damage penalty.
        ///  The damage penalty should be a POSITIVE integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamagePenalty(int nPenalty)
        {
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.CallBuiltIn(636);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage reduction.  You must specify the enhancment level
        ///  (IP_CONST_DAMAGEREDUCTION_*) that is required to get past the damage reduction
        ///  and the amount of HP of damage constant(IP_CONST_DAMAGESOAK_*) will be soaked
        ///  up if your weapon is not of high enough enhancement.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageReduction(IPConst nEnhancement, int nHPSoak)
        {
            Internal.NativeFunctions.StackPushInteger(nHPSoak);
            Internal.NativeFunctions.StackPushInteger((int)nEnhancement);
            Internal.NativeFunctions.CallBuiltIn(637);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage resistance.  You must specify the damage type
        ///  constant(IP_CONST_DAMAGETYPE_*) and the amount of HP of damage constant
        ///  (IP_CONST_DAMAGERESIST_*) that will be resisted against each round.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageResistance(IPConst nDamageType, int nHPResist)
        {
            Internal.NativeFunctions.StackPushInteger(nHPResist);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(638);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property damage vulnerability.  You must specify the damage type
        ///  constant(IP_CONST_DAMAGETYPE_*) that you want the user to be extra vulnerable to
        ///  and the percentage vulnerability constant(IP_CONST_DAMAGEVULNERABILITY_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDamageVulnerability(IPConst nDamageType, IPConst nVulnerability)
        {
            Internal.NativeFunctions.StackPushInteger((int)nVulnerability);
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(639);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Return Item property Darkvision.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDarkvision()
        {
            Internal.NativeFunctions.CallBuiltIn(640);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Return Item property decrease ability score.  You must specify the ability
        ///  constant(IP_CONST_ABILITY_*) and the modifier constant.  The modifier must be
        ///  a POSITIVE integer between 1 and 10 (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDecreaseAbility(IPConst nAbility, int nModifier)
        {
            Internal.NativeFunctions.StackPushInteger(nModifier);
            Internal.NativeFunctions.StackPushInteger((int)nAbility);
            Internal.NativeFunctions.CallBuiltIn(641);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property decrease Armor Class.  You must specify the armor
        ///  modifier type constant(IP_CONST_ACMODIFIERTYPE_*) and the armor class penalty.
        ///  The penalty must be a POSITIVE integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDecreaseAC(IPConst nModifierType, int nPenalty)
        {
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.StackPushInteger((int)nModifierType);
            Internal.NativeFunctions.CallBuiltIn(642);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property decrease skill.  You must specify the constant for the
        ///  skill to be decreased(SKILL_*) and the amount of the penalty.  The penalty
        ///  must be a POSITIVE integer between 1 and 10 (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyDecreaseSkill(Skill nSkill, int nPenalty)
        {
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(643);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property container reduced weight.  This is used for special
        ///  containers that reduce the weight of the objects inside them.  You must
        ///  specify the container weight reduction type constant(IP_CONST_CONTAINERWEIGHTRED_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyContainerReducedWeight(IPConst nContainerType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nContainerType);
            Internal.NativeFunctions.CallBuiltIn(644);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property extra melee damage type.  You must specify the extra
        ///  melee base damage type that you want applied.  It is a constant(IP_CONST_DAMAGETYPE_*).
        ///  NOTE: only the first 3 base types (piercing, slashing, & bludgeoning are applicable
        ///        here.
        ///  NOTE: It is also only applicable to melee weapons.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyExtraMeleeDamageType(IPConst nDamageType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(645);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property extra ranged damage type.  You must specify the extra
        ///  melee base damage type that you want applied.  It is a constant(IP_CONST_DAMAGETYPE_*).
        ///  NOTE: only the first 3 base types (piercing, slashing, & bludgeoning are applicable
        ///        here.
        ///  NOTE: It is also only applicable to ranged weapons.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyExtraRangeDamageType(IPConst nDamageType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nDamageType);
            Internal.NativeFunctions.CallBuiltIn(646);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property haste.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyHaste()
        {
            Internal.NativeFunctions.CallBuiltIn(647);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Holy Avenger.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyHolyAvenger()
        {
            Internal.NativeFunctions.CallBuiltIn(648);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property immunity to miscellaneous effects.  You must specify the
        ///  effect to which the user is immune, it is a constant(IP_CONST_IMMUNITYMISC_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyImmunityMisc(IPConst nImmunityType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nImmunityType);
            Internal.NativeFunctions.CallBuiltIn(649);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property improved evasion.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyImprovedEvasion()
        {
            Internal.NativeFunctions.CallBuiltIn(650);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property bonus spell resistance.  You must specify the bonus spell
        ///  resistance constant(IP_CONST_SPELLRESISTANCEBONUS_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyBonusSpellResistance(IPConst nBonus)
        {
            Internal.NativeFunctions.StackPushInteger((int)nBonus);
            Internal.NativeFunctions.CallBuiltIn(651);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property saving throw bonus vs. a specific effect or damage type.
        ///  You must specify the save type constant(IP_CONST_SAVEVS_*) that the bonus is
        ///  applied to and the bonus that is be applied.  The bonus must be an integer
        ///  between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyBonusSavingThrowVsX(IPConst nBonusType, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nBonusType);
            Internal.NativeFunctions.CallBuiltIn(652);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property saving throw bonus to the base type (ie. will, reflex,
        ///  fortitude).  You must specify the base type constant(IP_CONST_SAVEBASETYPE_*)
        ///  to which the user gets the bonus and the bonus that he/she will get.  The
        ///  bonus must be an integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyBonusSavingThrow(IPConst nBaseSaveType, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nBaseSaveType);
            Internal.NativeFunctions.CallBuiltIn(653);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property keen.  This means a critical threat range of 19-20 on a
        ///  weapon will be increased to 17-20 etc.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyKeen()
        {
            Internal.NativeFunctions.CallBuiltIn(654);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property light.  You must specify the intesity constant of the
        ///  light(IP_CONST_LIGHTBRIGHTNESS_*) and the color constant of the light
        ///  (IP_CONST_LIGHTCOLOR_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyLight(IPConst nBrightness, IPConst nColor)
        {
            Internal.NativeFunctions.StackPushInteger((int)nColor);
            Internal.NativeFunctions.StackPushInteger((int)nBrightness);
            Internal.NativeFunctions.CallBuiltIn(655);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Max range strength modification (ie. mighty).  You must
        ///  specify the maximum modifier for strength that is allowed on a ranged weapon.
        ///  The modifier must be a positive integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyMaxRangeStrengthMod(int nModifier)
        {
            Internal.NativeFunctions.StackPushInteger(nModifier);
            Internal.NativeFunctions.CallBuiltIn(656);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property no damage.  This means the weapon will do no damage in
        ///  combat.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyNoDamage()
        {
            Internal.NativeFunctions.CallBuiltIn(657);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property on hit -> do effect property.  You must specify the on
        ///  hit property constant(IP_CONST_ONHIT_*) and the save DC constant(IP_CONST_ONHIT_SAVEDC_*).
        ///  Some of the item properties require a special parameter as well.  If the
        ///  property does not require one you may leave out the last one.  The list of
        ///  the ones with 3 parameters and what they are are as follows:
        ///       ABILITYDRAIN      :nSpecial is the ability it is to drain.
        ///                          constant(IP_CONST_ABILITY_*)
        ///       BLINDNESS         :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       CONFUSION         :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       DAZE              :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       DEAFNESS          :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       DISEASE           :nSpecial is the type of desease that will effect the victim.
        ///                          constant(DISEASE_*)
        ///       DOOM              :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       FEAR              :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       HOLD              :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       ITEMPOISON        :nSpecial is the type of poison that will effect the victim.
        ///                          constant(IP_CONST_POISON_*)
        ///       SILENCE           :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       SLAYRACE          :nSpecial is the race that will be slain.
        ///                          constant(IP_CONST_RACIALTYPE_*)
        ///       SLAYALIGNMENTGROUP:nSpecial is the alignment group that will be slain(ie. chaotic).
        ///                          constant(IP_CONST_ALIGNMENTGROUP_*)
        ///       SLAYALIGNMENT     :nSpecial is the specific alignment that will be slain.
        ///                          constant(IP_CONST_ALIGNMENT_*)
        ///       SLEEP             :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       SLOW              :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        ///       STUN              :nSpecial is the duration/percentage of effecting victim.
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        /// </summary>
        public static NWN.ItemProperty ItemPropertyOnHitProps(IPConst nProperty, int nSaveDC, int nSpecial = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nSpecial);
            Internal.NativeFunctions.StackPushInteger(nSaveDC);
            Internal.NativeFunctions.StackPushInteger((int)nProperty);
            Internal.NativeFunctions.CallBuiltIn(658);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property reduced saving throw vs. an effect or damage type.  You must
        ///  specify the constant to which the penalty applies(IP_CONST_SAVEVS_*) and the
        ///  penalty to be applied.  The penalty must be a POSITIVE integer between 1 and 20
        ///  (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyReducedSavingThrowVsX(IPConst nBaseSaveType, int nPenalty)
        {
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.StackPushInteger((int)nBaseSaveType);
            Internal.NativeFunctions.CallBuiltIn(659);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property reduced saving to base type.  You must specify the base
        ///  type to which the penalty applies (ie. will, reflex, or fortitude) and the penalty
        ///  to be applied.  The constant for the base type starts with (IP_CONST_SAVEBASETYPE_*).
        ///  The penalty must be a POSITIVE integer between 1 and 20 (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyReducedSavingThrow(IPConst nBonusType, int nPenalty)
        {
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.StackPushInteger((int)nBonusType);
            Internal.NativeFunctions.CallBuiltIn(660);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property regeneration.  You must specify the regeneration amount.
        ///  The amount must be an integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyRegeneration(int nRegenAmount)
        {
            Internal.NativeFunctions.StackPushInteger(nRegenAmount);
            Internal.NativeFunctions.CallBuiltIn(661);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property skill bonus.  You must specify the skill to which the user
        ///  will get a bonus(SKILL_*) and the amount of the bonus.  The bonus amount must
        ///  be an integer between 1 and 50.
        /// </summary>
        public static NWN.ItemProperty ItemPropertySkillBonus(Skill nSkill, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.CallBuiltIn(662);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property spell immunity vs. specific spell.  You must specify the
        ///  spell to which the user will be immune(IP_CONST_IMMUNITYSPELL_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertySpellImmunitySpecific(IPConst nSpell)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(663);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property spell immunity vs. spell school.  You must specify the
        ///  school to which the user will be immune(IP_CONST_SPELLSCHOOL_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertySpellImmunitySchool(IPConst nSchool)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSchool);
            Internal.NativeFunctions.CallBuiltIn(664);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Thieves tools.  You must specify the modifier you wish
        ///  the tools to have.  The modifier must be an integer between 1 and 12.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyThievesTools(int nModifier)
        {
            Internal.NativeFunctions.StackPushInteger(nModifier);
            Internal.NativeFunctions.CallBuiltIn(665);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Attack bonus.  You must specify an attack bonus.  The bonus
        ///  must be an integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyAttackBonus(int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.CallBuiltIn(666);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Attack bonus vs. alignment group.  You must specify the
        ///  alignment group constant(IP_CONST_ALIGNMENTGROUP_*) and the attack bonus.  The
        ///  bonus must be an integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyAttackBonusVsAlign(IPConst nAlignGroup, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nAlignGroup);
            Internal.NativeFunctions.CallBuiltIn(667);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property attack bonus vs. racial group.  You must specify the
        ///  racial group constant(IP_CONST_RACIALTYPE_*) and the attack bonus.  The bonus
        ///  must be an integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyAttackBonusVsRace(IPConst nRace, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nRace);
            Internal.NativeFunctions.CallBuiltIn(668);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property attack bonus vs. a specific alignment.  You must specify
        ///  the alignment you want the bonus to work against(IP_CONST_ALIGNMENT_*) and the
        ///  attack bonus.  The bonus must be an integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyAttackBonusVsSAlign(IPConst nAlignment, int nBonus)
        {
            Internal.NativeFunctions.StackPushInteger(nBonus);
            Internal.NativeFunctions.StackPushInteger((int)nAlignment);
            Internal.NativeFunctions.CallBuiltIn(669);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property attack penalty.  You must specify the attack penalty.
        ///  The penalty must be a POSITIVE integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyAttackPenalty(int nPenalty)
        {
            Internal.NativeFunctions.StackPushInteger(nPenalty);
            Internal.NativeFunctions.CallBuiltIn(670);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property unlimited ammo.  If you leave the parameter field blank
        ///  it will be just a normal bolt, arrow, or bullet.  However you may specify that
        ///  you want the ammunition to do special damage (ie. +1d6 Fire, or +1 enhancement
        ///  bonus).  For this parmeter you use the constants beginning with:
        ///       (IP_CONST_UNLIMITEDAMMO_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyUnlimitedAmmo(IPConst nAmmoDamage = IPConst.UnlimitedAmmo_Basic)
        {
            Internal.NativeFunctions.StackPushInteger((int)nAmmoDamage);
            Internal.NativeFunctions.CallBuiltIn(671);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property limit use by alignment group.  You must specify the
        ///  alignment group(s) that you want to be able to use this item(IP_CONST_ALIGNMENTGROUP_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyLimitUseByAlign(IPConst nAlignGroup)
        {
            Internal.NativeFunctions.StackPushInteger((int)nAlignGroup);
            Internal.NativeFunctions.CallBuiltIn(672);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property limit use by class.  You must specify the class(es) who
        ///  are able to use this item(IP_CONST_CLASS_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyLimitUseByClass(IPConst nClass)
        {
            Internal.NativeFunctions.StackPushInteger((int)nClass);
            Internal.NativeFunctions.CallBuiltIn(673);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property limit use by race.  You must specify the race(s) who are
        ///  allowed to use this item(IP_CONST_RACIALTYPE_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyLimitUseByRace(IPConst nRace)
        {
            Internal.NativeFunctions.StackPushInteger((int)nRace);
            Internal.NativeFunctions.CallBuiltIn(674);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property limit use by specific alignment.  You must specify the
        ///  alignment(s) of those allowed to use the item(IP_CONST_ALIGNMENT_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyLimitUseBySAlign(IPConst nAlignment)
        {
            Internal.NativeFunctions.StackPushInteger((int)nAlignment);
            Internal.NativeFunctions.CallBuiltIn(675);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property vampiric regeneration.  You must specify the amount of
        ///  regeneration.  The regen amount must be an integer between 1 and 20.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyVampiricRegeneration(int nRegenAmount)
        {
            Internal.NativeFunctions.StackPushInteger(nRegenAmount);
            Internal.NativeFunctions.CallBuiltIn(677);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Trap.  You must specify the trap level constant
        ///  (IP_CONST_TRAPSTRENGTH_*) and the trap type constant(IP_CONST_TRAPTYPE_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyTrap(IPConst nTrapLevel, IPConst nTrapType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTrapType);
            Internal.NativeFunctions.StackPushInteger((int)nTrapLevel);
            Internal.NativeFunctions.CallBuiltIn(678);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property true seeing.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyTrueSeeing()
        {
            Internal.NativeFunctions.CallBuiltIn(679);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Monster on hit apply effect property.  You must specify
        ///  the property that you want applied on hit.  There are some properties that
        ///  require an additional special parameter to be specified.  The others that
        ///  don't require any additional parameter you may just put in the one.  The
        ///  special cases are as follows:
        ///       ABILITYDRAIN:nSpecial is the ability to drain.
        ///                    constant(IP_CONST_ABILITY_*)
        ///       DISEASE     :nSpecial is the disease that you want applied.
        ///                    constant(DISEASE_*)
        ///       LEVELDRAIN  :nSpecial is the number of levels that you want drained.
        ///                    integer between 1 and 5.
        ///       POISON      :nSpecial is the type of poison that will effect the victim.
        ///                    constant(IP_CONST_POISON_*)
        ///       WOUNDING    :nSpecial is the amount of wounding.
        ///                    integer between 1 and 5.
        ///  NOTE: Any that do not appear in the above list do not require the second
        ///        parameter.
        ///  NOTE: These can only be applied to monster NATURAL weapons (ie. bite, claw,
        ///        gore, and slam).  IT WILL NOT WORK ON NORMAL WEAPONS.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyOnMonsterHitProperties(IPConst nProperty, int nSpecial = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nSpecial);
            Internal.NativeFunctions.StackPushInteger((int)nProperty);
            Internal.NativeFunctions.CallBuiltIn(680);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property turn resistance.  You must specify the resistance bonus.
        ///  The bonus must be an integer between 1 and 50.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyTurnResistance(int nModifier)
        {
            Internal.NativeFunctions.StackPushInteger(nModifier);
            Internal.NativeFunctions.CallBuiltIn(681);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Massive Critical.  You must specify the extra damage
        ///  constant(IP_CONST_DAMAGEBONUS_*) of the criticals.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyMassiveCritical(int nDamage)
        {
            Internal.NativeFunctions.StackPushInteger(nDamage);
            Internal.NativeFunctions.CallBuiltIn(682);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property free action.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyFreeAction()
        {
            Internal.NativeFunctions.CallBuiltIn(683);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property monster damage.  You must specify the amount of damage
        ///  the monster's attack will do(IP_CONST_MONSTERDAMAGE_*).
        ///  NOTE: These can only be applied to monster NATURAL weapons (ie. bite, claw,
        ///        gore, and slam).  IT WILL NOT WORK ON NORMAL WEAPONS.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyMonsterDamage(int nDamage)
        {
            Internal.NativeFunctions.StackPushInteger(nDamage);
            Internal.NativeFunctions.CallBuiltIn(684);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property immunity to spell level.  You must specify the level of
        ///  which that and below the user will be immune.  The level must be an integer
        ///  between 1 and 9.  By putting in a 3 it will mean the user is immune to all
        ///  3rd level and lower spells.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyImmunityToSpellLevel(int nLevel)
        {
            Internal.NativeFunctions.StackPushInteger(nLevel);
            Internal.NativeFunctions.CallBuiltIn(685);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property special walk.  If no parameters are specified it will
        ///  automatically use the zombie walk.  This will apply the special walk animation
        ///  to the user.
        /// </summary>
        public static NWN.ItemProperty ItemPropertySpecialWalk(int nWalkType = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nWalkType);
            Internal.NativeFunctions.CallBuiltIn(686);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property healers kit.  You must specify the level of the kit.
        ///  The modifier must be an integer between 1 and 12.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyHealersKit(int nModifier)
        {
            Internal.NativeFunctions.StackPushInteger(nModifier);
            Internal.NativeFunctions.CallBuiltIn(687);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property weight increase.  You must specify the weight increase
        ///  constant(IP_CONST_WEIGHTINCREASE_*).
        /// </summary>
        public static NWN.ItemProperty ItemPropertyWeightIncrease(IPConst nWeight)
        {
            Internal.NativeFunctions.StackPushInteger((int)nWeight);
            Internal.NativeFunctions.CallBuiltIn(688);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  ***********************  END OF ITEM PROPERTY FUNCTIONS  **************************
        ///  Returns true if 1d20 roll + skill rank is greater than or equal to difficulty
        ///  - oTarget: the creature using the skill
        ///  - nSkill: the skill being used
        ///  - nDifficulty: Difficulty class of skill
        /// </summary>
        public static bool GetIsSkillSuccessful(NWGameObject oTarget, Skill nSkill, int nDifficulty)
        {
            Internal.NativeFunctions.StackPushInteger(nDifficulty);
            Internal.NativeFunctions.StackPushInteger((int)nSkill);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(689);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Creates an effect that inhibits spells
        ///  - nPercent - percentage of failure
        ///  - nSpellSchool - the school of spells affected.
        /// </summary>
        public static Effect EffectSpellFailure(int nPercent = 100, SpellSchool nSpellSchool = SpellSchool.General)
        {
            Internal.NativeFunctions.StackPushInteger((int)nSpellSchool);
            Internal.NativeFunctions.StackPushInteger(nPercent);
            Internal.NativeFunctions.CallBuiltIn(690);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Causes the object to instantly speak a translated string.
        ///  (not an action, not blocked when uncommandable)
        ///  - nStrRef: Reference of the string in the talk table
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void SpeakStringByStrRef(int nStrRef, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTalkVolume);
            Internal.NativeFunctions.StackPushInteger(nStrRef);
            Internal.NativeFunctions.CallBuiltIn(691);
        }

        /// <summary>
        ///  Sets the given creature into cutscene mode.  This prevents the player from
        ///  using the GUI and camera controls.
        ///  - oCreature: creature in a cutscene
        ///  - nInCutscene: true to move them into cutscene, false to remove cutscene mode
        ///  - nLeftClickingEnabled: true to allow the user to interact with the game world using the left mouse button only.
        ///                          false to stop the user from interacting with the game world.
        ///  Note: SetCutsceneMode(oPlayer, true) will also make the player 'plot' (unkillable).
        ///  SetCutsceneMode(oPlayer, false) will restore the player's plot flag to what it
        ///  was when SetCutsceneMode(oPlayer, true) was called.
        /// </summary>
        public static void SetCutsceneMode(NWGameObject oCreature, bool nInCutscene = true, bool nLeftClickingEnabled = false)
        {
            Internal.NativeFunctions.StackPushInteger(nLeftClickingEnabled ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(nInCutscene ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(692);
        }

        /// <summary>
        ///  Gets the last player character to cancel from a cutscene.
        /// </summary>
        public static NWGameObject GetLastPCToCancelCutscene()
        {
            Internal.NativeFunctions.CallBuiltIn(693);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Gets the length of the specified wavefile, in seconds
        ///  Only works for sounds used for dialog.
        /// </summary>
        public static float GetDialogSoundLength(int nStrRef)
        {
            Internal.NativeFunctions.StackPushInteger(nStrRef);
            Internal.NativeFunctions.CallBuiltIn(694);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Fades the screen for the given creature/player from black to regular screen
        ///  - oCreature: creature controlled by player that should fade from black
        /// </summary>
        public static void FadeFromBlack(NWGameObject oCreature, float fSpeed = FadeSpeed.Medium)
        {
            Internal.NativeFunctions.StackPushFloat(fSpeed);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(695);
        }

        /// <summary>
        ///  Fades the screen for the given creature/player from regular screen to black
        ///  - oCreature: creature controlled by player that should fade to black
        /// </summary>
        public static void FadeToBlack(NWGameObject oCreature, float fSpeed = FadeSpeed.Medium)
        {
            Internal.NativeFunctions.StackPushFloat(fSpeed);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(696);
        }

        /// <summary>
        ///  Removes any fading or black screen.
        ///  - oCreature: creature controlled by player that should be cleared
        /// </summary>
        public static void StopFade(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(697);
        }

        /// <summary>
        ///  Sets the screen to black.  Can be used in preparation for a fade-in (FadeFromBlack)
        ///  Can be cleared by either doing a FadeFromBlack, or by calling StopFade.
        ///  - oCreature: creature controlled by player that should see black screen
        /// </summary>
        public static void BlackScreen(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(698);
        }

        /// <summary>
        ///  Returns the base attach bonus for the given creature.
        /// </summary>
        public static int GetBaseAttackBonus(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(699);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set a creature's immortality flag.
        ///  -oCreature: creature affected
        ///  -bImmortal: true = creature is immortal and cannot be killed (but still takes damage)
        ///              false = creature is not immortal and is damaged normally.
        ///  This scripting command only works on Creature objects.
        /// </summary>
        public static void SetImmortal(NWGameObject oCreature, bool bImmortal)
        {
            Internal.NativeFunctions.StackPushInteger(bImmortal ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(700);
        }

        /// <summary>
        ///  Open's this creature's inventory panel for this player
        ///  - oCreature: creature to view
        ///  - oPlayer: the owner of this creature will see the panel pop up
        ///  * DM's can view any creature's inventory
        ///  * Players can view their own inventory, or that of their henchman, familiar or animal companion
        /// </summary>
        public static void OpenInventory(NWGameObject oCreature, NWGameObject oPlayer)
        {
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(701);
        }

        /// <summary>
        ///  Stores the current camera mode and position so that it can be restored (using
        ///  RestoreCameraFacing())
        /// </summary>
        public static void StoreCameraFacing()
        {
            Internal.NativeFunctions.CallBuiltIn(702);
        }

        /// <summary>
        ///  Restores the camera mode and position to what they were last time StoreCameraFacing
        ///  was called.  RestoreCameraFacing can only be called once, and must correspond to a
        ///  previous call to StoreCameraFacing.
        /// </summary>
        public static void RestoreCameraFacing()
        {
            Internal.NativeFunctions.CallBuiltIn(703);
        }

        /// <summary>
        ///  Levels up a creature using default settings.
        ///  If successfull it returns the level the creature now is, or 0 if it fails.
        ///  If you want to give them a different level (ie: Give a Fighter a level of Wizard)
        ///     you can specify that in the nClass.
        ///  However, if you specify a class to which the creature no package specified,
        ///    they will use the default package for that class for their levelup choices.
        ///    (ie: no Barbarian Savage/Wizard Divination combinations)
        ///  If you turn on bReadyAllSpells, all memorized spells will be ready to cast without resting.
        ///  if nPackage is PACKAGE_INVALID then it will use the starting package assigned to that class or just the class package
        /// </summary>
        public static int LevelUpHenchman(NWGameObject oCreature, ClassType nClass = ClassType.Invalid, bool bReadyAllSpells = false, Package nPackage = Package.Invalid)
        {
            Internal.NativeFunctions.StackPushInteger((int)nPackage);
            Internal.NativeFunctions.StackPushInteger(bReadyAllSpells ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger((int)nClass);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(704);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the droppable flag on an item
        ///  - oItem: the item to change
        ///  - bDroppable: true or false, whether the item should be droppable
        ///  Droppable items will appear on a creature's remains when the creature is killed.
        /// </summary>
        public static void SetDroppableFlag(NWGameObject oItem, bool bDroppable)
        {
            Internal.NativeFunctions.StackPushInteger(bDroppable ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(705);
        }

        /// <summary>
        ///  Gets the weight of an item, or the total carried weight of a creature in tenths
        ///  of pounds (as per the baseitems.2da).
        ///  - oTarget: the item or creature for which the weight is needed
        /// </summary>
        public static int GetWeight(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(706);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the object that acquired the module item.  May be a creature, item, or placeable
        /// </summary>
        public static NWGameObject GetModuleItemAcquiredBy()
        {
            Internal.NativeFunctions.CallBuiltIn(707);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the immortal flag on a creature
        /// </summary>
        public static bool GetImmortal(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(708);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Does a single attack on every hostile creature within 10ft. of the attacker
        ///  and determines damage accordingly.  If the attacker has a ranged weapon
        ///  equipped, this will have no effect.
        ///  ** NOTE ** This is meant to be called inside the spell script for whirlwind
        ///  attack, it is not meant to be used to queue up a new whirlwind attack.  To do
        ///  that you need to call ActionUseFeat(FEAT_WHIRLWIND_ATTACK, oEnemy)
        ///  - int bDisplayFeedback: true or false, whether or not feedback should be
        ///    displayed
        ///  - int bImproved: If true, the improved version of whirlwind is used
        /// </summary>
        public static void DoWhirlwindAttack(bool bDisplayFeedback = true, bool bImproved = false)
        {
            Internal.NativeFunctions.StackPushInteger(bImproved ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bDisplayFeedback ? 1 : 0);
            Internal.NativeFunctions.CallBuiltIn(709);
        }

        /// <summary>
        ///  Gets a value from a 2DA file on the server and returns it as a string
        ///  avoid using this function in loops
        ///  - s2DA: the name of the 2da file, 16 chars max
        ///  - sColumn: the name of the column in the 2da
        ///  - nRow: the row in the 2da
        ///  * returns an empty string if file, row, or column not found
        /// </summary>
        public static string Get2DAString(string s2DA, string sColumn, int nRow)
        {
            Internal.NativeFunctions.StackPushInteger(nRow);
            Internal.NativeFunctions.StackPushStringUTF8(sColumn);
            Internal.NativeFunctions.StackPushStringUTF8(s2DA);
            Internal.NativeFunctions.CallBuiltIn(710);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Returns an effect of type EFFECT_TYPE_ETHEREAL which works just like EffectSanctuary
        ///  except that the observers get no saving throw
        /// </summary>
        public static Effect EffectEthereal()
        {
            Internal.NativeFunctions.CallBuiltIn(711);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Gets the current AI Level that the creature is running at.
        ///  Returns one of the following:
        ///  AI_LEVEL_INVALID, AI_LEVEL_VERY_LOW, AI_LEVEL_LOW, AI_LEVEL_NORMAL, AI_LEVEL_HIGH, AI_LEVEL_VERY_HIGH
        /// </summary>
        public static AILevel GetAILevel(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(712);
            return (AILevel)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the current AI Level of the creature to the value specified. Does not work on Players.
        ///  The game by default will choose an appropriate AI level for
        ///  creatures based on the circumstances that the creature is in.
        ///  Explicitly setting an AI level will over ride the game AI settings.
        ///  The new setting will last until SetAILevel is called again with the argument AI_LEVEL_DEFAULT.
        ///  AI_LEVEL_DEFAULT  - Default setting. The game will take over seting the appropriate AI level when required.
        ///  AI_LEVEL_VERY_LOW - Very Low priority, very stupid, but low CPU usage for AI. Typically used when no players are in the area.
        ///  AI_LEVEL_LOW      - Low priority, mildly stupid, but slightly more CPU usage for AI. Typically used when not in combat, but a player is in the area.
        ///  AI_LEVEL_NORMAL   - Normal priority, average AI, but more CPU usage required for AI. Typically used when creature is in combat.
        ///  AI_LEVEL_HIGH     - High priority, smartest AI, but extremely high CPU usage required for AI. Avoid using this. It is most likely only ever needed for cutscenes.
        /// </summary>
        public static void SetAILevel(NWGameObject oTarget, AILevel nAILevel)
        {
            Internal.NativeFunctions.StackPushInteger((int)nAILevel);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(713);
        }

        /// <summary>
        ///  This will return true if the creature running the script is a familiar currently
        ///  possessed by his master.
        ///  returns false if not or if the creature object is invalid
        /// </summary>
        public static bool GetIsPossessedFamiliar(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(714);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  This will cause a Player Creature to unpossess his/her familiar.  It will work if run
        ///  on the player creature or the possessed familiar.  It does not work in conjunction with
        ///  any DM possession.
        /// </summary>
        public static void UnpossessFamiliar(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(715);
        }

        /// <summary>
        ///  This will return true if the area is flagged as either interior or underground.
        /// </summary>
        public static bool GetIsAreaInterior(NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(716);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Send a server message (szMessage) to the oPlayer.
        /// </summary>
        public static void SendMessageToPCByStrRef(NWGameObject oPlayer, int nStrRef)
        {
            Internal.NativeFunctions.StackPushInteger(nStrRef);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(717);
        }

        /// <summary>
        ///  Increment the remaining uses per day for this creature by one.
        ///  Total number of feats per day can not exceed the maximum.
        ///  - oCreature: creature to modify
        ///  - nFeat: constant FEAT_*
        /// </summary>
        public static void IncrementRemainingFeatUses(NWGameObject oCreature, Feat nFeat)
        {
            Internal.NativeFunctions.StackPushInteger((int)nFeat);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(718);
        }

        /// <summary>
        ///  Force the character of the player specified to be exported to its respective directory
        ///  i.e. LocalVault/ServerVault/ etc.
        /// </summary>
        public static void ExportSingleCharacter(NWGameObject oPlayer)
        {
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(719);
        }

        /// <summary>
        ///  This will play a sound that is associated with a stringRef, it will be a mono sound from the location of the object running the command.
        ///  if nRunAsAction is off then the sound is forced to play intantly.
        /// </summary>
        public static void PlaySoundByStrRef(int nStrRef, bool nRunAsAction = true)
        {
            Internal.NativeFunctions.StackPushInteger(nRunAsAction ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(nStrRef);
            Internal.NativeFunctions.CallBuiltIn(720);
        }

        /// <summary>
        ///  Set the name of oCreature's sub race to sSubRace.
        /// </summary>
        public static void SetSubRace(NWGameObject oCreature, string sSubRace)
        {
            Internal.NativeFunctions.StackPushString(sSubRace);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(721);
        }

        /// <summary>
        ///  Set the name of oCreature's Deity to sDeity.
        /// </summary>
        public static void SetDeity(NWGameObject oCreature, string sDeity)
        {
            Internal.NativeFunctions.StackPushString(sDeity);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(722);
        }

        /// <summary>
        ///  Returns true if the creature oCreature is currently possessed by a DM character.
        ///  Returns false otherwise.
        ///  Note: GetIsDMPossessed() will return false if oCreature is the DM character.
        ///  To determine if oCreature is a DM character use GetIsDM()
        /// </summary>
        public static bool GetIsDMPossessed(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(723);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Gets the current weather conditions for the area oArea.
        ///    Returns: WEATHER_CLEAR, WEATHER_RAIN, WEATHER_SNOW, WEATHER_INVALID
        ///    Note: If called on an Interior area, this will always return WEATHER_CLEAR.
        /// </summary>
        public static Weather GetWeather(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(724);
            return (Weather)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns AREA_NATURAL if the area oArea is natural, AREA_ARTIFICIAL otherwise.
        ///  Returns AREA_INVALID, on an error.
        /// </summary>
        public static bool GetIsAreaNatural(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(725);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Returns AREA_ABOVEGROUND if the area oArea is above ground, AREA_UNDERGROUND otherwise.
        ///  Returns AREA_INVALID, on an error.
        /// </summary>
        public static bool GetIsAreaAboveGround(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(726);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Use this to get the item last equipped by a player character in OnPlayerEquipItem..
        /// </summary>
        public static NWGameObject GetPCItemLastEquipped()
        {
            Internal.NativeFunctions.CallBuiltIn(727);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this to get the player character who last equipped an item in OnPlayerEquipItem..
        /// </summary>
        public static NWGameObject GetPCItemLastEquippedBy()
        {
            Internal.NativeFunctions.CallBuiltIn(728);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this to get the item last unequipped by a player character in OnPlayerEquipItem..
        /// </summary>
        public static NWGameObject GetPCItemLastUnequipped()
        {
            Internal.NativeFunctions.CallBuiltIn(729);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Use this to get the player character who last unequipped an item in OnPlayerUnEquipItem..
        /// </summary>
        public static NWGameObject GetPCItemLastUnequippedBy()
        {
            Internal.NativeFunctions.CallBuiltIn(730);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Creates a new copy of an item, while making a single change to the appearance of the item.
        ///  Helmet models and simple items ignore iIndex.
        ///  iType                            iIndex                              iNewValue
        ///  ITEM_APPR_TYPE_SIMPLE_MODEL      [Ignored]                           Model #
        ///  ITEM_APPR_TYPE_WEAPON_COLOR      ITEM_APPR_WEAPON_COLOR_*            1-4
        ///  ITEM_APPR_TYPE_WEAPON_MODEL      ITEM_APPR_WEAPON_MODEL_*            Model #
        ///  ItemApprType.ArmorModel       ITEM_APPR_ARMOR_MODEL_*             Model #
        ///  ItemApprType.ArmorColor       ITEM_APPR_ARMOR_COLOR_* [0]         0-175 [1]
        /// 
        ///  [0] Alternatively, where ItemApprType.ArmorColor is specified, if per-part coloring is
        ///  desired, the following equation can be used for nIndex to achieve that:
        /// 
        ///    ITEM_APPR_ARMOR_NUM_COLORS + (ITEM_APPR_ARMOR_MODEL_ * ITEM_APPR_ARMOR_NUM_COLORS) + ITEM_APPR_ARMOR_COLOR_
        /// 
        ///  For example, to change the CLOTH1 channel of the torso, nIndex would be:
        /// 
        ///    6 + (7 * 6) + 2 = 50
        /// 
        ///  [1] When specifying per-part coloring, the value 255 is allowed and corresponds with the logical
        ///  function 'clear colour override', which clears the per-part override for that part.
        /// </summary>
        private static NWGameObject CopyItemAndModify(NWGameObject oItem, int nType, int nIndex, int nNewValue, bool bCopyVars = true)
        {
            Internal.NativeFunctions.StackPushInteger(bCopyVars ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(nNewValue);
            Internal.NativeFunctions.StackPushInteger((int)nIndex);
            Internal.NativeFunctions.StackPushInteger((int)nType);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(731);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Queries the current value of the appearance settings on an item. The parameters are
        ///  identical to those of CopyItemAndModify().
        /// </summary>
        private static int GetItemAppearance(NWGameObject oItem, int nType, int nIndex)
        {
            Internal.NativeFunctions.StackPushInteger(nIndex);
            Internal.NativeFunctions.StackPushInteger(nType);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(732);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Creates an item property that (when applied to a weapon item) causes a spell to be cast
        ///  when a successful strike is made, or (when applied to armor) is struck by an opponent.
        ///  - nSpell uses the IP_CONST_ONHIT_CASTSPELL_* constants
        /// </summary>
        public static NWN.ItemProperty ItemPropertyOnHitCastSpell(IPConst nSpell, int nLevel)
        {
            Internal.NativeFunctions.StackPushInteger(nLevel);
            Internal.NativeFunctions.StackPushInteger((int)nSpell);
            Internal.NativeFunctions.CallBuiltIn(733);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns the SubType number of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertySubType(NWN.ItemProperty iProperty)
        {
            Internal.NativeFunctions.StackPushItemProperty(iProperty.Handle);
            Internal.NativeFunctions.CallBuiltIn(734);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the status of ACTION_MODE_* modes on a creature.
        /// </summary>
        public static int GetActionMode(NWGameObject oCreature, ActionMode nMode)
        {
            Internal.NativeFunctions.StackPushInteger((int)nMode);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(735);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the status of modes ACTION_MODE_* on a creature.
        /// </summary>
        public static void SetActionMode(NWGameObject oCreature, ActionMode nMode, int nStatus)
        {
            Internal.NativeFunctions.StackPushInteger(nStatus);
            Internal.NativeFunctions.StackPushInteger((int)nMode);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(736);
        }

        /// <summary>
        ///  Returns the current arcane spell failure factor of a creature
        /// </summary>
        public static int GetArcaneSpellFailure(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(737);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Makes a player examine the object oExamine. This causes the examination
        ///  pop-up box to appear for the object specified.
        /// </summary>
        public static void ActionExamine(NWGameObject oExamine)
        {
            Internal.NativeFunctions.StackPushObject(oExamine != null ? oExamine.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(738);
        }

        /// <summary>
        ///  Creates a visual effect (ITEM_VISUAL_*) that may be applied to
        ///  melee weapons only.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyVisualEffect(ItemVisual nEffect)
        {
            Internal.NativeFunctions.StackPushInteger((int)nEffect);
            Internal.NativeFunctions.CallBuiltIn(739);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Sets the lootable state of a *living* NPC creature.
        ///  This function will *not* work on players or dead creatures.
        /// </summary>
        public static void SetLootable(NWGameObject oCreature, bool bLootable)
        {
            Internal.NativeFunctions.StackPushInteger(bLootable ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(740);
        }

        /// <summary>
        ///  Returns the lootable state of a creature.
        /// </summary>
        public static bool GetLootable(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(741);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Returns the current movement rate factor
        ///  of the cutscene 'camera man'.
        ///  NOTE: This will be a value between 0.1, 2.0 (10%-200%)
        /// </summary>
        public static float GetCutsceneCameraMoveRate(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(742);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Sets the current movement rate factor for the cutscene
        ///  camera man.
        ///  NOTE: You can only set values between 0.1, 2.0 (10%-200%)
        /// </summary>
        public static void SetCutsceneCameraMoveRate(NWGameObject oCreature, float fRate)
        {
            Internal.NativeFunctions.StackPushFloat(fRate);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(743);
        }

        /// <summary>
        ///  Returns true if the item is cursed and cannot be dropped
        /// </summary>
        public static bool GetItemCursedFlag(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(744);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  When cursed, items cannot be dropped
        /// </summary>
        public static void SetItemCursedFlag(NWGameObject oItem, bool nCursed)
        {
            Internal.NativeFunctions.StackPushInteger(nCursed ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(745);
        }

        /// <summary>
        ///  Sets the maximum number of henchmen
        /// </summary>
        public static void SetMaxHenchmen(int nNumHenchmen)
        {
            Internal.NativeFunctions.StackPushInteger(nNumHenchmen);
            Internal.NativeFunctions.CallBuiltIn(746);
        }

        /// <summary>
        ///  Gets the maximum number of henchmen
        /// </summary>
        public static int GetMaxHenchmen()
        {
            Internal.NativeFunctions.CallBuiltIn(747);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the associate type of the specified creature.
        ///  - Returns ASSOCIATE_TYPE_NONE if the creature is not the associate of anyone.
        /// </summary>
        public static AssociateType GetAssociateType(NWGameObject oAssociate)
        {
            Internal.NativeFunctions.StackPushObject(oAssociate != null ? oAssociate.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(748);
            return (AssociateType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the spell resistance of the specified creature.
        ///  - Returns 0 if the creature has no spell resistance or an invalid
        ///    creature is passed in.
        /// </summary>
        public static int GetSpellResistance(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(749);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Changes the current Day/Night cycle for this player to night
        ///  - oPlayer: which player to change the lighting for
        ///  - fTransitionTime: how long the transition should take
        /// </summary>
        public static void DayToNight(NWGameObject oPlayer, float fTransitionTime = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fTransitionTime);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(750);
        }

        /// <summary>
        ///  Changes the current Day/Night cycle for this player to daylight
        ///  - oPlayer: which player to change the lighting for
        ///  - fTransitionTime: how long the transition should take
        /// </summary>
        public static void NightToDay(NWGameObject oPlayer, float fTransitionTime = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fTransitionTime);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(751);
        }

        /// <summary>
        ///  Returns whether or not there is a direct line of sight
        ///  between the two objects. (Not blocked by any geometry).
        /// 
        ///  PLEASE NOTE: This is an expensive function and may
        ///               degrade performance if used frequently.
        /// </summary>
        public static bool LineOfSightObject(NWGameObject oSource, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oSource != null ? oSource.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(752);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Returns whether or not there is a direct line of sight
        ///  between the two vectors. (Not blocked by any geometry).
        /// 
        ///  This function must be run on a valid object in the area
        ///  it will not work on the module or area.
        /// 
        ///  PLEASE NOTE: This is an expensive function and may
        ///               degrade performance if used frequently.
        /// </summary>
        public static bool LineOfSightVector(NWN.Vector? vSource, NWN.Vector? vTarget)
        {
            Internal.NativeFunctions.StackPushVector(vTarget.HasValue ? vTarget.Value : new NWN.Vector());
            Internal.NativeFunctions.StackPushVector(vSource.HasValue ? vSource.Value : new NWN.Vector());
            Internal.NativeFunctions.CallBuiltIn(753);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Returns the class that the spellcaster cast the
        ///  spell as.
        ///  - Returns CLASS_TYPE_INVALID if the caster has
        ///    no valid class (placeables, etc...)
        /// </summary>
        public static ClassType GetLastSpellCastClass()
        {
            Internal.NativeFunctions.CallBuiltIn(754);
            return (ClassType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the number of base attacks for the specified
        ///  creatures. The range of values accepted are from
        ///  1 to 6
        ///  Note: This function does not work on Player Characters
        /// </summary>
        public static void SetBaseAttackBonus(int nBaseAttackBonus, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nBaseAttackBonus);
            Internal.NativeFunctions.CallBuiltIn(755);
        }

        /// <summary>
        ///  Restores the number of base attacks back to it's
        ///  original state.
        /// </summary>
        public static void RestoreBaseAttackBonus(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(756);
        }

        /// <summary>
        ///  Creates a cutscene ghost effect, this will allow creatures
        ///  to pathfind through other creatures without bumping into them
        ///  for the duration of the effect.
        /// </summary>
        public static Effect EffectCutsceneGhost()
        {
            Internal.NativeFunctions.CallBuiltIn(757);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Creates an item property that offsets the effect on arcane spell failure
        ///  that a particular item has. Parameters come from the ITEM_PROP_ASF_* group.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyArcaneSpellFailure(int nModLevel)
        {
            Internal.NativeFunctions.StackPushInteger(nModLevel);
            Internal.NativeFunctions.CallBuiltIn(758);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns the amount of gold a store currently has. -1 indicates it is not using gold.
        ///  -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreGold(NWGameObject oidStore)
        {
            Internal.NativeFunctions.StackPushObject(oidStore != null ? oidStore.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(759);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the amount of gold a store has. -1 means the store does not use gold.
        /// </summary>
        public static void SetStoreGold(NWGameObject oidStore, int nGold)
        {
            Internal.NativeFunctions.StackPushInteger(nGold);
            Internal.NativeFunctions.StackPushObject(oidStore != null ? oidStore.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(760);
        }

        /// <summary>
        ///  Gets the maximum amount a store will pay for any item. -1 means price unlimited.
        ///  -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreMaxBuyPrice(NWGameObject oidStore)
        {
            Internal.NativeFunctions.StackPushObject(oidStore != null ? oidStore.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(761);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the maximum amount a store will pay for any item. -1 means price unlimited.
        /// </summary>
        public static void SetStoreMaxBuyPrice(NWGameObject oidStore, int nMaxBuy)
        {
            Internal.NativeFunctions.StackPushInteger(nMaxBuy);
            Internal.NativeFunctions.StackPushObject(oidStore != null ? oidStore.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(762);
        }

        /// <summary>
        ///  Gets the amount a store charges for identifying an item. Default is 100. -1 means
        ///  the store will not identify items.
        ///  -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreIdentifyCost(NWGameObject oidStore)
        {
            Internal.NativeFunctions.StackPushObject(oidStore != null ? oidStore.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(763);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the amount a store charges for identifying an item. Default is 100. -1 means
        ///  the store will not identify items.
        /// </summary>
        public static void SetStoreIdentifyCost(NWGameObject oidStore, int nCost)
        {
            Internal.NativeFunctions.StackPushInteger(nCost);
            Internal.NativeFunctions.StackPushObject(oidStore != null ? oidStore.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(764);
        }

        /// <summary>
        ///  Sets the creature's appearance type to the value specified (uses the APPEARANCE_TYPE_XXX constants)
        /// </summary>
        public static void SetCreatureAppearanceType(NWGameObject oCreature, AppearanceType nAppearanceType)
        {
            Internal.NativeFunctions.StackPushInteger((int)nAppearanceType);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(765);
        }

        /// <summary>
        ///  Returns the default package selected for this creature to level up with
        ///  - returns PACKAGE_INVALID if error occurs
        /// </summary>
        public static Package GetCreatureStartingPackage(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(766);
            return (Package)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns an effect that when applied will paralyze the target's legs, rendering
        ///  them unable to walk but otherwise unpenalized. This effect cannot be resisted.
        /// </summary>
        public static Effect EffectCutsceneImmobilize()
        {
            Internal.NativeFunctions.CallBuiltIn(767);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Is this creature in the given subarea? (trigger, area of effect object, etc..)
        ///  This function will tell you if the creature has triggered an onEnter event,
        ///  not if it is physically within the space of the subarea
        /// </summary>
        public static bool GetIsInSubArea(NWGameObject oCreature, NWGameObject oSubArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oSubArea != null ? oSubArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(768);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Returns the Cost Table number of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyCostTable(NWN.ItemProperty iProp)
        {
            Internal.NativeFunctions.StackPushItemProperty(iProp.Handle);
            Internal.NativeFunctions.CallBuiltIn(769);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the Cost Table value (index of the cost table) of the item property.
        ///  See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyCostTableValue(NWN.ItemProperty iProp)
        {
            Internal.NativeFunctions.StackPushItemProperty(iProp.Handle);
            Internal.NativeFunctions.CallBuiltIn(770);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the Param1 number of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyParam1(NWN.ItemProperty iProp)
        {
            Internal.NativeFunctions.StackPushItemProperty(iProp.Handle);
            Internal.NativeFunctions.CallBuiltIn(771);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the Param1 value of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyParam1Value(NWN.ItemProperty iProp)
        {
            Internal.NativeFunctions.StackPushItemProperty(iProp.Handle);
            Internal.NativeFunctions.CallBuiltIn(772);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Is this creature able to be disarmed? (checks disarm flag on creature, and if
        ///  the creature actually has a weapon equipped in their right hand that is droppable)
        /// </summary>
        public static int GetIsCreatureDisarmable(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(773);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets whether this item is 'stolen' or not
        /// </summary>
        public static void SetStolenFlag(NWGameObject oItem, bool nStolenFlag)
        {
            Internal.NativeFunctions.StackPushInteger(nStolenFlag ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(774);
        }

        /// <summary>
        ///  Instantly gives this creature the benefits of a rest (restored hitpoints, spells, feats, etc..)
        /// </summary>
        public static void ForceRest(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(775);
        }

        /// <summary>
        ///  Forces this player's camera to be set to this height. Setting this value to zero will
        ///  restore the camera to the racial default height.
        /// </summary>
        public static void SetCameraHeight(NWGameObject oPlayer, float fHeight = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fHeight);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(776);
        }

        /// <summary>
        ///  Changes the sky that is displayed in the specified area.
        ///  nSkyBox = SKYBOX_* constants (associated with skyboxes.2da)
        ///  If no valid area (or object) is specified, it uses the area of caller.
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetSkyBox(Skybox nSkyBox, NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nSkyBox);
            Internal.NativeFunctions.CallBuiltIn(777);
        }

        /// <summary>
        ///  Returns the creature's currently set PhenoType (body type).
        /// </summary>
        public static PhenoType GetPhenoType(NWGameObject oCreature)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(778);
            return (PhenoType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the creature's PhenoType (body type) to the type specified.
        ///  nPhenoType = PHENOTYPE_NORMAL
        ///  nPhenoType = PHENOTYPE_BIG
        ///  nPhenoType = PHENOTYPE_CUSTOM* - The custom PhenoTypes should only ever
        ///  be used if you have specifically created your own custom content that
        ///  requires the use of a new PhenoType and you have specified the appropriate
        ///  custom PhenoType in your custom content.
        ///  SetPhenoType will only work on part based creature (i.e. the starting
        ///  default playable races).
        /// </summary>
        public static void SetPhenoType(PhenoType nPhenoType, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nPhenoType);
            Internal.NativeFunctions.CallBuiltIn(779);
        }

        /// <summary>
        ///  Sets the fog color in the area specified.
        ///  nFogType = FOG_TYPE_* specifies wether the Sun, Moon, or both fog types are set.
        ///  nFogColor = FOG_COLOR_* specifies the color the fog is being set to.
        ///  The fog color can also be represented as a hex RGB number if specific color shades
        ///  are desired.
        ///  The format of a hex specified color would be 0xFFEEDD where
        ///  FF would represent the amount of red in the color
        ///  EE would represent the amount of green in the color
        ///  DD would represent the amount of blue in the color.
        ///  If no valid area (or object) is specified, it uses the area of caller.
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetFogColor(FogType nFogType, FogColor nFogColor, NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nFogColor);
            Internal.NativeFunctions.StackPushInteger((int)nFogType);
            Internal.NativeFunctions.CallBuiltIn(780);
        }

        /// <summary>
        ///  Gets the current cutscene state of the player specified by oCreature.
        ///  Returns true if the player is in cutscene mode.
        ///  Returns false if the player is not in cutscene mode, or on an error
        ///  (such as specifying a non creature object).
        /// </summary>
        public static bool GetCutsceneMode(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(781);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Gets the skybox that is currently displayed in the specified area.
        ///  Returns:
        ///      SKYBOX_* constant
        ///  If no valid area (or object) is specified, it uses the area of caller.
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static Skybox GetSkyBox(NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(782);
            return (Skybox)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the fog color in the area specified.
        ///  nFogType specifies wether the Sun, or Moon fog type is returned. 
        ///     Valid values for nFogType are FOG_TYPE_SUN or FOG_TYPE_MOON.
        ///  If no valid area (or object) is specified, it uses the area of caller.
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static FogColor GetFogColor(FogType nFogType, NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nFogType);
            Internal.NativeFunctions.CallBuiltIn(783);
            return (FogColor)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the fog amount in the area specified.
        ///  nFogType = FOG_TYPE_* specifies wether the Sun, Moon, or both fog types are set.
        ///  nFogAmount = specifies the density that the fog is being set to.
        ///  If no valid area (or object) is specified, it uses the area of caller.
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetFogAmount(FogType nFogType, int nFogAmount, NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nFogAmount);
            Internal.NativeFunctions.StackPushInteger((int)nFogType);
            Internal.NativeFunctions.CallBuiltIn(784);
        }

        /// <summary>
        ///  Gets the fog amount in the area specified.
        ///  nFogType = nFogType specifies wether the Sun, or Moon fog type is returned. 
        ///     Valid values for nFogType are FOG_TYPE_SUN or FOG_TYPE_MOON.
        ///  If no valid area (or object) is specified, it uses the area of caller.
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static int GetFogAmount(FogType nFogType, NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nFogType);
            Internal.NativeFunctions.CallBuiltIn(785);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  returns true if the item CAN be pickpocketed
        /// </summary>
        public static bool GetPickpocketableFlag(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(786);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Sets the Pickpocketable flag on an item
        ///  - oItem: the item to change
        ///  - bPickpocketable: true or false, whether the item can be pickpocketed.
        /// </summary>
        public static void SetPickpocketableFlag(NWGameObject oItem, bool bPickpocketable)
        {
            Internal.NativeFunctions.StackPushInteger(bPickpocketable ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(787);
        }

        /// <summary>
        ///  returns the footstep type of the creature specified.
        ///  The footstep type determines what the creature's footsteps sound
        ///  like when ever they take a step.
        ///  returns FOOTSTEP_TYPE_INVALID if used on a non-creature object, or if
        ///  used on creature that has no footstep sounds by default (e.g. Will-O'-Wisp).
        /// </summary>
        public static FootstepType GetFootstepType(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(788);
            return (FootstepType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the footstep type of the creature specified.
        ///  Changing a creature's footstep type will change the sound that
        ///  its feet make when ever the creature makes takes a step.
        ///  By default a creature's footsteps are detemined by the appearance
        ///  type of the creature. SetFootstepType() allows you to make a
        ///  creature use a difference footstep type than it would use by default
        ///  for its given appearance.
        ///  - nFootstepType (FOOTSTEP_TYPE_*):
        ///       FOOTSTEP_TYPE_NORMAL
        ///       FOOTSTEP_TYPE_LARGE
        ///       FOOTSTEP_TYPE_DRAGON
        ///       FOOTSTEP_TYPE_SoFT
        ///       FOOTSTEP_TYPE_HOOF
        ///       FOOTSTEP_TYPE_HOOF_LARGE
        ///       FOOTSTEP_TYPE_BEETLE
        ///       FOOTSTEP_TYPE_SPIDER
        ///       FOOTSTEP_TYPE_SKELETON
        ///       FOOTSTEP_TYPE_LEATHER_WING
        ///       FOOTSTEP_TYPE_FEATHER_WING
        ///       FOOTSTEP_TYPE_DEFAULT - Makes the creature use its original default footstep sounds.
        ///       FOOTSTEP_TYPE_NONE
        ///  - oCreature: the creature to change the footstep sound for.
        /// </summary>
        public static void SetFootstepType(FootstepType nFootstepType, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nFootstepType);
            Internal.NativeFunctions.CallBuiltIn(789);
        }

        /// <summary>
        ///  returns the Wing type of the creature specified.
        ///       CREATURE_WING_TYPE_NONE
        ///       CREATURE_WING_TYPE_DEMON
        ///       CREATURE_WING_TYPE_ANGEL
        ///       CREATURE_WING_TYPE_BAT
        ///       CREATURE_WING_TYPE_DRAGON
        ///       CREATURE_WING_TYPE_BUTTERFLY
        ///       CREATURE_WING_TYPE_BIRD
        ///  returns CREATURE_WING_TYPE_NONE if used on a non-creature object,
        ///  if the creature has no wings, or if the creature can not have its
        ///  wing type changed in the toolset.
        /// </summary>
        public static CreatureWingType GetCreatureWingType(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(790);
            return (CreatureWingType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the Wing type of the creature specified.
        ///  - nWingType (CREATURE_WING_TYPE_*)
        ///       CREATURE_WING_TYPE_NONE
        ///       CREATURE_WING_TYPE_DEMON
        ///       CREATURE_WING_TYPE_ANGEL
        ///       CREATURE_WING_TYPE_BAT
        ///       CREATURE_WING_TYPE_DRAGON
        ///       CREATURE_WING_TYPE_BUTTERFLY
        ///       CREATURE_WING_TYPE_BIRD
        ///  - oCreature: the creature to change the wing type for.
        ///  Note: Only two creature model types will support wings. 
        ///  The MODELTYPE for the part based (playable races) 'P' 
        ///  and MODELTYPE 'W'in the appearance.2da
        /// </summary>
        public static void SetCreatureWingType(CreatureWingType nWingType, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nWingType);
            Internal.NativeFunctions.CallBuiltIn(791);
        }

        /// <summary>
        ///  returns the model number being used for the body part and creature specified
        ///  The model number returned is for the body part when the creature is not wearing
        ///  armor (i.e. whether or not the creature is wearing armor does not affect
        ///  the return value).
        ///  Note: Only works on part based creatures, which is typically restricted to
        ///  the playable races (unless some new part based custom content has been 
        ///  added to the module).
        /// 
        ///  returns CREATURE_PART_INVALID if used on a non-creature object,
        ///  or if the creature does not use a part based model.
        /// 
        ///  - nPart (CREATURE_PART_*)
        ///       CREATURE_PART_RIGHT_FOOT
        ///       CREATURE_PART_LEFT_FOOT
        ///       CREATURE_PART_RIGHT_SHIN
        ///       CREATURE_PART_LEFT_SHIN
        ///       CREATURE_PART_RIGHT_THIGH
        ///       CREATURE_PART_LEFT_THIGH
        ///       CREATURE_PART_PELVIS
        ///       CREATURE_PART_TORSO
        ///       CREATURE_PART_BELT
        ///       CREATURE_PART_NECK
        ///       CREATURE_PART_RIGHT_FOREARM
        ///       CREATURE_PART_LEFT_FOREARM
        ///       CREATURE_PART_RIGHT_BICEP
        ///       CREATURE_PART_LEFT_BICEP
        ///       CREATURE_PART_RIGHT_SHOULDER
        ///       CREATURE_PART_LEFT_SHOULDER
        ///       CREATURE_PART_RIGHT_HAND
        ///       CREATURE_PART_LEFT_HAND
        ///       CREATURE_PART_HEAD
        /// </summary>
        public static int GetCreatureBodyPart(CreaturePart nPart, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nPart);
            Internal.NativeFunctions.CallBuiltIn(792);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the body part model to be used on the creature specified.
        ///  The model names for parts need to be in the following format:
        ///    p<m/f><race letter><phenotype>_<body part><model number>.mdl
        /// 
        ///  - nPart (CREATURE_PART_*)
        ///       CREATURE_PART_RIGHT_FOOT
        ///       CREATURE_PART_LEFT_FOOT
        ///       CREATURE_PART_RIGHT_SHIN
        ///       CREATURE_PART_LEFT_SHIN
        ///       CREATURE_PART_RIGHT_THIGH
        ///       CREATURE_PART_LEFT_THIGH
        ///       CREATURE_PART_PELVIS
        ///       CREATURE_PART_TORSO
        ///       CREATURE_PART_BELT
        ///       CREATURE_PART_NECK
        ///       CREATURE_PART_RIGHT_FOREARM
        ///       CREATURE_PART_LEFT_FOREARM
        ///       CREATURE_PART_RIGHT_BICEP
        ///       CREATURE_PART_LEFT_BICEP
        ///       CREATURE_PART_RIGHT_SHOULDER
        ///       CREATURE_PART_LEFT_SHOULDER
        ///       CREATURE_PART_RIGHT_HAND
        ///       CREATURE_PART_LEFT_HAND
        ///       CREATURE_PART_HEAD
        ///  - nModelNumber: CREATURE_MODEL_TYPE_*
        ///       CREATURE_MODEL_TYPE_NONE
        ///       CREATURE_MODEL_TYPE_SKIN (not for use on shoulders, pelvis or head).
        ///       CREATURE_MODEL_TYPE_TATTOO (for body parts that support tattoos, i.e. not heads/feet/hands).
        ///       CREATURE_MODEL_TYPE_UNDEAD (undead model only exists for the right arm parts).
        ///  - oCreature: the creature to change the body part for.
        ///  Note: Only part based creature appearance types are supported. 
        ///  i.e. The model types for the playable races ('P') in the appearance.2da
        /// </summary>
        public static void SetCreatureBodyPart(CreaturePart nPart, int nModelNumber, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nModelNumber);
            Internal.NativeFunctions.StackPushInteger((int)nPart);
            Internal.NativeFunctions.CallBuiltIn(793);
        }

        /// <summary>
        ///  returns the Tail type of the creature specified.
        ///       CREATURE_TAIL_TYPE_NONE
        ///       CREATURE_TAIL_TYPE_LIZARD
        ///       CREATURE_TAIL_TYPE_BONE
        ///       CREATURE_TAIL_TYPE_DEVIL
        ///  returns CREATURE_TAIL_TYPE_NONE if used on a non-creature object,
        ///  if the creature has no Tail, or if the creature can not have its
        ///  Tail type changed in the toolset.
        /// </summary>
        public static CreatureTailType GetCreatureTailType(NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(794);
            return (CreatureTailType)Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the Tail type of the creature specified.
        ///  - nTailType (CREATURE_TAIL_TYPE_*)
        ///       CREATURE_TAIL_TYPE_NONE
        ///       CREATURE_TAIL_TYPE_LIZARD
        ///       CREATURE_TAIL_TYPE_BONE
        ///       CREATURE_TAIL_TYPE_DEVIL
        ///  - oCreature: the creature to change the Tail type for.
        ///  Note: Only two creature model types will support Tails. 
        ///  The MODELTYPE for the part based (playable) races 'P' 
        ///  and MODELTYPE 'T'in the appearance.2da
        /// </summary>
        public static void SetCreatureTailType(CreatureTailType nTailType, NWGameObject oCreature = null)
        {
            Internal.NativeFunctions.StackPushObject(oCreature != null ? oCreature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nTailType);
            Internal.NativeFunctions.CallBuiltIn(795);
        }

        /// <summary>
        ///  returns the Hardness of a Door or Placeable object.
        ///  - oObject: a door or placeable object.
        ///  returns -1 on an error or if used on an object that is
        ///  neither a door nor a placeable object.
        /// </summary>
        public static int GetHardness(NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(796);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the Hardness of a Door or Placeable object.
        ///  - nHardness: must be between 0 and 250.
        ///  - oObject: a door or placeable object.
        ///  Does nothing if used on an object that is neither
        ///  a door nor a placeable.
        /// </summary>
        public static void SetHardness(int nHardness, NWGameObject oObject = null)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nHardness);
            Internal.NativeFunctions.CallBuiltIn(797);
        }

        /// <summary>
        ///  When set the object can not be opened unless the
        ///  opener possesses the required key. The key tag required
        ///  can be specified either in the toolset, or by using
        ///  the SetLockKeyTag() scripting command.
        ///  - oObject: a door, or placeable.
        ///  - nKeyRequired: true/false
        /// </summary>
        public static void SetLockKeyRequired(NWGameObject oObject, bool nKeyRequired = true)
        {
            Internal.NativeFunctions.StackPushInteger(nKeyRequired ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(798);
        }

        /// <summary>
        ///  Set the key tag required to open object oObject.
        ///  This will only have an effect if the object is set to
        ///  "Key required to unlock or lock" either in the toolset
        ///  or by using the scripting command SetLockKeyRequired().
        ///  - oObject: a door, placeable or trigger.
        ///  - sNewKeyTag: the key tag required to open the locked object.
        /// </summary>
        public static void SetLockKeyTag(NWGameObject oObject, string sNewKeyTag)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sNewKeyTag);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(799);
        }

        /// <summary>
        ///  Sets whether or not the object can be locked.
        ///  - oObject: a door or placeable.
        ///  - nLockable: true/false
        /// </summary>
        public static void SetLockLockable(NWGameObject oObject, bool nLockable = true)
        {
            Internal.NativeFunctions.StackPushInteger(nLockable ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(800);
        }

        /// <summary>
        ///  Sets the DC for unlocking the object.
        ///  - oObject: a door or placeable object.
        ///  - nNewUnlockDC: must be between 0 and 250.
        /// </summary>
        public static void SetLockUnlockDC(NWGameObject oObject, int nNewUnlockDC)
        {
            Internal.NativeFunctions.StackPushInteger(nNewUnlockDC);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(801);
        }

        /// <summary>
        ///  Sets the DC for locking the object.
        ///  - oObject: a door or placeable object.
        ///  - nNewLockDC: must be between 0 and 250.
        /// </summary>
        public static void SetLockLockDC(NWGameObject oObject, int nNewLockDC)
        {
            Internal.NativeFunctions.StackPushInteger(nNewLockDC);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(802);
        }

        /// <summary>
        ///  Sets whether or not the trapped object can be disarmed.
        ///  - oTrapObject: a placeable, door or trigger
        ///  - nDisarmable: true/false
        /// </summary>
        public static void SetTrapDisarmable(NWGameObject oTrapObject, bool nDisarmable = true)
        {
            Internal.NativeFunctions.StackPushInteger(nDisarmable ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(803);
        }

        /// <summary>
        ///  Sets whether or not the trapped object can be detected.
        ///  - oTrapObject: a placeable, door or trigger
        ///  - nDetectable: true/false
        ///  Note: Setting a trapped object to not be detectable will
        ///  not make the trap disappear if it has already been detected.
        /// </summary>
        public static void SetTrapDetectable(NWGameObject oTrapObject, bool nDetectable = true)
        {
            Internal.NativeFunctions.StackPushInteger(nDetectable ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(804);
        }

        /// <summary>
        ///  Sets whether or not the trap is a one-shot trap
        ///  (i.e. whether or not the trap resets itself after firing).
        ///  - oTrapObject: a placeable, door or trigger
        ///  - nOneShot: true/false
        /// </summary>
        public static void SetTrapOneShot(NWGameObject oTrapObject, bool nOneShot = true)
        {
            Internal.NativeFunctions.StackPushInteger(nOneShot ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(805);
        }

        /// <summary>
        ///  Set the tag of the key that will disarm oTrapObject.
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static void SetTrapKeyTag(NWGameObject oTrapObject, string sKeyTag)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sKeyTag);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(806);
        }

        /// <summary>
        ///  Set the DC for disarming oTrapObject.
        ///  - oTrapObject: a placeable, door or trigger
        ///  - nDisarmDC: must be between 0 and 250.
        /// </summary>
        public static void SetTrapDisarmDC(NWGameObject oTrapObject, int nDisarmDC)
        {
            Internal.NativeFunctions.StackPushInteger(nDisarmDC);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(807);
        }

        /// <summary>
        ///  Set the DC for detecting oTrapObject.
        ///  - oTrapObject: a placeable, door or trigger
        ///  - nDetectDC: must be between 0 and 250.
        /// </summary>
        public static void SetTrapDetectDC(NWGameObject oTrapObject, int nDetectDC)
        {
            Internal.NativeFunctions.StackPushInteger(nDetectDC);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(808);
        }

        /// <summary>
        ///  Creates a square Trap object.
        ///  - nTrapType: The base type of trap (TRAP_BASE_TYPE_*)
        ///  - lLocation: The location and orientation that the trap will be created at.
        ///  - fSize: The size of the trap. Minimum size allowed is 1.0f.
        ///  - sTag: The tag of the trap being created.
        ///  - nFaction: The faction of the trap (STANDARD_FACTION_*).
        ///  - sOnDisarmScript: The OnDisarm script that will fire when the trap is disarmed.
        ///                     If "" no script will fire.
        ///  - sOnTrapTriggeredScript: The OnTrapTriggered script that will fire when the
        ///                            trap is triggered.
        ///                            If "" the default OnTrapTriggered script for the trap
        ///                            type specified will fire instead (as specified in the
        ///                            traps.2da).
        /// </summary>
        public static NWGameObject CreateTrapAtLocation(TrapBaseType nTrapType, NWN.Location lLocation, float fSize = 2.0f, string sTag = "", StandardFaction nFaction = StandardFaction.Hostile, string sOnDisarmScript = "", string sOnTrapTriggeredScript = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sOnTrapTriggeredScript);
            Internal.NativeFunctions.StackPushStringUTF8(sOnDisarmScript);
            Internal.NativeFunctions.StackPushInteger((int)nFaction);
            Internal.NativeFunctions.StackPushStringUTF8(sTag);
            Internal.NativeFunctions.StackPushFloat(fSize);
            Internal.NativeFunctions.StackPushLocation(lLocation.Handle);
            Internal.NativeFunctions.StackPushInteger((int)nTrapType);
            Internal.NativeFunctions.CallBuiltIn(809);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Creates a Trap on the object specified.
        ///  - nTrapType: The base type of trap (TRAP_BASE_TYPE_*)
        ///  - oObject: The object that the trap will be created on. Works only on Doors and Placeables.
        ///  - nFaction: The faction of the trap (STANDARD_FACTION_*).
        ///  - sOnDisarmScript: The OnDisarm script that will fire when the trap is disarmed.
        ///                     If "" no script will fire.
        ///  - sOnTrapTriggeredScript: The OnTrapTriggered script that will fire when the
        ///                            trap is triggered.
        ///                            If "" the default OnTrapTriggered script for the trap
        ///                            type specified will fire instead (as specified in the
        ///                            traps.2da).
        ///  Note: After creating a trap on an object, you can change the trap's properties
        ///        using the various SetTrap* scripting commands by passing in the object
        ///        that the trap was created on (i.e. oObject) to any subsequent SetTrap* commands.
        /// </summary>
        public static void CreateTrapOnObject(int nTrapType, NWGameObject oObject, StandardFaction nFaction = StandardFaction.Hostile, string sOnDisarmScript = "", string sOnTrapTriggeredScript = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sOnTrapTriggeredScript);
            Internal.NativeFunctions.StackPushStringUTF8(sOnDisarmScript);
            Internal.NativeFunctions.StackPushInteger((int)nFaction);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger(nTrapType);
            Internal.NativeFunctions.CallBuiltIn(810);
        }

        /// <summary>
        ///  Set the Will saving throw value of the Door or Placeable object oObject.
        ///  - oObject: a door or placeable object.
        ///  - nWillSave: must be between 0 and 250.
        /// </summary>
        public static void SetWillSavingThrow(NWGameObject oObject, int nWillSave)
        {
            Internal.NativeFunctions.StackPushInteger(nWillSave);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(811);
        }

        /// <summary>
        ///  Set the Reflex saving throw value of the Door or Placeable object oObject.
        ///  - oObject: a door or placeable object.
        ///  - nReflexSave: must be between 0 and 250.
        /// </summary>
        public static void SetReflexSavingThrow(NWGameObject oObject, int nReflexSave)
        {
            Internal.NativeFunctions.StackPushInteger(nReflexSave);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(812);
        }

        /// <summary>
        ///  Set the Fortitude saving throw value of the Door or Placeable object oObject.
        ///  - oObject: a door or placeable object.
        ///  - nFortitudeSave: must be between 0 and 250.
        /// </summary>
        public static void SetFortitudeSavingThrow(NWGameObject oObject, int nFortitudeSave)
        {
            Internal.NativeFunctions.StackPushInteger(nFortitudeSave);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(813);
        }

        /// <summary>
        ///  returns the resref (TILESET_RESREF_*) of the tileset used to create area oArea.
        ///       TILESET_RESREF_BEHOLDER_CAVES
        ///       TILESET_RESREF_CASTLE_INTERIOR
        ///       TILESET_RESREF_CITY_EXTERIOR
        ///       TILESET_RESREF_CITY_INTERIOR
        ///       TILESET_RESREF_CRYPT
        ///       TILESET_RESREF_DESERT
        ///       TILESET_RESREF_DROW_INTERIOR
        ///       TILESET_RESREF_DUNGEON
        ///       TILESET_RESREF_FOREST
        ///       TILESET_RESREF_FROZEN_WASTES
        ///       TILESET_RESREF_ILLITHID_INTERIOR
        ///       TILESET_RESREF_MICROSET
        ///       TILESET_RESREF_MINES_AND_CAVERNS
        ///       TILESET_RESREF_RUINS
        ///       TILESET_RESREF_RURAL
        ///       TILESET_RESREF_RURAL_WINTER
        ///       TILESET_RESREF_SEWERS
        ///       TILESET_RESREF_UNDERDARK
        ///  * returns an empty string on an error.
        /// </summary>
        public static string GetTilesetResRef(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(814);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger
        ///  * Returns true if oTrapObject can be recovered.
        /// </summary>
        public static bool GetTrapRecoverable(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(815);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Sets whether or not the trapped object can be recovered.
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static void SetTrapRecoverable(NWGameObject oTrapObject, bool nRecoverable = true)
        {
            Internal.NativeFunctions.StackPushInteger(nRecoverable ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(816);
        }

        /// <summary>
        ///  Get the XP scale being used for the module.
        /// </summary>
        public static int GetModuleXPScale()
        {
            Internal.NativeFunctions.CallBuiltIn(817);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the XP scale used by the module.
        ///  - nXPScale: The XP scale to be used. Must be between 0 and 200.
        /// </summary>
        public static void SetModuleXPScale(int nXPScale)
        {
            Internal.NativeFunctions.StackPushInteger(nXPScale);
            Internal.NativeFunctions.CallBuiltIn(818);
        }

        /// <summary>
        ///  Get the feedback message that will be displayed when trying to unlock the object oObject.
        ///  - oObject: a door or placeable.
        ///  Returns an empty string "" on an error or if the game's default feedback message is being used
        /// </summary>
        public static string GetKeyRequiredFeedback(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(819);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Set the feedback message that is displayed when trying to unlock the object oObject.
        ///  This will only have an effect if the object is set to
        ///  "Key required to unlock or lock" either in the toolset
        ///  or by using the scripting command SetLockKeyRequired().
        ///  - oObject: a door or placeable.
        ///  - sFeedbackMessage: the string to be displayed in the player's text window.
        ///                      to use the game's default message, set sFeedbackMessage to ""
        /// </summary>
        public static void SetKeyRequiredFeedback(NWGameObject oObject, string sFeedbackMessage)
        {
            Internal.NativeFunctions.StackPushString(sFeedbackMessage);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(820);
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger
        ///  * Returns true if oTrapObject is active
        /// </summary>
        public static bool GetTrapActive(NWGameObject oTrapObject)
        {
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(821);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Sets whether or not the trap is an active trap
        ///  - oTrapObject: a placeable, door or trigger
        ///  - nActive: true/false
        ///  Notes:
        ///  Setting a trap as inactive will not make the
        ///  trap disappear if it has already been detected.
        ///  Call SetTrapDetectedBy() to make a detected trap disappear.
        ///  To make an inactive trap not detectable call SetTrapDetectable()
        /// </summary>
        public static void SetTrapActive(NWGameObject oTrapObject, bool nActive = true)
        {
            Internal.NativeFunctions.StackPushInteger(nActive ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oTrapObject != null ? oTrapObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(822);
        }

        /// <summary>
        ///  Locks the player's camera pitch to its current pitch setting,
        ///  or unlocks the player's camera pitch.
        ///  Stops the player from tilting their camera angle. 
        ///  - oPlayer: A player object.
        ///  - bLocked: true/false.
        /// </summary>
        public static void LockCameraPitch(NWGameObject oPlayer, bool bLocked = true)
        {
            Internal.NativeFunctions.StackPushInteger(bLocked ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(823);
        }

        /// <summary>
        ///  Locks the player's camera distance to its current distance setting,
        ///  or unlocks the player's camera distance.
        ///  Stops the player from being able to zoom in/out the camera.
        ///  - oPlayer: A player object.
        ///  - bLocked: true/false.
        /// </summary>
        public static void LockCameraDistance(NWGameObject oPlayer, bool bLocked = true)
        {
            Internal.NativeFunctions.StackPushInteger(bLocked ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(824);
        }

        /// <summary>
        ///  Locks the player's camera direction to its current direction,
        ///  or unlocks the player's camera direction to enable it to move
        ///  freely again.
        ///  Stops the player from being able to rotate the camera direction.
        ///  - oPlayer: A player object.
        ///  - bLocked: true/false.
        /// </summary>
        public static void LockCameraDirection(NWGameObject oPlayer, bool bLocked = true)
        {
            Internal.NativeFunctions.StackPushInteger(bLocked ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(825);
        }

        /// <summary>
        ///  Get the last object that default clicked (left clicked) on the placeable object
        ///  that is calling this function.
        ///  Should only be called from a placeables OnClick event.
        ///  * Returns OBJECT_INVALID if it is called by something other than a placeable.
        /// </summary>
        public static NWGameObject GetPlaceableLastClickedBy()
        {
            Internal.NativeFunctions.CallBuiltIn(826);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  returns true if the item is flagged as infinite.
        ///  - oItem: an item.
        ///  The infinite property affects the buying/selling behavior of the item in a store.
        ///  An infinite item will still be available to purchase from a store after a player
        ///  buys the item (non-infinite items will disappear from the store when purchased).
        /// </summary>
        public static bool GetInfiniteFlag(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(827);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Sets the Infinite flag on an item
        ///  - oItem: the item to change
        ///  - bInfinite: true or false, whether the item should be Infinite
        ///  The infinite property affects the buying/selling behavior of the item in a store.
        ///  An infinite item will still be available to purchase from a store after a player
        ///  buys the item (non-infinite items will disappear from the store when purchased).
        /// </summary>
        public static void SetInfiniteFlag(NWGameObject oItem, bool bInfinite = true)
        {
            Internal.NativeFunctions.StackPushInteger(bInfinite ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(828);
        }

        /// <summary>
        ///  Gets the size of the area.
        ///  - nAreaDimension: The area dimension that you wish to determine.
        ///       AREA_HEIGHT
        ///       AREA_WIDTH
        ///  - oArea: The area that you wish to get the size of.
        ///  Returns: The number of tiles that the area is wide/high, or zero on an error.
        ///  If no valid area (or object) is specified, it uses the area of the caller.
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static int GetAreaSize(AreaProperty nAreaDimension, NWGameObject oArea = null)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushInteger((int)nAreaDimension);
            Internal.NativeFunctions.CallBuiltIn(829);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the name of oObject.
        ///  - oObject: the object for which you are changing the name (a creature, placeable, item, or door).
        ///  - sNewName: the new name that the object will use.
        ///  Note: SetName() does not work on player objects.
        ///        Setting an object's name to "" will make the object
        ///        revert to using the name it had originally before any
        ///        SetName() calls were made on the object.
        /// </summary>
        public static void SetName(NWGameObject oObject, string sNewName = "")
        {
            Internal.NativeFunctions.StackPushString(sNewName);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(830);
        }

        /// <summary>
        ///  Get the PortraitId of oTarget.
        ///  - oTarget: the object for which you are getting the portrait Id.
        ///  Returns: The Portrait Id number being used for the object oTarget.
        ///           The Portrait Id refers to the row number of the Portraits.2da
        ///           that this portrait is from.
        ///           If a custom portrait is being used, oTarget is a player object,
        ///           or on an error returns PORTRAIT_INVALID. In these instances
        ///           try using GetPortraitResRef() instead.
        /// </summary>
        public static int GetPortraitId(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(831);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Change the portrait of oTarget to use the Portrait Id specified.
        ///  - oTarget: the object for which you are changing the portrait.
        ///  - nPortraitId: The Id of the new portrait to use. 
        ///                 nPortraitId refers to a row in the Portraits.2da
        ///  Note: Not all portrait Ids are suitable for use with all object types.
        ///        Setting the portrait Id will also cause the portrait ResRef
        ///        to be set to the appropriate portrait ResRef for the Id specified.
        /// </summary>
        public static void SetPortraitId(NWGameObject oTarget, int nPortraitId)
        {
            Internal.NativeFunctions.StackPushInteger(nPortraitId);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(832);
        }

        /// <summary>
        ///  Get the Portrait ResRef of oTarget.
        ///  - oTarget: the object for which you are getting the portrait ResRef.
        ///  Returns: The Portrait ResRef being used for the object oTarget.
        ///           The Portrait ResRef will not include a trailing size letter.
        /// </summary>
        public static string GetPortraitResRef(NWGameObject oTarget = null)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(833);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Change the portrait of oTarget to use the Portrait ResRef specified.
        ///  - oTarget: the object for which you are changing the portrait.
        ///  - sPortraitResRef: The ResRef of the new portrait to use. 
        ///                     The ResRef should not include any trailing size letter ( e.g. po_el_f_09_ ).
        ///  Note: Not all portrait ResRefs are suitable for use with all object types.
        ///        Setting the portrait ResRef will also cause the portrait Id
        ///        to be set to PORTRAIT_INVALID.
        /// </summary>
        public static void SetPortraitResRef(NWGameObject oTarget, string sPortraitResRef)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sPortraitResRef);
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(834);
        }

        /// <summary>
        ///  Set oPlaceable's useable object status.
        ///  Note: Only works on non-static placeables.
        /// </summary>
        public static void SetUseableFlag(NWGameObject oPlaceable, bool nUseableFlag)
        {
            Internal.NativeFunctions.StackPushInteger(nUseableFlag ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oPlaceable != null ? oPlaceable.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(835);
        }

        /// <summary>
        ///  Get the description of oObject.
        ///  - oObject: the object from which you are obtaining the description. 
        ///             Can be a creature, item, placeable, door, trigger or module object.
        ///  - bOriginalDescription:  if set to true any new description specified via a SetDescription scripting command
        ///                    is ignored and the original object's description is returned instead.
        ///  - bIdentified: If oObject is an item, setting this to true will return the identified description,
        ///                 setting this to false will return the unidentified description. This flag has no
        ///                 effect on objects other than items.
        /// </summary>
        public static string GetDescription(NWGameObject oObject, bool bOriginalDescription = false, bool bIdentifiedDescription = true)
        {
            Internal.NativeFunctions.StackPushInteger(bIdentifiedDescription ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(bOriginalDescription ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(836);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Set the description of oObject.
        ///  - oObject: the object for which you are changing the description 
        ///             Can be a creature, placeable, item, door, or trigger.
        ///  - sNewDescription: the new description that the object will use.
        ///  - bIdentified: If oObject is an item, setting this to true will set the identified description,
        ///                 setting this to false will set the unidentified description. This flag has no
        ///                 effect on objects other than items.
        ///  Note: Setting an object's description to "" will make the object
        ///        revert to using the description it originally had before any
        ///        SetDescription() calls were made on the object.
        /// </summary>
        public static void SetDescription(NWGameObject oObject, string sNewDescription = "", bool bIdentifiedDescription = true)
        {
            Internal.NativeFunctions.StackPushInteger(bIdentifiedDescription ? 1 : 0);
            Internal.NativeFunctions.StackPushString(sNewDescription);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(837);
        }

        /// <summary>
        ///  Get the PC that sent the last player chat(text) message.
        ///  Should only be called from a module's OnPlayerChat event script.
        ///  * Returns OBJECT_INVALID on error.
        ///  Note: Private tells do not trigger a OnPlayerChat event.
        /// </summary>
        public static NWGameObject GetPCChatSpeaker()
        {
            Internal.NativeFunctions.CallBuiltIn(838);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Get the last player chat(text) message that was sent.
        ///  Should only be called from a module's OnPlayerChat event script.
        ///  * Returns empty string "" on error.
        ///  Note: Private tells do not trigger a OnPlayerChat event.
        /// </summary>
        public static string GetPCChatMessage()
        {
            Internal.NativeFunctions.CallBuiltIn(839);
            return Internal.NativeFunctions.StackPopString();
        }

        /// <summary>
        ///  Get the volume of the last player chat(text) message that was sent.
        ///  Returns one of the following TALKVOLUME_* constants based on the volume setting
        ///  that the player used to send the chat message.
        ///                 TALKVOLUME_TALK
        ///                 TALKVOLUME_WHISPER
        ///                 TALKVOLUME_SHOUT
        ///                 TALKVOLUME_SILENT_SHOUT (used for DM chat channel)
        ///                 TALKVOLUME_PARTY
        ///  Should only be called from a module's OnPlayerChat event script.
        ///  * Returns -1 on error.
        ///  Note: Private tells do not trigger a OnPlayerChat event.
        /// </summary>
        public static int GetPCChatVolume()
        {
            Internal.NativeFunctions.CallBuiltIn(840);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the last player chat(text) message before it gets sent to other players.
        ///  - sNewChatMessage: The new chat text to be sent onto other players.
        ///                     Setting the player chat message to an empty string "",
        ///                     will cause the chat message to be discarded 
        ///                     (i.e. it will not be sent to other players).
        ///  Note: The new chat message gets sent after the OnPlayerChat script exits.
        /// </summary>
        public static void SetPCChatMessage(string sNewChatMessage = "")
        {
            Internal.NativeFunctions.StackPushString(sNewChatMessage);
            Internal.NativeFunctions.CallBuiltIn(841);
        }

        /// <summary>
        ///  Set the last player chat(text) volume before it gets sent to other players.
        ///  - nTalkVolume: The new volume of the chat text to be sent onto other players.
        ///                 TALKVOLUME_TALK
        ///                 TALKVOLUME_WHISPER
        ///                 TALKVOLUME_SHOUT
        ///                 TALKVOLUME_SILENT_SHOUT (used for DM chat channel)
        ///                 TALKVOLUME_PARTY
        ///                 TALKVOLUME_TELL (sends the chat message privately back to the original speaker)
        ///  Note: The new chat message gets sent after the OnPlayerChat script exits.
        /// </summary>
        public static void SetPCChatVolume(TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTalkVolume);
            Internal.NativeFunctions.CallBuiltIn(842);
        }

        /// <summary>
        ///  Get the Color of oObject from the color channel specified.
        ///  - oObject: the object from which you are obtaining the color. 
        ///             Can be a creature that has color information (i.e. the playable races).
        ///  - nColorChannel: The color channel that you want to get the color value of.
        ///                    COLOR_CHANNEL_SKIN
        ///                    COLOR_CHANNEL_HAIR
        ///                    COLOR_CHANNEL_TATTOO_1
        ///                    COLOR_CHANNEL_TATTOO_2
        ///  * Returns -1 on error.
        /// </summary>
        public static int GetColor(NWGameObject oObject, ColorChannel nColorChannel)
        {
            Internal.NativeFunctions.StackPushInteger((int)nColorChannel);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(843);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Set the color channel of oObject to the color specified.
        ///  - oObject: the object for which you are changing the color.
        ///             Can be a creature that has color information (i.e. the playable races).
        ///  - nColorChannel: The color channel that you want to set the color value of.
        ///                    COLOR_CHANNEL_SKIN
        ///                    COLOR_CHANNEL_HAIR
        ///                    COLOR_CHANNEL_TATTOO_1
        ///                    COLOR_CHANNEL_TATTOO_2
        ///  - nColorValue: The color you want to set (0-175).
        /// </summary>
        public static void SetColor(NWGameObject oObject, ColorChannel nColorChannel, int nColorValue)
        {
            Internal.NativeFunctions.StackPushInteger(nColorValue);
            Internal.NativeFunctions.StackPushInteger((int)nColorChannel);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(844);
        }

        /// <summary>
        ///  Returns Item property Material.  You need to specify the Material Type.
        ///  - nMasterialType: The Material Type should be a positive integer between 0 and 77 (see iprp_matcost.2da).
        ///  Note: The Material Type property will only affect the cost of the item if you modify the cost in the iprp_matcost.2da.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyMaterial(int nMaterialType)
        {
            Internal.NativeFunctions.StackPushInteger(nMaterialType);
            Internal.NativeFunctions.CallBuiltIn(845);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns Item property Quality. You need to specify the Quality.
        ///  - nQuality:  The Quality of the item property to create (see iprp_qualcost.2da).
        ///               IP_CONST_QUALITY_*
        ///  Note: The quality property will only affect the cost of the item if you modify the cost in the iprp_qualcost.2da.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyQuality(IPConst nQuality)
        {
            Internal.NativeFunctions.StackPushInteger((int)nQuality);
            Internal.NativeFunctions.CallBuiltIn(846);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns a generic Additional Item property. You need to specify the Additional property.
        ///  - nProperty: The item property to create (see iprp_addcost.2da).
        ///               IP_CONST_ADDITIONAL_*
        ///  Note: The additional property only affects the cost of the item if you modify the cost in the iprp_addcost.2da.
        /// </summary>
        public static NWN.ItemProperty ItemPropertyAdditional(IPConst nAdditionalProperty)
        {
            Internal.NativeFunctions.StackPushInteger((int)nAdditionalProperty);
            Internal.NativeFunctions.CallBuiltIn(847);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Sets a new tag for oObject.
        ///  Will do nothing for invalid objects or the module object.
        /// 
        ///  Note: Care needs to be taken with this function.
        ///        Changing the tag for creature with waypoints will make them stop walking them.
        ///        Changing waypoint, door or trigger tags will break their area transitions.
        /// </summary>
        public static void SetTag(NWGameObject oObject, string sNewTag)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sNewTag);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(848);
        }

        /// <summary>
        ///  Returns the string tag set for the provided effect.
        ///  - If no tag has been set, returns an empty string.
        /// </summary>
        public static string GetEffectTag(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(849);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Tags the effect with the provided string.
        ///  - Any other tags in the link will be overwritten.
        /// </summary>
        public static Effect TagEffect(NWN.Effect eEffect, string sNewTag)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sNewTag);
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(850);
            return new NWN.Effect(Internal.NativeFunctions.StackPopEffect());
        }

        /// <summary>
        ///  Returns the caster level of the creature who created the effect.
        ///  - If not created by a creature, returns 0.
        ///  - If created by a spell-like ability, returns 0.
        /// </summary>
        public static int GetEffectCasterLevel(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(851);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the total duration of the effect in seconds.
        ///  - Returns 0 if the duration type of the effect is not DurationType.Temporary.
        /// </summary>
        public static int GetEffectDuration(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(852);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the remaining duration of the effect in seconds.
        ///  - Returns 0 if the duration type of the effect is not DurationType.Temporary.
        /// </summary>
        public static int GetEffectDurationRemaining(NWN.Effect eEffect)
        {
            Internal.NativeFunctions.StackPushEffect(eEffect.Handle);
            Internal.NativeFunctions.CallBuiltIn(853);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the string tag set for the provided item property.
        ///  - If no tag has been set, returns an empty string.
        /// </summary>
        public static string GetItemPropertyTag(NWN.ItemProperty nProperty)
        {
            Internal.NativeFunctions.StackPushItemProperty(nProperty.Handle);
            Internal.NativeFunctions.CallBuiltIn(854);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Tags the item property with the provided string.
        ///  - Any tags currently set on the item property will be overwritten.
        /// </summary>
        public static NWN.ItemProperty TagItemProperty(NWN.ItemProperty nProperty, string sNewTag)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sNewTag);
            Internal.NativeFunctions.StackPushItemProperty(nProperty.Handle);
            Internal.NativeFunctions.CallBuiltIn(855);
            return new NWN.ItemProperty(Internal.NativeFunctions.StackPopItemProperty());
        }

        /// <summary>
        ///  Returns the total duration of the item property in seconds.
        ///  - Returns 0 if the duration type of the item property is not DurationType.Temporary.
        /// </summary>
        public static int GetItemPropertyDuration(NWN.ItemProperty nProperty)
        {
            Internal.NativeFunctions.StackPushItemProperty(nProperty.Handle);
            Internal.NativeFunctions.CallBuiltIn(856);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the remaining duration of the item property in seconds.
        ///  - Returns 0 if the duration type of the item property is not DurationType.Temporary.
        /// </summary>
        public static int GetItemPropertyDurationRemaining(NWN.ItemProperty nProperty)
        {
            Internal.NativeFunctions.StackPushItemProperty(nProperty.Handle);
            Internal.NativeFunctions.CallBuiltIn(857);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Instances a new area from the given resref, which needs to be a existing module area.
        ///  Will optionally set a new area tag and displayed name. The new area is accessible
        ///  immediately, but initialisation scripts for the area and all contained creatures will only
        ///  run after the current script finishes (so you can clean up objects before returning).
        /// 
        ///  Returns the new area, or OBJECT_INVALID on failure.
        /// 
        ///  Note: When spawning a second instance of a existing area, you will have to manually
        ///        adjust all transitions (doors, triggers) with the relevant script commands,
        ///        or players might end up in the wrong area.
        /// </summary>
        public static NWGameObject CreateArea(string sResRef, string sNewTag = "", string sNewName = "")
        {
            Internal.NativeFunctions.StackPushString(sNewName);
            Internal.NativeFunctions.StackPushStringUTF8(sNewTag);
            Internal.NativeFunctions.StackPushStringUTF8(sResRef);
            Internal.NativeFunctions.CallBuiltIn(858);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Destroys the given area object and everything in it.
        /// 
        ///  Return values:
        ///     0: Object not an area or invalid.
        ///    -1: Area contains spawn location and removal would leave module without entrypoint.
        ///    -2: Players in area.
        ///     1: Area destroyed successfully.
        /// </summary>
        public static int DestroyArea(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(859);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Creates a copy of a existing area, including everything inside of it (except players).
        /// 
        ///  Returns the new area, or OBJECT_INVALID on error.
        /// 
        ///  Note: You will have to manually adjust all transitions (doors, triggers) with the
        ///        relevant script commands, or players might end up in the wrong area.
        /// </summary>
        public static NWGameObject CopyArea(NWGameObject oArea)
        {
            Internal.NativeFunctions.StackPushObject(oArea != null ? oArea.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(860);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Returns the first area in the module.
        /// </summary>
        public static NWGameObject GetFirstArea()
        {
            Internal.NativeFunctions.CallBuiltIn(861);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Returns the next area in the module (after GetFirstArea), or OBJECT_INVALID if no more
        ///  areas are loaded.
        /// </summary>
        public static NWGameObject GetNextArea()
        {
            Internal.NativeFunctions.CallBuiltIn(862);
            return Internal.NativeFunctions.StackPopObject();
        }

        /// <summary>
        ///  Sets the transition target for oTransition.
        /// 
        ///  Notes:
        ///  - oTransition can be any valid game object, except areas.
        ///  - oTarget can be any valid game object with a location, or OBJECT_INVALID (to unlink).
        ///  - Rebinding a transition will NOT change the other end of the transition; for example,
        ///    with normal doors you will have to do either end separately.
        ///  - Any valid game object can hold a transition target, but only some are used by the game engine
        ///    (doors and triggers). This might change in the future. You can still set and query them for
        ///    other game objects from nwscript.
        ///  - Transition target objects are cached: The toolset-configured destination tag is
        ///    used for a lookup only once, at first use. Thus, attempting to use SetTag() to change the
        ///    destination for a transition will not work in a predictable fashion.
        /// </summary>
        public static void SetTransitionTarget(NWGameObject oTransition, NWGameObject oTarget)
        {
            Internal.NativeFunctions.StackPushObject(oTarget != null ? oTarget.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(oTransition != null ? oTransition.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(863);
        }

        /// <summary>
        ///  Sets whether the provided item should be hidden when equipped.
        ///  - The intended usage of this function is to provide an easy way to hide helmets, but it
        ///    can be used equally for any slot which has creature mesh visibility when equipped,
        ///    e.g.: armour, helm, cloak, left hand, and right hand.
        ///  - nValue should be true or false.
        /// </summary>
        public static void SetHiddenWhenEquipped(NWGameObject oItem, bool nValue)
        {
            Internal.NativeFunctions.StackPushInteger(nValue ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(864);
        }

        /// <summary>
        ///  Returns whether the provided item is hidden when equipped.
        /// </summary>
        public static bool GetHiddenWhenEquipped(NWGameObject oItem)
        {
            Internal.NativeFunctions.StackPushObject(oItem != null ? oItem.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(865);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Sets if the given creature has explored tile at x, y of the given area.
        ///  Note that creature needs to be a player- or player-possessed creature.
        /// 
        ///  Keep in mind that tile exploration also controls object visibility in areas
        ///  and the fog of war for interior and underground areas.
        /// 
        ///  Return values:
        ///   -1: Area or creature invalid.
        ///    0: Tile was not explored before setting newState.
        ///    1: Tile was explored before setting newState.
        /// </summary>
        public static int SetTileExplored(NWGameObject creature, NWGameObject area, int x, int y, bool newState)
        {
            Internal.NativeFunctions.StackPushInteger(newState ? 1 : 0);
            Internal.NativeFunctions.StackPushInteger(y);
            Internal.NativeFunctions.StackPushInteger(x);
            Internal.NativeFunctions.StackPushObject(area != null ? area.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(creature != null ? creature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(866);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns whether the given tile at x, y, for the given creature in the stated
        ///  area is visible on the map.
        ///  Note that creature needs to be a player- or player-possessed creature.
        /// 
        ///  Keep in mind that tile exploration also controls object visibility in areas
        ///  and the fog of war for interior and underground areas.
        /// 
        ///  Return values:
        ///   -1: Area or creature invalid.
        ///    0: Tile is not explored yet.
        ///    1: Tile is explored.
        /// </summary>
        public static int GetTileExplored(NWGameObject creature, NWGameObject area, int x, int y)
        {
            Internal.NativeFunctions.StackPushInteger(y);
            Internal.NativeFunctions.StackPushInteger(x);
            Internal.NativeFunctions.StackPushObject(area != null ? area.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushObject(creature != null ? creature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(867);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the creature to auto-explore the map as it walks around.
        /// 
        ///  Keep in mind that tile exploration also controls object visibility in areas
        ///  and the fog of war for interior and underground areas.
        /// 
        ///  This means that if you turn off auto exploration, it falls to you to manage this
        ///  through SetTileExplored(); otherwise, the player will not be able to see anything.
        /// 
        ///  Valid arguments: true and false.
        ///  Does nothing for non-creatures.
        ///  Returns the previous state (or -1 if non-creature).
        /// </summary>
        public static int SetCreatureExploresMinimap(NWGameObject creature, bool newState)
        {
            Internal.NativeFunctions.StackPushInteger(newState ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(creature != null ? creature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(868);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns true if the creature is set to auto-explore the map as it walks around (on by default).
        ///  Returns false if creature is not actually a creature.
        /// </summary>
        public static bool GetCreatureExploresMinimap(NWGameObject creature)
        {
            Internal.NativeFunctions.StackPushObject(creature != null ? creature.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(869);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Get the surface material at the given location. (This is
        ///  equivalent to the walkmesh type).
        ///  Returns 0 if the location is invalid or has no surface type.
        /// </summary>
        public static int GetSurfaceMaterial(NWN.Location at)
        {
            Internal.NativeFunctions.StackPushLocation(at.Handle);
            Internal.NativeFunctions.CallBuiltIn(870);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Returns the z-offset at which the walkmesh is at the given location.
        ///  Returns -6.0 for invalid locations.
        /// </summary>
        public static float GetGroundHeight(NWN.Location at)
        {
            Internal.NativeFunctions.StackPushLocation(at.Handle);
            Internal.NativeFunctions.CallBuiltIn(871);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        ///  Gets the attack bonus limit.
        ///  - The default value is 20.
        /// </summary>
        public static int GetAttackBonusLimit()
        {
            Internal.NativeFunctions.CallBuiltIn(872);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the damage bonus limit.
        ///  - The default value is 100.
        /// </summary>
        public static int GetDamageBonusLimit()
        {
            Internal.NativeFunctions.CallBuiltIn(873);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the saving throw bonus limit.
        ///  - The default value is 20.
        /// </summary>
        public static int GetSavingThrowBonusLimit()
        {
            Internal.NativeFunctions.CallBuiltIn(874);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the ability bonus limit.
        ///  - The default value is 12.
        /// </summary>
        public static int GetAbilityBonusLimit()
        {
            Internal.NativeFunctions.CallBuiltIn(875);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the ability penalty limit.
        ///  - The default value is 30.
        /// </summary>
        public static int GetAbilityPenaltyLimit()
        {
            Internal.NativeFunctions.CallBuiltIn(876);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Gets the skill bonus limit.
        ///  - The default value is 50.
        /// </summary>
        public static int GetSkillBonusLimit()
        {
            Internal.NativeFunctions.CallBuiltIn(877);
            return Internal.NativeFunctions.StackPopInteger();
        }

        /// <summary>
        ///  Sets the attack bonus limit.
        ///  - The minimum value is 0.
        /// </summary>
        public static void SetAttackBonusLimit(int nNewLimit)
        {
            Internal.NativeFunctions.StackPushInteger(nNewLimit);
            Internal.NativeFunctions.CallBuiltIn(878);
        }

        /// <summary>
        ///  Sets the damage bonus limit.
        ///  - The minimum value is 0.
        /// </summary>
        public static void SetDamageBonusLimit(int nNewLimit)
        {
            Internal.NativeFunctions.StackPushInteger(nNewLimit);
            Internal.NativeFunctions.CallBuiltIn(879);
        }

        /// <summary>
        ///  Sets the saving throw bonus limit.
        ///  - The minimum value is 0.
        /// </summary>
        public static void SetSavingThrowBonusLimit(int nNewLimit)
        {
            Internal.NativeFunctions.StackPushInteger(nNewLimit);
            Internal.NativeFunctions.CallBuiltIn(880);
        }

        /// <summary>
        ///  Sets the ability bonus limit.
        ///  - The minimum value is 0.
        /// </summary>
        public static void SetAbilityBonusLimit(int nNewLimit)
        {
            Internal.NativeFunctions.StackPushInteger(nNewLimit);
            Internal.NativeFunctions.CallBuiltIn(881);
        }

        /// <summary>
        ///  Sets the ability penalty limit.
        ///  - The minimum value is 0.
        /// </summary>
        public static void SetAbilityPenaltyLimit(int nNewLimit)
        {
            Internal.NativeFunctions.StackPushInteger(nNewLimit);
            Internal.NativeFunctions.CallBuiltIn(882);
        }

        /// <summary>
        ///  Sets the skill bonus limit.
        ///  - The minimum value is 0.
        /// </summary>
        public static void SetSkillBonusLimit(int nNewLimit)
        {
            Internal.NativeFunctions.StackPushInteger(nNewLimit);
            Internal.NativeFunctions.CallBuiltIn(883);
        }

        /// <summary>
        ///  Get if oPlayer is currently connected over a relay (instead of directly).
        ///  Returns false for any other object, including OBJECT_INVALID.
        /// </summary>
        public static bool GetIsPlayerConnectionRelayed(NWGameObject oPlayer)
        {
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(884);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        ///  Returns the event script for the given object and handler.
        ///  Will return "" if unset, the object is invalid, or the object cannot
        ///  have the requested handler.
        /// </summary>
        private static string GetEventScript(NWGameObject oObject, int nHandler)
        {
            Internal.NativeFunctions.StackPushInteger(nHandler);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(885);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        ///  Sets the given event script for the given object and handler.
        ///  Returns 1 on success, 0 on failure.
        ///  Will fail if oObject is invalid or does not have the requested handler.
        /// </summary>
        private static bool SetEventScript(NWGameObject oObject, int nHandler, string sScript)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sScript);
            Internal.NativeFunctions.StackPushInteger(nHandler);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(886);
            return Internal.NativeFunctions.StackPopInteger() == 1;
        }

        /// <summary>
        /// Gets a visual transform on the given object.
        /// - oObject can be any valid Creature, Placeable, Item or Door.
        /// - nTransform is one of OBJECT_VISUAL_TRANSFORM_*
        /// Returns the current (or default) value.
        /// </summary>
        public static float GetObjectVisualTransform(NWGameObject oObject, ObjectVisualTransform nTransform)
        {
            Internal.NativeFunctions.StackPushInteger((int)nTransform);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(887);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        /// Sets a visual transform on the given object.
        /// - oObject can be any valid Creature, Placeable, Item or Door.
        /// - nTransform is one of OBJECT_VISUAL_TRANSFORM_*
        /// - fValue depends on the transformation to apply.
        /// Returns the old/previous value.
        /// </summary>
        public static float SetObjectVisualTransform(NWGameObject oObject, ObjectVisualTransform nTransform, float fValue)
        {
            Internal.NativeFunctions.StackPushFloat(fValue);
            Internal.NativeFunctions.StackPushInteger((int)nTransform);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(888);
            return Internal.NativeFunctions.StackPopFloat();
        }

        /// <summary>
        /// Sets an integer material shader uniform override.
        /// - sMaterial needs to be a material on that object.
        /// - sParam needs to be a valid shader parameter already defined on the material.
        /// </summary>
        public static void SetMaterialShaderUniformInt(NWGameObject oObject, string sMaterial, string sParam, int nValue)
        {
            Internal.NativeFunctions.StackPushInteger(nValue);
            Internal.NativeFunctions.StackPushStringUTF8(sParam);
            Internal.NativeFunctions.StackPushStringUTF8(sMaterial);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(889);
        }

        /// <summary>
        /// Sets a vec4 material shader uniform override.
        /// - sMaterial needs to be a material on that object.
        /// - sParam needs to be a valid shader parameter already defined on the material.
        /// - You can specify a single float value to set just a float, instead of a vec4.
        /// </summary>
        public static void SetMaterialShaderUniformVec4(NWGameObject oObject, string sMaterial, string sParam, float fValue1, float fValue2 = 0.0f, float fValue3 = 0.0f, float fValue4 = 0.0f)
        {
            Internal.NativeFunctions.StackPushFloat(fValue4);
            Internal.NativeFunctions.StackPushFloat(fValue3);
            Internal.NativeFunctions.StackPushFloat(fValue2);
            Internal.NativeFunctions.StackPushFloat(fValue1);
            Internal.NativeFunctions.StackPushStringUTF8(sParam);
            Internal.NativeFunctions.StackPushStringUTF8(sMaterial);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(890);
        }

        /// <summary>
        /// Resets material shader parameters on the given object:
        /// - Supply a material to only reset shader uniforms for meshes with that material.
        /// - Supply a parameter to only reset shader uniforms of that name.
        /// - Supply both to only reset shader uniforms of that name on meshes with that material.
        /// </summary>
        public static void ResetMaterialShaderUniforms(NWGameObject oObject, string sMaterial = "", string sParam = "")
        {
            Internal.NativeFunctions.StackPushStringUTF8(sParam);
            Internal.NativeFunctions.StackPushStringUTF8(sMaterial);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(891);
        }

        /// <summary>
        /// Vibrate the player's device or controller. Does nothing if vibration is not supported.
        /// - nMotor is one of VIBRATOR_MOTOR_*
        /// - fStrength is between 0.0 and 1.0
        /// - fSeconds is the number of seconds to vibrate
        /// </summary>
        public static void Vibrate(NWGameObject oPlayer, VibratorMotor nMotor, float fStrength, float fSeconds)
        {
            Internal.NativeFunctions.StackPushFloat(fSeconds);
            Internal.NativeFunctions.StackPushFloat(fStrength);
            Internal.NativeFunctions.StackPushInteger((int)nMotor);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(892);
        }

        /// <summary>
        /// Unlock an achievement for the given player who must be logged in.
        /// - sId is the achievement ID on the remote server
        /// - nLastValue is the previous value of the associated achievement stat
        /// - nCurValue is the current value of the associated achievement stat
        /// - nMaxValue is the maximum value of the associate achievement stat
        /// </summary>
        public static void UnlockAchievement(NWGameObject oPlayer, string sId, int nLastValue = 0, int nCurValue = 0, int nMaxValue = 0)
        {
            Internal.NativeFunctions.StackPushInteger(nMaxValue);
            Internal.NativeFunctions.StackPushInteger(nCurValue);
            Internal.NativeFunctions.StackPushInteger(nLastValue);
            Internal.NativeFunctions.StackPushStringUTF8(sId);
            Internal.NativeFunctions.StackPushObject(oPlayer != null ? oPlayer.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(893);
        }

        /// <summary>
        /// Execute a script chunk.
        /// The script chunk runs immediately, same as ExecuteScript().
        /// The script is jitted in place and currently not cached: Each invocation will recompile the script chunk.
        /// Note that the script chunk will run as if a separate script. This is not eval().
        /// By default, the script chunk is wrapped into void main() {}. Pass in bWrapIntoMain = false to override.
        /// Returns "" on success, or the compilation error.
        /// </summary>
        public static string ExecuteScriptChunk(string sScriptChunk, NWGameObject oObject, bool bWrapIntoMain = true)
        {
            Internal.NativeFunctions.StackPushInteger(bWrapIntoMain ? 1 : 0);
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.StackPushString(sScriptChunk);
            Internal.NativeFunctions.CallBuiltIn(894);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        /// Returns a UUID. This UUID will not be associated with any object.
        /// The generated UUID is currently a v4.
        /// </summary>
        public static string GetRandomUUID()
        {
            Internal.NativeFunctions.CallBuiltIn(895);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        /// Returns the given objects' UUID. This UUID is persisted across save boundaries,
        /// like Save/RestoreCampaignObject and save games.
        ///
        /// Thus, reidentification is only guaranteed in scenarios where players cannot introduce
        /// new objects (i.e. servervault servers).
        ///
        /// UUIDs are guaranteed to be unique in any single running game.
        ///
        /// If a loaded object would collide with a UUID already present in the game, the
        /// object receives no UUID and a warning is emitted to the log. Requesting a UUID
        /// for the new object will generate a random one.
        ///
        /// This UUID is useful to, for example:
        /// - Safely identify servervault characters
        /// - Track serialisable objects (like items or creatures) as they are saved to the
        ///   campaign DB - i.e. persistent storage chests or dropped items.
        /// - Track objects across multiple game instances (in trusted scenarios).
        ///
        /// Currently, the following objects can carry UUIDs:
        ///   Items, Creatures, Placeables, Triggers, Doors, Waypoints, Stores,
        ///   Encounters, Areas.
        ///
        /// Will return "" (empty string) when the given object cannot carry a UUID.
        /// </summary>
        public static string GetObjectUUID(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(896);
            return Internal.NativeFunctions.StackPopStringUTF8();
        }

        /// <summary>
        /// Forces the given object to receive a new UUID, discarding the current value.
        /// </summary>
        public static void ForceRefreshObjectUUID(NWGameObject oObject)
        {
            Internal.NativeFunctions.StackPushObject(oObject != null ? oObject.Self : NWGameObject.OBJECT_INVALID);
            Internal.NativeFunctions.CallBuiltIn(897);
        }

        /// <summary>
        /// Looks up a object on the server by it's UUID.
        /// Returns OBJECT_INVALID if the UUID is not on the server.
        /// </summary>
        public static NWGameObject GetObjectByUUID(string sUUID)
        {
            Internal.NativeFunctions.StackPushStringUTF8(sUUID);
            Internal.NativeFunctions.CallBuiltIn(898);
            return Internal.NativeFunctions.StackPopObject();
        }
    }
}
