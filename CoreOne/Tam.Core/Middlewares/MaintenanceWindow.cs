using System;

namespace Tam.Core.Middlewares
{
    public class MaintenanceWindow
    {
        private Func<bool> enabledFunc;
        private byte[] response;
        public string Text { get; set; }
        public bool Enabled => enabledFunc();
        public int RetryAfterInSeconds { get; set; } = 3600;
        public string ContentType { get; set; } = "text/html";
        public byte[] Response => response;

        public MaintenanceWindow(Func<bool> enabledFunc, byte[] response)
        {
            this.enabledFunc = enabledFunc;
            this.response = response;
        }
    }
}