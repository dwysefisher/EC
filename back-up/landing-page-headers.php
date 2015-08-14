<?php if ( 'academics_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/academics-header.jpg" class="landing-header-img" alt="Eureka College - Academics" />
<?php elseif ( 'admissions_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/admissions-header.jpg" class="secondary-header-img" alt="Eureka College - Admissions" />
<?php elseif ( 'alumni_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/alumni-header.jpg" class="landing-header-img" alt="Eureka College - Alumni" />
<?php elseif ( 'arts_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/arts-header.jpg" class="landing-header-img" alt="Eureka College - Arts" />
<?php elseif ( 'athletics_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/athletics-header.jpg" class="landing-header-img" alt="Eureka College - Athletics" />
<?php elseif ( 'career_service' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/career-services-header.jpg" class="landing-header-img" alt="Eureka College - Career Services" />
<?php elseif ( 'discover_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/discover-header.jpg" class="landing-header-img" alt="Eureka College - Discover" />
<?php elseif ( 'giving_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/giving-header.jpg" class="landing-header-img" alt="Eureka College - Giving" />
<?php elseif ( 'library_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/library-header.jpg" class="landing-header-img" alt="Eureka College - Library" />
<?php elseif ( 'on_campus_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/oncampus-header.jpg" class="landing-header-img" alt="Eureka College - On Campus" />
<?php elseif ( 'parents_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/parents-header.jpg" class="landing-header-img" alt="Eureka College - Parents" />
<?php elseif ( 'reagan_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/reagan-header.jpg" class="landing-header-img" alt="Eureka College - Reagan" />
<?php elseif ( 'staff_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/staff-header.jpg" class="landing-header-img" alt="Eureka College - Staff" />
<?php elseif ( 'students_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/student-header.jpg" class="landing-header-img" alt="Eureka College - Students" />
<?php elseif ( 'student_life_page' == get_post_type() ) :?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/studentlife-header.jpg" class="landing-header-img" alt="Eureka College - Student Life" />
<?php elseif ( 'visitors_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/visitors-header.jpg" class="landing-header-img" alt="Eureka College - Visitors" />
<?php else : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/discover-header.jpg" class="landing-header-img" alt="Eureka College" />
	
<?php endif; ?>