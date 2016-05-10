using System;

namespace DotaApiViewer
{
    internal class DotApiViewerException : Exception
    {
        public DotApiViewerException()
        {
        }

        public DotApiViewerException(string message) : base(message)
        {
        }

        public DotApiViewerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}