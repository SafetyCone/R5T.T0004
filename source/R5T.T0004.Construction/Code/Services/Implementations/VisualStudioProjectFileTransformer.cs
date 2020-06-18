using System;
using System.Threading.Tasks;

using R5T.T0002;


namespace R5T.T0004.Construction
{
    public class VisualStudioProjectFileTransformer : IVisualStudioProjectFileTransformer
    {
        public Task CopySourceToDestinationAsync(IVisualStudioProjectFile source, IVisualStudioProjectFile destination)
        {
            destination.GenerateDocumentationFile = source.GenerateDocumentationFile;
            destination.IsPackable = source.IsPackable;
            destination.LanguageVersion = source.LanguageVersion;
            destination.NoWarn = source.NoWarn;
            destination.OutputType = source.OutputType;
            destination.SDK = source.SDK;
            destination.TargetFramework = source.TargetFramework;

            destination.ClonePackageReferencesFrom(source);
            destination.CloneProjectReferencesFrom(source);

            return Task.CompletedTask;
        }
    }
}
