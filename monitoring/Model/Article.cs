using System;
using SolrNet.Attributes;
using System.Collections.Generic;

public class Article
{
    public string Id { get; set; }
    public List<string> Topics { get; set; }
    public string SourceName { get; set; } //Outlet Name
    public string AuthorName { get; set; } //author
    public string HeadLine { get; set; } //title
    public string BodyText { get; set; } //fulltext
    public string UrlAddress { get; set; } //url
    public DateTime PublicationDate { get; set; } //pub_date
    public string Language { get; set; } //language
    public DateTime CreateDate { get; set; }
    public string ServiceName { get; set; }
}