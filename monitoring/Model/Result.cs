using System;
using SolrNet.Attributes;
using System.Collections.Generic;
using SolrNet;

public class Result
{
    public List<Article> Articles { get; set; }
    public int ResultCount { get; set; }
}