using System;
using System.Text.Json.Serialization;

namespace DataNotificationOne.Domain.Models
{
    public class OverviewModel
    {
        public string? Symbol { get; set; }
        public string? AssetType { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CIK { get; set; }
        public string? Exchange { get; set; }
        public string? Currency { get; set; }
        public string? Country { get; set; }
        public string? Sector { get; set; }
        public string? Industry { get; set; }
        public string? Address { get; set; }
        public string? OfficialSite { get; set; }
        public string? FiscalYearEnd { get; set; }

        [JsonPropertyName("LatestQuarter")]
        public DateTime? LatestQuarter { get; set; }

        public decimal? MarketCapitalization { get; set; }
        public decimal? EBITDA { get; set; }
        public decimal? PERatio { get; set; }
        public decimal? PEGRatio { get; set; }
        public decimal? BookValue { get; set; }
        public decimal? DividendPerShare { get; set; }
        public decimal? DividendYield { get; set; }
        public decimal? EPS { get; set; }
        public decimal? RevenuePerShareTTM { get; set; }
        public decimal? ProfitMargin { get; set; }
        public decimal? OperatingMarginTTM { get; set; }
        public decimal? ReturnOnAssetsTTM { get; set; }
        public decimal? ReturnOnEquityTTM { get; set; }
        public decimal? RevenueTTM { get; set; }
        public decimal? GrossProfitTTM { get; set; }
        public decimal? DilutedEPSTTM { get; set; }
        public decimal? QuarterlyEarningsGrowthYOY { get; set; }
        public decimal? QuarterlyRevenueGrowthYOY { get; set; }
        public decimal? AnalystTargetPrice { get; set; }

        public int? AnalystRatingStrongBuy { get; set; }
        public int? AnalystRatingBuy { get; set; }
        public int? AnalystRatingHold { get; set; }
        public int? AnalystRatingSell { get; set; }
        public int? AnalystRatingStrongSell { get; set; }

        public decimal? TrailingPE { get; set; }
        public decimal? ForwardPE { get; set; }
        public decimal? PriceToSalesRatioTTM { get; set; }
        public decimal? PriceToBookRatio { get; set; }
        public decimal? EVToRevenue { get; set; }
        public decimal? EVToEBITDA { get; set; }
        public double? Beta { get; set; }

        [JsonPropertyName("52WeekHigh")]
        public decimal? Week52High { get; set; }

        [JsonPropertyName("52WeekLow")]
        public decimal? Week52Low { get; set; }

        [JsonPropertyName("50DayMovingAverage")]
        public decimal? MovingAverage50Day { get; set; }

        [JsonPropertyName("200DayMovingAverage")]
        public decimal? MovingAverage200Day { get; set; }

        public long? SharesOutstanding { get; set; }
        public long? SharesFloat { get; set; }
        public decimal? PercentInsiders { get; set; }
        public decimal? PercentInstitutions { get; set; }

        [JsonPropertyName("DividendDate")]
        public DateTime? DividendDate { get; set; }

        [JsonPropertyName("ExDividendDate")]
        public DateTime? ExDividendDate { get; set; }
    }
}
