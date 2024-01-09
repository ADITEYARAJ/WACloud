

namespace Template.WhatsAppApi.Contract
{
    public enum ComponentTypes
    {
        /// <summary>
        ///  A required text-only components for all templates.
        /// </summary>
        Body = 0,
        
        /// <summary>
        /// Header supports - text, media (images, videos, documents) and locations.
        /// </summary>
        Header = 1,

        /// <summary>
        /// Optional text component.
        /// </summary>
        Footer = 2,

        /// <summary>
        /// Optional interactive components specific to actions.
        /// </summary>
        BUTTONS = 3

    }
}
