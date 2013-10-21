<?php

/**
 * MyProjectNameHere <http://www.example.com>
 * Controller Class
 *
 * It is recommended to extend Controller classes from WWW_Factory in order to 
 * provide various useful functions and API access for the Controller.
 *
 * @package    Factory
 * @author     DeveloperNameHere <email@example.com>
 * @copyright  Copyright (c) 2012, ProjectOwnerNameHere
 * @license    Unrestricted
 * @tutorial   /doc/pages/guide_mvc.htm
 * @since      1.0.0
 * @version    1.0.0
 */


class WWW_controller_newsitems extends WWW_Factory {
	
	/**
	 * Simple example call to get data
	 *
	 * Please note that only public methods can be called through API, protected 
	 * and private methods remain hidden. This method would be accessible over API 
	 * with 'www-command=example-get' call.
	 *
	 * @param array $input input data sent to controller
	 * @input [key] This key is one of the accepted input values
	 * @return array
	 * @output [key] This is an output value that might exist in the output array
	 * @response [500] Data returned
	 */
	
	public function get($configuration){
	
		$results = false;		
		
		$fulltext = isset($configuration['fulltext']) ? $configuration['fulltext'] : false;
		$since = isset($configuration['since']) ? $configuration['since'] : false;
		$page = isset($configuration['page']) ? $configuration['page'] : false;
		$itemsPerPage = isset($configuration['itemsPerPage']) ? $configuration['itemsPerPage'] : false;
		$source = isset($configuration['source']) ? $configuration['source'] : false;
		
		$queryS3 = '';
		if ($fulltext){
			$queryS3 = 'fulltext:' . $fulltext;
		}

		if ($since){
			if ($fulltext){
				$queryS3 .= ' AND ';
			}
			$queryS3 .= 'pub_date:[ ' . $since  . ' TO * ]';
		}

		$queryWISE = '';
		if ($fulltext){
			$queryWISE = 'BodyText:' . $fulltext;
		}

		if ($since){
			if ($fulltext){
				$queryWISE .= ' AND ';
			}
			$queryWISE .= 'PublicationDate:[ ' . $since  . ' TO * ]';
		}
		
		require_once('Apache/Solr/Service.php');
		
		$solrS3 = new Apache_Solr_Service('ssindx13.qwestcolo.local', 8080, '/solr/agents/');
		$solrWISE = new Apache_Solr_Service('10.4.0.133', 8080, '/solr/wise/');
		
		if ($itemsPerPage <> true){
		$itemsPerPage = 20;
		}

		if ($page <> true){
		$page = 0;
		}

		if ($source == "all" || $source == null)
		{
			$source = '';
			$splitedItemsPerPage = $itemsPerPage / 2;
		}
		else
		{
			$splitedItemsPerPage = $itemsPerPage;
		}

		$searchOptionsS3 = array( 'sort' => 'pub_date desc' );
		$searchOptionsWise = array( 'sort' => 'PublicationDate desc' );
		
		try
		{
		switch ($source)
		{
			case "wise": 
				$resultsWISE = $solrWISE->search($queryWISE, $page * $itemsPerPage, $itemsPerPage);		
				break;
			case "s3": 
				$resultsS3 = $solrS3->search($queryS3, $page * $itemsPerPage, $itemsPerPage);		
				break;
			default:
				$resultsS3 = $solrS3->search($queryS3, $page * $splitedItemsPerPage, $splitedItemsPerPage);
				$resultsWISE = $solrWISE->search($queryWISE, $page * $splitedItemsPerPage, $splitedItemsPerPage);		
				break;
		}      
		}
		catch (Exception $e)
		{
		// in production you'd probably log or email this error to an admin
			// and then show a special message to the user but for this example
			// we're going to show the full exception
			die("<html><head><title>SEARCH EXCEPTION</title><body><pre>{$e->__toString()}</pre></body></html>");
		}

		$results["results"] = array();
		
		$totalWise = 0;
		// display results
		if (isset($resultsWISE))
		{
		  $totalWise = (int) $resultsWISE->response->numFound; 

		  foreach ($resultsWISE->response->docs as $doc)
		  {		

			$article = array();
			
			$article["Id"] = $doc->Id;
			$article["Topics"] = $doc->Topics;
			$article["SourceName"] = $doc->SourceName;
			$article["AuthorName"] = $doc->AuthorName;
			$article["HeadLine"] = $doc->HeadLine;
			$article["BodyText"] = $doc->BodyText;
			$article["UrlAddress"] = $doc->UrlAddress;
			$article["PublicationDate"] = $doc->PublicationDate;
			$article["Language"] = $doc->Language;
			$article["CreateDate"] = $doc->PublicationDate;
			$article["ServiceName"] = 'wise';
			
			$results["results"][] = $article;
		  } 
		}

		$totalS3 = 0;
		// display results
		if (isset($resultsS3))
		{
		  $totalS3 = (int) $resultsS3->response->numFound;  

		  foreach ($resultsS3->response->docs as $doc)
		  {
			$article = array();
			
			$article["Id"] = $doc->id;
			$article["Topics"] = $doc->topics;
			$article["SourceName"] = $doc->outlet_name;
			$article["AuthorName"] = $doc->author;
			$article["HeadLine"] = $doc->title;
			$article["BodyText"] = $doc->fulltext;
			$article["UrlAddress"] = $doc->url;
			$article["PublicationDate"] = $doc->pub_date;
			$article["Language"] = $doc->language;
			$article["CreateDate"] = $doc->created_date;
			$article["ServiceName"] = 'S3';
			
			$results["results"][] = $article;
		  } 
		}

		$results["resultCount"] = $totalS3 + $totalWise;

		
		// ID needs to be set
		if(isset($results["resultCount"])){
			// Loading the model and data to the model
			$data=$this->getModel('newsitems');
			if($data->load($results)){
				// Returning an array representation of the model data
				return $data->get();
			} else {
				// Action failed because entry was not found
				return $this->resultFalse('Entry not found');
			}
		} else {
			// Action failed because incorrect request was made to the controller
			return $this->resultError('ID not defined');
		}
		
	}
	
