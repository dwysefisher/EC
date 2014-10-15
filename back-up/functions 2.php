<?php
/*
Author: Eddie Machado
URL: htp://themble.com/bones/

This is where you can drop your custom functions or
just edit things like thumbnail sizes, header images,
sidebars, comments, ect.
*/

/************* INCLUDE NEEDED FILES ***************/

/*
1. library/bones.php
	- head cleanup (remove rsd, uri links, junk css, ect)
	- enqueueing scripts & styles
	- theme support functions
	- custom menu output & fallbacks
	- related post function
	- page-navi function
	- removing <p> from around images
	- customizing the post excerpt
	- custom google+ integration
	- adding custom fields to user profiles
*/
require_once('library/bones.php'); // if you remove this, bones will break
/*
2. library/custom-post-type.php
	- an example custom post type
	- example custom taxonomy (like categories)
	- example custom taxonomy (like tags)
*/
require_once('library/custom-post-type.php'); // you can disable this if you like
/*
3. library/admin.php
	- removing some default WordPress dashboard widgets
	- an example custom dashboard widget
	- adding custom login css
	- changing text in footer of admin
*/
// require_once('library/admin.php'); // this comes turned off by default
/*
4. library/translation/translation.php
	- adding support for other languages
*/
// require_once('library/translation/translation.php'); // this comes turned off by default

/************* THUMBNAIL SIZE OPTIONS *************/

// Thumbnail sizes
add_image_size( 'bones-thumb-600', 600, 150, true );
add_image_size( 'bones-thumb-300', 300, 100, true );
/*
to add more sizes, simply copy a line from above
and change the dimensions & name. As long as you
upload a "featured image" as large as the biggest
set width or height, all the other sizes will be
auto-cropped.

To call a different size, simply change the text
inside the thumbnail function.

For example, to call the 300 x 300 sized image,
we would use the function:
<?php the_post_thumbnail( 'bones-thumb-300' ); ?>
for the 600 x 100 image:
<?php the_post_thumbnail( 'bones-thumb-600' ); ?>

You can change the names and dimensions to whatever
you like. Enjoy!
*/

if ( ! isset( $content_width ) )
	$content_width = 800;

/************* ACTIVE SIDEBARS ********************/

