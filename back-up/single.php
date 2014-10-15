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
						<?php if (have_posts()) : while (have_posts()) : the_post(); ?>

							<article id="post-<?php the_ID(); ?>" <?php post_class('clearfix'); ?> role="article" itemscope itemtype="http://schema.org/BlogPosting">

								<header class="article-header">

									<h1 class="entry-title single-title" itemprop="headline"><?php the_title(); ?></h1>
									

								</header> <!-- end article header -->

								<section class="entry-content clearfix" itemprop="articleBody">
									<?php the_content(); ?>
								</section> <!-- end article section -->

								<footer class="article-footer">
									<?php the_tags('<p class="tags"><span class="tags-title">' . __('Tags:', 'bonestheme') . '</span> ', ', ', '</p>'); ?>

								</footer> <!-- end article footer -->

								<?php comments_template(); ?>

							</article> <!-- end article -->

						<?php endwhile; ?>

						<?php else : ?>

							<article id="post-not-found" class="hentry clearfix">
									<header class="article-header">
										<h1><?php _e("Oops, Post Not Found!", "bonestheme"); ?></h1>
									</header>
									<section class="entry-content">
										<p><?php _e("Uh Oh. Something is missing. Try double checking things.", "bonestheme"); ?></p>
									</section>
									<footer class="article-footer">
											<p><?php _e("This is the error message in the single.php template.", "bonestheme"); ?></p>
									</footer>
							</article>

						<?php endif; ?>
						
						<?php
							if(is_single('186') || is_single('1258') || is_single('56') || is_single('320') || is_single('325') || is_single('327') || is_single('329') || is_single('331')){
								include "related-news-sections.php";
							}
						?>

					</div> <!-- end #main -->

					<?php if ( 'students_page' != get_post_type() && 'staff_page' != get_post_type() && 'faculty_page' != get_post_type() && 'parents_page' != get_post_type() && 'visitors_page' != get_post_type() ) : ?>
								<?php get_sidebar(); ?>
					<?php endif; ?>

				</div> <!-- end #inner-content -->

			

<?php get_footer(); ?>