	/**
	 * Simple example call to get multiple database rows
	 *
	 * @param array $input input data sent to controller
	 * @input [key] This key is one of the accepted input values
	 * @return array
	 * @output [key] This is an output value that might exist in the output array
	 * @response [500] Data returned
	 */
	public function all($configuration){
	
		// Loading the model and sending input data to the request
		$data=$this->getModel('example');
		$data=$data->all($configuration);
		
		// This returns empty array if none were found
		if($data){
			return $this->resultTrue('Request complete',$data);
		} else {
			return $this->resultFalse('Search failed');
		}
		
	}
	
	/**
	 * Simple example call to add rows to database
	 *
	 * @param array $input input data sent to controller
	 * @input [key] This key is one of the accepted input values
	 * @return array
	 * @output [key] This is an output value that might exist in the output array
	 * @response [500] Data returned
	 */
	public function add($input){
			
		// This flag checks if error has been encountered during form validation
		$errorsEncountered=array();
		$errorFields=array();
		
		// Validating input
		if(isset($input['name']) && trim($input['name'])!=''){
			$input['name']=trim($input['name']);
		} else {
			$errorFields['name']=true;
			$errorsEncountered[]='name-incorrect';
		}
		
		// Data is only added if no errors were encountered
		if(empty($errorsEncountered)){
			// Getting model and setting the parameters
			$data=$this->getModel('example');
			$data->name=$input['name'];
			// Attempting to save
			if($data->save()){
				return $this->resultTrue('Entry added');
			} else {
				return $this->resultError('Failed to add entry');
			}
		} else {
			return $this->resultFalse('Input data incorrect');
		}
		
	}

	/**
	 * Simple example call to edit rows to database
	 *
	 * @param array $input input data sent to controller
	 * @input [key] This key is one of the accepted input values
	 * @return array
	 * @output [key] This is an output value that might exist in the output array
	 * @response [500] Data returned
	 */
	public function edit($input){
	
		// ID has to be set when editing
		if(isset($input['id'])){
			
			// This flag checks if error has been encountered during form validation
			$errorsEncountered=array();
			$errorFields=array();
			
			// Validating input
			if(isset($input['name']) && trim($input['name'])!=''){
				$input['name']=trim($input['name']);
			} else {
				$errorFields['name']=true;
				$errorsEncountered[]='name-incorrect';
			}
			
			// Data is only edited if no errors were encountered
			if(empty($errorsEncountered)){
				// Getting model
				$data=$this->getModel('example');
				// Data loading has to work based on the provided ID
				if($data->load($input['id'])){
					// Setting the changed input parameters
					$data->name=$input['name'];
					// Attempting to save
					if($data->save()){
						return $this->resultTrue('Entry edited');
					} else {
						return $this->resultError('Failed to edit entry');
					}
				} else {
					return $this->resultFalse('Entry ID not found');
				}
			} else {
				return $this->resultFalse('Input data incorrect');
			}
				
		} else {
			return $this->resultFalse('Entry ID is missing');
		}
		
	}

	/**
	 * Simple example call to delete a row from database
	 *
	 * @param array $input input data sent to controller
	 * @input [key] This key is one of the accepted input values
	 * @return array
	 * @output [key] This is an output value that might exist in the output array
	 * @response [500] Data returned
	 */
	public function delete($input){
	
		// ID has to be set when deleting
		if(isset($input['id'])){
			// Getting model
			$data=$this->getModel('example');
			// Attempting to delete
			if($data->delete($input['id'])){
				return $this->resultTrue('Entry deleted');
			} else {
				return $this->resultError('Failed to delete entry');
			}
		} else {
			return $this->resultFalse('Entry ID is missing');
		}
		
	}
	
}
	
?>