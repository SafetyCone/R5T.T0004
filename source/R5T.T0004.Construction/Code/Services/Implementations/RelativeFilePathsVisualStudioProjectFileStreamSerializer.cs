using System;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;

using R5T.D0001;
using R5T.D0010;
using R5T.T0002;

using R5T.Magyar.Xml;


namespace R5T.T0004.Construction
{
    public class RelativeFilePathsVisualStudioProjectFileStreamSerializer : IRelativeFilePathsVisualStudioProjectFileStreamSerializer
    {
        private INowUtcProvider NowUtcProvider { get; }


        public RelativeFilePathsVisualStudioProjectFileStreamSerializer(
            INowUtcProvider nowUtcProvider)
        {
            this.NowUtcProvider = nowUtcProvider;
        }

        public Task<IVisualStudioProjectFile> DeserializeAsync(Stream stream, IMessageSink messageSink)
        {
            var xElement = XElement.Load(stream, LoadOptions.PreserveWhitespace); // Visual Studio project files have good whitespacing, so preserve.

            var projectXElement = new ProjectXElement(xElement);

            var xElementVisualStudioProjectFile = new XElementVisualStudioProjectFile(projectXElement);

            return Task.FromResult(xElementVisualStudioProjectFile as IVisualStudioProjectFile);
        }

        public async Task SerializeAsync(Stream stream, IVisualStudioProjectFile visualStudioProjectFile, IMessageSink messageSink)
        {
            if(visualStudioProjectFile is XElementVisualStudioProjectFile xElementVisualStudioProjectFile)
            {
                using (var xmlWriter = XmlWriterHelper.New(stream))
                {
                    var projectXElement = xElementVisualStudioProjectFile.ProjectXElement;

                    var xElement = projectXElement.Value;

                    xElement.Save(xmlWriter);
                }
            }
            else
            {
                await messageSink.AddErrorMessageAsync(this.NowUtcProvider, $"Input {nameof(visualStudioProjectFile)} was not a {nameof(XElementVisualStudioProjectFile)}.");
            }
        }
    }
}

