using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0001;
using R5T.D0010;
using R5T.D0010.Default;
using R5T.T0002;
using R5T.T0003;


namespace R5T.T0004.Construction
{
    public class VisualStudioProjectFileValueEqualityComparer : IVisualStudioProjectFileValueEqualityComparer
    {
        private INowUtcProvider NowUtcProvider { get; }


        public VisualStudioProjectFileValueEqualityComparer(INowUtcProvider nowUtcProvider)
        {
            this.NowUtcProvider = nowUtcProvider;
        }

        private async Task<bool> CompareSettable<T>(Settable<T> settableX, Settable<T> settableY, IMessageSink messageSink, string propertyName)
        {
            var areEqual = settableX == settableY;
            if (!areEqual)
            {
                var message = $"{propertyName} values are not equal:\nX:{settableX}\nY:{settableY}";

                await messageSink.AddErrorMessageAsync(this.NowUtcProvider, message);
            }

            return areEqual;
        }

        public async Task<bool> Equals(IVisualStudioProjectFile x, IVisualStudioProjectFile y, IMessageSink messageSink)
        {
            var areEqual = true;

            var generateDocumentationFileAreEqual = await this.CompareSettable(x.GenerateDocumentationFile, y.GenerateDocumentationFile, messageSink, nameof(x.GenerateDocumentationFile));
            areEqual &= generateDocumentationFileAreEqual;

            var isPackableAreEqual = await this.CompareSettable(x.IsPackable, y.IsPackable, messageSink, nameof(x.IsPackable));
            areEqual &= isPackableAreEqual;

            var languageVersionAreEqual = await this.CompareSettable(x.LanguageVersion, y.LanguageVersion, messageSink, nameof(x.LanguageVersion));
            areEqual &= languageVersionAreEqual;

            var noWarnAreEqual = await this.CompareSettable(x.NoWarn, y.NoWarn, messageSink, nameof(x.NoWarn));
            areEqual &= noWarnAreEqual;

            var outputTypeAreEqual = await this.CompareSettable(x.OutputType, y.OutputType, messageSink, nameof(x.OutputType));
            areEqual &= outputTypeAreEqual;

            var sdkAreEqual = await this.CompareSettable(x.SDK, y.SDK, messageSink, nameof(x.SDK));
            areEqual &= sdkAreEqual;

            var targetFrameworkAreEqual = await this.CompareSettable(x.TargetFramework, y.TargetFramework, messageSink, nameof(x.TargetFramework));
            areEqual &= targetFrameworkAreEqual;


            var packageReferenceComparisonMessageRepository = new InMemoryMessageRepository();

            var packageReferencesEqual = await x.PackageReferences.SequenceEqualAsync(y.PackageReferences, DefaultIPackageReferenceEqualityComparer.Instance, this.NowUtcProvider, packageReferenceComparisonMessageRepository, false);
            if(!packageReferencesEqual)
            {
                await messageSink.AddErrorMessageAsync(this.NowUtcProvider, "Package references not equal.");

                await messageSink.CopyFromAsync(packageReferenceComparisonMessageRepository);
            }
            areEqual &= packageReferencesEqual;

            var projectReferenceComparisonMessageRepository = new InMemoryMessageRepository();

            var projectReferencesEqual = await x.ProjectReferences.SequenceEqualAsync(y.ProjectReferences, DefaultProjectReferenceEqualityComparer.Instance, this.NowUtcProvider, projectReferenceComparisonMessageRepository);
            if (!projectReferencesEqual)
            {
                await messageSink.AddErrorMessageAsync(this.NowUtcProvider, "Project references not equal.");

                await messageSink.CopyFromAsync(packageReferenceComparisonMessageRepository);
            }
            areEqual &= projectReferencesEqual;

            return areEqual;
        }
    }
}
