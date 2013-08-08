	  // Additional JS functions here
	  window.fbAsyncInit = function() {
	    FB.init({
	      appId      : '269931296468801', // App ID
	      channelUrl : 'http://worldcrime.devharis.com/', // Channel File
	      status     : true, // check login status
	      cookie     : true, // enable cookies to allow the server to access the session
	      xfbml      : true  // parse XFBML
	    });
		FB.Event.subscribe('auth.login', function(response) {
          window.location.reload();
        });
	
	    FB.getLoginStatus(function(response) {
	    	console.log(response);
		  if (response.status === 'connected') {
		    FB.api('/me', function(user) {
	            if (user) {
	              console.log(user);

	             
				  var location = user.location.name;
	              var imagesrc = 'https://graph.facebook.com/' + user.id + '/picture';
	              var name = user.name
	              $('#fb-root').append('<img src="'+imagesrc+'"></img><p><b>Welcome</b> '+name+'<p><b>Location:</b> '+location);
	            }
        	});
		  } else if (response.status === 'not_authorized') {
		  	 $('#fb-root').append('<a href="#" onclick="FB.login();"><img src="./img/fbicon.gif" /></a>');

		  } else {
		  	 $('#fb-root').append('<a href="#" onclick="FB.login();"><img src="./img/fbicon.gif" /></a>');
		  }
		 });
			
	  };

	  // Load the SDK Asynchronously
	  (function(d){
	     var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
	     if (d.getElementById(id)) {return;}
	     js = d.createElement('script'); js.id = id; js.async = true;
	     js.src = "//connect.facebook.net/en_US/all.js";
	     ref.parentNode.insertBefore(js, ref);
	   }(document));
	   function login() {
    	FB.login(function(response) {
		        if (response.authResponse) {
		            // connected
		        } else {
		            // cancelled
		        }
	    	});
		}
		function postToFeed() {
		var title = $("#moreInfo").find('h3').text();
		var text = $(".timeposition").text()+$("#moreInfo").find('div').text();
        // calling the API ...
        var obj = {
          method: 'feed',
          redirect_uri: 'http://worldcrime.devharis.com/',
          link: 'http://worldcrime.devharis.com/',
          picture: 'http://worldcrime.devharis.com/img/123.jpg',
          name: 'Globe News',
          caption: title,
          description: text
        };

        function callback(response) {
          document.getElementById('msg').innerHTML = "Post ID: " + response['post_id'];
        }

        FB.ui(obj, callback);
      }