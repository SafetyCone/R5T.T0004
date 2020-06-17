using System;
using System.Xml.Linq;

using R5T.T0005;


namespace R5T.T0004
{
    public class ItemGroupXElement : TypedXElement
    {
        public ProjectXElement ProjectXElement
        {
            get
            {
                var xParent = this.Value.Parent;

                var projectXElement = new ProjectXElement(xParent);
                return projectXElement;
            }
        }


        public ItemGroupXElement(XElement value)
            : base(value)
        {
        }
    }
}
