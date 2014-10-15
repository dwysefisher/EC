				<div id="sidebar1" class="sidebar fourcol last clearfix" role="complementary">
					<!-- ********************************************************** -->
					<!-- CHECK POST TYPE AND DISPLAY APPROPRIATE SIDEBAR WIDGET AREA -->
					<!-- ********************************************************** -->
					<?php if ( 'academics_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'academic_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'admissions_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'admissions_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'alumni_page' == get_post_type() || is_single('186')) : ?>
						<?php dynamic_sidebar( 'alumni_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'class_note' == get_post_type() || 'maroon_and_gold_arti' == get_post_type()) : ?>
						<?php dynamic_sidebar( 'alumni_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'arts_page' == get_post_type() || is_single('1258')) : ?>
						<?php dynamic_sidebar( 'arts_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'athletics_page' == get_post_type() || is_single('56') ) : ?>
						<?php dynamic_sidebar( 'athletics_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'career_service' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'career_services_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'discover_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'discover_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'giving_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'giving_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'library_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'library_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'on_campus_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'on_campus_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'reagan_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'reagan_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'stake_holder_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'stake_holder_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'student_life_page' == get_post_type() || is_single('65')) : ?>
						<?php dynamic_sidebar( 'student_life_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'students_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'students_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'staff_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'staff_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'faculty_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'faculty_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'news_article' == get_post_type()) : ?>
						<?php if(is_single('56') || is_single('65') || is_single('186') || is_single('1258')) :?>
						<?php else : ?>
							<?php dynamic_sidebar( 'news_sidebar' ); ?>
						<?php endif; ?>
					<?php endif; ?>
					
					<?php if ( 'parents_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'parents_sidebar' ); ?>
					<?php endif; ?>
					
					<?php if ( 'visitors_page' == get_post_type() ) : ?>
						<?php dynamic_sidebar( 'visitors_sidebar' ); ?>
					<?php endif; ?>
					
					<!-- ********************************************************** -->
					<!-- GLOBAL SIDEBAR WIDGET AREA THAT APPEARS ON ALL SECONDARY PAGES -->
					<!-- ********************************************************** -->
					<?php if ( is_active_sidebar( 'global_sidebar' ) ) : ?>
						<?php dynamic_sidebar( 'global_sidebar' ); ?>
					<?php endif; ?>

				</div>