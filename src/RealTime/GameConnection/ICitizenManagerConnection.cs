// ICitizenManagerConnection.cs

namespace RealTime.GameConnection
{
    using UnityEngine;

    /// <summary>An interface for the game specific logic related to the citizen management.</summary>
    /// <typeparam name="TCitizen">The type of the citizen data structure.</typeparam>
    internal interface ICitizenManagerConnection<TCitizen>
        where TCitizen : struct
    {
        /// <summary>Releases the specified citizen.</summary>
        /// <param name="citizenId">The ID of the citizen to release.</param>
        void ReleaseCitizen(uint citizenId);

        /// <summary>Gets the ID of the building this citizen is currently moving to.</summary>
        /// <param name="instanceId">The citizen's instance ID.</param>
        /// <returns>The ID of the building the citizen is moving to, or 0 if none.</returns>
        ushort GetTargetBuilding(ushort instanceId);

        /// <summary>Gets the ID of the game node this citizen is currently moving to.</summary>
        /// <param name="instanceId">The citizen's instance ID.</param>
        /// <returns>The ID of the game node the citizen is moving to, or 0 if none.</returns>
        ushort GetTargetNode(ushort instanceId);

        /// <summary>Determines whether the citizen's instance with specified ID has particular flags.</summary>
        /// <param name="instanceId">The instance ID to check.</param>
        /// <param name="flags">The flags to check.</param>
        /// <param name="all">
        /// <c>true</c> to check all flags from the specified <paramref name="flags"/>, <c>false</c> to check any flags.
        /// </param>
        /// <returns><c>true</c> if the citizen instance has the specified flags; otherwise, <c>false</c>.</returns>
        bool InstanceHasFlags(ushort instanceId, CitizenInstance.Flags flags, bool all = false);

        /// <summary>Gets the current flags of the citizen's instance with specified ID.</summary>
        /// <param name="instanceId">The instance ID to check.</param>
        /// <param name="mask">The flags mask to apply for the check.</param>
        /// <returns>The citizen instance flags masked by the specified <paramref name="mask"/>.</returns>
        CitizenInstance.Flags GetInstanceFlags(ushort instanceId, CitizenInstance.Flags mask);

        /// <summary>
        /// Gets the current wait counter value of the citizen's instance with specified ID.
        /// </summary>
        /// <param name="instanceId">The instance ID to check.</param>
        /// <returns>The wait counter value of the citizen's instance.</returns>
        byte GetInstanceWaitCounter(ushort instanceId);

        /// <summary>
        /// Determines whether the area around the citizen's instance with specified ID is currently marked for evacuation.
        /// </summary>
        /// <param name="instanceId">The ID of the citizen's instance to check.</param>
        /// <returns><c>true</c> if the area around the citizen's instance is marked for evacuation; otherwise, <c>false</c>.</returns>
        bool IsAreaEvacuating(ushort instanceId);

        /// <summary>Modifies the goods storage in the specified unit.</summary>
        /// <param name="unitId">The unit ID to process.</param>
        /// <param name="amount">The amount to modify the storage by.</param>
        /// <returns><c>true</c> on success; otherwise, <c>false</c>.</returns>
        bool ModifyUnitGoods(uint unitId, ushort amount);

        /// <summary>Gets the count of the currently active citizens instances.</summary>
        /// <returns>The number of active citizens instances.</returns>
        uint GetInstancesCount();

        /// <summary>Gets the maximum count of the active citizens instances.</summary>
        /// <returns>The maximum number of active citizens instances.</returns>
        uint GetMaxInstancesCount();

        /// <summary>Gets the maximum count of the citizens.</summary>
        /// <returns>The maximum number of the citizens.</returns>
        uint GetMaxCitizensCount();

        /// <summary>Gets the location of the citizen with specified ID.</summary>
        /// <param name="citizenId">The ID of the citizen to query location of.</param>
        /// <returns>A <see cref="Citizen.Location"/> value that describes the citizen's current location.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the argument is 0.</exception>
        Citizen.Location GetCitizenLocation(uint citizenId);

        /// <summary>Gets the wealth of the citizen with specified ID.</summary>
        /// <param name="citizenId">The ID of the citizen to query wealth of.</param>
        /// <returns>A <see cref="Citizen.Wealth"/> value that describes the citizen's current wealth.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the argument is 0.</exception>
        Citizen.Wealth GetCitizenWealth(uint citizenId);

        /// <summary>Attempts to get IDs of the citizen's family members IDs.</summary>
        /// <param name="citizenId">The ID of the citizen to get family members for.</param>
        /// <param name="targetBuffer">An array of 4 elements to store the results in.</param>
        /// <returns><c>true</c> if the specified citizen has at least one family member; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="targetBuffer"/> is null.</exception>
        bool TryGetFamily(uint citizenId, uint[] targetBuffer);

        /// <summary>Gets the game's citizens array (direct reference).</summary>
        /// <returns>The reference to the game's array containing the <typeparamref name="TCitizen"/> items.</returns>
        TCitizen[] GetCitizensArray();

        /// <summary>Releases the citizen instance's path and cancels any ongoing movement.</summary>
        /// <param name="instanceId">The citizen's instance ID.</param>
        /// <param name="resetTarget"><c>true</c> to reset the current citizen's target.</param>
        void StopMoving(ushort instanceId, bool resetTarget);

        /// <summary>Provides a direct reference to the citizen data structure located in the game's buffer.</summary>
        /// <param name="instanceId">The ID of the citizen instance to get the citizen data structure for.</param>
        /// <returns>A direct reference to the <typeparamref name="TCitizen"/> data structure.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when <paramref name="instanceId"/> is 0.</exception>
        ref TCitizen GetCitizen(ushort instanceId);

        /// <summary>Gets the citizen instance's current position.</summary>
        /// <param name="instanceId">The ID of the citizen's instance to get the position of.</param>
        /// <returns>A <see cref="Vector3"/> that specifies the instance position.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when <paramref name="instanceId"/> is 0.</exception>
        Vector3 GetCitizenPosition(ushort instanceId);
    }
}
