using System;
using System.Threading.Tasks;

using R5T.T0002;


namespace R5T.T0004.Construction
{
    /// <summary>
    /// Working at the <see cref="IVisualStudioProjectFile"/>-level of abstraction, transforms one instance to be equal to the other instance.
    /// </summary>
    public interface IVisualStudioProjectFileTransformer
    {
        Task CopySourceToDestinationAsync(IVisualStudioProjectFile source, IVisualStudioProjectFile destination);
    }
}
