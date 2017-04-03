namespace Client.Helpers
{
    public static class UrlHelper
    {
        public static string AppendParameter(string url, string name, string value)
        {
            if (value == null) return url;
            url += url.Contains("?") ? "&" : "?";
            url += name + "=" + System.Uri.EscapeDataString(value);
            return url;
        }
    }
}
