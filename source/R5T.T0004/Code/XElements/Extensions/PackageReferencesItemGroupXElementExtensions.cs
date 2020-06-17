using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using R5T.T0002;
using R5T.T0006;

using R5T.Magyar.Xml;


namespace R5T.T0004
{
    public static class PackageReferencesItemGroupXElementExtensions
    {
        public static IEnumerable<XElement> GetPackageReferenceXElements(this PackageReferencesItemGroupXElement packageReferencesItemGroupXElement)
        {
            var packageReferenceXElements = packageReferencesItemGroupXElement.Value.Elements(ProjectFileXmlElementName.PackageReference);
            return packageReferenceXElements;
        }

        public static IEnumerable<XElement> GetPackageReferenceXElementsWhereName(this PackageReferencesItemGroupXElement packageReferencesItemGroupXElement, string name)
        {
            var xPackageReferences = packageReferencesItemGroupXElement.GetPackageReferenceXElements()
                .Where(xElement => xElement.Attribute(ProjectFileXmlElementName.Include).Value == name);

            return xPackageReferences;
        }

        public static IEnumerable<IPackageReference> GetPackageReferences(this PackageReferencesItemGroupXElement packageReferencesItemGroupXElement)
        {
            var packageReferences = packageReferencesItemGroupXElement.GetPackageReferenceXElements()
                .Select(xElement => new PackageReferenceXElement(xElement));

            return packageReferences;
        }

        public static IPackageReference AddPackageReference(this PackageReferencesItemGroupXElement packageReferencesItemGroupXElement, string name, string versionString)
        {
            var projectReference = PackageReferenceXElement.New(packageReferencesItemGroupXElement, name, versionString);
            return projectReference;
        }

        /// <summary>
        /// Note, no need for package version string since you can't have mulitple versions of the same package.
        /// </summary>
        public static bool HasPackageReference(this PackageReferencesItemGroupXElement packageReferencesItemGroupXElement, string name, out IPackageReference packageReference)
        {
            packageReference = packageReferencesItemGroupXElement.GetPackageReferenceXElementsWhereName(name)
                .Select(xElement => PackageReferenceXElement.From(xElement))
                .SingleOrDefault();

            var hasPackageReference = PackageReferenceHelper.WasFound(packageReference);
            return hasPackageReference;
        }

        public static bool HasPackageReference(this PackageReferencesItemGroupXElement packageReferencesItemGroupXElement, string name)
        {
            var hasPackageReference = packageReferencesItemGroupXElement.HasPackageReference(name, out _);
            return hasPackageReference;
        }

        public static bool RemovePackageReference(this PackageReferencesItemGroupXElement packageReferencesItemGroupXElement, IPackageReference packageReference)
        {
            var xPackageReference = packageReferencesItemGroupXElement.GetPackageReferenceXElementsWhereName(packageReference.Name)
                .SingleOrDefault();

            var wasFound = XElementHelper.WasFound(xPackageReference);
            if(wasFound)
            {
                xPackageReference.Remove();
            }

            return wasFound;
        }
    }
}
