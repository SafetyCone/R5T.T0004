using System;
using System.IO;
using System.Threading.Tasks;

using R5T.D0010;
using R5T.T0002;


namespace R5T.T0004.Construction
{
    /// <summary>
    /// Produces a <see cref="IVisualStudioProjectFile"/> where file paths (like project-references) are still relative file paths.
    /// </summary>
    public interface IRelativeFilePathsVisualStudioProjectFileStreamSerializer
    {
        Task<IVisualStudioProjectFile> DeserializeAsync(Stream stream, IMessageSink messageSink);
        Task SerializeAsync(Stream stream, IVisualStudioProjectFile visualStudioProjectFile, IMessageSink messageSink);
    }
}
