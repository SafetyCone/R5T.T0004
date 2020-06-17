using System;
using System.Xml.Linq;


namespace R5T.T0004
{
    public class PackageReferencesItemGroupXElement : ItemGroupXElement
    {
        #region Static

        public static PackageReferencesItemGroupXElement New(XElement value)
        {
            var packageReferencesItemGroupXElement = new PackageReferencesItemGroupXElement(value);
            return packageReferencesItemGroupXElement;
        }

        #endregion


        public PackageReferencesItemGroupXElement(XElement value)
            : base(value)
        {
        }
    }
}
