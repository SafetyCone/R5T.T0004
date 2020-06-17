using System;
using System.Xml.Linq;


namespace R5T.T0004
{
    public class ProjectReferencesItemGroupXElement : ItemGroupXElement
    {
        #region Static

        public static ProjectReferencesItemGroupXElement New(XElement xElement)
        {
            var projectReferencesItemGroupXElement = new ProjectReferencesItemGroupXElement(xElement);
            return projectReferencesItemGroupXElement;
        }

        #endregion


        public ProjectReferencesItemGroupXElement(XElement value)
            : base(value)
        {
        }
    }
}
