using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using R5T.T0002;
using R5T.T0006;

using R5T.Magyar.Xml;


namespace R5T.T0004
{
    public static class ProjectReferencesItemGroupXElementExtensions
    {
        public static IEnumerable<XElement> GetProjectReferenceXElements(this ProjectReferencesItemGroupXElement projectReferencesItemGroupXElement)
        {
            var projectReferenceXElements = projectReferencesItemGroupXElement.Value.Elements(ProjectFileXmlElementName.ProjectReference);
            return projectReferenceXElements;
        }

        public static IEnumerable<XElement> GetProjectReferenceXElementsWhereProjectFilePath(this ProjectReferencesItemGroupXElement projectReferencesItemGroupXElement, string projectFilePath)
        {
            var xProjectReferences = projectReferencesItemGroupXElement.GetProjectReferenceXElements()
                .Where(xElement => xElement.Attribute(ProjectFileXmlElementName.Include).Value == projectFilePath);

            return xProjectReferences;
        }

        public static IEnumerable<IProjectReference> GetProjectReferences(this ProjectReferencesItemGroupXElement projectReferencesItemGroupXElement)
        {
            var projectReferences = projectReferencesItemGroupXElement.GetProjectReferenceXElements()
                .Select(xElement => new ProjectReferenceXElement(xElement));

            return projectReferences;
        }

        public static IProjectReference AddProjectReference(this ProjectReferencesItemGroupXElement projectReferencesItemGroupXElement, string projectFilePath)
        {
            var hasProjectReferenceAlready = projectReferencesItemGroupXElement.HasProjectReference(projectFilePath);
            if(hasProjectReferenceAlready)
            {
                throw new InvalidOperationException($"Project already has project reference:\n{projectFilePath}");
            }

            var projectReference = ProjectReferenceXElement.New(projectReferencesItemGroupXElement, projectFilePath);
            return projectReference;
        }

        public static bool HasProjectReference(this ProjectReferencesItemGroupXElement projectReferencesItemGroupXElement, string projectFilePath, out IProjectReference projectReference)
        {
            projectReference = projectReferencesItemGroupXElement.GetProjectReferenceXElementsWhereProjectFilePath(projectFilePath)
                .Select(xElement => ProjectReferenceXElement.From(xElement))
                .SingleOrDefault();

            var hasProjectReference = ProjectReferenceHelper.WasFound(projectReference);
            return hasProjectReference;
        }

        public static bool HasProjectReference(this ProjectReferencesItemGroupXElement projectReferencesItemGroupXElement, string projectFilePath)
        {
            var hasProjectReference = projectReferencesItemGroupXElement.HasProjectReference(projectFilePath, out _);
            return hasProjectReference;
        }

        public static bool RemoveProjectReference(this ProjectReferencesItemGroupXElement projectReferencesItemGroupXElement, IProjectReference projectReference)
        {
            var xProjectReference = projectReferencesItemGroupXElement.GetProjectReferenceXElementsWhereProjectFilePath(projectReference.ProjectFilePath)
                .SingleOrDefault();

            var wasFound = XElementHelper.WasFound(xProjectReference);
            if(wasFound)
            {
                xProjectReference.Remove();
            }

            return wasFound;
        }
    }
}
