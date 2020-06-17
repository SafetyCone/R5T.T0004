using System;
using System.IO;
using System.Threading.Tasks;

using R5T.D0010;
using R5T.T0002;

using R5T.Lombardy;


namespace R5T.T0004.Construction
{
    public class FunctionalVisualStudioProjectFileSerializationModifier : IFunctionalVisualStudioProjectFileSerializationModifier
    {
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }


        public FunctionalVisualStudioProjectFileSerializationModifier(
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
        }

        public Task<T> ModifyDeserializeationAsync<T>(T visualStudioProjectFile, string projectFilePath, IMessageSink messageSink)
            where T: IVisualStudioProjectFile
        {
            // Change all project reference paths to be absolute, not relative, using the input project file path.
            foreach (var projectReference in visualStudioProjectFile.ProjectReferences)
            {
                var projectReferenceAbsolutePath = this.StringlyTypedPathOperator.Combine(projectFilePath, projectReference.ProjectFilePath);

                projectReference.ProjectFilePath = projectReferenceAbsolutePath;
            }

            return Task.FromResult(visualStudioProjectFile);
        }

        public Task<T> ModifySerializationAsync<T>(T visualStudioProjectFile, string projectFilePath, IMessageSink messageSink)
            where T: IVisualStudioProjectFile
        {
            // Change all project reference paths to be relative, not absolute, using the input project file path.
            foreach (var projectReference in visualStudioProjectFile.ProjectReferences)
            {
                var projectReferenceRelativePath = this.StringlyTypedPathOperator.GetRelativePathFileToFile(projectFilePath, projectReference.ProjectFilePath);

                projectReference.ProjectFilePath = projectReferenceRelativePath;
            }

            return Task.FromResult(visualStudioProjectFile);
        }
    }
}
