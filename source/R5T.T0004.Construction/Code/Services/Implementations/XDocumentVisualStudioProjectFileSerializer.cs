using System;
using System.Threading.Tasks;

using R5T.D0010;
using R5T.D0017;
using R5T.D0022;

using R5T.Magyar.IO;


namespace R5T.T0004.Construction
{
    public class XDocumentVisualStudioProjectFileSerializer : IXDocumentVisualStudioProjectFileSerializer
    {
        private IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer RelativePathsXElementVisualStudioProjectFileStreamSerializer { get; }
        private IFunctionalVisualStudioProjectFileSerializationModifier FunctionalVisualStudioProjectFileSerializationModifier { get; }
        private IMessageSink MessageSink { get; }
        private IVisualStudioProjectFileXDocumentPrettifier ProjectXElementPrettifier { get; }


        public XDocumentVisualStudioProjectFileSerializer(
            IRelativePathsXDocumentVisualStudioProjectFileStreamSerializer relativePathsXElementVisualStudioProjectFileStreamSerializer,
            IFunctionalVisualStudioProjectFileSerializationModifier functionalVisualStudioProjectFileSerializationModifier,
            IMessageSink messageSink,
            IVisualStudioProjectFileXDocumentPrettifier projectXElementPrettifier)
        {
            this.RelativePathsXElementVisualStudioProjectFileStreamSerializer = relativePathsXElementVisualStudioProjectFileStreamSerializer;
            this.FunctionalVisualStudioProjectFileSerializationModifier = functionalVisualStudioProjectFileSerializationModifier;
            this.MessageSink = messageSink;
            this.ProjectXElementPrettifier = projectXElementPrettifier;
        }

        public async Task<XDocumentVisualStudioProjectFile> DeserializeAsync(string projectFilePath)
        {
            using (var fileStream = FileStreamHelper.NewRead(projectFilePath))
            {
                // Deserialize.
                var xElementVisualStudioProjectFile = await this.RelativePathsXElementVisualStudioProjectFileStreamSerializer.DeserializeAsync(fileStream, this.MessageSink);

                // Modify.
                var modifiedXElementVisualStudioProjectFile = await this.FunctionalVisualStudioProjectFileSerializationModifier.ModifyDeserializeationAsync(xElementVisualStudioProjectFile, projectFilePath, this.MessageSink);
                return modifiedXElementVisualStudioProjectFile;
            }
        }

        public async Task SerializeAsync(string projectFilePath, XDocumentVisualStudioProjectFile xElementVisualStudioProjectFile, bool overwrite = true)
        {
            // Modify.
            var modifiedXElementVisualStudioProjectFile = await this.FunctionalVisualStudioProjectFileSerializationModifier.ModifySerializationAsync(xElementVisualStudioProjectFile, projectFilePath, this.MessageSink);

            // Prettify.
            await this.ProjectXElementPrettifier.Prettify(modifiedXElementVisualStudioProjectFile.VisualStudoProjectFileXDocument);

            // Serialize.
            using (var fileStream = FileStreamHelper.NewWrite(projectFilePath, overwrite))
            {
                await this.RelativePathsXElementVisualStudioProjectFileStreamSerializer.SerializeAsync(fileStream, modifiedXElementVisualStudioProjectFile, this.MessageSink);
            }   
        }
    }
}
