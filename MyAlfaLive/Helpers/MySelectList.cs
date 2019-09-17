namespace System.Web.Mvc
{
    using System.Collections;

    /// <summary>
    /// Custom SelectList with dataValueField = "Id" and dataTextField = "Text"
    /// </summary>
    public class MySelectList : SelectList
    {
        /// <summary>
        /// New SelectList with dataValueField = "Id" and dataTextField = "Text"
        /// </summary>
        public MySelectList(IEnumerable items):
            base (items, "Id", "Text")
        {
        }

        /// <summary>
        /// New SelectList with dataValueField = "Id" and dataTextField = "Text"
        /// </summary>
        public MySelectList(IEnumerable items, object selectedValue) :
            base(items, "Id", "Text", selectedValue)
        {
        }
    }
}