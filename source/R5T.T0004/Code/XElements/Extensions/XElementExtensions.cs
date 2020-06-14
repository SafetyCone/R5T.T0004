using System;
using System.Xml.Linq;


namespace R5T.T0004
{
    public static class XElementExtensions
    {
        public static ItemGroupXElement AsItemGroup(this XElement xElement)
        {
            var projectReferenceItemGroup = new ItemGroupXElement(xElement);
            return projectReferenceItemGroup;
        }

        public static PackageReferencesItemGroupXElement AsPackageReferencesItemGroup(this XElement xElement)
        {
            var packageReferenceItemGroup = new PackageReferencesItemGroupXElement(xElement);
            return packageReferenceItemGroup;
        }

        public static ProjectXElement AsProject(this XElement xElement)
        {
            var project = new ProjectXElement(xElement);
            return project;
        }

        public static ProjectReferencesItemGroupXElement AsProjectReferencesItemGroup(this XElement xElement)
        {
            var projectReferenceItemGroup = new ProjectReferencesItemGroupXElement(xElement);
            return projectReferenceItemGroup;
        }

        public static PropertyGroupXElement AsPropertyGroup(this XElement xElement)
        {
            var propertyGroup = new PropertyGroupXElement(xElement);
            return propertyGroup;
        }
    }
}
