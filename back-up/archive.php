<?php get_header(); ?>

			<div id="content">

				<div id="inner-content" class="wrap clearfix">
						<div class="secondary-header-images">
							<?php include "secondary-page-headers.php"; ?>
						</div>
						<?php if ( 'students_page' == get_post_type() || 'staff_page' == get_post_type() || 'faculty_page' == get_post_type() || 'parents_page' == get_post_type() || 'visitors_page' == get_post_type() ) : ?>
								<?php get_sidebar(); ?>
						<?php endif; ?>
						<div id="main" class="eightcol first clearfix" role="main">

							<?php if (is_category()) { ?>
								<h1 class="archive-title h2">
									<span><?php _e("Posts Categorized:", "bonestheme"); ?></span> <?php single_cat_title(); ?>
								</h1>

							<?php } elseif (is_tag()) { ?>
								<h1 class="archive-title h2">
									<span><?php _e("Posts Tagged:", "bonestheme"); ?></span> <?php single_tag_title(); ?>
								</h1>

							<?php } elseif (is_author()) {
								global $post;
								$author_id = $post->post_author;
							?>
								<h1 class="archive-title h2">

									<span><?php _e("Posts By:", "bonestheme"); ?></span> <?php the_author_meta('display_name', $author_id); ?>

								</h1>
							<?php } elseif (is_day()) { ?>
								<h1 class="archive-title h2">
									<span><?php _e("Daily Archives:", "bonestheme"); ?></span> <?php the_time('l, F j, Y'); ?>
								</h1>

							<?php } elseif (is_month()) { ?>
									<h1 class="archive-title h2">
										<span><?php _e("Monthly Archives:", "bonestheme"); ?></span> <?php the_time('F Y'); ?>
									</h1>

							<?php } elseif (is_year()) { ?>
									<h1 class="archive-title h2">
										<span><?php _e("Yearly Archives:", "bonestheme"); ?></span> <?php the_time('Y'); ?>
									</h1>
							<?php } ?>
							
							<?php if ( 'news_article' == get_post_type()) : ?>
								<div class="one_third media-rel-contact">
									<h3>Media Relations</h3>
									<strong>Michele Lehman</strong><br />
									Coordinator of Media Relations<br />
									309-467-6318<br />
									<a href="mailto:mlehman@eureka.edu" title="Email Michele Lehman">mlehman@eureka.edu</a>
								</div>
							<?php endif; ?>
							<?php if ( 'news_article' == get_post_type()) : ?>
								<div class="two_third last_column">
							<?php endif; ?>
							<?php if (have_posts()) : while (have_posts()) : the_post(); ?>
							<article id="post-<?php the_ID(); ?>" <?php post_class('clearfix'); ?> role="article">
								<section class="entry-content clearfix">
									<h3 class="h2"><a href="<?php the_permalink() ?>" rel="bookmark" title="<?php the_title_attribute(); ?>"><?php the_title(); ?></a></h3>
									<?php /*the_post_thumbnail( 'bones-thumb-300' );*/ ?>

									<?php
										$no_images = preg_replace('/<img[^>]+\>/i','', get_the_content());
										$no_shortcodes = preg_replace("~(?:\[/?)[^/\]]+/?\]~s", '', $no_images);  # strip shortcodes, keep shortcode content
										echo wp_trim_words( $no_shortcodes, 75 );
									?>
									<a href="<?php the_permalink() ?>" rel="bookmark" title="<?php the_title_attribute(); ?>">Continue Reading &raquo;</a>
						
								</section> <!-- end article section -->
							</article> <!-- end article -->
							<?php endwhile; ?>
							<?php if ( 'news_article' == get_post_type()) : ?>
								</div><!-- end .two_third -->
								<div style="clear: both;"></div>
							<?php endif; ?>

									<?php if (function_exists('bones_page_navi')) { ?>
										<?php bones_page_navi(); ?>
									<?php } else { ?>
										<nav class="wp-prev-next">
											<ul class="clearfix">
												<li class="prev-link"><?php next_posts_link(__('&laquo; Older Entries', "bonestheme")) ?></li>
												<li class="next-link"><?php previous_posts_link(__('Newer Entries &raquo;', "bonestheme")) ?></li>
											</ul>
										</nav>
									<?php } ?>

							<?php else : ?>

									<article id="post-not-found" class="hentry clearfix">
										<header class="article-header">
											<h1><?php _e("Oops, Post Not Found!", "bonestheme"); ?></h1>
										</header>
										<section class="entry-content">
											<p><?php _e("Uh Oh. Something is missing. Try double checking things.", "bonestheme"); ?></p>
										</section>
										<footer class="article-footer">
												<p><?php _e("This is the error message in the archive.php template.", "bonestheme"); ?></p>
										</footer>
									</article>

							<?php endif; ?>

						</div> <!-- end #main -->

						<?php if ( 'students_page' != get_post_type() && 'staff_page' != get_post_type() && 'faculty_page' != get_post_type() && 'parents_page' != get_post_type() && 'visitors_page' != get_post_type() ) : ?>
								<?php get_sidebar(); ?>
						<?php endif; ?>

								</div> <!-- end #inner-content -->

			

<?php get_footer(); ?>
