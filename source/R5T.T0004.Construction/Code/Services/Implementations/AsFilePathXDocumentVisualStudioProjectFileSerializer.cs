using System;
using System.Threading.Tasks;

using R5T.D0010;
using R5T.D0017;

using R5T.Magyar.IO;


namespace R5T.T0004.Construction
{
    public class AsFilePathXDocumentVisualStudioProjectFileSerializer : IAsFilePathXDocumentVisualStudioProjectFileSerializer
    {
        private IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer RelativePathsXDocumentVisualStudioProjectFileStreamSerializer { get; }
        private IFunctionalVisualStudioProjectFileSerializationModifier FunctionalVisualStudioProjectFileSerializationModifier { get; }
        private IMessageSink MessageSink { get; }
        private IVisualStudioProjectFileXDocumentPrettifier VisualStudioProjectFileXDocumentPrettifier { get; }


        public AsFilePathXDocumentVisualStudioProjectFileSerializer(
            IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer relativePathsXDocumentVisualStudioProjectFileStreamSerializer,
            IFunctionalVisualStudioProjectFileSerializationModifier functionalVisualStudioProjectFileSerializationModifier,
            IMessageSink messageSink,
            IVisualStudioProjectFileXDocumentPrettifier visualStudioProjectFileXDocumentPrettifier)
        {
            this.RelativePathsXDocumentVisualStudioProjectFileStreamSerializer = relativePathsXDocumentVisualStudioProjectFileStreamSerializer;
            this.FunctionalVisualStudioProjectFileSerializationModifier = functionalVisualStudioProjectFileSerializationModifier;
            this.MessageSink = messageSink;
            this.VisualStudioProjectFileXDocumentPrettifier = visualStudioProjectFileXDocumentPrettifier;
        }

        public async Task SerializeAsync(string actualfilePath, string asFilePath, XDocumentVisualStudioProjectFile xElementVisualStudioProjectFile, bool overwrite = true)
        {
            // Modify.
            var modifiedXElementVisualStudioProjectFile = await this.FunctionalVisualStudioProjectFileSerializationModifier.ModifySerializationAsync(xElementVisualStudioProjectFile, asFilePath, this.MessageSink);

            // Prettify.
            await this.VisualStudioProjectFileXDocumentPrettifier.Prettify(modifiedXElementVisualStudioProjectFile.VisualStudoProjectFileXDocument);

            // Serialize.
            using (var fileStream = FileStreamHelper.NewWrite(actualfilePath, overwrite))
            {
                await this.RelativePathsXDocumentVisualStudioProjectFileStreamSerializer.SerializeAsync(fileStream, modifiedXElementVisualStudioProjectFile, this.MessageSink);
            }
        }
    }
}
