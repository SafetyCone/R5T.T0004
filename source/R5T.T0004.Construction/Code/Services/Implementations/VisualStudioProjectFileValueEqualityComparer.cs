using System;
using System.Threading.Tasks;

using R5T.D0010;
using R5T.T0002;


namespace R5T.T0004.Construction
{
    public class VisualStudioProjectFileValueEqualityComparer : IVisualStudioProjectFileValueEqualityComparer
    {
        public Task<bool> Equals(IVisualStudioProjectFile x, IVisualStudioProjectFile y, IMessageSink messageSink)
        {
            var sdkEqual = x.SDK == y.SDK;

        }
    }
}
