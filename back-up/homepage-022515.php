<?php
/*
Template Name: Homepage Template
*/
?>
<?php get_header(); ?>
			<?php include 'ec-alerts.php'; ?>
			<div id="home-content">

				<div id="home-inner-content" class="wrap clearfix">
						
						<div id="home-main" class="twelvecol first clearfix" role="main">

							<?php if (have_posts()) : while (have_posts()) : the_post(); ?>

							<article id="post-<?php the_ID(); ?>" <?php post_class('clearfix'); ?> role="article" itemscope itemtype="http://schema.org/BlogPosting">

								<header class="article-header">

									<h1 class="page-title" itemprop="headline"><?php the_title(); ?></h1>
									


								</header> <!-- end article header -->

								<section class="entry-content clearfix" itemprop="articleBody">
									<?php the_content(); ?>
							</section> <!-- end article section -->

								<footer class="article-footer">
									<?php the_tags('<span class="tags">' . __('Tags:', 'bonestheme') . '</span> ', ', ', ''); ?>

								</footer> <!-- end article footer -->

							</article> <!-- end article -->

							<?php endwhile; else : ?>
							<?php endif; ?>
						</div> <!-- end #main -->
						<!-- ***************************MOBILE ICON NAV*************************** -->
						<div class="homepage-mobile-links">
							<div class="homepage-mobile-links-left-column">
								<a class="homepage-mobile-icon-btn" href="/events" title="Eureka Events"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile_Events5.jpg" border="0" width="200" height="115" alt="Eureka Events" /></a>
								<a class="homepage-mobile-icon-btn" href="/athletics/eureka-athletics" title="Eureka Athletics"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile_Athletics.jpg" border="0" width="200" height="115" alt="Eureka Athletics" /></a>
								<a class="homepage-mobile-icon-btn" href="http://reagan.eureka.edu/society/" target="_blank" title="The Reagan Society"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile_Reagan1.jpg" border="0" width="200" height="115" alt="The Reagan Society" /></a>
							</div>
							<div class="homepage-mobile-links-right-column">
								<a class="homepage-mobile-icon-btn" href="/academics/eureka-academics/" title="Eureka Academics"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile_Academics.jpg" border="0" width="200" height="115" alt="Eureka Academics" /></a>
								<a class="homepage-mobile-icon-btn" href="/arts/arts" title="Eureka Arts"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile_Arts.jpg" border="0" width="200" height="115" alt="Eureka Arts" /></a>
								<a class="homepage-mobile-icon-btn" href="/alumni/eureka-alumni" title="Eureka Alumni"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile_Alumni3.jpg" border="0" width="200" height="115" alt="Eureka Alumni" /></a>
							</div>
							<div class="homepage-mobile-links-two-column-width">
								<a href="/giving/give-now/" title="Giving to Eureka"><img src="<?php echo get_template_directory_uri(); ?>/library/images/mobile_giving2x.jpg" border="0" width="400" height="115" alt="Giving to Eureka" /></a>
							</div>
						</div>
						<div id="home-sidebar">
							<div id="home-news-events">
								<div id="home-news-event-header">
									<h3><a href="http://www.eureka.edu/news/" style="color:#fff;">News</a> and <a href="http://www.eureka.edu/events/" style="color:#fff;">Events</a></h3><p style="color:#cc9900; margin-top:-20px;" class="small">Click News or Events above to view additional items</p>
								</div>
								<?php
									global $post;
									$all_events = tribe_get_events(array('eventDisplay'=>'upcoming','posts_per_page'=>2,'tax_query'=> array(array('taxonomy' => 'tribe_events_cat','field' => 'slug','terms' => 'homepage-events'))));

									foreach($all_events as $post) {
										setup_postdata($post);
								?>
									<div class="home-event-container">
										<div class="home-event-icon">
											<a href="<?php the_permalink(); ?>"><img src="<?php echo get_template_directory_uri(); ?>/library/images/homepage-event-icon.png" border="0" width="16" height="17" alt="Eureka Events" /></a>
										</div>
										<div class="home-event-content">
											<div class="home-event-date-title">
												<div class="home-event-day"><?php echo tribe_get_start_date($post->ID, false, 'd'); ?></div>
												<div class="home-event-month-year">
													<div class="home-event-month"><?php echo tribe_get_start_date($post->ID, false, 'M'); ?></div>
													<div class="home-event-year"><?php echo tribe_get_start_date($post->ID, false, 'o'); ?></div>
												</div>
											</div>
											<div class="home-event-details">
												<div class="home-event-title"><strong class="home-event-post-title"><a href="<?php the_permalink(); ?>"><?php the_title(); ?></a></strong></div>
												<p class="home-event-post-excerpt">
													<?php
														$no_images = preg_replace('/<img[^>]+\>/i','', get_the_content());
														$no_shortcodes = preg_replace("~(?:\[/?)[^/\]]+/?\]~s", '', $no_images);  # strip shortcodes, keep shortcode content
														echo wp_trim_words( $no_shortcodes, 25 );
													?>
												</p>
											</div>
										</div>
									</div>
								<?php } //endforeach ?>
								<?php wp_reset_query(); ?>
								
								<?php
									$args = array( 'post_type' => 'news_article', 'posts_per_page' => 2 );
									$loop = new WP_Query( $args );
								?>
								<?php while ($loop->have_posts()) : $loop->the_post(); ?>
									<div class="home-news-container">
										<div class="home-news-icon">
											<a href="<?php the_permalink(); ?>"><img src="<?php echo get_template_directory_uri(); ?>/library/images/homepage-news-icon.png" border="0" width="16" height="17" alt="Eureka News" /></a>
										</div>
										<div class="home-news-content">
											<div class="home-news-details">
												<div class="home-news-title"><strong class="home-event-post-title"><a href="<?php the_permalink(); ?>"><?php the_title(); ?></a></strong></div>
												<p class="home-news-post-excerpt">
													<?php
														$no_images = preg_replace('/<img[^>]+\>/i','', get_the_content());
														$no_shortcodes = preg_replace("~(?:\[/?)[^/\]]+/?\]~s", '', $no_images);  # strip shortcodes, keep shortcode content
														echo wp_trim_words( $no_shortcodes, 25 );
													?>
												</p>
											</div>
										</div>
									</div>
								<?php endwhile; ?>
								<?php wp_reset_query(); ?>
							</div>
						</div>
						<div class="homepage-lower-content">
							<div class="homepage-lower-links">
								<div class="homepage-lower-links-column-one">
									<a href="/events/" title="Homecoming 2014"><img src="<?php echo get_template_directory_uri(); ?>/library/images/tickets-btn.png" border="0" style="background-color:#990033 !important;" alt="Events &amp; Ticketing" /></a>
								</div>
								<div class="homepage-lower-links-column-two">
									<a href="/giving/give-now/" title="Give Now"><img src="<?php echo get_template_directory_uri(); ?>/library/images/give-now200px-btn.png" border="0" alt="Give Now" /></a>
								</div>
								<div class="homepage-lower-links-column-three">
									<a href="/admissions/apply-now/" title="Apply Today"><img src="<?php echo get_template_directory_uri(); ?>/library/images/apply225px-btn.png" border="0" alt="Apply Today" /></a>
								</div>
								<div class="homepage-lower-links-column-four">
									<a href="http://reagan.eureka.edu/society/" target="_blank" title="The Reagan Society"><img src="<?php echo get_template_directory_uri(); ?>/library/images/reagan200px-btn.png" border="0" alt="The Reagan Society" /></a>
								</div>
								<div class="homepage-lower-links-column-five">
									<a href="/arts/" title="Arts at Eureka"><img src="<?php echo get_template_directory_uri(); ?>/library/images/arts-btn.png" border="0" alt="Arts at Eureka" /></a>
								</div>
							</div>
							<div class="homepage-lower-graphics">
								<div class="homepage-lower-graphics-column-one">
									<a href="/academics/eureka-academics/" title="13:1 Student-to-Faculty Ratio"><img src="<?php echo get_template_directory_uri(); ?>/library/images/faculty-to-student-ratio.png" border="0" width="113" height="161" alt="13:1 Student-to-Faculty Ratio" /></a>
								</div>
								<div class="homepage-lower-graphics-column-two">
									<a href="/admissions/scholarships/" title="100% Merit-based Financial Aid"><img src="<?php echo get_template_directory_uri(); ?>/library/images/financial-aid-at-eureka.png" border="0" width="140" height="150" alt="100% Merit Based Financial Aid" /></a>
								</div>
								<div class="homepage-lower-graphics-column-three">
									<a href="/academics/study-abroad/" title="Students Studying Abroad in 21 Countries and 13 States"><img src="<?php echo get_template_directory_uri(); ?>/library/images/studying-abroad-at-eureka.png" border="0" width="235" height="170" alt="Eureka Study Abroad Program" /></a>
								</div>
								<div class="homepage-lower-graphics-column-four">
									<a href="/discover/visit-eureka/" title="Visit Eureka"><img src="<?php echo get_template_directory_uri(); ?>/library/images/eureka-illinois.png" border="0" width="160" height="142" alt="Located in Eureka, IL" /></a>
								</div>
							</div>
						</div>

				</div> <!-- end #inner-content -->

			

<?php get_footer(); ?>