using System;
using System.Threading.Tasks;

using R5T.T0002;


namespace R5T.T0004.Construction
{
    /// <summary>
    /// Allows serializing a <see cref="IVisualStudioProjectFile"/> to one path, while allowing the file to think it's being serialized to a different path (for determining any project-reference project file relative paths).
    /// </summary>
    public interface IAsFilePathVisualStudioProjectFileSerializer
    {
        Task SerializeAsync(string actualfilePath, string asFilePath, IVisualStudioProjectFile visualStudioProjectFile, bool overwrite = true);
    }
}
