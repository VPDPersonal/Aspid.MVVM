using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types
{
    /// <summary>
    /// Adds an Inspector dropdown that lets you swap the object's script
    /// to any subtype of the field's declaring class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When the user picks a type, the editor locates the corresponding <c>MonoScript</c>
    /// asset and writes it to <c>m_Script</c> on the <c>SerializedObject</c>, effectively
    /// changing the component or ScriptableObject to the chosen subtype.
    /// </para>
    /// <para>
    /// The picker is automatically constrained to subtypes of the class that declares
    /// the field — no extra configuration is needed.
    /// </para>
    /// </remarks>
    /// <example>
    /// Place a field of this type inside the root component class.
    /// The Inspector will render a dropdown listing all subtypes of <c>BaseEnemy</c>:
    /// <code>
    /// public abstract class BaseEnemy : MonoBehaviour
    /// {
    ///     [SerializeField] private ComponentTypeSelector _typeSelector;
    /// }
    ///
    /// public class FastEnemy : BaseEnemy { }
    /// public class TankEnemy : BaseEnemy { }
    /// </code>
    /// Selecting "TankEnemy" in the Inspector rewrites the object's <c>m_Script</c>
    /// so it becomes a <c>TankEnemy</c> instance.
    /// </example>
    [Serializable]
    public struct ComponentTypeSelector { }
}
