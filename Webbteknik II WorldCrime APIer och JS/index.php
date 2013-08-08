<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Globe News</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
	

    <!-- Le styles -->
    <link href="./css/bootstrap.css" rel="stylesheet">
    <style type="text/css">
      body {
        padding-top: 60px;
        padding-bottom: 40px;
      }
    </style>
    <link href="./css/bootstrap-responsive.css" rel="stylesheet">

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" mref="../assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="../assets/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="./img/eye.png">
  </head>
  	<?php
		$msg = "One or more API's are not avalible, the current information might not be up to date."
		$xml = file_get_contents('http://brottsplatskartan.se/api.php?action=getEvents&period=2880');
		if($xml == null)
		{
			echo '<script type="text/javascript">alert("' . $msg . '"); </script>';	
		}
		else
		{
			file_put_contents('./source/swedenXML.xml', $xml);	
		}				
	?>
  <body>
    <div class="navbar navbar-inverse navbar-fixed-top">
      <div class="navbar-inner">
        <div class="container">
          <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </a>
          
          <div class="nav-collapse">
              <a class="row" id="sample-ui" href="#">Globe News<img src="./img/eye.png" width="5%" /></a>
              
            <form class="navbar-form pull-right">
            	<div id="fb-root"></div>
            </form>
          </div><!--/.nav-collapse -->
        </div>
      </div>
    </div>

    <div class="container">
      <!-- Main hero unit for a primary marketing message or call to action -->
      <div class="sp"></div>
      <div class="extra"></div>
      <div class="tutorial">
      	<div class="row">
      		<h2>Color explanation...</h2><img src="./img/tut2.png"/>
      	</div>
      	
      </div>
      <div class="hero-unit mapHolder">
      	<div class="row">
	       <div id="maps" style="height: 600px; max-width: 1114px;"></div>
       </div>
     </div>
    </div> <!-- /container -->
    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="./js/jquery.js"></script>
    <script src="./js/bootstrap-transition.js"></script>
    <script src="./js/bootstrap-alert.js"></script>
    <script src="./js/bootstrap-modal.js"></script>
    <script src="./js/bootstrap-dropdown.js"></script>
    <script src="./js/bootstrap-scrollspy.js"></script>
    <script src="./js/bootstrap-tab.js"></script>
    <script src="./js/bootstrap-tooltip.js"></script>
    <script src="./js/bootstrap-popover.js"></script>
    <script src="./js/bootstrap-button.js"></script>
    <script src="./js/bootstrap-collapse.js"></script>
    <script src="./js/bootstrap-carousel.js"></script>
    <script src="./js/bootstrap-typeahead.js"></script>
	<script src="https://www.google.com/jsapi"></script>
	<script>
	$(document).ready(function() {
		init();
	});
	</script>
	<script src="./js/GoogleEarth.js"></script>
	<script src="./js/Facebook.js"></script>
  </body>
</html>
