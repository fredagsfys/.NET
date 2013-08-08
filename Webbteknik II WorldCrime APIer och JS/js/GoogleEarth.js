		var ge;

    	google.load("earth", "1");
     	google.load("maps", "2");
     	
		function init() 
	    {
	      google.earth.createInstance('maps', initCallback, failureCallback);

	      addSampleUIHtml(
	        '<input id="location" type="text" value="" placeholder="Place to go..."/>'
	      );

	      addSampleButton();
		}

	    function initCallback(instance) 
	    {
	      ge = instance;
	      ge.getWindow().setVisibility(true);
	      ge.getNavigationControl().setVisibility(ge.VISIBILITY_SHOW);

	      $.ajaxSetup({
				url : "/source/swedenXML.xml",
				type : "get",

				headers : {
					"Accept" : "application/xml",
					"Content-type" : "application/x-www-form-urlencoded"
				}
			});

			$.ajax({
				success : function(data) {
					var events = data;
					$(events).find('event').each (function() 
				    { 
				    	var id = $(this).attr('id');
					    var lat = $(this).find('lat').text();
					    var lng = $(this).find('lng').text();
					    var title = $(this).find('title').text().substr(0, 1).toUpperCase();
					    title += $(this).find('title').text().substr(1).toLowerCase();
					    var date = $(this).find('date').text();
					    var place = $(this).find('place').text();
					    
					    var text = $(this).find('text').contents().unwrap('<div>').text().substr(0, 200)+"....";
					    text += '</br> [<a class="mInfo" rel="'+id+'" href="#">Continue reading</a>]';
					    
					    if(lat != 0 && lng != 0)
					    {
					    	latCon = parseFloat(lat);
						    lngCon = parseFloat(lng);

							/// Create the placemark.
							var placemark = ge.createPlacemark('');

							
							if(text.indexOf("Text:") == 1)
							{
								var mytextSplit2 = text.split("Text:");
								placemark.setDescription('<div id="crimeInfo"><h5>'+title+'</h5>'+mytextSplit2[0]+'</div>');	
	
							}
							else
							{
								var mytextSplit = text.split("Publicerad:");
								placemark.setDescription('<div id="crimeInfo"><h5>'+title+'</h5>'+mytextSplit[0]+'</div>');

							}
							// Define a custom icon.
							var icon = ge.createIcon('');
							if(title.indexOf('Misshandel') > -1)
							{
								icon.setHref('http://maps.google.com/mapfiles/kml/paddle/purple-circle.png');
							}
							else if(title.indexOf('Trafik') > -1 || title.indexOf('Rattfylleri') > -1)
							{
								icon.setHref('http://maps.google.com/mapfiles/kml/paddle/wht-circle.png');
							}
							else if(title.indexOf('Stöld') > -1 || title.indexOf('Inbrott') > -1 || title.indexOf('Snatt') > -1)
							{
								icon.setHref('http://maps.google.com/mapfiles/kml/paddle/red-circle.png');
							}
							else
							{
								icon.setHref('http://maps.google.com/mapfiles/kml/paddle/ylw-circle.png');
							}
	
							var style = ge.createStyle('');
							style.getIconStyle().setIcon(icon);
							placemark.setStyleSelector(style);
	
							// Set the placemark's location.  
							var point = ge.createPoint('');
							point.setLatitude(latCon);
							point.setLongitude(lngCon);
							placemark.setGeometry(point);
							
							// Add the placemark to Earth.
							ge.getFeatures().appendChild(placemark);
						}
				    });
				    $(".mInfo").live("click", function(data)
					{ 
						var currId = $(this).attr('rel');
						moreInfo(currId);
					});
				},
				error : function(object, error) {
					console.log(error);
				}
			});
			
			localStorageCall();
	    }
		function localStorageCall()
		{
			if(localStorage.lol != null && localStorage.lol2 != null)
		  	{
		  		$(".tutorial").hide();
			  	$(".extra").prepend("<div id='moreInfo'></div>");
			    $("#moreInfo").append(localStorage.lol);
			    
			    var locations = localStorage.lol2.split('|');
			    
			    latCon = parseFloat(locations[0]);
				lngCon = parseFloat(locations[1]);
			    
			    var geocodeLocation = document.getElementById('location').value;
	
			      var geocoder = new google.maps.ClientGeocoder();
			      geocoder.getLatLng(geocodeLocation, function() {
			          var lookAt = ge.createLookAt('');
			          lookAt.set(latCon, lngCon, 10, ge.ALTITUDE_RELATIVE_TO_GROUND,
			                     0, 10, 20000);
			          ge.getView().setAbstractView(lookAt);
			        
			     });

		  	}
		}
		function failureCallback(errorCode) 
		{

	    }

		function addSampleUIHtml(html) 
		{
		 	document.getElementById('sample-ui').innerHTML += html;
		}

		function addSampleButton() 
		{
	        $('#sample-ui').append('<button type="button" onclick="buttonClick();" class="btn btn-primary">Find</button>');
		 }

		 function buttonClick() 
		 {
		      var geocodeLocation = document.getElementById('location').value;

		      var geocoder = new google.maps.ClientGeocoder();
		      geocoder.getLatLng(geocodeLocation, function(point) {
		        if (point) {
		          var lookAt = ge.createLookAt('');
		          lookAt.set(point.y, point.x, 10, ge.ALTITUDE_RELATIVE_TO_GROUND,
		                     0, 10, 20000);
		          ge.getView().setAbstractView(lookAt);
		        }
		      });
	     }
	     function moreInfo(id)
	     {
	     	$('#moreInfo').remove();
	     	$.get("/source/swedenXML.xml", function(data){
				$(data).find('event').each (function()
		     	{
		     		var findId = $(this).attr('id');
	        		if(findId == id)
	        		{
	        			var lat = $(this).find('lat').text();
					    var lng = $(this).find('lng').text();
					    var title = $(this).find('title').text().substr(0, 1).toUpperCase();
					    title += $(this).find('title').text().substr(1).toLowerCase();
					    var date = $(this).find('date').text();
					    var place = $(this).find('place').text();
					    var link = $(this).find('link').text();
					    
					    var text = $(this).find('text').contents().unwrap('<div>').text();
					    if(text.indexOf("Text:") > -1)
						{
							var mytextSplit2 = text.split("Text:");
							text = mytextSplit2[0];	

						}
						else
						{
							var mytextSplit = text.split("Publicerad:");
							text = mytextSplit[0];
						}
					    $(".extra").prepend("<div id='moreInfo'></div>");
					    $('#moreInfo').hide();
					    $("#moreInfo").append("<h3>"+title+"</h3>");
					    $("#moreInfo").append("<hr/>");
					    $("#moreInfo").append("<p class='timeposition'><img  src='http://cdn3.iconfinder.com/data/icons/token/Token,%20128x128,%20PNG/Clock-Time.png' id='clockIcon'>"+date+' '+place+"<img id='mapIcon' src='http://cdn4.iconfinder.com/data/icons/Mobile-Icons/128/04_maps.png'> <b>"+lat+' | '+lng+"</b></p>");
					    $("#moreInfo").append("<div id='brodtext'>"+text+"</div>");
					    $("#moreInfo").append("<p class='infoPics'><hr/><a onclick='postToFeed();' href='#'><img src='./img/fb.png' width='100px'></img></a><img class='tw' src='./img/tw.png' width='75px'> </img><a href='"+link+"'><img class='polis' src='./img/polisen.png' width='100px'></img></a><a href='#' onclick='closeFunc();' class='closeButton btn btn-danger'>Stäng</a></p>")
					    $("#moreInfo").append("")
					    $(".tutorial").hide('slow');
					    $("#moreInfo").show('slow');
					    
					    
	        		}
				});
			});

		 }
		 function closeFunc()
		 {
		  	$('#moreInfo').hide('slow', function()
		  	{ 
		  		$('#moreInfo').remove();
		  		$(".tutorial").show('slow');
		  	});
		 }
		 $(window).unload( function() 
		 {  
		 	var lol = $('#moreInfo').html();
		 	var lol2 = $('#moreInfo').find('b').text();
		 	if(lol != null)
		 	{
		 		localStorage.setItem("lol", lol);
		 		localStorage.setItem("lol2", lol2);
		 	}
		 });
		 

