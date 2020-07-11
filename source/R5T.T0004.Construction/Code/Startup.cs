using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.D0001.Standard;
using R5T.D0010.Default;
using R5T.D0018.I0001;
using R5T.D0020.Default;
using R5T.D0029.Standard;

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
            var temporaryDirectoryFilePathProviderAction = services.AddTemporaryDirectoryFilePathProviderAction();
            var testingDataDirectoryContentPathsProviderAction = services.AddTestingDataDirectoryContentPathsProviderAction();

            // 1.
            var messageSinkAction = services.AddConsoleMessageSinkAction(
                messageFormatterAction);
            var visualStudioProjectFileValueEqualityComparerAction = services.AddVisualStudioProjectFileValueEqualityComparerAction(
                nowUtcProviderAction);

            // 2.
            var asFilePathVisualStudioProjectFileSerializerAction = services.AddAsFilePathVisualStudioProjectFileSerializerAction(
                nowUtcProviderAction,
                messageSinkAction,
                stringlyTypedPathOperatorAction);
            var visualStudioProjectFileSerializerAction = services.AddVisualStudioProjectFileSerializerAction(
                nowUtcProviderAction,
                messageSinkAction,
                stringlyTypedPathOperatorAction);


            services
                .Run(asFilePathVisualStudioProjectFileSerializerAction)
                .Run(visualStudioProjectFileSerializerAction)
                .Run(fileEqualityComparerAction)
                .Run(messageSinkAction)
                .Run(newVisualStudioProjectFileGeneratorAction)
                .Run(temporaryDirectoryFilePathProviderAction)
                .Run(testingDataDirectoryContentPathsProviderAction)
                .Run(visualStudioProjectFileValueEqualityComparerAction)
                ;
        }
    }
}
