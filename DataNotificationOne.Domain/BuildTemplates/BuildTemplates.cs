namespace DataNotificationOne.Domain.BuildTemplates
{
    public class BuildTemplates
    {

        public string BuildDailyHtmlForClient(
               string templateHtml,
               string clientName,
               string symbol,
               DateTime date,
               decimal open,
               decimal high,
               decimal low,
               decimal close,
               decimal variation,
               bool isAlta,
               string messageIsAlta)
        {
            var isAltaLabel = isAlta ? "Alta" : "Baixa";
            var isAltaClass = isAlta ? "badge-up" : "badge-down";

            return templateHtml
                .Replace("{{ClientName}}", clientName ?? string.Empty)
                .Replace("{{Date}}", date.ToString("yyyy-MM-dd"))
                .Replace("{{Symbol}}", symbol)
                .Replace("{{Open}}", open.ToString("0.##"))
                .Replace("{{High}}", high.ToString("0.##"))
                .Replace("{{Low}}", low.ToString("0.##"))
                .Replace("{{Close}}", close.ToString("0.##"))
                .Replace("{{Variation}}", variation.ToString("0.##"))
                .Replace("{{IsAltaLabel}}", isAltaLabel)
                .Replace("{{IsAltaClass}}", isAltaClass)
                .Replace("{{MessageIsAlta}}", messageIsAlta ?? string.Empty);
        }
        //Genérico
        public string BuildDailyHtmlGeneric(
                string templateHtml,
                string symbol,
                DateTime date,
                decimal open,
                decimal high,
                decimal low,
                decimal close)
        {

            return templateHtml
                .Replace("{{Date}}", date.ToString("yyyy-MM-dd"))
                .Replace("{{Symbol}}", symbol)
                .Replace("{{Open}}", open.ToString("0.##"))
                .Replace("{{High}}", high.ToString("0.##"))
                .Replace("{{Low}}", low.ToString("0.##"))
                .Replace("{{Close}}", close.ToString("0.##"));
        }
        //HTML da Variancia do ativo
        public string BuildDailyHtmlVariance(
               string templateHtml,
               string symbol,
               DateTime date,
               decimal open,
               decimal high,
               decimal low,
               decimal close,
               decimal variation,
               bool isAlta,
               string messageIsAlta)
        {
            var isAltaLabel = isAlta ? "Alta" : "Baixa";
            var isAltaClass = isAlta ? "badge-up" : "badge-down";

            return templateHtml
                .Replace("{{Date}}", date.ToString("yyyy-MM-dd"))
                .Replace("{{Symbol}}", symbol)
                .Replace("{{Open}}", open.ToString("0.##"))
                .Replace("{{High}}", high.ToString("0.##"))
                .Replace("{{Low}}", low.ToString("0.##"))
                .Replace("{{Close}}", close.ToString("0.##"))
                .Replace("{{Variation}}", variation.ToString("0.##"))
                .Replace("{{IsAltaLabel}}", isAltaLabel)
                .Replace("{{IsAltaClass}}", isAltaClass)
                .Replace("{{MessageIsAlta}}", messageIsAlta ?? string.Empty);
        }
    }
}
