using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet.Attributes;

public class SolrWISEdoc
{
        
    [SolrField("Id")]
    public string Id { get; set; }

    [SolrField("WiseId")]
    public string WiseId { get; set; }

    [SolrField("HeadLine")]
    public string HeadLine { get; set; }

    [SolrField("BodyText")]
    public string BodyText { get; set; }

    [SolrField("UrlAddress")]
    public string UrlAddress { get; set; }

    [SolrField("OriginalUrlAddress")]
    public string OriginalUrlAddress { get; set; }

    [SolrField("UrlId")]
    public int UrlId { get; set; }

    [SolrField("HostName")]
    public string HostName { get; set; }

    [SolrField("HostId")]
    public int HostId { get; set; }

    [SolrField("SourceName")]
    public string SourceName { get; set; }

    [SolrField("SourceId")]
    public int SourceId { get; set; }

    [SolrField("SourceAddress")]
    public string SourceAddress { get; set; }

    [SolrField("AuthorName")]
    public string AuthorName { get; set; }

    [SolrField("SiteName")]
    public string SiteName { get; set; }

    [SolrField("SiteId")]
    public int SiteId { get; set; }

    [SolrField("Location")]
    public string Location { get; set; }

    [SolrField("LocationId")]
    public int LocationId { get; set; }

    [SolrField("Language")]
    public string Language { get; set; }

    [SolrField("LanguageId")]
    public int LanguageId { get; set; }

    [SolrField("Enconding")]
    public string Enconding { get; set; }

    [SolrField("EncondingId")]
    public int EncondingId { get; set; }

    [SolrField("SystemId")]
    public int SystemId { get; set; }

    [SolrField("Topics")]
    public string[] Topics { get; set; }

    [SolrField("PublicationDate")]
    public DateTime PublicationDate { get; set; }

    [SolrField("DateYear")]
    public int DateYear { get; set; }

    [SolrField("DateMonth")]
    public int DateMonth { get; set; }

    [SolrField("DateDay")]
    public int DateDay { get; set; }

    [SolrField("DateHour")]
    public int DateHour { get; set; }

    [SolrField("DateMinute")]
    public int DateMinute { get; set; }

    [SolrField("DateSecond")]
    public int DateSecond { get; set; }

    [SolrField("timestamp")]
    public DateTime timestamp { get; set; }

    [SolrField("AEV_EURO")]
    public string AEV_EURO { get; set; }

    [SolrField("AEV_DOLAR")]
    public string AEV_DOLAR { get; set; }        

    [SolrField("MediaCirculation")]
    public string MediaCirculation { get; set; }

}




