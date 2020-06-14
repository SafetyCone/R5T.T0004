using System;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using R5T.D0010;

using R5T.Bedford;
using R5T.Chalandri;
using R5T.Evosmos;
using R5T.Liverpool;
using R5T.Magyar.IO;
using R5T.Magyar.Xml;


namespace R5T.T0004.Construction
{
    class Program : AsyncHostedServiceProgramBase
    {
        static async Task Main(string[] args)
        {
            await HostedServiceProgram.RunAsync<Program, Startup>();
        }


        private IServiceProvider ServiceProvider { get; }

        private IFileEqualityComparer FileEqualityComparer { get; }
        private IMessageSink MessageSink { get; }
        private ITemporaryDirectoryFilePathProvider TemporaryDirectoryFilePathProvider { get; }
        private ITestingDataDirectoryContentPathsProvider TestingDataDirectoryContentPathsProvider { get; }


        public Program(IApplicationLifetime applicationLifetime,
            IServiceProvider serviceProvider,
            IFileEqualityComparer fileEqualityComparer,
            IMessageSink messageSink,
            ITemporaryDirectoryFilePathProvider temporaryDirectoryFilePathProvider,
            ITestingDataDirectoryContentPathsProvider testingDataDirectoryContentPathsProvider)
            : base(applicationLifetime)
        {
            this.ServiceProvider = serviceProvider;
            this.FileEqualityComparer = fileEqualityComparer;
            this.MessageSink = messageSink;
            this.TemporaryDirectoryFilePathProvider = temporaryDirectoryFilePathProvider;
            this.TestingDataDirectoryContentPathsProvider = testingDataDirectoryContentPathsProvider;
        }

        protected override async Task SubMainAsync()
        {
            //await this.CompareIdenticalFiles();
            //await this.CompareRoundTripBasicXmlSerializedFiles();
            await this.CompareRoundTripRelativeFilePathsSerializedFiles();
        }

        private async Task CompareRoundTripRelativeFilePathsSerializedFiles()
        {
            var inputProjectFilePath = this.TestingDataDirectoryContentPathsProvider.GetExampleVisualStudioProjectFilePath01();
            var outputProjectFilePath = this.TemporaryDirectoryFilePathProvider.GetTemporaryDirectoryFilePath("ProjectFile01.csproj");

            var relativeFilePathsVisualStudioProjectFileStreamSerializer = this.ServiceProvider.GetRequiredService<IRelativeFilePathsVisualStudioProjectFileStreamSerializer>();

            using (var inputFileStream = FileStreamHelper.NewRead(inputProjectFilePath))
            {
                var visualStudioProjectFile = await relativeFilePathsVisualStudioProjectFileStreamSerializer.DeserializeAsync(inputFileStream, this.MessageSink);

                using (var outputFileStream = FileStreamHelper.NewWrite(outputProjectFilePath))
                {
                    await relativeFilePathsVisualStudioProjectFileStreamSerializer.SerializeAsync(outputFileStream, visualStudioProjectFile, this.MessageSink);
                }
            }

            await this.CompareFiles(inputProjectFilePath, outputProjectFilePath, "Files not equal.");
        }

        private async Task CompareRoundTripBasicXmlSerializedFiles()
        {
            var inputProjectFilePath = this.TestingDataDirectoryContentPathsProvider.GetExampleVisualStudioProjectFilePath01();
            var outputProjectFilePath = this.TemporaryDirectoryFilePathProvider.GetTemporaryDirectoryFilePath("ProjectFile01.csproj");

            //var xElement = XElement.Load(inputProjectFilePath); // Does not preserve insignifcant whitespace.
            var xElement = XElement.Load(inputProjectFilePath, LoadOptions.PreserveWhitespace); // Because insignification whitespace is preserved, project file is identical when round-tripped.

            //xElement.Save(outputProjectFilePath); // Adds an XML document declaration, so no good a a project file serializer.

            using (var fileStream = FileStreamHelper.NewWrite(outputProjectFilePath))
            using (var xmlWriter = XmlWriterHelper.New(fileStream))
            {
                xElement.Save(xmlWriter);
            }

            await this.CompareFiles(inputProjectFilePath, outputProjectFilePath, "Files not equal.");
        }

        private async Task CompareIdenticalFiles()
        {
            var exampleProjectFilePath = this.TestingDataDirectoryContentPathsProvider.GetExampleVisualStudioProjectFilePath01();

            await this.CompareFiles(exampleProjectFilePath, exampleProjectFilePath, "Identical files not equal.");
        }

        private Task CompareFiles(string filePath1, string filePath2, string messageIfFilesNotEqual)
        {
            var equal = this.FileEqualityComparer.Equals(filePath1, filePath2);
            if (!equal)
            {
                throw new Exception(messageIfFilesNotEqual);
            }

            return Task.CompletedTask;
        }
    }
}
