using System;
using System.Threading.Tasks;

using R5T.T0002;
using R5T.T0003;


namespace R5T.T0004.Construction
{
    public class VisualStudioProjectFileTransformer : IVisualStudioProjectFileTransformer
    {
        public Task CopySourceToDestinationAsync(IVisualStudioProjectFile source, IVisualStudioProjectFile destination)
        {
            destination.GenerateDocumentationFile.CloneFrom(source.GenerateDocumentationFile);
            destination.IsPackable.CloneFrom(source.IsPackable);
            destination.LanguageVersion.CloneFrom(source.LanguageVersion);
            destination.NoWarn.CloneFrom(source.NoWarn);
            destination.OutputType.CloneFrom(source.OutputType);
            destination.SDK.CloneFrom(source.SDK);
            destination.TargetFramework.CloneFrom(source.TargetFramework);

            destination.ClonePackageReferencesFrom(source);
            destination.CloneProjectReferencesFrom(source);

            return Task.CompletedTask;
        }
    }
}
