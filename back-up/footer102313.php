				<div class="mobile-full-site-btn">
					<a id="full-site-btn" class="full-site-btn" title="View the Desktop Version">Full Site</a>
				</div>
				<footer class="footer" role="contentinfo">
					<div id="inner-footer" class="wrap clearfix">
						<!--<div class="mobile-footer-quick-links">
							<div class="mobile-footer-quick-links-inner">
								<span class="mobile-footer-quick-links-title">Quick Links</span>
								<?php //wp_nav_menu( array( 'menu' => 'mobile-footer-menu', 'menu_class' => 'mobile-footer-menu-list' ) ); ?>
							</div>
						</div>-->
						<div class="footer-top-links">
							<?php wp_nav_menu( array( 'menu' => 'footer-top-nav', 'menu_class' => 'footer-top-nav-links' ) ); ?>
						</div>
						<div class="footer-main-container">
							<div class="footer-quick-links">
								<div class="quick-links-column-one">
									<span class="quick-links-header">Quick Links</span>
									<?php wp_nav_menu( array( 'menu' => 'footer-quick-links-column-one', 'menu_class' => 'quick-links-list' ) ); ?>
								</div>
								<div class="quick-links-column-two">
									<span class="quick-links-header">&nbsp;</span>
									<?php wp_nav_menu( array( 'menu' => 'footer-quick-links-column-two', 'menu_class' => 'quick-links-list' ) ); ?>
								</div>
								<div class="quick-links-column-three">
									<span class="quick-links-header">&nbsp;</span>
									<?php wp_nav_menu( array( 'menu' => 'footer-quick-links-column-three', 'menu_class' => 'quick-links-list' ) ); ?>
								</div>
								<div class="quick-links-column-four">
									<span class="quick-links-header">&nbsp;</span>
									<?php wp_nav_menu( array( 'menu' => 'footer-quick-links-column-four', 'menu_class' => 'quick-links-list' ) ); ?>
								</div>
							</div>
							<div class="footer-brand-content">
								<div class="footer-college-seal">
									<img src="<?php echo get_template_directory_uri(); ?>/library/images/eureka-college-seal.jpg" width="204" height="204" alt="Eureka College" />
								</div>
								<div class="footer-learn-serve-lead">
									<div class="learn-serve-lead-header">
										<img src="<?php echo get_template_directory_uri(); ?>/library/images/learn-serve-lead-headline.jpg" width="259" height="21" alt="Learn. Serve. Lead" />
									</div>
									<div class="learn-serve-lead-content">
										<p>Equipping students for excellence in learning, service, and leadership for 158 years. Become the change-maker you want to be at Eureka College.</p>
									</div>
									<div class="reagan-tcucc">
										<a href="http://reagan.eureka.edu/society/" target="_blank" class="footer-reagan-logo" title="The Reagan Society"><img src="<?php echo get_template_directory_uri(); ?>/library/images/reagan-signature.jpg" border="0" width="133" height="29" alt="The Reagan Society" /></a>
										<a href="http://tcucc.org/" target="_blank" class="footer-tcucc-logo" title="TCUCC.org"><img src="<?php echo get_template_directory_uri(); ?>/library/images/colleges-universities-of-christian-church-logo.jpg" border="0" width="104" height="29" alt="TCUCC.org" /></a>
									</div>
								</div>
							</div>
						</div>
						<div class="footer-bottom">
							<div class="footer-social-icons">
								<a href="https://www.facebook.com/pages/Eureka-College/104824589400" target="_blank" class="social-icon" title="Visit us on Facebook"><img src="<?php echo get_template_directory_uri(); ?>/library/images/facebook-icon.jpg" border="0" width="21" height="21" alt="Visit us on Facebook." /></a>																<a href="https://twitter.com/EurekaCollege" target="_blank" class="social-icon" title="Follow us on Twitter"><img src="<?php echo get_template_directory_uri(); ?>/library/images/twitter-icon.jpg" border="0" width="21" height="21" alt="Follow us on Twitter." /></a>																<a href="http://www.linkedin.com/company/eureka-college" target="_blank" class="social-icon" title="Connect with us on LinkedIn."><img src="<?php echo get_template_directory_uri(); ?>/library/images/linked-in-icon.png" border="0" width="21" height="21" alt="Connect with us on LinkedIn." /></a>
								<a href="http://www.youtube.com/user/EurekaCollegeMedia" target="_blank" class="social-icon" title="Watch us on YouTube"><img src="<?php echo get_template_directory_uri(); ?>/library/images/youtube-icon.jpg" border="0" width="21" height="21" alt="Watch us on YouTube." /></a>
								<a href="http://pinterest.com/eurekacollege" target="_blank" class="social-icon" title="Follow us on Pinterest"><img src="<?php echo get_template_directory_uri(); ?>/library/images/pinterest-icon.jpg" border="0" width="21" height="21" alt="Follow us on Pinterest." /></a>
							</div>
							<div class="footer-copyright">
								<p class="source-org copyright">Copyright &copy; <?php echo date('Y'); ?>. Eureka College. All rights reserved.</p>
							</div>
						</div>
					</div> <!-- end #inner-footer -->
				</footer> <!-- end footer -->
			</div> <!-- end #content -->
		</div> <!-- end #container -->

		<!-- all js scripts are loaded in library/bones.php -->
		<!--START GOOGLE ANALYTICS-->
		<script type="text/javascript">
 
			var _gaq = _gaq || [];
			_gaq.push(['_setAccount', 'UA-43347713-1']);
			_gaq.push(['_trackPageview']);
 
			(function() {
				var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
				ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
				var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
			})();
 
		</script>
		<!--END ANALYTICS-->
		<?php wp_footer(); ?>

	</body>

</html> <!-- end page. what a ride! -->