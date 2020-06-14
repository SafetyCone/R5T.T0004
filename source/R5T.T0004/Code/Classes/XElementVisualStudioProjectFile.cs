using System;
using System.Collections.Generic;

using R5T.T0002;
using R5T.T0003;
using R5T.T0006;

using R5T.Ostersund;


namespace R5T.T0004
{
    public class XElementVisualStudioProjectFile : IVisualStudioProjectFile
    {
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

        public IEnumerable<IProjectReference> ProjectReferences => throw new NotImplementedException();

        public IEnumerable<IPackageReference> PackageReferences => throw new NotImplementedException();


        public XElementVisualStudioProjectFile(ProjectXElement projectXElement)
        {
            this.ProjectXElement = projectXElement;
        }

        public IProjectReference AddProjectReference(string projectFilePath)
        {
            throw new NotImplementedException();
        }

        public bool HasProjectReference(string projectFilePath, out IProjectReference projectReference)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProjectReference(IProjectReference projectReference)
        {
            throw new NotImplementedException();
        }

        public IPackageReference AddPackageReference(string name, string versionString)
        {
            throw new NotImplementedException();
        }

        public bool HasPackageReference(string name, string versionString, out IPackageReference packageReference)
        {
            throw new NotImplementedException();
        }

        public bool RemovePackageReference(IPackageReference packageReference)
        {
            throw new NotImplementedException();
        }
    }
}
