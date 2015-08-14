<?php if ( 'academics_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/academics-header.jpg" class="secondary-header-img" alt="Eureka College - Academics" />
<?php elseif ( 'admissions_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/admissions-header.jpg" class="secondary-header-img" alt="Eureka College - Admissions" />
<?php elseif ( 'alumni_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/alumni-header.jpg" class="secondary-header-img" alt="Eureka College - Alumni" />
<?php elseif ( is_single('6016') OR is_single('5605') OR is_single('6458') OR is_single('6478') OR is_single('6126') OR is_single('6522')  ) : ?>
<img src="<?php echo get_template_directory_uri(); ?>/library/images/summerarts-header-strings.jpg" class="secondary-header-img" alt="Eureka College - Chamber Strings at Eureka" />
<?php elseif ( 'arts_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/arts-header.jpg" class="secondary-header-img" alt="Eureka College - Arts" />
<?php elseif ( 'athletics_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/athletics-header.jpg" class="secondary-header-img" alt="Eureka College - Athletics" />
<?php elseif ( 'career_service' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/career-services-header.jpg" class="secondary-header-img" alt="Eureka College - Career Services" />
<?php elseif ( 'discover_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/discover-header.jpg" class="secondary-header-img" alt="Eureka College - Discover" />
<?php elseif ( 'faculty_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/faculty-header.jpg" class="secondary-header-img" alt="Eureka College - Faculty" />
<?php elseif ( is_single('11610') ) : ?>
<img src="<?php echo get_template_directory_uri(); ?>/library/images/reagan-center-giving-banner.jpg" class="secondary-header-img" alt="Eureka College - Reagan Center Revitalization" />
<?php elseif ( 'giving_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/giving-header.jpg" class="secondary-header-img" alt="Eureka College - Giving" />
<?php elseif ( 'library_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/library-header.jpg" class="secondary-header-img" alt="Eureka College - Library" />
<?php elseif ( is_single('9762') ) : ?><img src="<?php echo get_template_directory_uri(); ?>/library/images/titleix-header.jpg" class="secondary-header-img" alt="Eureka College - Title IX" />
<?php elseif ( 'on_campus_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/oncampus-header.jpg" class="secondary-header-img" alt="Eureka College - On Campus" />
<?php elseif ( 'parents_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/parents-header.jpg" class="secondary-header-img" alt="Eureka College - Parents" />
<?php elseif ( 'reagan_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/reagan-header.jpg" class="secondary-header-img" alt="Eureka College - Reagan" />
<?php elseif ( 'staff_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/staff-header.jpg" class="secondary-header-img" alt="Eureka College - Staff" />
<?php elseif ( 'students_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/student-header.jpg" class="secondary-header-img" alt="Eureka College - Students" />
<?php elseif ( is_single('7020') OR is_single('7201') OR is_single('11230') OR is_single('11239') ) : ?>
<img src="<?php echo get_template_directory_uri(); ?>/library/images/graduation-header.jpg" class="secondary-header-img" alt="Eureka College - Graduation" />
<?php elseif ( is_single('5706') ) : ?><img src="<?php echo get_template_directory_uri(); ?>/library/images/titleix-header.jpg" class="secondary-header-img" alt="Eureka College - Title IX" />
<?php elseif ( 'student_life_page' == get_post_type() ) :?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/studentlife-header.jpg" class="secondary-header-img" alt="Eureka College - Student Life" />
<?php elseif ( 'visitors_page' == get_post_type() ) : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/visitors-header.jpg" class="secondary-header-img" alt="Eureka College - Visitors" />
<?php else : ?>
	<img src="<?php echo get_template_directory_uri(); ?>/library/images/discover-header.jpg" class="secondary-header-img" alt="Eureka College" />

<?php endif; ?>