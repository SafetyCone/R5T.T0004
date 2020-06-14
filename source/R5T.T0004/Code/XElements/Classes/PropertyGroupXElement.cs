using System;
using System.Xml.Linq;

using R5T.T0005;


namespace R5T.T0004
{
    public class PropertyGroupXElement : TypedXElement
    {
        #region Static

        public static PropertyGroupXElement New(XElement xElement)
        {
            var propertyGroupXElement = new PropertyGroupXElement(xElement);
            return propertyGroupXElement;
        }

        #endregion


        public ProjectXElement ProjectElement { get; set; }


        public PropertyGroupXElement(XElement value)
            : base(value)
        {
        }
    }
}
