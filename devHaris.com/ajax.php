<?php
defined('DEV') || define('DEV', false);
$section = isset($_GET['section']) ? $_GET['section'] : 'index';
switch ($section)
{
	default:
		// 404
		break;

	case 'blog-listing':
	case 'blog-item':
		if (file_exists('ajax/' . $section . '.php')) require_once 'ajax/' . $section . '.php';
		break;

}