using System;
using System.Threading.Tasks;

using R5T.T0002;


namespace R5T.T0004.Construction
{
    public class NewXDocumentVisualStudioProjectFileGenerator : INewVisualStudioProjectFileGenerator
    {
        public Task<IVisualStudioProjectFile> CreateNewVisualStudioProjectFile()
        {
            var xDocumentVisualStudioProjectFile = XDocumentVisualStudioProjectFile.New();

            return Task.FromResult(xDocumentVisualStudioProjectFile as IVisualStudioProjectFile);
        }
    }
}
