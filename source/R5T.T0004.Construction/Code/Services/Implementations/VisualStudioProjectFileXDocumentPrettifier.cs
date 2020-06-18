using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.T0006;

using R5T.Magyar;
using R5T.Magyar.Xml;


namespace R5T.T0004.Construction
{
    /// <summary>
    /// Totally useless, because adding any XText nodes causes XmlWriter to disbale autoindent.
    /// </summary>
    public class VisualStudioProjectFileXDocumentPrettifier : IVisualStudioProjectFileXDocumentPrettifier
    {
        /// <summary>
        /// Two spaces.
        /// </summary>
        public const string Indent = "  ";
        public static readonly string NewLine = Environment.NewLine; // @"\r\n"; // "\r\n";


        private string GetIndent()
        {
            return VisualStudioProjectFileXDocumentPrettifier.Indent;
        }

        private string GetIndentForLevel(int level)
        {
            var indent = this.GetIndent();

            var output = StringHelper.Repeat(indent, level);
            return output;
        }

        private string GetNewLine()
        {
            return VisualStudioProjectFileXDocumentPrettifier.NewLine;
        }

        public Task Prettify(VisualStudioProjectFileXDocument visualStudioProjectFileXDocument)
        {
            var xProjectXElement = visualStudioProjectFileXDocument.GetXProjectXElement();

            this.PrettifyProjectXElement(xProjectXElement);

            return Task.CompletedTask;
        }

        private void PrettifyProjectXElement(XElement xProjectXElement)
        {
            // Ensures there are no empty [ItemGroup] elements ([ItemGroup] elements without children).
            var itemGroupElements = xProjectXElement.Elements(ProjectFileXmlElementName.ItemGroup);

            var emptyItemGroupElements = new List<XElement>();
            foreach (var itemGroupElement in itemGroupElements)
            {
                if (!itemGroupElement.HasElements)
                {
                    emptyItemGroupElements.Add(itemGroupElement);
                }
            }

            emptyItemGroupElements.ForEach(x => x.Remove());

            // Ensure all root <Project> element children are sandwiched by XText nodes containing the proper blank lines.
            var newLine = this.GetNewLine();
            var indent = this.GetIndentForLevel(1);
            var beforeElementText = $"{newLine}{newLine}{indent}";

            var xElements = xProjectXElement.Elements();
            foreach (var xElement in xElements)
            {
                var priorNode = xElement.PreviousNode;
                if(!priorNode.IsText())
                {
                    // Add a blank-line XText element just before the element.
                    var xText = new XText(beforeElementText);
                    xElement.AddBeforeSelf(xText);
                }
            }

            var afterElementText = $"{newLine}{newLine}";
            var lastNode = xProjectXElement.LastNode;
            if(!lastNode.IsText())
            {
                // Add a blank line after the last node.
                var xText = new XText(afterElementText);
                lastNode.AddAfterSelf(xText);
            }
            
            // Now format each child element, recursively.
            foreach (var xElement in xElements)
            {
                this.PrettifyChildren(xElement, 2);
            }

            // Now, ensure a blank line after the end tag for the project element.
            var lastNodeText = this.GetNewLine();
            var lastNodeInFile = xProjectXElement.NextNode;
            var hasLastNewLineNode = XNodeHelper.WasFound(lastNodeInFile);
            if(hasLastNewLineNode)
            {
                if(!lastNodeInFile.IsText())
                {
                    // Add a blank line after the project node.
                    var xText = new XText(lastNodeText);
                    xProjectXElement.AddAfterSelf(xText);
                }
            }
            else
            {
                // Add a blank line after the project node.
                var xText = new XText(lastNodeText);
                xProjectXElement.AddAfterSelf(xText);
            }
        }

        private void PrettifyChildren(XElement xElement, int level)
        {
            var nodeIsLeaf = XElementHelper.IsLeaf(xElement);
            if (nodeIsLeaf)
            {
                return;
            }

            var newLine = this.GetNewLine();

            var indentBeforeElement = this.GetIndentForLevel(level);
            var beforeElementText = $"{newLine}{indentBeforeElement}";

            var childXElements = xElement.Elements().ToList();
            foreach (var childXElement in childXElements)
            {
                var priorNode = childXElement.PreviousNode;
                if (!priorNode.IsText())
                {
                    // Add a blank-line XText element just before the element.
                    var xText = new XText(beforeElementText);
                    childXElement.AddBeforeSelf(xText);
                }
            }

            var indentAfterElement = this.GetIndentForLevel(level - 1);
            var afterElementText = $"{newLine}{indentAfterElement}";

            var lastNode = xElement.LastNode;
            if (!lastNode.IsText())
            {
                // Add a blank line after the last node.
                var xText = new XText(afterElementText);
                lastNode.AddAfterSelf(xText);
            }

            // Now format each child element, recursively.
            foreach (var childXElement in childXElements)
            {
                this.PrettifyChildren(childXElement, level + 1);
            }
        }
    }
}
