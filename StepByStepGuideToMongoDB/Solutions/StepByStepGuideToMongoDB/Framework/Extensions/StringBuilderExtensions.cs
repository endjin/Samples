namespace StepByStepGuideToMongoDB.Framework.Extensions
{
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static void AppendIfValueNotEmpty(this StringBuilder sb, string key, string value)
        {
            if (!string.IsNullOrEmpty(value.Trim()))
            {
                sb.Append(key);
                sb.AppendLine(value);
            }
        }
    }
}