using System;
using SolrNet.Attributes;
using System.Collections.Generic;
using SolrNet;

public class Result
{
    public SolrQueryResults<Article> Articles { get; set; }
    public int ResultCount { get; set; }
}