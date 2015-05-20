<!doctype html>



<!--[if lt IE 7]><html <?php language_attributes(); ?> class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->

<!--[if (IE 7)&!(IEMobile)]><html <?php language_attributes(); ?> class="no-js 	lt-ie9 lt-ie8"><![endif]-->

<!--[if (IE 8)&!(IEMobile)]><html <?php language_attributes(); ?> class="no-js lt-ie9"><![endif]-->

<!--[if gt IE 8]><!--> <html <?php language_attributes(); ?> class="no-js"><!--<![endif]-->



	<head>

		<meta charset="utf-8">

<meta name="google-site-verification" content="Kyw1hfRM9OHw-ViKwhr_uYbPOUsu24LEylaMAfdd-X4" />

		<!-- WEB FONTS -->

		<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700' rel='stylesheet' type='text/css'>

		<link href='http://fonts.googleapis.com/css?family=Bitter:700|Satisfy' rel='stylesheet' type='text/css'>

		<script type="text/javascript" src="//use.typekit.net/vys3eui.js"></script>

		<script type="text/javascript">try{Typekit.load();}catch(e){}</script>

<link href="//netdna.bootstrapcdn.com/font-awesome/4.0.0/css/font-awesome.css" rel="stylesheet">



		<!-- Google Chrome Frame for IE -->

		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">



		<title><?php wp_title(''); ?></title>



		<!-- mobile meta (hooray!) -->

		<meta name="HandheldFriendly" content="True">

		<meta name="MobileOptimized" content="320">

		<meta id="viewport" name="viewport" content="width=device-width, user-scalable=no" />


		<!-- icons & favicons (for more: http://www.jonathantneal.com/blog/understand-the-favicon/) -->

		<link rel="apple-touch-icon" sizes="76x76" href="<?php echo get_template_directory_uri(); ?>/library/images/apple-icon-touch-76x76.png">

		<link rel="apple-touch-icon" sizes="76x76" href="touch-icon-76x76.png">

		<link rel="apple-touch-icon" sizes="120x120" href="touch-icon-120x120.png">

		<link rel="apple-touch-icon" sizes="152x152" href="touch-icon-152x152.png">		

<link rel="icon" href="<?php echo get_template_directory_uri(); ?>/favicon.png">

		<!--[if IE]>

			<link rel="shortcut icon" href="<?php echo get_template_directory_uri(); ?>/favicon.ico">

		<![endif]-->

		<!-- or, set /favicon.ico for IE10 win -->

		<meta name="msapplication-TileColor" content="#f01d4f">

		<meta name="msapplication-TileImage" content="<?php echo get_template_directory_uri(); ?>/library/images/win8-tile-icon.png">

<meta name="twitter:site" content="@eurekacollege">

		<link rel="pingback" href="<?php bloginfo('pingback_url'); ?>">



<!--[if lt IE 9]>

<script type="text/javascript" src="<?php echo get_template_directory_uri(); ?>/library/js/libs/html5shiv.js"></script>

<script type="text/javascript" src="<?php echo get_template_directory_uri(); ?>/library/js/libs/html5shiv-printshiv.js"></script>

<![endif]-->

		

		<!-- wordpress head functions -->

		<?php wp_head(); ?>

		<!-- end of wordpress head -->



		<!-- drop Google Analytics Here -->

		<!-- end analytics -->



	</head>



	<body>



		<div id="container">



			<header class="header" role="banner">

				<div class="top-nav">

					<div class="top-nav-links">

						<?php wp_nav_menu( array( 'menu' => 'top-nav') ); ?>

					</div>

					<div class="top-nav-search">

						<?php include 'searchform.php'; ?>

					</div>

				</div>

				<div class="header-container">

					<?php if( is_front_page() ) : ?>

					<div class="home-header-inner">

						<div class="home-header-logo">

							<div class="home-header-logo-inner">

								<a href="<?php echo home_url(); ?>" title="Eureka Homepage"><img src="<?php echo get_template_directory_uri(); ?>/library/images/eureka-logo.png" border="0" width="368" height="110" alt="Eureka College" /></a>

							</div>

							<div class="mobile-menu-search-btns">

								<div class="mobile-menu-btn">

									<a id="mobile-menu" title="Quick Menu"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-menu-icon.jpg" border="0" width="24" height="24" alt="Quick Menu" /></a>

								</div>

								<div class="mobile-search-btn">

									<a href="/a-z-index" title="Eureka A-Z Index"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-search-icon.jpg" border="0" width="24" height="21" alt="Eureka A-Z Index" /></a>

								</div>

							</div>

						</div>

						<div class="home-header-hero">

							<div class="cycle-slideshow" data-cycle-fx="fade" data-cycle-pause-on-hover="false" data-cycle-timeout="10000" data-cycle-prev="#prev" data-cycle-next="#next" data-index="1">

								<img src="<?php echo get_template_directory_uri(); ?>/library/images/learn-header.jpg" alt="Eureka College" />

								<img src="<?php echo get_template_directory_uri(); ?>/library/images/serve-header.jpg" alt="Eureka College" />

								<img src="<?php echo get_template_directory_uri(); ?>/library/images/lead-header.jpg" alt="Eureka College" />

							</div>

						</div>

					</div>

					<div class="slide-show-info">

						<div class="cycle-slideshow slide-title-container" data-cycle-slides="> div" data-cycle-fx="fade" data-cycle-pause-on-hover="false" data-cycle-timeout="10000" data-cycle-prev="#prev" data-cycle-next="#next" data-index="2">

							<div class="slide-title"><span class="cursive">Learn</span>. Serve. Lead.</div>

							<div class="slide-title">Learn. <span class="cursive">Serve</span>. Lead.</div>

							<div class="slide-title">Learn. Serve. <span class="cursive">Lead</span>.</div>

						</div>

						<div class="slide-caption-nav">

							<div class="cycle-slideshow slide-caption-container" data-cycle-slides="> div" data-cycle-fx="fade" data-cycle-pause-on-hover="false" data-cycle-timeout="10000" data-cycle-prev="#prev" data-cycle-next="#next" data-index="3">

								<div class="slide-caption">Personalized student learning is our top priority.</div>

								<div class="slide-caption">Service is at the center of a Eureka College liberal arts education.</div>

								<div class="slide-caption">Leaders find their vision and voice at Eureka.</div>

							</div>

							<div class="slide-nav">

								<a href="#" id="prev"><img src="<?php echo get_template_directory_uri(); ?>/library/images/slide-nav-prev.png" border="0" width="22" height="22" alt="Eureka College" /></a>

								<a href="#" id="next"><img src="<?php echo get_template_directory_uri(); ?>/library/images/slide-nav-next.png" border="0" width="22" height="22" alt="Eureka College" /></a>

							</div>

						</div>

					</div>

					<?php else : ?>

					<div class="secondary-header-inner">

						<div class="header-logo">

							<a href="<?php echo home_url(); ?>" title="Eureka Homepage"><img src="<?php echo get_template_directory_uri(); ?>/library/images/eureka-logo.png" border="0" width="368" height="110" alt="Eureka College" /></a>

						</div>

						<div class="mobile-menu-search-btns">

							<div class="mobile-menu-btn">

								<a id="mobile-menu" title="Quick Menu"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-menu-icon.jpg" border="0" width="24" height="24" alt="Quick Menu" /></a>

							</div>

							<div class="mobile-search-btn">

								<a href="/a-z-index" title="Eureka A-Z Index"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-search-icon.jpg" border="0" width="24" height="21" alt="Eureka A-Z Index" /></a>

							</div>

						</div>

					</div>

					<?php endif; ?>

					<div class="header-main-nav">

						<ul class="main-nav">

							<li class="main-nav-top-level"><a href="/discover/discover-eureka/">Discover</a>

								<div class="main-nav-drop-one">

									<div class="nav-drop-inner">

										<div class="nav-column-links">

											<strong><a href="/discover/" title="Discover Eureka">Discover</a></strong>

											<?php wp_nav_menu( array( 'menu' => 'discover-drop-down-nav', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-links academics-drop-link-col-two">

											<?php wp_nav_menu( array( 'menu' => 'discover-drop-down-nav-column-two', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-text">

											<?php dynamic_sidebar( 'discover_dropdown_text_area' ); ?>

										</div>

										<div class="nav-column-image">

											<?php dynamic_sidebar( 'discover_dropdown_image_area' ); ?>

										</div>

									</div>

								</div>

							</li>

							<li class="main-nav-top-level"><a href="/admissions/eureka-admissions/">Admissions</a>

								<div class="main-nav-drop-two">

									<div class="nav-drop-inner">

										<div class="nav-column-links">

											<strong><a href="/admissions/eureka-admissions/" title="Eureka Admissions">Admissions</a></strong>

											<?php wp_nav_menu( array( 'menu' => 'admissions-drop-down-nav-one', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-links academics-drop-link-col-two">

											<?php wp_nav_menu( array( 'menu' => 'admissions-drop-down-nav-column-two', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-links academics-drop-link-col-two">

											<?php wp_nav_menu( array( 'menu' => 'admissions-drop-down-nav-column-three', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-image">

											<?php dynamic_sidebar( 'admissions_dropdown_image_area' ); ?>

										</div>

									</div>

								</div>

							</li>

							<li class="main-nav-top-level"><a href="/academics/eureka-academics/">Academics</a>

								<div class="main-nav-drop-three">

									<div class="nav-drop-inner">

										<div class="nav-column-links">

											<strong><a href="/academics/eureka-academics" title="Eureka Academics">Academics</a></strong>

											<?php wp_nav_menu( array( 'menu' => 'academics-drop-down-nav', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-links academics-drop-link-col-two">

											<?php wp_nav_menu( array( 'menu' => 'academics-drop-down-nav-column-two', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-text">

											<?php dynamic_sidebar( 'academics_dropdown_text_area' ); ?>

										</div>

										<div class="nav-column-image">

											<?php dynamic_sidebar( 'academics_dropdown_image_area' ); ?>

										</div>

									</div>

								</div>

							</li>

							<li class="main-nav-top-level"><a href="/student-life/eureka-student-life/">Student Life</a>

								<div class="main-nav-drop-four">

									<div class="nav-drop-inner">

										<div class="nav-column-links">

											<strong><a href="/student-life/eureka-student-life" title="Eureka Student Life">Student Life</a></strong>

											<?php wp_nav_menu( array( 'menu' => 'student-life-drop-down-nav', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-links academics-drop-link-col-two">

											<?php wp_nav_menu( array( 'menu' => 'student-life-drop-down-nav-two', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-links academics-drop-link-col-two">

											<?php wp_nav_menu( array( 'menu' => 'student-life-drop-down-nav-three', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-image">

											<?php dynamic_sidebar( 'student_life_dropdown_image_area' ); ?>

										</div>

									</div>

								</div>

							</li>

							<li class="main-nav-top-level"><a href="/athletics/eureka-athletics/">Athletics</a>

								<div class="main-nav-drop-five">

									<div class="nav-drop-inner">

										<div class="nav-column-links">

											<strong><a href="/athletics/eureka-athletics/" title="Eureka Athletics">Athletics</a></strong>

											<?php wp_nav_menu( array( 'menu' => 'athletics-drop-down-nav', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-text">

											<?php dynamic_sidebar( 'athletics_dropdown_text_area' ); ?>

										</div>

										<div class="nav-column-image">

											<?php dynamic_sidebar( 'athletics_dropdown_image_area' ); ?>

										</div>

									</div>

								</div>

							</li>

							<li class="main-nav-top-level main-nav-last"><a href="/alumni/eureka-alumni/">Alumni</a>

								<div class="main-nav-drop-six">

									<div class="nav-drop-inner">

										<div class="nav-column-links">

											<strong><a href="/alumni/eureka-alumni/" title="Eureka Alumni">Alumni</a></strong>

											<?php wp_nav_menu( array( 'menu' => 'alumni-drop-down-nav', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-links academics-drop-link-col-two">

											<?php wp_nav_menu( array( 'menu' => 'alumni-drop-down-nav-two', 'menu_class' => 'drop-menu-list' ) ); ?>

										</div>

										<div class="nav-column-text">

											<?php dynamic_sidebar( 'alumni_dropdown_text_area' ); ?>

										</div>

										<div class="nav-column-image">

											<?php dynamic_sidebar( 'alumni_dropdown_image_area' ); ?>

										</div>

									</div>

								</div>

							</li>

						</ul>

					</div>

				</div>

				<div id="mobile-main-nav">

					<div id="mobile-main-nav-drop-down-btn">

						<a id="mobile-drop-down-btn" title="Discover Eureka">

							<span class="mobile-drop-down-btn-txt">DISCOVER EUREKA</span>

							<span class="mobile-drop-down-btn-img"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-main-nav-arrow.jpg" border="0" width="17" height="17" alt="Press to Reveal Navigation" /></span>

						</a>

					</div>

					<div id="mobile-main-nav-drop-down-content" class="m-hide">

						<div class="mobile-main-nav-drop-down-content-col-left">

							<div class="mobile-main-nav-drop-down-content-col-left-top">

								<a id="mobile-main-nav-discover-btn">Discover</a>

								<a id="mobile-main-nav-admissions-btn">Admissions</a>

								<a id="mobile-main-nav-academics-btn">Academics</a>

								<a id="mobile-main-nav-student-life-btn">Student Life</a>

								<a id="mobile-main-nav-athletics-btn">Athletics</a>

								<a id="mobile-main-nav-alumni-btn">Alumni</a>

							</div>

							<div class="mobile-main-nav-drop-down-content-col-left-bottom">

								<a id="mobile-main-nav-students-btn">Students</a>

								<a id="mobile-main-nav-staff-btn">Staff</a>

								<a id="mobile-main-nav-faculty-btn">Faculty</a>

								<a id="mobile-main-nav-parents-btn">Parents</a>

								<a id="mobile-main-nav-visitors-btn">Visitors</a>

							</div>

						</div>

						<div class="mobile-main-nav-drop-down-content-col-right">

							<div class="mobile-main-nav-drop-down-content-col-right-inner">

								<div class="mobile-main-nav-discover-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-discover-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-discover-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-discover-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-discover-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-main-nav-admissions-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-admissions-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-admissions-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-admissions-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-admissions-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-main-nav-academics-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-academics-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-academics-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-academics-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-academics-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-main-nav-student-life-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-student-life-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-student-life-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-student-life-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-student-life-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-main-nav-athletics-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-athletics-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

								</div>

								<div class="mobile-main-nav-alumni-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-alumni-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-alumni-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-alumni-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-alumni-extended m-hide' ) ); ?>

								</div>

							

								<div class="mobile-main-nav-students-menu m-hide">

									<ul class="mobile-drop-menu-list">

										<li><a href="/on-campus/directory" title="Directory"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-directory-icon.png" width="24" height="24" />Directory</a></li>

										<li><a href="https://ww10.eureka.edu/sonisweb/" target="_blank" title="Sonis Web"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-sonis-icon.png" width="24" height="24" />Sonis Web</a></li>

										<li><a href="http://moodle.eureka.edu/" target="_blank" title="Moodle"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-moodle-icon.png" width="24" height="24" />Moodle</a></li>

										<li><a href="/library/databases" title="Databases"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-database-icon.png" width="24" height="24" />Databases</a></li>

										<li><a href="/library/melick-library" title="Library"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-library-icon.png" width="24" height="24" />Library</a></li>

										<li><a href="/events" title="Calendar"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-calendar-icon.png" width="24" height="24" />Events</a></li>

										<li><a href="/news" title="News"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-news-icon.png" width="24" height="24" />News</a></li>

										<li><a href="/arts/arts" title="Arts"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-arts-icon.png" width="24" height="24" />Arts</a></li>

										<li><a href="/athletics/eureka-athletics/" title="Athletics"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-athletics-icon.png" width="24" height="24" />Athletics</a></li>

										<li><a href="/giving/give-now" title="Give Now"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-give-icon.png" width="24" height="24" />Give Now</a></li>

										<li><a href="/academics/faculty/" title="Faculty"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-faculty-icon.png" width="24" height="24" />Faculty</a></li>

										<li><a href="/on-campus/technology/" title="IT/Helpdesk"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-it-icon.png" width="24" height="24" />IT/Helpdesk</a></li>

										<li><a href="http://www.eurekacollegedining.com/" target="_blank" title="EC Dining"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-dining-icon.png" width="24" height="24" />EC Dining</a></li>

										<li><a href="http://mail.eureka.edu" target="_blank" title="Email"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-email-icon.png" width="24" height="24" />Email</a></li>

									</ul>

								</div>

								<div class="mobile-main-nav-staff-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-staff-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-staff-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-staff-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-staff-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-main-nav-faculty-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-faculty-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-faculty-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-faculty-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-faculty-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-main-nav-parents-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-parents-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-main-nav-parents-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-parents-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-main-nav-parents-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-main-nav-visitors-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-visitors-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

								</div>

							</div>

						</div>

					</div>

					<div id="mobile-quick-menu" class="m-hide">

						<div class="mobile-quick-menu-content-col-left">

							<div class="mobile-quick-menu-content-col-left-top">

								<a id="mobile-quick-menu-students-btn" class="m-active">Students</a>

								<a id="mobile-quick-menu-staff-btn">Staff</a>

								<a id="mobile-quick-menu-faculty-btn">Faculty</a>

								<a id="mobile-quick-menu-parents-btn">Parents</a>

								<a id="mobile-quick-menu-visitors-btn">Visitors</a>

							</div>

							<div class="mobile-quick-menu-content-col-left-bottom">

								<a id="mobile-quick-menu-discover-btn">Discover</a>

								<a id="mobile-quick-menu-admissions-btn">Admissions</a>

								<a id="mobile-quick-menu-academics-btn">Academics</a>

								<a id="mobile-quick-menu-student-life-btn">Student Life</a>

								<a id="mobile-quick-menu-athletics-btn">Athletics</a>

								<a id="mobile-quick-menu-alumni-btn">Alumni</a>

							</div>

						</div>

						<div class="mobile-quick-menu-content-col-right sub-red">

							<div class="mobile-quick-menu-content-col-right-inner">

								<div class="mobile-quick-menu-discover-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-discover-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-discover-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-discover-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-discover-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-quick-menu-admissions-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-admissions-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-admissions-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-admissions-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-admissions-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-quick-menu-academics-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-academics-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-academics-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-academics-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-academics-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-quick-menu-student-life-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-student-life-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-student-life-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-student-life-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-student-life-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-quick-menu-athletics-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-athletics-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

								</div>

								<div class="mobile-quick-menu-alumni-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-alumni-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-alumni-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-alumni-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-alumni-extended m-hide' ) ); ?>

								</div>

							

								<div class="mobile-quick-menu-students-menu m-show">

									<ul class="mobile-drop-menu-list">

										<li><a href="/on-campus/directory" title="Directory"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-directory-icon.png" width="24" height="24" />Directory</a></li>

										<li><a href="https://ww10.eureka.edu/sonisweb/" target="_blank" title="Sonis Web"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-sonis-icon.png" width="24" height="24" />Sonis Web</a></li>

										<li><a href="http://moodle.eureka.edu/" target="_blank" title="Moodle"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-moodle-icon.png" width="24" height="24" />Moodle</a></li>

										<li><a href="/library/databases" title="Databases"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-database-icon.png" width="24" height="24" />Databases</a></li>

										<li><a href="/library/melick-library" title="Library"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-library-icon.png" width="24" height="24" />Library</a></li>

										<li><a href="/events" title="Calendar"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-calendar-icon.png" width="24" height="24" />Events</a></li>

										<li><a href="/news" title="News"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-news-icon.png" width="24" height="24" />News</a></li>

										<li><a href="/arts/arts" title="Arts"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-arts-icon.png" width="24" height="24" />Arts</a></li>

										<li><a href="/athletics/eureka-athletics/" title="Athletics"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-athletics-icon.png" width="24" height="24" />Athletics</a></li>

										<li><a href="/giving/give-now" title="Give Now"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-give-icon.png" width="24" height="24" />Give Now</a></li>

										<li><a href="/academics/faculty/" title="Faculty"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-faculty-icon.png" width="24" height="24" />Faculty</a></li>

										<li><a href="/on-campus/technology/" title="IT/Helpdesk"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-it-icon.png" width="24" height="24" />IT/Helpdesk</a></li>

										<li><a href="http://www.eurekacollegedining.com/" target="_blank" title="EC Dining"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-dining-icon.png" width="24" height="24" />EC Dining</a></li>

										<li><a href="http://mail.eureka.edu" target="_blank" title="Email"><img class="mobile-student-menu-icon" src="<?php echo get_template_directory_uri(); ?>/library/images/mobile-student-menu-email-icon.png" width="24" height="24" />Email</a></li>

									</ul>

								</div>

								<div class="mobile-quick-menu-staff-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-staff-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-staff-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-staff-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-staff-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-quick-menu-faculty-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-faculty-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-faculty-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-faculty-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-faculty-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-quick-menu-parents-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-parents-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

									<ul class="mobile-show-more">

										<li><a id="mobile-quick-menu-parents-more-btn">Show More</a></li>

									</ul>

									<?php wp_nav_menu( array( 'menu' => 'mobile-parents-menu-extended', 'menu_class' => 'mobile-drop-menu-list mobile-quick-menu-parents-extended m-hide' ) ); ?>

								</div>

								<div class="mobile-quick-menu-visitors-menu m-hide">

									<?php wp_nav_menu( array( 'menu' => 'mobile-visitors-menu', 'menu_class' => 'mobile-drop-menu-list' ) ); ?>

								</div>

							</div>

						</div>

					</div>

				</div>

			</header> <!-- end header -->