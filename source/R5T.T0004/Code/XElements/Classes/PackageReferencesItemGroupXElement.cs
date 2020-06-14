using System;
using System.Xml.Linq;


namespace R5T.T0004
{
    public class PackageReferencesItemGroupXElement : ItemGroupXElement
    {
        public ProjectXElement ProjectElement { get; set; }


        public PackageReferencesItemGroupXElement(XElement value)
            : base(value)
        {
        }
    }
}
