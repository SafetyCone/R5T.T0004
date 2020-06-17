using System;
using System.Xml.Linq;

using R5T.T0002;
using R5T.T0005;
using R5T.T0006;

using R5T.Magyar;
using R5T.Magyar.Xml;


namespace R5T.T0004
{
    public class ProjectReferenceXElement : TypedXElement, IProjectReference
    {
        #region Static

        public static ProjectReferenceXElement From(XElement value)
        {
            var projectReferenceXElement = new ProjectReferenceXElement(value);
            return projectReferenceXElement;
        }

        public static ProjectReferenceXElement New(string projectFilePath)
        {
            var xProjectReference = new XElement(ProjectFileXmlElementName.ProjectReference);
            xProjectReference.AddAttribute(ProjectFileXmlElementName.Include, projectFilePath);

            var projectReferenceXElement = ProjectReferenceXElement.From(xProjectReference);
            return projectReferenceXElement;
        }

        public static ProjectReferenceXElement New(XElement parent, string projectFilePath)
        {
            var projectReference = ProjectReferenceXElement.New(projectFilePath);

            parent.Add(projectReference.Value);

            return projectReference;
        }

        public static ProjectReferenceXElement New(ProjectReferencesItemGroupXElement parent, string projectFilePath)
        {
            var projectReference = ProjectReferenceXElement.New(parent.Value, projectFilePath);
            return projectReference;
        }

        #endregion


        public string ProjectFilePath
        {
            get
            {
                var hasAttribute = this.Value.HasAttribute(ProjectFileXmlElementName.Include, out var xAttribute);
                if (hasAttribute)
                {
                    var projectFilePath = xAttribute.Value;
                    return projectFilePath;
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


        public ProjectReferenceXElement(XElement value)
            : base(value)
        {
        }
    }
}
