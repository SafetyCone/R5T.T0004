using System;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace R5T.T0004.Construction
{
    /// <summary>
    /// Prettifies the XElement structure of a Visual Studio project file <see cref="ProjectXElement"/>.
    /// 1) Ensures all root [Project] element children are sandwiched by XText nodes containing blank lines.
    /// 2) Ensures there are no empty [ItemGroup] elements ([ItemGroup] elements without children).
    /// </summary>
    /// <remarks>
    /// Note that adding ANY XText nodes causes the XmlWriter to stop auto-indenting. This is insanely obnoxious, but means that if you take responsibility for prettifying the XElement, you have to take FULL resposibility for prettifying the XElement.
    /// </remarks>
    public interface IVisualStudioProjectFileXDocumentPrettifier
    {
        Task Prettify(VisualStudioProjectFileXDocument visualStudioProjectFileXDocument);
    }
}
