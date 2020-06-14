using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using R5T.T0003;
using R5T.T0006;

using R5T.Magyar.Xml;


namespace R5T.T0004
{
    public static class ProjectXElementExtensions
    {
        #region Property Group

        public static IEnumerable<XElement> PropertyGroupElements(this ProjectXElement projectXElement)
        {
            var propertyGroupElements = projectXElement.Value.Elements(ProjectFileXmlElementName.PropertyGroup);
            return propertyGroupElements;
        }

        public static XElement GetPropertyGroupElementWithChildSingleOrDefault(this ProjectXElement projectXElement, string childName)
        {
            var propertyGroupElement = projectXElement.PropertyGroupElements()
                .Where(x => x.HasChild(childName))
                .SingleOrDefault()
                ;

            return propertyGroupElement;
        }

        public static bool HasPropertyGroupElementWithChildSingleOrDefault(this ProjectXElement projectXElement, string childName, out XElement propertyGroupElement)
        {
            propertyGroupElement = projectXElement.GetPropertyGroupElementWithChildSingleOrDefault(childName);

            var hasPropertyGroupElementWithChildSingleOrDefault = XElementHelper.WasFound(propertyGroupElement);
            return hasPropertyGroupElementWithChildSingleOrDefault;
        }

        /// <summary>
        /// The primary property group element makes use of the fact that the <see cref="ProjectFileXmlElementName.TargetFramework"/> property always exists.
        /// </summary>
        public static bool HasPrimaryPropertyGroupElement(this ProjectXElement projectXElement, out PropertyGroupXElement primaryPropertyGroupXElement)
        {
            var hasPrimaryPropertyGroupElement = projectXElement.HasPropertyGroupElementWithChildSingleOrDefault(ProjectFileXmlElementName.TargetFramework, out var propertyGroupElement);

            primaryPropertyGroupXElement = hasPrimaryPropertyGroupElement ? PropertyGroupXElement.New(propertyGroupElement) : default;

            return hasPrimaryPropertyGroupElement;
        }

        /// <summary>
        /// The primary property group element makes use of the fact that the <see cref="ProjectFileXmlElementName.TargetFramework"/> property always exists.
        /// </summary>
        public static PropertyGroupXElement AcquirePrimaryPropertyGroupXElement(this ProjectXElement projectXElement)
        {
            // Does the primary <PropertyGroup> element exist? (A <PropertyGroup> element with a child named <TargetFramework>, which should always exist.)
            var hasPrimaryPropertyGroupXElement = projectXElement.HasPrimaryPropertyGroupElement(out var primaryPropertyGroupXElement);
            if(!hasPrimaryPropertyGroupXElement)
            {
                // No? Then acquire the first property group element.
                primaryPropertyGroupXElement = projectXElement.AcquireFirstPropertyGroupXElement();
            }

            return primaryPropertyGroupXElement;
        }

        public static PropertyGroupXElement AcquireFirstPropertyGroupXElement(this ProjectXElement projectXElement)
        {
            // Do any <PropertyGroup> elements exist?
            var anyPropertyGroupElementExists = projectXElement.AnyPropertyGroupElementExists(out var propertyGroupXElement);
            if(!anyPropertyGroupElementExists)
            {
                // No? Create and add a <PropertyGroup> element,
                var xElement = projectXElement.Value.AddElement(ProjectFileXmlElementName.PropertyGroup);

                propertyGroupXElement = PropertyGroupXElement.New(xElement);
            }

            return propertyGroupXElement;
        }

        public static bool AnyPropertyGroupElementExists(this ProjectXElement projectXElement, out PropertyGroupXElement propertyGroupXElement)
        {
            var anyPropertyGroupElementExists = projectXElement.Value.HasElement(ProjectFileXmlElementName.PropertyGroup, out var xElement);

            propertyGroupXElement = anyPropertyGroupElementExists ? PropertyGroupXElement.New(xElement) : default;

            return anyPropertyGroupElementExists;
        }

