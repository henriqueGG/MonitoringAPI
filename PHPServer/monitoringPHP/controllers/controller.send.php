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


class WWW_controller_send extends WWW_Factory {
	
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
	
	public function notification($configuration){
	
		$results = false;		
		
		$msg = isset($configuration['msg']) ? $configuration['msg'] : false;

		//Titanium notification

        $key        = "I3elZAPUjQeELY1GoVSMvUGrCI0d4kvN";
	    $username   = "cisionpush";
	    $password   = "cisionpush";
	    $channel    = "alert";
	    $message    = $msg;
	    $title      = "Cision Notify";
	    $tmp_fname  = 'cookie.txt';
	    $json       = '{"alert":"'. $message .'","title":"'. $title .'","vibrate":true,"sound":"default"}';
	 
	    /*** PUSH NOTIFICATION ***********************************/
	 
	    $post_array = array('login' => $username, 'password' => $password);
	 
	    /*** INIT CURL *******************************************/
	    $curlObj    = curl_init();
	    $c_opt      = array(CURLOPT_URL => 'http://api.cloud.appcelerator.com/v1/users/login.json?key='.$key,
	                        CURLOPT_COOKIEJAR => $tmp_fname, 
	                        CURLOPT_COOKIEFILE => $tmp_fname, 
	                        CURLOPT_RETURNTRANSFER => true, 
	                        CURLOPT_POST => 1,
	                        CURLOPT_POSTFIELDS  =>  "login=".$username."&password=".$password,
	                        CURLOPT_FOLLOWLOCATION  =>  1,
	                        CURLOPT_TIMEOUT => 60,
	                        CURLOPT_PROXY => "proxyinfor",
							CURLOPT_PROXYPORT => 8080
					);
	 
	    /*** LOGIN **********************************************/
	    curl_setopt_array($curlObj, $c_opt); 
	    $session = curl_exec($curlObj);     		
		
	 
	    /*** SEND PUSH ******************************************/
	    $c_opt[CURLOPT_URL]         = "http://api.cloud.appcelerator.com/v1/push_notification/notify.json?key=".$key; 
	    $c_opt[CURLOPT_POSTFIELDS]  = "channel=".$channel."&payload=".$json; 
	 
	    curl_setopt_array($curlObj, $c_opt); 
	    $session = curl_exec($curlObj);     
	 
	    /*** THE END ********************************************/
	    curl_close($curlObj);

	    $results["responseTitanium"] = $session;



	    //Chrome Extension notification

	    $url ="https://accounts.google.com/o/oauth2/token";
	
		$data = array(
	        'client_id' => "544402308341.apps.googleusercontent.com",
	        'client_secret' => "4r1KZ1CPi3sYH6OFnG0TSZMl",
	        'refresh_token'=> "1/2s8_Rn0FrFCzT8qK95434RZdG00H2YECwMgbApTMBCc",
			'grant_type'=> "refresh_token"
	    );
		 
	    $ch = curl_init("https://accounts.google.com/o/oauth2/token");    
		
		curl_setopt($ch, CURLOPT_POST, true);
	    curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
	    curl_setopt($ch, CURLOPT_HTTPAUTH, CURLAUTH_ANY);
	    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
	    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
		curl_setopt($ch, CURLOPT_PROXY, "proxyinfor");
	    curl_setopt($ch, CURLOPT_PROXYPORT, 8080);
		
	    $result = curl_exec($ch);
		
	    
		$access_token= json_decode($result,true)['access_token'];
	 
		$url ="https://www.googleapis.com/gcm_for_chrome/v1/messages";
		
		$m = new Mongo("mongodb://10.4.0.133");

		$db = $m->chromeExtension;

		$collection = $db->channelRegisters;
		
		$cursor = $collection->find();
		
		foreach ($cursor as $doc) {
			$data = json_encode(array(
	        'channelId' => $doc["channelID"],
	        'subchannelId' => "1",
	        'payload'=> $msg
			));
		 
			$ch = curl_init();
			$curlConfig = array(
				CURLOPT_URL            => "https://www.googleapis.com/gcm_for_chrome/v1/messages",
				CURLOPT_POST           => true,
				CURLOPT_RETURNTRANSFER => true,
				CURLOPT_POSTFIELDS     => $data,
				CURLOPT_SSL_VERIFYPEER => false,
				CURLOPT_PROXY => "proxyinfor",
				CURLOPT_PROXYPORT => 8080,
				CURLOPT_HTTPHEADER     => array(
					'Authorization: Bearer ' . $access_token,
					'Content-Type: application/json'
				)
			);
			curl_setopt_array($ch, $curlConfig);
			$result = curl_exec($ch);
			if(strstr($result,"error"))
			{
				$results["responseChromeExtensions"] = $result;
			}
			else
				$results["responseChromeExtensions"] = "Successfully sent message to Chrome";		
		}
		
		// ID needs to be set
		if(isset($results["responseTitanium"])){
			// Loading the model and data to the model
			$data=$this->getModel('send');
			if($data->load($results)){
				// Returning an array representation of the model data
				return $data->notification();
			} else {
				// Action failed because entry was not found
				return $this->resultFalse('Entry not found');
			}
		} else {
			// Action failed because incorrect request was made to the controller
			return $this->resultError('ID not defined');
		}
		
	}

	public function subscribe($configuration){

		$channelID = isset($configuration['channelID']) ? $configuration['channelID'] : false;

		if(!empty($channelID)){

			$m = new Mongo("mongodb://10.4.0.133");

			$db = $m->chromeExtension;

			$collection = $db->channelRegisters;
			
			$query = array('channelID' => $channelID);
			
			if($collection->count($query) > 0){
				$results["response"] = 'allready inserted';

			}else{
				$results["response"] = 'success';

				$collection->insert($query);
			}
		}
		else{
			$results["response"] = 'fail';
		}

		// ID needs to be set
		if(isset($results["response"])){
			// Loading the model and data to the model
			$data=$this->getModel('send');
			if($data->load($results)){
				// Returning an array representation of the model data
				return $data->notification();
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