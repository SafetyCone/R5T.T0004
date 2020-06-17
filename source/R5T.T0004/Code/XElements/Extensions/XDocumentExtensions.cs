using System;
using System.Xml.Linq;


namespace R5T.T0004
{
    public static class XDocumentExtensions
    {
        public static VisualStudioProjectFileXDocument AsVisualStudioProjectFile(this XDocument xDocument)
        {
            var visualStudioProjectFileXDocument = new VisualStudioProjectFileXDocument(xDocument);
            return visualStudioProjectFileXDocument;
        }
    }
}