        #endregion

        #region Project Attributes

        public static bool HasSdk(this ProjectXElement projectXElement, out string sdk)
        {
            var hasSdk = projectXElement.Value.HasAttribute(ProjectFileXmlElementName.Sdk, out var xSdkAttribute);

            sdk = hasSdk ? xSdkAttribute.Value : default;

            return hasSdk;
        }

        public static Settable<string> GetSdkSettable(this ProjectXElement projectXElement)
        {
            var hasSdk = projectXElement.HasSdk(out var sdk);

            var sdkSettable = Settable<string>.New(sdk, hasSdk);
            return sdkSettable;
        }

        public static void SetSdkSettable(this ProjectXElement projectXElement, Settable<string> sdkSettable)
        {
            if (sdkSettable.IsSet)
            {
                var sdkAttribute = projectXElement.Value.AcquireAttribute(ProjectFileXmlElementName.Sdk);
                sdkAttribute.Value = sdkSettable.Value;
            }
            else
            {
                projectXElement.Value.RemoveAttribute(ProjectFileXmlElementName.Sdk);
            }
        }

        #endregion

        #region Properties

        public static XElement AcquirePropertyElement(this ProjectXElement projectXElement, string propertyElementName)
        {
            // Does the exact element we want exist? (A child of a <PropertyGroup> element with the right name?)
            var hasPropertyElement = projectXElement.HasPropertyElement(propertyElementName, out var xPropertyElement);
            if(!hasPropertyElement)
            {
                // No? Then acquire the primary <PropertyGroup> element and add the child element.
                var propertyGroupElement = projectXElement.AcquirePrimaryPropertyGroupXElement();

                xPropertyElement = propertyGroupElement.Value.AcquireElement(propertyElementName);
            }

            return xPropertyElement;
        }

        public static bool HasPropertyElement(this ProjectXElement projectXElement, string propertyElementName, out XElement xPropertyElement)
        {
            xPropertyElement = projectXElement.GetPropertyGroupElementWithChildSingleOrDefault(propertyElementName)
                ?.GetChild(propertyElementName); // If default, then not found, else child must exist since the property group element with child WAS found.

            var hasPropertyElement = XElementHelper.WasFound(xPropertyElement);
            return hasPropertyElement;
        }

        public static bool RemovePropertyElement(this ProjectXElement projectXElement, string propertyElementName)
        {
            var hasPropertyElement = projectXElement.HasPropertyElement(propertyElementName, out var xPropertyElement);
            if(hasPropertyElement)
            {
                xPropertyElement.Remove();
            }

            return hasPropertyElement;
        }

        public static bool HasProperty<T>(this ProjectXElement projectXElement, out T propertyValue, string propertyElementName, Func<string, T> propertyValueFromString)
        {
            var hasPropertyElement = projectXElement.HasPropertyElement(propertyElementName, out var xPropertyElement);

            propertyValue = hasPropertyElement ? propertyValueFromString(xPropertyElement.Value) : default;

            return hasPropertyElement;
        }

        public static Settable<T> GetPropertySettable<T>(this ProjectXElement projectXElement, string propertyElementName, Func<string, T> propertyValueFromString)
        {
            var hasProperty = projectXElement.HasProperty(out var propertyValue, propertyElementName, propertyValueFromString);

            var propertySettable = Settable<T>.New(propertyValue, hasProperty);
            return propertySettable;
        }

        public static void SetPropertySettable<T>(this ProjectXElement projectXElement, Settable<T> settable, string propertyElementName, Func<T, string> propertyValueToString)
        {
            if (settable.IsSet)
            {
                var generateDocumentationFileElement = projectXElement.AcquirePropertyElement(propertyElementName);
                generateDocumentationFileElement.Value = propertyValueToString(settable.Value);
            }
            else
            {
                projectXElement.RemovePropertyElement(propertyElementName);
            }
        }

        #endregion
    }
}