// Sidebars & Widgetizes Areas
function bones_register_sidebars() {
	register_sidebar(array(
		'id' => 'global_sidebar',
		'name' => __('Global Sidebar Widget Area', 'bonestheme'),
		'description' => __('This widget area appears in the sidebar on all secondary pages throughout the site.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'academic_sidebar',
		'name' => __('Academic Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Academic section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'admissions_sidebar',
		'name' => __('Admissions Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Admissions section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'alumni_sidebar',
		'name' => __('Alumni Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Alumni section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'athletics_sidebar',
		'name' => __('Athletics Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Athletics section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'discover_sidebar',
		'name' => __('Discover Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Discover section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'giving_sidebar',
		'name' => __('Giving Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Giving section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'library_sidebar',
		'name' => __('Library Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Library section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'on_campus_sidebar',
		'name' => __('On Campus Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the On Campus section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'stake_holder_sidebar',
		'name' => __('Stake Holder Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Stake Holder section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'student_life_sidebar',
		'name' => __('Student Life Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Student Life section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'students_sidebar',
		'name' => __('Students Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Students stakeholder section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'staff_sidebar',
		'name' => __('Staff Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Staff stakeholder section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'faculty_sidebar',
		'name' => __('Faculty Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Faculty stakeholder section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'news_sidebar',
		'name' => __('News Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the News section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'parents_sidebar',
		'name' => __('Parents Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Parents stakeholder section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'visitors_sidebar',
		'name' => __('Visitors Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Visitors stakeholder section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'reagan_sidebar',
		'name' => __('Reagan Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Reagan section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'career_services_sidebar',
		'name' => __('Career Services Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Career Services section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'arts_sidebar',
		'name' => __('Arts Sidebar', 'bonestheme'),
		'description' => __('This sidebar appears on all pages in the Arts section.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	
	register_sidebar(array(
		'id' => 'discover_dropdown_text_area',
		'name' => __('Discover Dropdown Text Area', 'bonestheme'),
		'description' => __('This widget area is located where the text area appears on the Discover dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'discover_dropdown_image_area',
		'name' => __('Discover Dropdown Image Area', 'bonestheme'),
		'description' => __('This widget area is located where the image area appears on the Discover dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'admissions_dropdown_text_area',
		'name' => __('Admissions Dropdown Text Area', 'bonestheme'),
		'description' => __('This widget area is located where the text area appears on the Admissions dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'admissions_dropdown_image_area',
		'name' => __('Admissions Dropdown Image Area', 'bonestheme'),
		'description' => __('This widget area is located where the image area appears on the Admissions dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'academics_dropdown_text_area',
		'name' => __('Academics Dropdown Text Area', 'bonestheme'),
		'description' => __('This widget area is located where the text area appears on the Academics dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'academics_dropdown_image_area',
		'name' => __('Academics Dropdown Image Area', 'bonestheme'),
		'description' => __('This widget area is located where the image area appears on the Academics dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'student_life_dropdown_text_area',
		'name' => __('Student Life Dropdown Text Area', 'bonestheme'),
		'description' => __('This widget area is located where the text area appears on the Student Life dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'student_life_dropdown_image_area',
		'name' => __('Student Life Dropdown Image Area', 'bonestheme'),
		'description' => __('This widget area is located where the image area appears on the Student Life dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'athletics_dropdown_text_area',
		'name' => __('Athletics Dropdown Text Area', 'bonestheme'),
		'description' => __('This widget area is located where the text area appears on the Athletics dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'athletics_dropdown_image_area',
		'name' => __('Athletics Dropdown Image Area', 'bonestheme'),
		'description' => __('This widget area is located where the image area appears on the Athletics dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'alumni_dropdown_text_area',
		'name' => __('Alumni Dropdown Text Area', 'bonestheme'),
		'description' => __('This widget area is located where the text area appears on the Alumni dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));
	register_sidebar(array(
		'id' => 'alumni_dropdown_image_area',
		'name' => __('Alumni Dropdown Image Area', 'bonestheme'),
		'description' => __('This widget area is located where the image area appears on the Alumni dropdown.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));

	/*
	to add more sidebars or widgetized areas, just copy
	and edit the above sidebar code. In order to call
	your new sidebar just use the following code:

	Just change the name to whatever your new
	sidebar's id is, for example:

	register_sidebar(array(
		'id' => 'sidebar2',
		'name' => __('Sidebar 2', 'bonestheme'),
		'description' => __('The second (secondary) sidebar.', 'bonestheme'),
		'before_widget' => '<div id="%1$s" class="widget %2$s">',
		'after_widget' => '</div>',
		'before_title' => '<h4 class="widgettitle">',
		'after_title' => '</h4>',
	));

	To call the sidebar in your template, you can just copy
	the sidebar.php file and rename it to your sidebar's name.
	So using the above example, it would be:
	sidebar-sidebar2.php

	*/
} // don't remove this bracket!

/************* COMMENT LAYOUT *********************/

// Comment Layout
function bones_comments($comment, $args, $depth) {
   $GLOBALS['comment'] = $comment; ?>
	<li <?php comment_class(); ?>>
		<article id="comment-<?php comment_ID(); ?>" class="clearfix">
			<header class="comment-author vcard">
				<?php
				/*
					this is the new responsive optimized comment image. It used the new HTML5 data-attribute to display comment gravatars on larger screens only. What this means is that on larger posts, mobile sites don't have a ton of requests for comment images. This makes load time incredibly fast! If you'd like to change it back, just replace it with the regular wordpress gravatar call:
					echo get_avatar($comment,$size='32',$default='<path_to_url>' );
				*/
				?>
				<!-- custom gravatar call -->
				<?php
					// create variable
					$bgauthemail = get_comment_author_email();
				?>
				<img data-gravatar="http://www.gravatar.com/avatar/<?php echo md5($bgauthemail); ?>?s=32" class="load-gravatar avatar avatar-48 photo" height="32" width="32" src="<?php echo get_template_directory_uri(); ?>/library/images/nothing.gif" />
				<!-- end custom gravatar call -->
				<?php printf(__('<cite class="fn">%s</cite>', 'bonestheme'), get_comment_author_link()) ?>
				<time datetime="<?php echo comment_time('Y-m-j'); ?>"><a href="<?php echo htmlspecialchars( get_comment_link( $comment->comment_ID ) ) ?>"><?php comment_time(__('F jS, Y', 'bonestheme')); ?> </a></time>
				<?php edit_comment_link(__('(Edit)', 'bonestheme'),'  ','') ?>
			</header>
			<?php if ($comment->comment_approved == '0') : ?>
				<div class="alert alert-info">
					<p><?php _e('Your comment is awaiting moderation.', 'bonestheme') ?></p>
				</div>
			<?php endif; ?>
			<section class="comment_content clearfix">
				<?php comment_text() ?>
			</section>
			<?php comment_reply_link(array_merge( $args, array('depth' => $depth, 'max_depth' => $args['max_depth']))) ?>
		</article>
	<!-- </li> is added by WordPress automatically -->
<?php
} // don't remove this bracket!

/************* SEARCH FORM LAYOUT *****************/

// Search Form
function bones_wpsearch($form) {
	$form = '<form role="search" method="get" id="searchform" action="' . home_url( '/' ) . '" >
	<label class="screen-reader-text" for="s">' . __('Search for:', 'bonestheme') . '</label>
	<input type="text" value="' . get_search_query() . '" name="s" id="s" placeholder="'.esc_attr__('Search the Site...','bonestheme').'" />
	<input type="submit" id="searchsubmit" value="'. esc_attr__('Search') .'" />
	</form>';
	return $form;
} // don't remove this bracket!

/*******************CUSTOM EXCERPT LENGTH*************************/
function custom_excerpt_length( $length ) {
	return 75;
}
add_filter( 'excerpt_length', 'custom_excerpt_length', 999 );



/*******************FIX TO ALLOW DUPLICATE PAGE SLUGS IN DIFFERENT POST TYPES*************************/
function wp_cpt_unique_post_slug($slug, $post_ID, $post_status, $post_type, $post_parent, $original_slug) {
    if ( in_array( $post_status, array( 'draft', 'pending', 'auto-draft' ) ) )
        return $slug;

    global $wpdb, $wp_rewrite;

    // store slug made by original function
    $wp_slug = $slug;

    // reset slug to original slug
    $slug = $original_slug;

    $feeds = $wp_rewrite->feeds;
    if ( ! is_array( $feeds ) )
        $feeds = array();

    $hierarchical_post_types = get_post_types( array('hierarchical' => true) );
    if ( 'attachment' == $post_type ) {
        // Attachment slugs must be unique across all types.
        $check_sql = "SELECT post_name FROM $wpdb->posts WHERE post_name = %s AND ID != %d LIMIT 1";
        $post_name_check = $wpdb->get_var( $wpdb->prepare( $check_sql, $slug, $post_ID ) );

        if ( $post_name_check || in_array( $slug, $feeds ) || apply_filters( 'wp_unique_post_slug_is_bad_attachment_slug', false, $slug ) ) {
            $suffix = 2;
            do {
                $alt_post_name = substr ($slug, 0, 200 - ( strlen( $suffix ) + 1 ) ) . "-$suffix";
                $post_name_check = $wpdb->get_var( $wpdb->prepare($check_sql, $alt_post_name, $post_ID ) );
                $suffix++;
            } while ( $post_name_check );
            $slug = $alt_post_name;
        }
    } elseif ( in_array( $post_type, $hierarchical_post_types ) ) {
        if ( 'nav_menu_item' == $post_type )
            return $slug;
        // Page slugs must be unique within their own trees. Pages are in a separate
        // namespace than posts so page slugs are allowed to overlap post slugs.
        $check_sql = "SELECT post_name FROM $wpdb->posts WHERE post_name = %s AND post_type = %s AND ID != %d AND post_parent = %d LIMIT 1";
        $post_name_check = $wpdb->get_var( $wpdb->prepare( $check_sql, $slug, $post_type, $post_ID, $post_parent ) );

        if ( $post_name_check || in_array( $slug, $feeds ) || preg_match( "@^($wp_rewrite->pagination_base)?\d+$@", $slug ) || apply_filters( 'wp_unique_post_slug_is_bad_hierarchical_slug', false, $slug, $post_type, $post_parent ) ) {
            $suffix = 2;
            do {
                $alt_post_name = substr( $slug, 0, 200 - ( strlen( $suffix ) + 1 ) ) . "-$suffix";
                $post_name_check = $wpdb->get_var( $wpdb->prepare( $check_sql, $alt_post_name, $post_type, $post_ID, $post_parent ) );
                $suffix++;
            } while ( $post_name_check );
            $slug = $alt_post_name;
        }
    } else {
        // Post slugs must be unique across all posts.
        $check_sql = "SELECT post_name FROM $wpdb->posts WHERE post_name = %s AND post_type = %s AND ID != %d LIMIT 1";
        $post_name_check = $wpdb->get_var( $wpdb->prepare( $check_sql, $slug, $post_type, $post_ID ) );

        if ( $post_name_check || in_array( $slug, $feeds ) || apply_filters( 'wp_unique_post_slug_is_bad_flat_slug', false, $slug, $post_type ) ) {
            $suffix = 2;
            do {
                $alt_post_name = substr( $slug, 0, 200 - ( strlen( $suffix ) + 1 ) ) . "-$suffix";
                $post_name_check = $wpdb->get_var( $wpdb->prepare( $check_sql, $alt_post_name, $post_type, $post_ID ) );
                $suffix++;
            } while ( $post_name_check );
            $slug = $alt_post_name;
        }
    }

    return $slug;
}

// add ie conditional html5 shim to header
function add_ie_html5_shim () {
    echo '<!--[if lt IE 9]>';
    echo '<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>';
    echo '<![endif]-->';
}
add_action('wp_head', 'add_ie_html5_shim');

add_filter('wp_unique_post_slug', 'wp_cpt_unique_post_slug', 10, 6);

add_filter("gform_confirmation_anchor", create_function("","return true;"));

?>