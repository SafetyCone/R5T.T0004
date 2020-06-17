using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using R5T.T0002;
using R5T.T0003;
using R5T.T0006;

using R5T.Magyar.Xml;
using R5T.Ostersund;


namespace R5T.T0004
{
    public class XDocumentVisualStudioProjectFile : IVisualStudioProjectFile
    {
        #region Static

        public static XDocumentVisualStudioProjectFile New()
        {
            var xProjectXElement = new XElement(ProjectFileXmlElementName.Project);

            var xDocument = new XDocument();
            xDocument.Add(xProjectXElement);

            var visualStudioProjectFileXDocument = new VisualStudioProjectFileXDocument(xDocument);

            var xElementVisualStudioProjectFile = new XDocumentVisualStudioProjectFile(visualStudioProjectFileXDocument);
            return xElementVisualStudioProjectFile;
        }

        #endregion


        public VisualStudioProjectFileXDocument VisualStudoProjectFileXDocument { get; }
        public ProjectXElement ProjectXElement { get; }

        public Settable<string> SDK
        {
            get
            {
                var settable = this.ProjectXElement.GetSdkSettable();
                return settable;
            }
            set
            {
                this.ProjectXElement.SetSdkSettable(value);
            }
        }

        public Settable<bool> GenerateDocumentationFile
        {
            get
            {
                var settable = this.ProjectXElement.GetPropertySettable<bool>(ProjectFileXmlElementName.GenerateDocumentationFile, ProjectFileValues.ParseBoolean);
                return settable;
            }
            set
            {
                this.ProjectXElement.SetPropertySettable(value, ProjectFileXmlElementName.GenerateDocumentationFile, ProjectFileValues.FormatBoolean);
            }
        }
        public Settable<bool> IsPackable
        {
            get
            {
                var settable = this.ProjectXElement.GetPropertySettable<bool>(ProjectFileXmlElementName.IsPackable, ProjectFileValues.ParseBoolean);
                return settable;
            }
            set
            {
                this.ProjectXElement.SetPropertySettable(value, ProjectFileXmlElementName.IsPackable, ProjectFileValues.FormatBoolean);
            }
        }
        public Settable<Version> LanguageVersion
        {
            get
            {
                var settable = this.ProjectXElement.GetPropertySettable<Version>(ProjectFileXmlElementName.LanguageVersion, ProjectFileValues.ParseLanguageVersion);
                return settable;
            }
            set
            {
                this.ProjectXElement.SetPropertySettable(value, ProjectFileXmlElementName.LanguageVersion, ProjectFileValues.FormatLanguageVersion);
            }
        }
        public Settable<List<int>> NoWarn
        {
            get
            {
                var settable = this.ProjectXElement.GetPropertySettable<List<int>>(ProjectFileXmlElementName.NoWarn, ProjectFileValues.ParseNoWarn);
                return settable;
            }
            set
            {
                this.ProjectXElement.SetPropertySettable(value, ProjectFileXmlElementName.NoWarn, ProjectFileValues.FormatNoWarn);
            }
        }
        public Settable<OutputType> OutputType
        {
            get
            {
                var settable = this.ProjectXElement.GetPropertySettable<OutputType>(ProjectFileXmlElementName.OutputType, ProjectFileValues.ParseOutputType);
                return settable;
            }
            set
            {
                this.ProjectXElement.SetPropertySettable(value, ProjectFileXmlElementName.OutputType, ProjectFileValues.FormatOutputType);
            }
        }
        public Settable<TargetFramework> TargetFramework
        {
            get
            {
                var settable = this.ProjectXElement.GetPropertySettable<TargetFramework>(ProjectFileXmlElementName.TargetFramework, ProjectFileValues.ParseTargetFramework);
                return settable;
            }
            set
            {
                this.ProjectXElement.SetPropertySettable(value, ProjectFileXmlElementName.TargetFramework, ProjectFileValues.FormatTargetFramework);
            }
        }

        public IEnumerable<IProjectReference> ProjectReferences
        {
            get
            {
                if (this.ProjectXElement.HasProjectReferencesItemGroupElement(out var projectReferencesItemGroupXElement))
                {
                    var projectReferences = projectReferencesItemGroupXElement.GetProjectReferences();
                    return projectReferences;
                }
                else
                {
                    return Enumerable.Empty<IProjectReference>();
                }
            }
        }

        public IEnumerable<IPackageReference> PackageReferences
        {
            get
            {
                if (this.ProjectXElement.HasPackageReferencesItemGroupElement(out var packageReferencesItemGroupXElement))
                {
                    var packageReferences = packageReferencesItemGroupXElement.GetPackageReferences();
                    return packageReferences;
                }
                else
                {
                    return Enumerable.Empty<IPackageReference>();
                }
            }
        }


        public XDocumentVisualStudioProjectFile(VisualStudioProjectFileXDocument visualStudioProjectFileXDocument)
        {
            this.VisualStudoProjectFileXDocument = visualStudioProjectFileXDocument;

            this.ProjectXElement = this.VisualStudoProjectFileXDocument.GetProjectXElement();
        }

        public IProjectReference AddProjectReference(string projectFilePath)
        {
            var projectReferencesItemGroupXElement = this.ProjectXElement.AcquireProjectReferencesItemGroupXElement();

            var projectReference = projectReferencesItemGroupXElement.AddProjectReference(projectFilePath);
            return projectReference;
        }

        public bool HasProjectReference(string projectFilePath, out IProjectReference projectReference)
        {
            var hasAnyProjectReferences = this.ProjectXElement.HasProjectReferencesItemGroupElement(out var projectReferencesItemGroupXElement);
            if(!hasAnyProjectReferences)
            {
                projectReference = ProjectReferenceHelper.None;

                return false;
            }

            var hasProjectReference = projectReferencesItemGroupXElement.HasProjectReference(projectFilePath, out projectReference);
            return hasProjectReference;
        }

        public bool RemoveProjectReference(IProjectReference projectReference)
        {
            var hasAnyProjectReferences = this.ProjectXElement.HasProjectReferencesItemGroupElement(out var projectReferencesItemGroupXElement);
            if (!hasAnyProjectReferences)
            {
                return false;
            }

            var success = projectReferencesItemGroupXElement.RemoveProjectReference(projectReference);
            return success;
        }

        public IPackageReference AddPackageReference(string name, string versionString)
        {
            var packageReferencesItemGroupXElement = this.ProjectXElement.AcquirePackageReferencesItemGroupXElement();

            var packageReference = packageReferencesItemGroupXElement.AddPackageReference(name, versionString);
            return packageReference;
        }

        public bool HasPackageReference(string name, out IPackageReference packageReference)
        {
            var hasAnyPackageReferences = this.ProjectXElement.HasPackageReferencesItemGroupElement(out var packageReferencesItemGroupXElement);
            if(!hasAnyPackageReferences)
            {
                packageReference = PackageReferenceHelper.None;

                return false;
            }

            var hasPackageReference = packageReferencesItemGroupXElement.HasPackageReference(name, out packageReference);
            return hasPackageReference;
        }

        public bool RemovePackageReference(IPackageReference packageReference)
        {
            var hasAnyPackageReferences = this.ProjectXElement.HasPackageReferencesItemGroupElement(out var packageReferencesItemGroupXElement);
            if (!hasAnyPackageReferences)
            {
                return false;
            }

            var success = packageReferencesItemGroupXElement.RemovePackageReference(packageReference);
            return success;
        }
    }
}
