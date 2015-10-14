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
									<img src="<?php echo get_template_directory_uri(); ?>/library/images/ec-seal-gray-web.png" width="204" height="204" alt="Eureka College" />
								</div>
								<div class="footer-learn-serve-lead">
									<div class="learn-serve-lead-header">
										<img src="<?php echo get_template_directory_uri(); ?>/library/images/learn-serve-lead-headline.jpg" width="259" height="21" alt="Learn. Serve. Lead" />
									</div>
									<div class="learn-serve-lead-content">
										<p>Eureka College initiates transformational growth in all students through a diverse range of learning opportunities in a highly personalized setting, equipping our graduates to be engaged citizens and servant leaders.</p>
									</div>
									<div class="reagan-tcucc">
										<a href="http://tcucc.org/" target="_blank" class="footer-tcucc-logo" title="TCUCC.org"><img src="<?php echo get_template_directory_uri(); ?>/library/images/colleges-universities-of-christian-church-logo.jpg" border="0" width="104" height="29" alt="TCUCC.org" /></a>
										<a href="http://reagan.eureka.edu/" target="_blank" class="footer-reagan-logo" title="The Reagan Society"><img src="<?php echo get_template_directory_uri(); ?>/library/images/reagan-signature.jpg" border="0" width="133" height="29" alt="The Reagan Society" /></a>
									</div>
								</div>
							</div>
						</div>

    <div class="footer-social-icons">
    <p class="small" style="color:#FFF;">Connect with us!</p>  
    <a href="http://www.fb.com/eurekacollege" target="_blank" title="Like us on Facebook" alt="Like us on Facebook" class="social-icon"><i class="fa fa-facebook-square fa-2x fa-inverse"></i></a>
    <a href="http://www.twitter.com/eurekacollege" target="_blank" title="Follow us on Twitter" alt="Follow us on Twitter" class="social-icon"><i class="fa fa-twitter-square fa-2x fa-inverse"></i></a>
      <a href="http://www.linkedin.com/company/eureka-college" target="_blank" title="Connect with us on LinkedIn" alt="Connect with us on LinkedIn" class="social-icon"><i class="fa fa-linkedin-square fa-2x fa-inverse"></i></a>
      <a href="http://www.youtube.com/user/EurekaCollegeMedia" target="_blank" title="Watch us on YouTube" alt="Watch us on YouTube" class="social-icon"><i class="fa fa-youtube-square fa-2x fa-inverse"></i></a>
      <a href="http://pinterest.com/eurekacollege" target="_blank"  title="Follow us on Pinterest" alt="Follow us on Pinterest" class="social-icon"><i class="fa fa-pinterest-square fa-2x fa-inverse"></i></a>
      <a href="http://www.instagram.com/eurekacollege" target="_blank" alt="Follow us on Instagram" title="Follow us on Instagram"><i class="fa fa-instagram fa-2x fa-inverse"></i></a>
		</div>

<div class="vcard footer-address">
  <div class="fn org"><span class="organization-name">Eureka College</span></div>
 <div class="adr">
  <span class="street-address">300 East College Ave.</span>
  <span class="locality">Eureka</span>, <abbr class="region" title="Illinois">IL</abbr> <span class="postal-code">61530-1500</span> 
 </div>
  <div class="tel"><span class="type">Telephone: <a href="tel:13094673721">309-467-3721</a> &ndash; Main Switchboard <br />
    <a href="tel:18884387352">888-438-7352</a> | <a href="tel:13094676350">309-467-6350</a> &ndash; Admissions<br /></span></div>
  <div><span class="tel">Fax: <span class="value">309-467-6576</span> | <span><a href="http://www.eureka.edu/on-campus/directory/">Directory</a></span></div>
</div>
						<div class="footer-copyright">
								<p class="source-org copyright">Copyright &copy; <?php echo date('Y'); ?>. Eureka College. All rights reserved.</p>
							</div>


						</div>
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

<!--START SITEIMPROVE ANALYTICS-->
<script type="text/javascript">
/*<![CDATA[*/
(function() {
var sz = document.createElement('script'); sz.type = 'text/javascript'; sz.async = true;
sz.src = '//us1.siteimprove.com/js/siteanalyze_90778714.js';
var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(sz, s);
})();
/*]]>*/
</script>
		<!--END SITEIMPROVE-->
		<?php wp_footer(); ?>

	</body>

</html> <!-- end page. what a ride! -->