function clickHandler(e) {
	
	chrome.pushMessaging.getChannelId(true, channelIdCallback);   

}

function channelIdCallback(message) 
{	
	var xhr = new XMLHttpRequest();
	xhr.open("GET", "http://diogorod-test.apigee.net/v1/monitoringphp/json.api?www-command=send-subscribe&channelID=" + message.channelId, true);
	xhr.onreadystatechange = function(response) {
		
		 if (xhr.readyState == 3) {			
			$( "#response" ).text( xhr.responseText );
		}
	}
	xhr.send();
	
}

document.addEventListener('DOMContentLoaded', function() {
  	
	
	document.getElementById('subscribe').addEventListener('click', clickHandler, false);

  
});