using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0001;

using R5T.Dacia;


namespace R5T.T0004.Construction
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="RelativePathsXDocumentVisualStudioProjectFileStreamSerializer"/> implementation of <see cref="IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddRelativeFilePathsVisualStudioProjectFileStreamSerializer(this IServiceCollection services,
            IServiceAction<INowUtcProvider> nowUtcProviderAction)
        {
            services
                .AddSingleton<IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer, RelativePathsXDocumentVisualStudioProjectFileStreamSerializer>()
                .Run(nowUtcProviderAction)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="RelativePathsXDocumentVisualStudioProjectFileStreamSerializer"/> implementation of <see cref="IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer> AddRelativeFilePathsVisualStudioProjectFileStreamSerializerAction(this IServiceCollection services,
            IServiceAction<INowUtcProvider> nowUtcProviderAction)
        {
            var serviceAction = ServiceAction<IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer>.New(() => services.AddRelativeFilePathsVisualStudioProjectFileStreamSerializer(
                nowUtcProviderAction));

            return serviceAction;
        }
    }
}
