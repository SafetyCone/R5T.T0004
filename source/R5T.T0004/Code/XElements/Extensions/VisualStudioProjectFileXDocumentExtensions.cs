using System;
using System.Xml.Linq;

using R5T.T0006;

using R5T.Magyar.Xml;


namespace R5T.T0004
{
    public static class VisualStudioProjectFileXDocumentExtensions
    {
        public static XElement GetXProjectXElement(this VisualStudioProjectFileXDocument visualStudioProjectFileXDocument)
        {
            var hasXProjectXElement = visualStudioProjectFileXDocument.HasXProjectXElement(out var xProjectXElement);
            if (!hasXProjectXElement)
            {
                throw new InvalidOperationException($"Visual Studio project file {nameof(XDocument)} does not have a {ProjectFileXmlElementName.Project} element.");
            }

            return xProjectXElement;
        }

        public static ProjectXElement GetProjectXElement(this VisualStudioProjectFileXDocument visualStudioProjectFileXDocument)
        {
            var xProjectXElement = visualStudioProjectFileXDocument.GetXProjectXElement();

            var projectXElement = xProjectXElement.AsProject();
            return projectXElement;
        }

        public static bool HasXProjectXElement(this VisualStudioProjectFileXDocument visualStudioProjectFileXDocument, out XElement xProjectXElement)
        {
            xProjectXElement = visualStudioProjectFileXDocument.Value.Element(ProjectFileXmlElementName.Project);

            var hasXProjectXElement = XElementHelper.WasFound(xProjectXElement);
            return hasXProjectXElement;
        }

        public static bool HasProjectXElement(this VisualStudioProjectFileXDocument visualStudioProjectFileXDocument, out ProjectXElement projectXElement)
        {
            var hasProjectXElement = visualStudioProjectFileXDocument.HasXProjectXElement(out var xProjectXElement);

            projectXElement = xProjectXElement.AsProject();

            return hasProjectXElement;
        }
    }
}
