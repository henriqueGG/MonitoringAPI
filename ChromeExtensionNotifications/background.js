function messageReceived(message) {

	var notification = window.webkitNotifications.createNotification(
      'icon.jpg', 'Push Message',
       message.payload );
      
	notification.show();  
}

function channelIdCallback(message) {
	chrome.app.window.create("PushHome.html?channelId="+message.channelId);
}

// This function gets called in the packaged app model on launch.
chrome.app.runtime.onLaunched.addListener(function(launchData) {	
  	chrome.pushMessaging.getChannelId(true, channelIdCallback);
});

//This is called when the extension is installed.
chrome.runtime.onInstalled.addListener(function(){
	chrome.pushMessaging.getChannelId(true, channelIdCallback);	
});

chrome.pushMessaging.onMessage.addListener(messageReceived);
