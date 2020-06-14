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
            var temporaryDirectoryFilePathProviderAction = services.AddTemporaryDirectoryFilePathProviderAction();
            var testingDataDirectoryContentPathsProviderAction = services.AddTestingDataDirectoryContentPathsProviderAction();

            // 1.
            var messageSink = services.AddConsoleMessageSinkAction(
                messageFormatterAction);
            var relativeFilePathsVisualStudioProjectFileStreamSerializer = services.AddRelativeFilePathsVisualStudioProjectFileStreamSerializerAction(
                nowUtcProviderAction);

            services
                .Run(fileEqualityComparerAction)
                .Run(messageSink)
                .Run(relativeFilePathsVisualStudioProjectFileStreamSerializer)
                .Run(temporaryDirectoryFilePathProviderAction)
                .Run(testingDataDirectoryContentPathsProviderAction)
                ;
        }
    }
}
