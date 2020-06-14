using System;
using System.Xml.Linq;


namespace R5T.T0004
{
    public class ProjectReferencesItemGroupXElement : ItemGroupXElement
    {
        public ProjectXElement ProjectElement { get; set; }


        public ProjectReferencesItemGroupXElement(XElement value)
            : base(value)
        {
        }
    }
}
