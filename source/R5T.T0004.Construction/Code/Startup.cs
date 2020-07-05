using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.D0001.Standard;
using R5T.D0010.Default;
using R5T.D0017.Default;
using R5T.D0018.I0001;
using R5T.D0019.Default;
using R5T.D0020.Default;
using R5T.D0022.Default;

using R5T.Bedford.Bath.Standard;
using R5T.Chalandri.DropboxRivetTestingData;
using R5T.Dacia;
using R5T.Evosmos.CDriveTemp;
using R5T.Lombardy.Standard;
using R5T.Richmond;


namespace R5T.T0004.Construction
{
    public class Startup : StartupBase
    {
        public Startup(ILogger<Startup> logger)
            : base(logger)
        {
        }

        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            // -1.
            var stringlyTypedPathOperatorAction = services.AddStringlyTypedPathOperatorAction();

            // 0.
            var fileEqualityComparerAction = services.AddTextFileEqualityComparerAction();
            var messageFormatterAction = services.AddMessageFormatterAction();
            var newVisualStudioProjectFileGeneratorAction = services.AddNewXDocumentVisualStudioProjectFileGeneratorAction();
            var nowUtcProviderAction = services.AddNowUtcProviderAction();
            var visualStudioProjectFileXDocumentPrettifierAction = services.AddVisualStudioProjectFileXDocumentPrettifierAction();
            var temporaryDirectoryFilePathProviderAction = services.AddTemporaryDirectoryFilePathProviderAction();
            var testingDataDirectoryContentPathsProviderAction = services.AddTestingDataDirectoryContentPathsProviderAction();
            var visualStudioProjectFileTransformerAction = services.AddVisualStudioProjectFileTransformerAction();

            // 1.
            var functionalVisualStudioProjectFileStreamSerializerAction = services.AddFunctionalVisualStudioProjectFileSerializationModifierAction(
                stringlyTypedPathOperatorAction);
            var messageSinkAction = services.AddConsoleMessageSinkAction(
                messageFormatterAction);
            var relativeFilePathsVisualStudioProjectFileStreamSerializerAction = services.AddRelativeFilePathsVisualStudioProjectFileStreamSerializerAction(
                nowUtcProviderAction);
            var visualStudioProjectFileValueEqualityComparerAction = services.AddVisualStudioProjectFileValueEqualityComparerAction(
                nowUtcProviderAction);

            // 2.

            // 3.
            var visualStudioProjectFileSerializerAction = ServiceAction<IXDocumentVisualStudioProjectFileSerializer>.New(() => services.AddSingleton<IXDocumentVisualStudioProjectFileSerializer, XDocumentVisualStudioProjectFileSerializer>());
            // dependencies...


            var asFilePathXElementVisualStudioProjectFileSerializer = ServiceAction<IAsFilePathXDocumentVisualStudioProjectFileSerializer>.New(() => services.AddSingleton<IAsFilePathXDocumentVisualStudioProjectFileSerializer, AsFilePathXDocumentVisualStudioProjectFileSerializer>());
            // dependencies...

            services
                .Run(asFilePathXElementVisualStudioProjectFileSerializer)
                .Run(fileEqualityComparerAction)
                .Run(functionalVisualStudioProjectFileStreamSerializerAction)
                .Run(messageSinkAction)
                .Run(newVisualStudioProjectFileGeneratorAction)
                .Run(visualStudioProjectFileXDocumentPrettifierAction)
                .Run(relativeFilePathsVisualStudioProjectFileStreamSerializerAction)
                .Run(temporaryDirectoryFilePathProviderAction)
                .Run(testingDataDirectoryContentPathsProviderAction)
                .Run(visualStudioProjectFileValueEqualityComparerAction)
                .Run(visualStudioProjectFileSerializerAction)
                .Run(visualStudioProjectFileTransformerAction)
                ;
        }
    }
}
