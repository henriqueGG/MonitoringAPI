using System;
using SolrNet.Attributes;
using System.Collections.Generic;

public class Article
{
    [SolrUniqueKey("id")]
    public string Id { get; set; }

    [SolrField("Topics")]
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

    /*
    <fields>
    <!--  added 5/21/2012 PB for TFS Task 2496  -->
     * 
    <field name="topics" type="string" indexed="true" stored="true" multiValued="true"/>
    <field name="id" type="string" indexed="true" stored="true" required="true" multiValued="false"/>
    <field name="media_type" type="string" indexed="true" stored="true" multiValued="false" required="true"/>
    <field name="outlet_name" type="string" indexed="true" stored="true" multiValued="false" omitNorms="true" required="true"/>
    <field name="outlet_name_token" type="textSpell" indexed="true" stored="false"/>
    <field name="outlet_name_prefix" type="textPrefix" indexed="true" stored="false"/>
    <field name="media_id" type="integer" indexed="true" stored="true" multiValued="false"/>
    <field name="author" type="string" indexed="true" stored="true" compressed="true" omitNorms="true"/>
    <field name="author_token" type="textSpell" indexed="true" stored="false"/>
    <field name="author_prefix" type="textPrefix" indexed="true" stored="false"/>
     */
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



    /* 
   <field name="editor_id" type="integer" indexed="true" stored="true" omitNorms="true"/>
   <field name="title" type="text" indexed="true" stored="true" compressed="true" multiValued="false" termVectors="true"/>
   <field name="title_cs" type="text_cs" indexed="true" stored="false" multiValued="false"/>
   <field name="fulltext" type="text" indexed="true" stored="true" compressed="true" termVectors="true" multiValued="false"/>
   <field name="fulltext_cs" type="text_cs" indexed="true" stored="false" multiValued="false"/>
   <field name="url" type="string" indexed="false" stored="true" compressed="true" multiValued="false"/>
   <field name="url_bigram" type="text_includesBiGram" indexed="true" stored="false" multiValued="false"/>
   <field name="video_url" type="string" indexed="false" stored="true" compressed="true" multiValued="false"/>
   <field name="image_url" type="string" indexed="false" stored="true" compressed="true" multiValued="false"/>
     */

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
     /* 
   <field name="pub_date" type="date" indexed="true" stored="true" multiValued="false" omitNorms="true" required="true"/>
   <field name="pub_value" type="sdouble" indexed="true" stored="true"/>
   <field name="feed_name" type="string" indexed="true" stored="true" compressed="true"/>
   <field name="indexed_date" type="date" indexed="true" stored="true" default="NOW" multiValued="false" omitNorms="true"/>
   <field name="language" type="string" indexed="true" stored="true" compressed="true"/>
   <field name="language_token" type="textSpell" indexed="true" stored="false"/>
   <field name="country" type="string" indexed="true" stored="true" compressed="true"/>
   <field name="country_token" type="textSpell" indexed="true" stored="false"/>
   <field name="duplicate_group" type="string" indexed="true" stored="true" multiValued="true"/>
   <field name="reach" type="sdouble" indexed="true" stored="true" multiValued="false"/>
   <field name="created_date" type="date" indexed="true" stored="true" multiValued="false"/>
      */

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
 
      /* 
   <field name="twitter_followers" type="slong" indexed="true" stored="true" multiValued="false"/>
   <field name="twitter_following" type="slong" indexed="true" stored="true" multiValued="false"/>
   <field name="twitter_updates" type="slong" indexed="true" stored="true" multiValued="false"/>
   <field name="video_start_time" type="long" indexed="false" stored="true" multiValued="false"/>
   <field name="video_stop_time" type="long" indexed="false" stored="true" multiValued="false"/>
   <field name="day_of_year" type="integer" indexed="true" stored="true" multiValued="false"/>
   <field name="package" type="string" indexed="true" stored="true" multiValued="false"/>
   <!--  added 1/14/2010 CTJ for CP 137 requirements  -->
   <field name="pub_value_native" type="double" indexed="false" stored="true"/>
   <field name="pub_value_currency" type="string" indexed="false" stored="true"/>
   */


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

    /*<!--
    added 1/28/2010 CTJ to speed up date range searching 
   -->
   <field name="indexed_date_rounded" type="tdate" indexed="true" stored="true" default="NOW/MINUTE" multiValued="false" omitNorms="true"/>
   <field name="pub_date_rounded" type="tdate" indexed="true" stored="true" multiValued="false" omitNorms="true"/>
   <!--  added 7/16/2010 PB for CP 139 requirements  -->
   <field name="data_provider" type="string" indexed="true" stored="true"/>
   <field name="market_id" type="integer" indexed="true" stored="true"/>
   <!--  added 7/29/2010 PB for CP 139 requirements  -->
   <field name="keyframe_url" type="string" indexed="false" stored="true"/>
   <!--  added  3/30/2012 for CP 142 per DL  -->
   <field name="organizations" type="string" indexed="true" stored="true" multiValued="true"/>
   <field name="people" type="string" indexed="true" stored="true" multiValued="true"/>
   <field name="locations" type="string" indexed="true" stored="true" multiValued="true"/>
   <field name="industries" type="string" indexed="true" stored="true" multiValued="true"/>
   <field name="subjects" type="string" indexed="true" stored="true" multiValued="true"/>
   */
    
    /*
     
   <!--  added 9/18/2012 PB for TFS Task 3117  -->
   <field name="provider_outlet_name" type="string" indexed="true" stored="true" multiValued="false" omitNorms="true" required="false"/>
   <!--  added 11/08/2012 PB for Jira AS-2  -->
   <field name="vendor_information" type="string" indexed="false" stored="true" multiValued="false" omitNorms="true" required="false"/>
   </fields>
 
 
    <!--
    Valid attributes for fields:
        name: mandatory - the name for the field
        type: mandatory - the name of a previously defined type from the <types> section
        indexed: true if this field should be indexed (searchable or sortable)
        stored: true if this field should be retrievable
        compressed: [false] if this field should be stored using gzip compression
          (this will only apply if the field type is compressable; among
          the standard field types, only TextField and StrField are)
        multiValued: true if this field may contain multiple values per document
        omitNorms: (expert) set to true to omit the norms associated with
          this field (this disables length normalization and index-time
          boosting for the field, and saves some memory).  Only full-text
          fields or fields that need an index-time boost need norms.
        termVectors: [false] set to true to store the term vector for a given field.
          When using MoreLikeThis, fields used for similarity should be stored for 
          best performance.
        termPositions: Store position information with the term vector.  This will increase storage costs.
        termOffsets: Store offset information with the term vector. This will increase storage costs.
   
   -->*/
}