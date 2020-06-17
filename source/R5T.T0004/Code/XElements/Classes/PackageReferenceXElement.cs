using System;
using System.Xml.Linq;

using R5T.T0002;
using R5T.T0005;
using R5T.T0006;

using R5T.Magyar;
using R5T.Magyar.Xml;


namespace R5T.T0004
{
    public class PackageReferenceXElement : TypedXElement, IPackageReference
    {
        #region Static

        public static PackageReferenceXElement From(XElement value)
        {
            var packageReferenceXElement = new PackageReferenceXElement(value);
            return packageReferenceXElement;
        }

        public static PackageReferenceXElement New(string name, string versionString)
        {
            var xPackageReference = new XElement(ProjectFileXmlElementName.PackageReference);
            xPackageReference.AddAttribute(ProjectFileXmlElementName.Include, name);
            xPackageReference.AddAttribute(ProjectFileXmlElementName.Version, versionString);

            var packageReferenceXElement = PackageReferenceXElement.From(xPackageReference);
            return packageReferenceXElement;
        }

        public static PackageReferenceXElement New(XElement parent, string name, string versionString)
        {
            var packageReference = PackageReferenceXElement.New(name, versionString);

            parent.Add(packageReference.Value);

            return packageReference;
        }

        public static PackageReferenceXElement New(PackageReferencesItemGroupXElement parent, string name, string versionString)
        {
            var packageReference = PackageReferenceXElement.New(parent.Value, name, versionString);
            return packageReference;
        }

        #endregion


        public string Name
        {
            get
            {
                var hasAttribute = this.Value.HasAttribute(ProjectFileXmlElementName.Include, out var xAttribute);
                if(hasAttribute)
                {
                    var name = xAttribute.Value;
                    return name;
                }
                else
                {
                    return StringHelper.Invalid;
                }
            }
            set
            {
                var xAttribute = this.Value.AcquireAttribute(ProjectFileXmlElementName.Include);
                xAttribute.Value = value;
            }
        }
        public string VersionString
        {
            get
            {
                var hasAttribute = this.Value.HasAttribute(ProjectFileXmlElementName.Version, out var xAttribute);
                if (hasAttribute)
                {
                    var versionString = xAttribute.Value;
                    return versionString;
                }
                else
                {
                    return StringHelper.Invalid;
                }
            }
            set
            {
                var xAttribute = this.Value.AcquireAttribute(ProjectFileXmlElementName.Version);
                xAttribute.Value = value;
            }
        }


        public PackageReferenceXElement(XElement value)
            : base(value)
        {
        }
    }
}
