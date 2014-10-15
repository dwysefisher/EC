<?php wp_reset_query(); ?>
	<?php
		if(is_single('320')){
			echo '<div class="related-news-section">';
			echo '<div class="related-news-header"><h2>Faculty News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Faculty', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) ); 
		}elseif(is_single('186')){
			echo '<div class="related-news-section two-thirds-width">';
			echo '<div class="related-news-header"><h2>Alumni News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Alumni', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) );
		}elseif(is_single('1258')){
			echo '<div class="related-news-section two-thirds-width">';
			echo '<div class="related-news-header"><h2>Art News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Art', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) );
		}elseif(is_single('56')){
			echo '<div class="related-news-section">';
			echo '<div class="related-news-header"><h2>Athletic News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Athletics', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) );
		}elseif(is_single('325')){
			echo '<div class="related-news-section">';
			echo '<div class="related-news-header"><h2>Parent News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Parents', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) );
		}elseif(is_single('327')){
			echo '<div class="related-news-section">';
			echo '<div class="related-news-header"><h2>Staff News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Staff', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) );
		}elseif(is_single('329')){
			echo '<div class="related-news-section">';
			echo '<div class="related-news-header"><h2>Student News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Students', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) );
		}elseif(is_single('331')){
			echo '<div class="related-news-section">';
			echo '<div class="related-news-header"><h2>Visitor News</h2></div>';
			$loop = new WP_Query( array( 'post_type' => 'news_article', 'category_name' => 'Visitors', 'posts_per_page' => 5, 'orderby' => 'date', 'order' => 'DESC' ) );
		}
		?>

		<?php while ( $loop->have_posts() ) : $loop->the_post(); ?>

		<div class="related-news-content">
			<?php the_title( '<h3 class="related-news-title"><a href="' . get_permalink() . '" title="' . the_title_attribute( 'echo=0' ) . '" rel="bookmark">', '</a></h3>' ); ?>
			<?php
				$no_images = preg_replace('/<img[^>]+\>/i','', get_the_content());
				$no_shortcodes = preg_replace("~(?:\[/?)[^/\]]+/?\]~s", '', $no_images);  # strip shortcodes, keep shortcode content
				echo wp_trim_words( $no_shortcodes, 25 );
			?>
		</div>
	<?php endwhile; ?>
	<?php wp_reset_query(); ?>
</div><!-- end .related-news-section -->