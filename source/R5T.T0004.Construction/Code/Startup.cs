using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.D0001.Standard;
using R5T.D0010.Default;

using R5T.Bedford.Bath.Standard;
using R5T.Chalandri.DropboxRivetTestingData;
using R5T.Dacia;
using R5T.Evosmos.CDriveTemp;
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
            // 0.
            var fileEqualityComparerAction = services.AddTextFileEqualityComparerAction();
            var messageFormatterAction = services.AddMessageFormatterAction();
            var nowUtcProviderAction = services.AddNowUtcProviderAction();
            var projectXElementPrettifierAction = ServiceAction<IVisualStudioProjectFileXDocumentPrettifier>.New(() => services.AddSingleton<IVisualStudioProjectFileXDocumentPrettifier, VisualStudioProjectFileXDocumentPrettifier>());
            var temporaryDirectoryFilePathProviderAction = services.AddTemporaryDirectoryFilePathProviderAction();
            var testingDataDirectoryContentPathsProviderAction = services.AddTestingDataDirectoryContentPathsProviderAction();

            // 1.
            var messageSinkAction = services.AddConsoleMessageSinkAction(
                messageFormatterAction);
            var relativeFilePathsVisualStudioProjectFileStreamSerializerAction = services.AddRelativeFilePathsVisualStudioProjectFileStreamSerializerAction(
                nowUtcProviderAction);

            // 2.
            var functionalVisualStudioProjectFileStreamSerializerAction = ServiceAction<IFunctionalVisualStudioProjectFileSerializationModifier>.New(() => services.AddSingleton<IFunctionalVisualStudioProjectFileSerializationModifier, FunctionalVisualStudioProjectFileSerializationModifier>());
            // dependencies...

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
                .Run(projectXElementPrettifierAction)
                .Run(relativeFilePathsVisualStudioProjectFileStreamSerializerAction)
                .Run(temporaryDirectoryFilePathProviderAction)
                .Run(testingDataDirectoryContentPathsProviderAction)
                .Run(visualStudioProjectFileSerializerAction)
                ;
        }
    }
}
