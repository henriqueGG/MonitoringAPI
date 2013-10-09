using System;
using SolrNet.Attributes;
using System.Collections.Generic;

public class ArchiveArticle
{
    [SolrUniqueKey("id")]
    public string Id { get; set; }

    [SolrField("topics")]
    public ICollection<string> topics { get; set; }

    [SolrField("media_type")]
    public string media_type { get; set; }

    [SolrField("outlet_name")]
    public string outlet_name { get; set; }

    [SolrField("outlet_name_token")]
    public string outlet_name_token { get; set; }

    [SolrField("outlet_name_prefix")]
    public string outlet_name_prefix { get; set; }

    [SolrField("media_id")]
    public int media_id { get; set; }

    [SolrField("author")]
    public string author { get; set; }

    [SolrField("author_token")]
    public string author_token { get; set; }

    [SolrField("author_prefix")]
    public string author_prefix { get; set; }
   
    [SolrField("editor_id")]
    public int editor_id { get; set; }

    [SolrField("title")]
    public string title { get; set; }

    [SolrField("title_cs")]
    public string title_cs { get; set; }

    [SolrField("fulltext")]
    public string fulltext { get; set; }

    [SolrField("fulltext_cs")]
    public string fulltext_cs { get; set; }

    [SolrField("url")]
    public string url { get; set; }

    [SolrField("url_bigram")]
    public string url_bigram { get; set; }

    [SolrField("video_url")]
    public string video_url { get; set; }

    [SolrField("image_url")]
    public string image_url { get; set; }

    [SolrField("pub_date")]
    public DateTime pub_date { get; set; }

    [SolrField("pub_value")]
    public double pub_value { get; set; }

    [SolrField("feed_name")]
    public string feed_name { get; set; }

    [SolrField("indexed_date")]
    public DateTime indexed_date { get; set; }

    [SolrField("language")]
    public string language { get; set; }

    [SolrField("language_token")]
    public string language_token { get; set; }

    [SolrField("country")]
    public string country { get; set; }

    [SolrField("country_token")]
    public string country_token { get; set; }

    [SolrField("duplicate_group")]
    public ICollection<string> duplicate_group { get; set; }

    [SolrField("reach")]
    public double reach { get; set; }

    [SolrField("created_date")]
    public DateTime created_date { get; set; }    

    [SolrField("twitter_followers")]
    public long twitter_followers { get; set; }

    [SolrField("twitter_following")]
    public long twitter_following { get; set; }

    [SolrField("twitter_updates")]
    public long twitter_updates { get; set; }

    [SolrField("video_start_time")]
    public long video_start_time { get; set; }

    [SolrField("video_stop_time")]
    public long video_stop_time { get; set; }

    [SolrField("day_of_year")]
    public int day_of_year { get; set; }

    [SolrField("package")]
    public string package { get; set; }

    [SolrField("pub_value_native")]
    public double pub_value_native { get; set; }

    [SolrField("pub_value_currency")]
    public string pub_value_currency { get; set; }
 
    [SolrField("indexed_date_rounded")]
    public DateTime indexed_date_rounded { get; set; }

    [SolrField("pub_date_rounded")]
    public DateTime pub_date_rounded { get; set; }

    [SolrField("data_provider")]
    public string data_provider { get; set; }

    [SolrField("market_id")]
    public int market_id { get; set; }

    [SolrField("keyframe_url")]
    public string keyframe_url { get; set; }

    [SolrField("organizations")]
    public ICollection<string> organizations { get; set; }

    [SolrField("people")]
    public ICollection<string> people { get; set; }

    [SolrField("locations")]
    public ICollection<string> locations { get; set; }

    [SolrField("industries")]
    public ICollection<string> industries { get; set; }

    [SolrField("subjects")]
    public ICollection<string> subjects { get; set; }

    [SolrField("provider_outlet_name")]
    public string provider_outlet_name { get; set; }

    [SolrField("vendor_information")]
    public string vendor_information { get; set; }   
}